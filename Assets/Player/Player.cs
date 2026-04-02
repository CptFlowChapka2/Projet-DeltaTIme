using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public Vector3 arm;
    private float currentAngle = 0f;
    
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
        ExtendRetract1Axis();
        Spin1Axis();
        grabber.transform.position = transform.position + arm;
    }

    private void GrabRelease()
    {
        if (grabReleaseAction.WasPerformedThisFrame())
        {
            grabber.OnGrabRelease();
        }
    }

    private void ExtendRetract1Axis()
    {
        float extendValue = extendRetractAction.ReadValue<float>() * Time.deltaTime * extentionSpeed; 
        float armMagnitude = arm.magnitude + extendValue;
        armMagnitude = Mathf.Clamp(armMagnitude, minExtentionLength, maxExtentionLength);
        arm = arm.normalized * armMagnitude;
    }

    private void Spin1Axis()
    {
        float spinValue = spinAction.ReadValue<float>() * Time.deltaTime * rotationSpeed;
        arm = Quaternion.AngleAxis(spinValue, Vector3.up) * arm;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, arm);
    }
}
