using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static System.MathF;

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

    protected override void Start()
    {
        base.Start();
        grabber.player = this;
        popupRenderer.gameObject.GetComponent<PopupMover>().playerToFollow = this;
        transform.position += new Vector3(0, 0.5f, 0);
        armVector = new Vector3(0, 0, 2.5f);
        playerInput = GetComponent<PlayerInput>(); 
        rb = GetComponent<Rigidbody>();
        spinAction = playerInput.actions["Spin"];
        extendRetractAction = playerInput.actions["Extend"];
        grabReleaseAction = playerInput.actions["Grab"];
    }

    private void Update()
    {
        if (isActive)
        {
            ActualVariant = 0;
            GrabRelease();
            
            Vector3 tempArm = armVector;
            ExtendRetract1Axis(out tempArm);
            armVector = tempArm;
            grabber.transform.position = transform.position + armVector;
            UpdateVisualArm();
        }
        else
        {
            ActualVariant = 1;
        }
    }

    private void FixedUpdate()
    {
        Spin1Axis();
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

    private void Spin1Axis()
    {
        float spinValue = spinAction.ReadValue<float>() * Time.fixedDeltaTime * rotationSpeed;
        rb.angularVelocity = new Vector3(0, spinValue, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maxExtentionLength);
    }

    private void UpdateVisualArm()
    {
        physicalArm.transform.localScale = new Vector3(physicalArm.transform.localScale.x, 
                                                    (armVector.magnitude - (Mathf.Abs((armOrigin.position - transform.position).magnitude))) / 2,
                                                    physicalArm.transform.localScale.z);
        physicalArm.transform.position = armOrigin.position + (armVector - (armOrigin.position - transform.position))/2;
    }
    
}

