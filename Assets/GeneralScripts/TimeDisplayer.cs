using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplayer : MonoBehaviour
{
    [SerializeField] private LvlInfos lvlInfos;
    [SerializeField] private GameObject needlePivot;
    [SerializeField] private CanvasRenderer needle;
    [SerializeField] private TextMeshProUGUI nbOfTicks;
    [SerializeField] private TextMeshProUGUI tickMargin;
    private float tickMarginValue;

    private void Start()
    {
        tickMarginValue = lvlInfos.lvlDurationWhenRush / lvlInfos.timePerTick;
        lvlInfos.Tick.AddListener(OnTick);
    }

    private void Update()
    {
        nbOfTicks.text = (lvlInfos.tNbrTick).ToString();
        tickMargin.text = "Margin : " + (tickMarginValue).ToString();
        needlePivot.transform.Rotate(transform.forward, -180 * Time.deltaTime);
        
        float alphaValue = Mathf.Clamp((lvlInfos.timer / 2), 0.1f, 1);

        needle.SetAlpha(alphaValue);
    }
    
    public void OnTick()
    {
        if (lvlInfos.validIngredientHasSpawned && tickMarginValue > 0)
        {
            tickMargin.color = Color.red;
            tickMarginValue--;
        }
    }
}
