using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnAllSlaveHelper : MonoBehaviour
{
    public UnityEvent m_OnTrigger;
    public void Trigger()
    {
        m_OnTrigger.Invoke();
    }
}
