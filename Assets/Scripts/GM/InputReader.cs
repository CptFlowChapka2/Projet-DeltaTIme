using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public float HorizontalMoveP1
    { 
        get
        {
            if (Keyboard.current.dKey.wasPressedThisFrame)
            {
                return 1f;
            }
            else if (Keyboard.current.aKey.wasPressedThisFrame)
            { 
                return -1f;
            }
            else
            {
                return 0f;
            }
        }
    }
    
    public float HorizontalMoveP2
    { 
        get
        {
            if (Keyboard.current.numpad6Key.wasPressedThisFrame)
            {
                return 1f;
            }
            else if (Keyboard.current.numpad4Key.wasPressedThisFrame)
            { 
                return -1f;
            }
            else
            {
                return 0f;
            }
        }
    }
    
    public float VerticalMoveP1
    {
        get
        {
            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
                return 1f;
            }
            else if (Keyboard.current.sKey.wasPressedThisFrame)
            { 
                return -1f;
            }
            else
            {
                return 0f;
            }
        }
    }
    
    public float VerticalMoveP2
    {
        get
        {
            if (Keyboard.current.numpad8Key.wasPressedThisFrame)
            {
                return 1f;
            }
            else if (Keyboard.current.numpad5Key.wasPressedThisFrame)
            { 
                return -1f;
            }
            else
            {
                return 0f;
            }
        }
    }
}
