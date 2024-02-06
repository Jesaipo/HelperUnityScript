using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputToEvent : MonoBehaviour
{
    public bool m_AnyKey = false;
    [MyBox.ConditionalField(nameof(m_AnyKey), inverse:true)]
    public KeyCode key;
    public UnityEvent KeyDownEvent;

    // Update is called once per frame
    void Update()
    {
        if(m_AnyKey)
        {
            if (Input.anyKeyDown)
            {
                KeyDownEvent.Invoke();
            }
        }
        else
        {
            if (Input.GetKeyDown(key))
            {
                KeyDownEvent.Invoke();
            }
        }
    }
}
