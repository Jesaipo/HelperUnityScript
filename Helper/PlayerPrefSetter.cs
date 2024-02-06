using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.SceneManagement;

public class PlayerPrefSetter : MonoBehaviour
{
    public bool m_UseSceneName = true;
    private string m_SceneName = "TagHere";
    public bool m_SetIfGreaterOnly = true;

    [ConditionalField(nameof(m_UseSceneName), inverse:true)]
    public string m_Key;

    public void SetStringValueForKey(string value)
    {
        PlayerPrefs.SetString((m_UseSceneName) ? m_SceneName : m_Key, value);
    }
    public void SetValueForKey(string value)
    {
        int intValue = int.Parse(value);
        SetValueForKey(intValue);
    }
    public void SetValueForKey(int value)
    {
        if(m_SetIfGreaterOnly)
        {
            int playerPrefValue = PlayerPrefs.GetInt((m_UseSceneName) ? m_SceneName : m_Key, 0);
            if(value < playerPrefValue)
            {
                return;
            }
        }

        PlayerPrefs.SetInt((m_UseSceneName) ? m_SceneName : m_Key, value);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_SceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
