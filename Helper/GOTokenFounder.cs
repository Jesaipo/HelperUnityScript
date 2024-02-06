using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.Events;


public class GOTokenFounder : MonoBehaviour
{
    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DebugFound()
    {
        Found();
    }

    public string m_GOTokenName;

    public bool m_FoundOnStart = false;

    public UnityEvent m_OnTokenNotFound;
    public UnityEvent m_OnTokenFound;

    // Start is called before the first frame update
    void Start()
    {
        if(m_FoundOnStart)
        {
            Found();
        }
    }

    public void Found()
    {
        var tokens = Object.FindObjectsOfType<GOToken>();
        foreach(GOToken token in tokens)
        {
            if(token.m_TokenName == m_GOTokenName)
            {
                m_OnTokenFound.Invoke();
                return;
            }
        }

        m_OnTokenNotFound.Invoke();

        //create token
        GameObject GOTokenGO = new GameObject("GOToken_" + m_GOTokenName);
        GOToken GOToken = GOTokenGO.AddComponent<GOToken>();
        GOToken.m_TokenName = m_GOTokenName;

        DontDestroyOnLoad(GOTokenGO);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
