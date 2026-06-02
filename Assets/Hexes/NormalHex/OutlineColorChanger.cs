using System;
using UnityEngine;

public class OutlineColorChanger : MonoBehaviour
{
    [SerializeField] private Hex hex;
    
    [SerializeField] private Color outlinePMouseColor;
    [SerializeField] private Color outlinePKeyboardColor;
    [SerializeField] private Color outlineAllPlayersColor;
    [SerializeField] private SpriteRenderer outlineSpriteRenderer;
    
    private Sprite currentSprite = null;
    private Color currentColor = new Color();

    private void Update()
    {
        if (hex.isHovered)
        {
            outlineSpriteRenderer.enabled = true;
            
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
            
            float alphaValue = Mathf.Clamp(Mathf.Abs(1 - hex.lvlInfos.timer), 0.6f, 1f);
            currentColor.a = alphaValue;
            
            outlineSpriteRenderer.color = currentColor;
        }
        else
        {
            outlineSpriteRenderer.enabled = false;
        }
    }
}
