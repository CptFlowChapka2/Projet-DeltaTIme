using System;
using UnityEngine;

public class OutlineColorChanger : MonoBehaviour
{
    [SerializeField] private Hex hex;
    
    [SerializeField] private Sprite outlinePMouseSprite;
    [SerializeField] private Color outlinePMouseColor;
    [SerializeField] private Sprite outlinePKeyboardSprite;
    [SerializeField] private Color outlinePKeyboardColor;
    [SerializeField] private Sprite outlineAllPlayersSprite;
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
                    currentSprite = outlineAllPlayersSprite;
                    currentColor = outlineAllPlayersColor;
                }
                else
                {
                    currentSprite = outlinePMouseSprite;
                    currentColor = outlinePMouseColor;
                }
            }
            else
            {
                currentSprite = outlinePKeyboardSprite;
                currentColor = outlinePKeyboardColor;
            }
            
            outlineSpriteRenderer.sprite = currentSprite;
            
            float alphaValue = Mathf.Clamp(Mathf.Abs(1 - hex.lvlInfos.timer), 0.1f, 0.9f);
            currentColor.a = alphaValue;
            
            outlineSpriteRenderer.color = currentColor;
        }
        else
        {
            outlineSpriteRenderer.enabled = false;
        }
    }
}
