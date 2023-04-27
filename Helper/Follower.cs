using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject m_ObjectToFollow;

    List<Vector3> m_LastPositions = new List<Vector3>();
    public int m_FrameDelay = 60;

    // Update is called once per frame
    void Update()
    {
        if(m_LastPositions.Count >= m_FrameDelay)
        {
            this.transform.position = m_LastPositions[0];
            m_LastPositions.RemoveAt(0);
        }

        m_LastPositions.Add(m_ObjectToFollow.transform.position);
    }
}
