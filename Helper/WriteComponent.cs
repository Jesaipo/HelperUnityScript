using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.Events;

[RequireComponent(typeof(GenerateSoundEmmitter))]
public class WriteComponent : MonoBehaviour
{
    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DebugText()
    {
        Write(m_DefaultString);
    }

    public string m_DefaultString;

    string m_ToWriteString;
    public float m_WriteSpeedPerS = 5f;
    int m_CurrentChar = -1;
    float m_CD = -1f;

    public bool m_WriteOnStart = false;

    public TMPro.TextMeshProUGUI m_TextComponent;

    public UnityEvent m_OnWriteOver;

    public UnityEvent m_OnPassDialog;

    GenerateSoundEmmitter m_SoundEmmitter;

    int m_NumberOfClick = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_SoundEmmitter = GetComponent<GenerateSoundEmmitter>();
        if (m_WriteOnStart)
        {
            Write(m_DefaultString);
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_CD -= Time.deltaTime;
        if (m_CurrentChar >= 0)
        {
            if(m_CD < 0)
            {
                m_CurrentChar++;
                m_SoundEmmitter.GenerateSoundEmmiter();
                if (m_CurrentChar > m_ToWriteString.Length)
                {
                    m_CurrentChar = -1;
                    m_OnWriteOver.Invoke();
                    m_NumberOfClick = 1; //Prochain click fait passer
                    return;
                }

                m_CD = 1f / m_WriteSpeedPerS;
                m_TextComponent.text = m_ToWriteString.Substring(0, m_CurrentChar);
            }
        }
    }

    public void Write(string text)
    {
        SetText(text);
        StartWriting();
    }

    public void SetText(string text)
    {
        m_ToWriteString = text;
    }

    public void StartWriting()
    {
        m_TextComponent.text = "";
        m_CurrentChar = 0;
    }

    public void Clean()
    {
        m_TextComponent.text = "";
        m_NumberOfClick = 0;
    }

    public void OnClickForSpeedUp()
    {
        m_NumberOfClick++;
        if(m_NumberOfClick == 1)
        {
            m_CurrentChar = m_ToWriteString.Length -1;
        }

        if (m_NumberOfClick == 2)
        {
            m_OnPassDialog.Invoke();
        }
    }
}
