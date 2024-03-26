using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererFollower : MonoBehaviour
{
    public GameObject m_TargetToFollow;
    public float m_DistanceForNewPoint = 0.01f;
    public bool m_ZToZero = true;
    LineRenderer m_CurrentLineRenderer;

    Vector3 m_LastPosition;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentLineRenderer = GetComponent<LineRenderer>();
        AddAPoint(true);
        AddAPoint(true);
    }


    private void Update()
    {
        AddAPoint();
    }

    // Update is called once per frame
    void AddAPoint(bool force = false)
    {
        Vector3 pos = m_TargetToFollow.transform.position;
        if (m_ZToZero)
        {
            pos.z = 0f;
        }

        if (force || (m_LastPosition - pos).magnitude > m_DistanceForNewPoint)
        {
            m_CurrentLineRenderer.positionCount++;
            int positionIndex = m_CurrentLineRenderer.positionCount - 1;
            

            m_CurrentLineRenderer.SetPosition(positionIndex, pos);
            m_LastPosition = pos;
        }
    }
}
