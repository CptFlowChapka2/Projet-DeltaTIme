using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.MathF;

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
    private InputAction spinAction;
    private InputAction extendRetractAction;
    private InputAction grabReleaseAction;
    public Rigidbody rb;
    
    public Vector3 armVector;
    
    [SerializeField] private GameObject physicalArm;
    public MeshRenderer popupRenderer;
    public Grabber grabber;
    [SerializeField] private Transform armOrigin;
    
    public PlayerState state = PlayerState.Idle;
    [SerializeField] private float maxTimerWhenKnocked;
    [SerializeField] private float knockingSpeed;
    [NonSerialized] public bool knockedLeft = false;
    [NonSerialized] public bool knockedRight = false;
    private float timerKnocked = 0;

    protected override void Start()
    {
        base.Start();
        grabber.player = this;
        popupRenderer.gameObject.GetComponent<PopupMover>().playerToFollow = this;
        //transform.position += new Vector3(0, 0.5f, 0);
        armVector = new Vector3(0, 0, 2.5f);
        playerInput = GetComponent<PlayerInput>(); 
        rb = GetComponent<Rigidbody>();
        spinAction = playerInput.actions["Spin"];
        extendRetractAction = playerInput.actions["Extend"];
        grabReleaseAction = playerInput.actions["Grab"];
    }

    private void Update()
    {
        GrabRelease();
            
        Vector3 tempArm = armVector;
        ExtendRetract1Axis(out tempArm);
        
        switch (state)
        {
            case PlayerState.Idle:
                ActualVariant = 0;
                armVector = tempArm;
                grabber.transform.position = transform.position + armVector;
                UpdatePysicalArm();
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
        switch (state)
        {
            case PlayerState.Idle:
                Spin1Axis();
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
        }
    }

    private void ExtendRetract1Axis(out Vector3 tempArm)
    {
        float extendValue = extendRetractAction.ReadValue<float>() * Time.deltaTime * extentionSpeed; 
        float armMagnitude = armVector.magnitude + extendValue;
        armMagnitude = Mathf.Clamp(armMagnitude, minExtentionLength, maxExtentionLength);
        tempArm = transform.forward * armMagnitude;
    }

    private Vector2 stickInputLastFrame = new Vector2();
    private void Spin1Axis()
    {
        bool controlMode = playerInput.currentActionMap.name.Equals("AnyPlayer/controller");
        float spinValue=0;
        if (controlMode)
        {
            Vector2 thisFrameRead = spinAction.ReadValue<Vector2>() ;
            if (thisFrameRead == Vector2.zero)
            {
                spinValue = 0;
                rb.angularVelocity = new Vector3(0, spinValue, 0);
                return;
            }
            
            //thisFrameRead = Vector2.MoveTowards(stickInputLastFrame, thisFrameRead, (Time.fixedDeltaTime * rotationSpeed));
            spinValue = -Vector2.SignedAngle(stickInputLastFrame, thisFrameRead)*(Time.fixedDeltaTime * rotationSpeed);
            stickInputLastFrame = thisFrameRead;
        }
        else
        { 
            spinValue = spinAction.ReadValue<float>() * Time.fixedDeltaTime * rotationSpeed;
        }
        rb.angularVelocity = new Vector3(0, spinValue, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxExtentionLength);
    }

    private void UpdatePysicalArm()
    {
        physicalArm.transform.localScale = new Vector3(physicalArm.transform.localScale.x, 
                                                    (armVector.magnitude - (Mathf.Abs((armOrigin.position - transform.position).magnitude))) / 2,
                                                    physicalArm.transform.localScale.z);
        physicalArm.transform.position = armOrigin.position + (armVector - (armOrigin.position - transform.position))/2;
    }
    
}

