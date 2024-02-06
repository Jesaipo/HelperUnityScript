using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFiltre : MonoBehaviour
{
    public bool ActiveOnMobile;
    private void Awake()
    {
        if(Application.isMobilePlatform)
        {
            this.gameObject.SetActive(ActiveOnMobile);
        }
        else
        {
            this.gameObject.SetActive(!ActiveOnMobile);
        }
    }
}
