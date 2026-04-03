using System;
using Unity.VisualScripting;
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
    [SerializeField] private Player otherPLayer;
    

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
        Vector3 tempArm = arm;
        ExtendRetract1Axis(tempArm,out  tempArm);
        Spin1Axis(out float spinValue,tempArm,out tempArm);
        Vector2 a1 = new Vector2(transform.position.x, transform.position.z);
        Vector2 a2 = new Vector2((transform.position + tempArm).x, (transform.position + tempArm).z);
        Vector2 b1 = new Vector2(otherPLayer.transform.position.x, otherPLayer.transform.position.z);
        Vector2 b2 = new Vector2((otherPLayer.transform.position + otherPLayer.arm).x, (otherPLayer.transform.position + otherPLayer.arm).z);

        if (LinesUtils.Intersect(a1, a2, b1, b2,out Vector2 pointOfIntersec,LinesUtils.Mode.Segments))return;
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
        Gizmos.DrawRay(transform.position, arm);
    }

    private void UpadateVisualArm()
    {
        visualArm.transform.localScale = new Vector3(visualArm.transform.localScale.x,arm.magnitude / 2,visualArm.transform.localScale.z);
        visualArm.transform.position = transform.position+ arm/2;
       
    }
    
}
//ça copie des  classe entiére en animal ici  par : orionsyndrome  https://discussions.unity.com/t/two-intersecting-lines/916519/2//
 static class LinesUtils {

  // returns true if the intersection exists, false otherwise
  // the actual point is returned through the 'out' parameter
  public static bool Intersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersect, Mode mode = Mode.Segments) {

    // default intersection point in case none exists
    intersect = new Vector2(float.NaN, float.NaN);

    // check the bounding rectangles (optimization for segments only)
    if(mode == Mode.Segments) {
      var ar = rectFromSeg(a1, a2);
      var br = rectFromSeg(b1, b2);
      if(!ar.Overlaps(br)) return false;
    }

    // compute point of intersection using homogeneous coordinates
    var aa = Vector3.Cross(hp(a1), hp(a2));
    var bb = Vector3.Cross(hp(b1), hp(b2));
    var cc = Vector3.Cross(aa, bb);

    // if Z is close to 0, no point of intersection exists
    if(Abs(cc.z) < 1E-6f) return false;

    // otherwise, compute the actual point in 2D
    var x = ((Vector2)cc) * (1f / cc.z);

    // test the intersection interval for rays and segments
    if(mode switch {
      Mode.Rays => !test(x, a1, a2) || !test(x, b1, b2),
      Mode.RayLine => !test(x, a1, a2),
      Mode.RaySegment => !test(x, a1, a2) || !test(x, b1, b2, bidi: true),
      Mode.Segments => !test(x, a1, a2, bidi: true) || !test(x, b1, b2, bidi: true),
      _ => false // lines can't fail at this
    }) return false;

    // adopt the 2D solution and return it
    intersect = x;
    return true;

    // -- local functions --
    // conversion to homogeneous coordinates
    static Vector3 hp(Vector2 p) => new Vector3(p.x, p.y, 1f);

    // interval test; segments are bidi(rectional), rays are not
    static bool test(Vector2 p, Vector2 a, Vector2 b, bool bidi = false) {
      int i = Abs(b.x - a.x) < Abs(b.y - a.y)? 1 : 0;
      float n = p[i] - a[i], d = b[i] - a[i]; // numerator and denominator of inverse lerp
      if(bidi && Abs(n) > Abs(d)) return false; // interval test for segments
      return n >= 0f == d >= 0f; // interval test for rays and segments
    }

    // produces a rectangle that encapsulates two arbitrary points
    static Rect rectFromSeg(Vector2 a, Vector2 b) {
      var min = leastOf(a, b);
      return new Rect(min, mostOf(a, b) - min);
    }

    static Vector2 leastOf(Vector2 a, Vector2 b) => new Vector2(Min(a.x, b.x), Min(a.y, b.y));
    static Vector2 mostOf(Vector2 a, Vector2 b) => new Vector2(Max(a.x, b.x), Max(a.y, b.y));

  }

  public enum Mode {
    Lines,
    Rays,
    RayLine,
    RaySegment,
    Segments
  }

}
