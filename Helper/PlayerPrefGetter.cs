using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPrefGetter : MonoBehaviour
{
    public string m_PlayerPrefKey;
    public bool m_GetOnStart;
    public bool m_GetOnUpdate = false;
    public UnityEvent m_OnPlayerPrefFound;
    public UnityEvent<string> m_OnPlayerPrefFoundString;
    public UnityEvent<int> m_OnPlayerPrefFoundInt;
    public UnityEvent m_OnPlayerPrefNOTFound;

    public void FoundPlayerRef()
    {
        
        if(PlayerPrefs.GetString(m_PlayerPrefKey, "NotFound") != "NotFound" || PlayerPrefs.GetInt(m_PlayerPrefKey, -1) != -1)
        {
            m_OnPlayerPrefFound.Invoke();
            string s = PlayerPrefs.GetString(m_PlayerPrefKey, "NotFound");
            if (s != "NotFound")
            {
                m_OnPlayerPrefFoundString.Invoke(s);
            }
            else
            {
                m_OnPlayerPrefFoundInt.Invoke(PlayerPrefs.GetInt(m_PlayerPrefKey, -1));
            }
               
        }
        else
        {
            m_OnPlayerPrefNOTFound.Invoke();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(m_GetOnStart)
        {
            FoundPlayerRef();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_GetOnUpdate)
        {
            FoundPlayerRef();
        }
    }
}
