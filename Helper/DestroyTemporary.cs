using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTemporary : MonoBehaviour
{
    public string m_Tag;
    public void TriggerDestruction()
    {
        Lifetime[] lifetimes = FindObjectsOfType(typeof(Lifetime)) as Lifetime[];
        foreach(Lifetime lf in lifetimes)
        {
            if(lf.enabled == false)
            {
                lf.enabled = true;
            }
            lf.Over();
        }
    }

    public void TriggerTagDestruction()
    {
        Lifetime[] lifetimes = FindObjectsOfType(typeof(Lifetime)) as Lifetime[];
        foreach (Lifetime lf in lifetimes)
        {
            if (lf.gameObject.CompareTag(m_Tag))
            {
                if (lf.enabled == false)
                {
                    lf.enabled = true;
                }
                lf.Over();
            }
        }
    }
}
