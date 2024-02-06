using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAllMasterHelper : MonoBehaviour
{
    public void TriggerOnAll()
    {
        OnAllSlaveHelper[] salves = FindObjectsOfType(typeof(OnAllSlaveHelper)) as OnAllSlaveHelper[];
        foreach (OnAllSlaveHelper s in salves)
        {
            s.Trigger();
        }
    }
}
