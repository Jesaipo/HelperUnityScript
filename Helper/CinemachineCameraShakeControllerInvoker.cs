using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCameraShakeControllerInvoker : MonoBehaviour
{
    public string cinemachineCameraShakeControllerUniqueID;
    public bool triggerOnStart;
    // Start is called before the first frame update
    void Start()
    {
        if (triggerOnStart)
            Trigger();
    }

    public void Trigger()
    {
        Trigger(cinemachineCameraShakeControllerUniqueID);
    }

    public void Trigger(string ID)
    {
        CinemachineCameraShakeController.Shake(ID);
    }
}
