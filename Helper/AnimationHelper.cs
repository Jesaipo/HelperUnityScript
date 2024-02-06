using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class AnimationHelper : MonoBehaviour
{
    public bool StartAnimatorAtRandomFrame = false;

    public UnityEvent AnimationStart;
    public UnityEvent AnimationStop;

    public UnityEvent Marker1;

    private void Start()
    {
        if(StartAnimatorAtRandomFrame)
        {
            Animator anim = GetComponent<Animator>();
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            anim.Play(state.fullPathHash, -1, Random.Range(0f, 1f));
        }
    }

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
