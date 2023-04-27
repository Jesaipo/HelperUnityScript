using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class AnimationHelper : MonoBehaviour
{
    public UnityEvent AnimationStart;
    public UnityEvent AnimationStop;

    public UnityEvent Marker1;

    public void OnAnimationStart()
    {
        AnimationStart.Invoke();
    }

    public void OnAnimationStop()
    {
        AnimationStop.Invoke();
    }

    public void OnMarker1()
    {
        Marker1.Invoke();
    }
}
