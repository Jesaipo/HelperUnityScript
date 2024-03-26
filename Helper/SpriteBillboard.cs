using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteBillboard : MonoBehaviour
{
    public string m_OptionnalCameraTag = "";

    public Camera m_Camera = null;
    // Update is called once per frame
    void Update()
    {
        if (m_Camera == null)
        {
            if (m_OptionnalCameraTag == "")
            {
                //Look at the main camera
                m_Camera = Camera.main;
            }
            else
            {
                var cameras = GameObject.FindGameObjectsWithTag(m_OptionnalCameraTag);
                foreach(GameObject go in cameras)
                {
                    if(go.activeSelf)
                    {
                        m_Camera = go.GetComponent<Camera>();
                        break;
                    }

                }
            }
        }

        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
    }
}
