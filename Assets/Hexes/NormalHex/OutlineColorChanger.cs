using System;
using UnityEngine;

public class OutlineColorChanger : MonoBehaviour
{
    [SerializeField] private Hex hex;
    
    [SerializeField] private Color outlinePMouseColor;
    [SerializeField] private Color outlinePKeyboardColor;
    [SerializeField] private Color outlineAllPlayersColor;
    
    [SerializeField] public MeshRenderer[] meshesToChangeA;
    [SerializeField] public MeshRenderer[] meshesToChangeB;

    private bool isActivated;
    public Color currentColor = new Color();

    private void Update()
    {
        if (hex.isHovered)
        {
            isActivated = true;
            if (hex.isPlayerHovering[0])
            {
                if (hex.isPlayerHovering[1])
                {
                    currentColor = outlineAllPlayersColor;
                }
                else
                {
                    currentColor = outlinePMouseColor;
                }
            }
            else
            {
                currentColor = outlinePKeyboardColor;
            }

            if ((hex.lvlInfos.timer > 0.5f && hex.lvlInfos.timer <= 1) || (hex.lvlInfos.timer > 1.5f && hex.lvlInfos.timer <= 2))
            {
                foreach (MeshRenderer mesh in meshesToChangeA)
                {
                    mesh.enabled = true;
                    mesh.material.color = currentColor;
                }
                foreach (MeshRenderer mesh in meshesToChangeB)
                {
                    mesh.enabled = false;
                }
            }
            else
            {
                foreach (MeshRenderer mesh in meshesToChangeB)
                {
                    mesh.enabled = true;
                    mesh.material.color = currentColor;
                }
                foreach (MeshRenderer mesh in meshesToChangeA)
                {
                    mesh.enabled = false;
                }
            }
        }
        else if (isActivated)
        {
            isActivated = false;
            foreach (MeshRenderer mesh in meshesToChangeA)
            {
                mesh.enabled = false;
            }
            foreach (MeshRenderer mesh in meshesToChangeB)
            {
                mesh.enabled = false;
            }
        }
    }
}
