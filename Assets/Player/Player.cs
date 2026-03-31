using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Grabbable
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float extentionSpeed;
    [SerializeField] private float maxExtentionLength;
    public PlayerInput playerInput;
    private InputAction spinAction;
    private InputAction extendRetractAction;
    private InputAction grabReleaseAction;

    public Vector3 arm;
    private float currentAngle = 0f;
    
    public Grabber grabber;

    protected override void Start()
    {
        base.Start();
        arm = new Vector3(1, 0, 0);
        playerInput = GetComponent<PlayerInput>(); 
        spinAction = playerInput.actions["Spin"];
        extendRetractAction = playerInput.actions["Extend"];
        grabReleaseAction = playerInput.actions["Grab"];
    }

    private void Update()
    {
        GrabRelease();
        ExtendRetract();
        Spin();
        grabber.transform.position = transform.position + arm;
    }

    private void GrabRelease()
    {
        if (grabReleaseAction.WasPerformedThisFrame())
        {
            grabber.OnGrabRelease();
        }
    }

    private void ExtendRetract()
    {
        float extendValue = extendRetractAction.ReadValue<float>() * Time.deltaTime * extentionSpeed; 
        float armMagnitude = arm.magnitude + extendValue;
        armMagnitude = Mathf.Clamp(armMagnitude, 1, maxExtentionLength);
        arm = arm.normalized * armMagnitude;
    }

    private void Spin()
    {
        float spinValue = spinAction.ReadValue<float>() * Time.deltaTime * rotationSpeed;
        //currentAngle += spinValue;
        //currentAngle = Mathf.Repeat(currentAngle, 360);
        arm = Quaternion.AngleAxis(spinValue, Vector3.up) * arm;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, arm);
    }
}
