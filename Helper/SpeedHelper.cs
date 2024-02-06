using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedHelper : MonoBehaviour
{

    public UnityEvent<float> m_OnSpeedUpdated;

    public float m_Factor = 1.0f;
    public float m_Offset = 0.0f;

    Vector3 m_PreviousPosition;
    float m_CurrentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        m_PreviousPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = (this.transform.position - m_PreviousPosition).magnitude;
        if(speed != m_CurrentSpeed)
        {
            m_OnSpeedUpdated.Invoke(m_Offset + speed * m_Factor);
            m_CurrentSpeed = speed;
        }

        m_PreviousPosition = this.transform.position;
    }
}
