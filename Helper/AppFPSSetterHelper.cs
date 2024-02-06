using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppFPSSetterHelper : MonoBehaviour
{
    public int m_FPS = 60;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = m_FPS;
    }

}
