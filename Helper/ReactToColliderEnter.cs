using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReactToColliderEnter : MonoBehaviour
{
    public UnityEvent OnReactToColliderEnterEvent;
    public void ReactToColliderEnterCallback()
    {
        OnReactToColliderEnterEvent.Invoke();
    }
}
