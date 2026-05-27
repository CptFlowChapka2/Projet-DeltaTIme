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

    private void Update()
    {
        nbOfTicks.text = (lvlInfos.tNbrTick).ToString();
        needlePivot.transform.Rotate(transform.forward, -180 * Time.deltaTime);
        
        float alphaValue = Mathf.Clamp((lvlInfos.timer / 2), 0.1f, 1);

        needle.SetAlpha(alphaValue);
    }
}
