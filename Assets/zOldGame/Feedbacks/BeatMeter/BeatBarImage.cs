using System;
using UnityEngine;

public class BeatBarImage : MonoBehaviour
{
    public Sprite onBeat;
    public Sprite offBeat;
    public SpriteRenderer thissprit;

    public void ListenOnStartCoyoteBeat()
    {
        thissprit.sprite = onBeat;
    }

    public void ListenOnEndCoyote()
    {
        thissprit.sprite = offBeat;
    }
}
