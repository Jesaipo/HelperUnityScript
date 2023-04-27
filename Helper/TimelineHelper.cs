using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.Playables;

public class TimelineHelper : MonoBehaviour
{

    public UnityEvent IsTimelineOver;
    PlayableDirector director;

    public void OnSpawnAnimationOver()
    {
        IsTimelineOver.Invoke();
    }
    void OnEnable()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnPlayableDirectorStopped;
    }

    void OnPlayableDirectorStopped(PlayableDirector aDirector)
    {
        if (director == aDirector)
        {
            Debug.Log("PlayableDirector named " + aDirector.name + " is now stopped.");
            IsTimelineOver.Invoke();
        }
    }

    void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }
}
