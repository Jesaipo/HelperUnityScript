using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraOverlayHelper : MonoBehaviour
{
    Camera m_MainCamera;
    Camera m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        m_MainCamera = Camera.main;
        m_Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_Camera.orthographicSize = m_MainCamera.orthographicSize;    
    }
}
