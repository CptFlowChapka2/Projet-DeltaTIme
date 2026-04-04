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
    [SerializeField] private GameObject visualArm;
   
    

    public Vector3 arm;
    
    public MeshRenderer popupRenderer;
    public Grabber grabber;

    protected override void Start()
    {
        base.Start();
        grabber.player = this;
        popupRenderer.gameObject.GetComponent<PopupMover>().playerToFollow = this;
        transform.position += new Vector3(0, 0.5f, 0);
        arm = new Vector3(1, 0, 0);
        playerInput = GetComponent<PlayerInput>(); 
        spinAction = playerInput.actions["Spin"];
        extendRetractAction = playerInput.actions["Extend"];
        grabReleaseAction = playerInput.actions["Grab"];
    }

    private void Update()
    {
        GrabRelease();
        Vector3 tempArm = arm;
        ExtendRetract1Axis(tempArm,out  tempArm);
        Spin1Axis(out float spinValue,tempArm,out tempArm);
        
        RaycastHit[] hits = Physics.BoxCastAll(transform.position + tempArm / 2,
            new Vector3(10f, tempArm.magnitude/2,0.2F), Vector3.up,
            visualArm.transform.rotation * Quaternion.AngleAxis(spinValue, Vector3.right),0,-1,QueryTriggerInteraction.Collide);
        
        var hitlist = hits.ToList();
        hitlist.RemoveAll(x=>x.collider.gameObject==visualArm||x.collider.gameObject==grabber.gameObject);
        //Debug.Log(hitlist.Count);
        if(hitlist.Any(x=>x.collider.gameObject.CompareTag("ArmBlocker")))return;
        
        arm = tempArm;
        grabber.transform.position = transform.position + arm;
        visualArm.transform.rotation*=Quaternion.AngleAxis(spinValue, Vector3.right);
        UpadateVisualArm();
    }

    private void GrabRelease()
    {
        if (grabReleaseAction.WasPerformedThisFrame())
        {
            grabber.OnGrabRelease();
        }
    }

    private void ExtendRetract1Axis(Vector3 armIn,out Vector3 tempArm )
    {
        float extendValue = extendRetractAction.ReadValue<float>() * Time.deltaTime * extentionSpeed; 
        float armMagnitude = arm.magnitude + extendValue;
        armMagnitude = Mathf.Clamp(armMagnitude, minExtentionLength, maxExtentionLength);
        tempArm = armIn;
        tempArm = tempArm.normalized * armMagnitude;
    }

    private void Spin1Axis(out float spinValue,Vector3 tempArmIn,out Vector3 tempArm)
    {
         spinValue = spinAction.ReadValue<float>() * Time.deltaTime * rotationSpeed;
        tempArm = tempArmIn;
        tempArm = Quaternion.AngleAxis(spinValue, Vector3.up) * tempArm;
        
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
    }

    private void UpadateVisualArm()
    {
        visualArm.transform.localScale = new Vector3(visualArm.transform.localScale.x,arm.magnitude / 2,visualArm.transform.localScale.z);
        visualArm.transform.position = transform.position+ arm/2;
       
    }
    
}

