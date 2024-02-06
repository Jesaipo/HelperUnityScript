using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActivateHelper : MonoBehaviour
{
    public List<GameObject> m_ToActivateGO = new List<GameObject>();

    public int m_MinNumberToActivate = 1;
    public int m_MaxNumberToActivate = 4;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> shuffle = MyRandom.RandomShuffleList(m_ToActivateGO);
        int count = MyRandom.GetRandomIntRange(m_MinNumberToActivate, m_MaxNumberToActivate);
        for (int i = 0; i < count; i++)
        {
            shuffle[i].SetActive(true);
        }
    }
}
