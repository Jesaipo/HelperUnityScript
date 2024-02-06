using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class RotationHelper : MonoBehaviour
{
    private RectTransform m_Rect;

    public bool m_FreezeRotationX = false;
    public bool m_FreezeRotationY = false;
    public bool m_FreezeRotationZ = false;

    public bool m_RotateYFromSpeed = false;
    private Vector3 m_PreviousPosition;

    private void Start()
    {
        m_Rect = this.GetComponent<RectTransform>();
        m_PreviousPosition = m_Rect.position;
    }

    private void Update()
    {
        Vector3 rotationVector = m_Rect.eulerAngles;

        if (m_FreezeRotationX)
        {
            rotationVector.x = 0;
        }

        if (m_FreezeRotationY)
        {
            rotationVector.y = 0;
        }
        else
        {
            if (m_RotateYFromSpeed)
            {
                if (m_Rect.position.x - m_PreviousPosition.x > 0)
                {
                    rotationVector.y = 0;
                }
                else if (m_Rect.position.x - m_PreviousPosition.x < 0)
                {
                    rotationVector.y = 180;
                }

                m_PreviousPosition = m_Rect.position;
            }
        }

        if (m_FreezeRotationZ)
        {
            rotationVector.z = 0;
        }

        m_Rect.eulerAngles = rotationVector;
    }
}
