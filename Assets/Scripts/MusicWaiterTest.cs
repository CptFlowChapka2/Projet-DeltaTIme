using System;
using UnityEngine;

public class MusicWaiterTest : MonoBehaviour
{
    private BeatEnabler beatEnabler;
    private float t;

    private void Start()
    {
        beatEnabler = GetComponent<BeatEnabler>();
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (t >= 0.2f)
        {
            beatEnabler.enabled = true;
            this.enabled = false;
        }
    }
}
