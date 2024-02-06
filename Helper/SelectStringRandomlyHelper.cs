using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectStringRandomlyHelper : MonoBehaviour
{
    public List<string> m_StringsList = new List<string>();
    List<int> m_StringsAvaiableIndex = new List<int>();
    public bool m_AvoidRepetition = true;
    public UnityEvent<string> m_OnStringSelected;

    private void Start()
    {
        ResetIndexList();
    }

    void ResetIndexList()
    {
        int i = 0;
        foreach (var s in m_StringsList)
        {
            m_StringsAvaiableIndex.Add(i);
            i++;
        }
    }

    public void SelectString()
    {
        int index = MyRandom.GetRandomIntRange(0, m_StringsAvaiableIndex.Count);
        m_OnStringSelected.Invoke(m_StringsList[m_StringsAvaiableIndex[index]]);
        if(m_AvoidRepetition)
        {
            m_StringsAvaiableIndex.RemoveAt(index);
            if(m_StringsAvaiableIndex.Count == 0)
            {
                ResetIndexList();
            }
        }
    }
}
