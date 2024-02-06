using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomEventHelper : MonoBehaviour
{
    public float m_Percent = 50f;
    public UnityEvent m_OnTriggerSuccess;
    public UnityEvent m_OnTriggerFailed;

    public void TriggerRandomly()
    {
        if(MyRandom.GetRandomFloatRange(0f,100f) < m_Percent)
        {
            m_OnTriggerSuccess.Invoke();
        }
        else
        {
            m_OnTriggerFailed.Invoke();
        }
    }
}
