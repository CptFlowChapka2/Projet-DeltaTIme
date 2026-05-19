using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;


public enum PlayerState
{
    None,
    Idle,
    Stunned,
    Knocked
}

public class Player : Grabbable
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float extentionSpeed;
    [SerializeField] private float maxExtentionLength;
    [SerializeField] private float minExtentionLength;
    public PlayerInput playerInput;
    private bool controls2D = false;
    private InputAction spinAction;
    private InputAction extendRetractAction;
    private InputAction grabReleaseAction;
    private InputAction pauseAction;
    private bool isInPause=false;
    public GameObject PauseMenu;
    public Player otherPlayer;
    public Rigidbody rb;
    
    public Vector3 armVector;
    
    [SerializeField] private GameObject physicalArm;
    public Grabber grabber;
    [SerializeField] private Transform armOrigin;
    
    public PlayerState state = PlayerState.Idle;
    [SerializeField] private float maxTimerWhenKnocked;
    [SerializeField] private float knockingSpeed;
    [NonSerialized] public bool knockedLeft = false;
    [NonSerialized] public bool knockedRight = false;
    private float timerKnocked = 0;

    private InputActionMap startInputActionMap;
    private InputActionMap UIInputActionMap;

    protected override void Start()
    {
        base.Start();
        grabber.player = this;
        //transform.position += new Vector3(0, 0.5f, 0);
        armVector = new Vector3(0, 0, 2.5f);
        playerInput = GetComponent<PlayerInput>(); 
        rb = GetComponent<Rigidbody>();
        startInputActionMap = playerInput.currentActionMap;
        playerInput.SwitchCurrentActionMap("UI");
        UIInputActionMap=playerInput.currentActionMap;
        playerInput.currentActionMap = startInputActionMap;
        playerInput.uiInputModule = FindAnyObjectByType<InputSystemUIInputModule>();
        
        if (playerInput.GetDevice<Gamepad>() is not null)
        {
            controls2D = true;
            spinAction = playerInput.actions["Spin2D"];
        }
        else
        {
            spinAction = playerInput.actions["Spin"];
        }
        extendRetractAction = playerInput.actions["Extend"];
        grabReleaseAction = playerInput.actions["Grab"];
        pauseAction = playerInput.actions["Pause"];
    }

    private void Update()
    {
        ListenForPause();
        if(isInPause) return;
        if (spinAction.WasPressedThisFrame() && soundManager.playersRotating < 2)
        {
            soundManager.playersRotating++;
        }

        if (spinAction.WasReleasedThisFrame() && soundManager.playersRotating > 0)
        {
            soundManager.playersRotating--;
        }
        
        GrabRelease();
            
        Vector3 tempArm = armVector;
        ExtendRetract1Axis(out tempArm);
        
        switch (state)
        {
            case PlayerState.Idle:
                ActualVariant = 0;
                armVector = tempArm;
                grabber.transform.position = transform.position + armVector;
                UpdatePhysicalArm();
                break;
            case PlayerState.Stunned:
                ActualVariant = 1;
                break;
            case PlayerState.Knocked:
                ActualVariant = 2;
                timerKnocked += Time.deltaTime;
                if (timerKnocked >= maxTimerWhenKnocked)
                {
                    timerKnocked = 0;
                    state = PlayerState.Idle;
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        if(isInPause) return;
        switch (state)
        {
            case PlayerState.Idle:
                Spin();
                break;
            case PlayerState.Stunned:
                break;
            case PlayerState.Knocked:
                if (knockedLeft)
                {
                    rb.angularVelocity = new Vector3(0, -knockingSpeed, 0);
                    knockedLeft = false;
                }
                else if (knockedRight)
                {
                    rb.angularVelocity = new Vector3(0, knockingSpeed, 0);
                    knockedRight = false;
                }
                else
                {
                    Vector3 decceleration = new Vector3(0, rotationSpeed / 90, 0);
                    if (rb.angularVelocity.y > 0)
                    {
                        rb.angularVelocity -= decceleration;
                    }
                    else if (rb.angularVelocity.y < 0)
                    {
                        rb.angularVelocity += decceleration;
                    }
                }
                break;
        }
    }

    private void OriginalBlocker(Vector3 tempArm, float spinValue)
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + tempArm / 2,
            new Vector3(10f, tempArm.magnitude/2,0.2F), Vector3.up,
            physicalArm.transform.rotation * Quaternion.AngleAxis(spinValue, Vector3.right),0,-1,QueryTriggerInteraction.Collide);
        
        var hitlist = hits.ToList();
        hitlist.RemoveAll(x=>x.collider.gameObject==physicalArm ||
                             x.collider.gameObject==grabber.gameObject);
        //todo revoir le blocker car du coup ça déconne quand on grab l'autre joueur
        //Debug.Log(hitlist.Count);
        //if(hitlist.Any(x=>x.collider.gameObject.CompareTag("ArmBlocker")))return;
    }

    private void GrabRelease()
    {
        if (grabReleaseAction.WasPerformedThisFrame())
        {
            grabber.OnGrabRelease();
            soundManager.grabRelease.Play();
        }
    }

    private void ExtendRetract1Axis(out Vector3 tempArm)
    {
        float extendValue = extendRetractAction.ReadValue<float>() * Time.deltaTime * extentionSpeed; 
        float armMagnitude = armVector.magnitude + extendValue;
        armMagnitude = Mathf.Clamp(armMagnitude, minExtentionLength, maxExtentionLength);
        tempArm = transform.forward * armMagnitude;
    }
    
    private void Spin()
    {
        float spinValue = 0;
        
        if (!controls2D)
        {
            spinValue = spinAction.ReadValue<float>() * Time.fixedDeltaTime * rotationSpeed;
        }
        else
        {
            Vector2 inputVector = spinAction.ReadValue<Vector2>();
            if (inputVector != Vector2.zero)
            {
                Vector3 projectedVector = new Vector3(inputVector.x, 0, inputVector.y);
                float incidenceAngle = Vector3.SignedAngle(armVector, projectedVector, Vector3.up);
                if (incidenceAngle > 0)
                {
                    spinValue = Time.fixedDeltaTime * rotationSpeed;
                }
                else
                {
                    spinValue = -(Time.fixedDeltaTime * rotationSpeed);
                }
            }
        }
        
        rb.angularVelocity = new Vector3(0, spinValue, 0);
    }

    private void ListenForPause()
    {
        bool pause = pauseAction.WasPressedThisFrame();
        if (!pause) return;
        otherPlayer.isInPause = !otherPlayer.isInPause;
        isInPause = !isInPause;
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        playerInput.currentActionMap = isInPause switch {
            true => UIInputActionMap,
            false => startInputActionMap
        };
        otherPlayer.playerInput.currentActionMap = isInPause switch {
                    true => UIInputActionMap,
                    false => startInputActionMap
        };
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, maxExtentionLength);
    // }

    private void UpdatePhysicalArm()
    {
        physicalArm.transform.localScale = new Vector3(physicalArm.transform.localScale.x, 
                                                    (armVector.magnitude - (Mathf.Abs((armOrigin.position - transform.position).magnitude))) / 2,
                                                    physicalArm.transform.localScale.z);
        physicalArm.transform.position = armOrigin.position + (armVector - (armOrigin.position - transform.position))/2;
    }
    
    
    
}

