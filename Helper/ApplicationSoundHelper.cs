using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ApplicationSoundHelper : MonoBehaviour
{
    int m_CurrentSound = 100;



    public UnityEvent m_OnSoundMuted;
    public UnityEvent m_OnSoundUnmuted;
    public UnityEvent<int> m_OnSoundSaved;
    public UnityEvent<float> m_OnInitSound;

    public void SetAndSaveSound(float sound)
    {
        SetAndSaveSound((int)(sound * 100f));
    }

    public void InitSound(int soundPercent)
    {
        m_OnInitSound.Invoke((float)(soundPercent) / 100f);
        SetAndSaveSound(soundPercent);
    }
    public void SetAndSaveSound(int soundPercent)
    {
        m_CurrentSound = soundPercent;
        SetSound(soundPercent);
        m_OnSoundSaved.Invoke(soundPercent);
    }

    void SetSound(int soundPercent)
    {
        AudioListener.volume = (float)(soundPercent) / 100f;
        if(soundPercent == 0)
        {
            m_OnSoundMuted.Invoke();
        }
        else
        {
            m_OnSoundUnmuted.Invoke();
        }
    }

    public void Mute()
    {
        SetSound(0);
    }

    public void Unmute()
    {
        SetSound(m_CurrentSound);
    }

}
