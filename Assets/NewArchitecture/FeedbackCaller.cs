using UnityEngine;

public abstract class FeedbackCaller : Doer
{
    public virtual void Call()
    {
        Debug.Log("Feedback called");
    }
}
