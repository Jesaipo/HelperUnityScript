using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{

    List<Animator> m_TreeAnimators = new List<Animator>();
    public float m_WindTimer = 3;
    float m_CurrentWindTimer = 3;
    int m_CurrentIndexTree = 0;
    // Start is called before the first frame update
    void Start()
    {
        var temp = GameObject.FindGameObjectsWithTag("Tree");
        foreach(GameObject go in temp)
        {
            m_TreeAnimators.Add(go.GetComponent<Animator>());
        }

        m_TreeAnimators.Sort((p1, p2) => (p1.transform.position.x - p1.transform.position.y).CompareTo(p2.transform.position.x - p2.transform.position.y));

        m_CurrentWindTimer = m_WindTimer;
    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentWindTimer -= Time.deltaTime;
        if(m_CurrentWindTimer > 0)
        {
            float percent = 1 - (m_CurrentWindTimer / m_WindTimer);
            int toGoIndex =(int)(percent * m_TreeAnimators.Count);
            for(; m_CurrentIndexTree < toGoIndex; m_CurrentIndexTree++)
            {
                m_TreeAnimators[m_CurrentIndexTree].SetTrigger("Wind");
            }
        }
        else
        {
            for (; m_CurrentIndexTree < m_TreeAnimators.Count; m_CurrentIndexTree++)
            {
                m_TreeAnimators[m_CurrentIndexTree].SetTrigger("Wind");
            }
            m_CurrentIndexTree = 0;
            m_CurrentWindTimer = m_WindTimer;
        }
    }
}
