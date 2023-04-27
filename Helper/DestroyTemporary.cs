using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTemporary : MonoBehaviour
{
    public void TriggerDestruction()
    {
        Lifetime[] lifetimes = FindObjectsOfType(typeof(Lifetime)) as Lifetime[];
        foreach(Lifetime lf in lifetimes)
        {
            lf.Over();
        }
    }
}
