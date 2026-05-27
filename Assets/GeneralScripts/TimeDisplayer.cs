using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplayer : MonoBehaviour
{
    [SerializeField] private LvlInfos lvlInfos;
    [SerializeField] private GameObject needlePivot;
    [SerializeField] private Image needle;
    [SerializeField] private TextMeshProUGUI nbOfTicks;

    private void Update()
    {
        nbOfTicks.text = (lvlInfos.tNbrTick).ToString();
        float decreaseValue = 255 - (62 * lvlInfos.timer * lvlInfos.timer);
        needlePivot.transform.Rotate(transform.forward, -180 * Time.deltaTime);

        if (lvlInfos.timer < 0.05f || lvlInfos.timer > 1.95f)
        {
            needle.color = Color.blue;
            needle.rectTransform.localScale = new Vector3(0.15f, 1.5f, 1.5f);
        }
        else
        {
            needle.color = Color.red;
            needle.rectTransform.localScale = new Vector3(0.1f, 1f, 1f);
        }
    }
}
