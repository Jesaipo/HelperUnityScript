using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FindObjectTagged : MonoBehaviour
{
    public string TagToFind = "Player";

    public UnityEvent<GameObject> FindedGOEvent;
    public UnityEvent<Transform> FindedTransformEvent;

    private void Update()
    {
        GameObject go = GameObject.FindGameObjectWithTag(TagToFind);
        if(go != null)
        {
            FindedGOEvent.Invoke(go);
            FindedTransformEvent.Invoke(go.transform);
            this.enabled = false;
        }
    }
}
