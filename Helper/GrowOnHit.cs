using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GrowOnHit : MonoBehaviour
{

    public GameObject m_Target;
    public bool m_DebugHit = false;

    public int m_HitNumber = 0;

    public Vector3 m_GrowEachHit;
    public int m_MaxGrowCount = 10;
    public int m_MinGrowCount = -5;
    public float m_TimeBeforeGoBackToNormal = 2f;
    public float m_LoseHitTimer = 0.5f;
    float m_CurrentLoseHitTimer;
    float m_CurrentTimerBeforeGoBackToNormal;
    public float m_TimeGrow= 0.5f;
    float m_CurrentGrow;

    Vector3 m_InitialSize;
    Vector3 m_CurrentSize;

    public UnityEvent<int> OnHit;
    public UnityEvent OnNegativeHit;
    public UnityEvent OnPostiveHit;

    // Start is called before the first frame update
    void Start()
    {
        m_InitialSize = m_Target.transform.localScale;
    }

    void Hit(int count)
    {
        if (m_HitNumber == m_MaxGrowCount)
        {
            m_HitNumber--;
            Hit(0);
        }
        else if (m_HitNumber == m_MinGrowCount)
        {
            m_HitNumber++;
            Hit(0);
        }
        else 
        { 
            m_HitNumber = Mathf.Min(m_HitNumber + count, m_MaxGrowCount);
            m_CurrentTimerBeforeGoBackToNormal = m_TimeBeforeGoBackToNormal;

            m_CurrentSize = m_Target.transform.localScale;
            m_CurrentGrow = 0;
        }

        OnHit.Invoke(count);

        if(count < 0)
        {
            OnNegativeHit.Invoke();
        }
        else if(count > 0)
        {
            OnPostiveHit.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_DebugHit)
        {
            Hit(1);
            m_DebugHit = false;
        }

        m_CurrentTimerBeforeGoBackToNormal -= Time.deltaTime;
        m_CurrentLoseHitTimer -= Time.deltaTime;

        if (m_HitNumber != 0 && m_CurrentTimerBeforeGoBackToNormal < 0)
        {
            if(m_CurrentLoseHitTimer < 0)
            {
                m_CurrentLoseHitTimer = m_LoseHitTimer;
                if (m_HitNumber > 0)
                {
                    m_HitNumber--;
                }
                else
                {
                    m_HitNumber++;
                }
                m_CurrentSize = m_Target.transform.localScale;
                m_CurrentGrow = 0;
            }
            
        }

        m_CurrentGrow += Time.deltaTime;
        Vector3 scale;
        if (m_CurrentGrow < m_TimeGrow)
        {
            scale = Vector3.Lerp(m_CurrentSize, GetTargetSize(), m_CurrentGrow / m_TimeGrow);
            
        }
        else
        {
            scale = GetTargetSize();
        }
        
        m_Target.transform.localScale = scale;

    }

    Vector3 GetTargetSize()
    {
        return m_InitialSize + m_GrowEachHit * m_HitNumber;
    }

    public void OnParticuleHit(int count)
    {
        Hit(count);
    }
}
