using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using MyBox;

public class Cooldown : MonoBehaviour
{
    [ButtonMethod(ButtonMethodDrawOrder.BeforeInspector)]
    private void DebugCooldownOver()
    {
        FinishNow();
    }

    public string m_Description;

    public UnityEvent OnCooldownOver;

    public float cooldownValue;

    public bool randomBetween2Value = false;
    [MyBox.ConditionalField(nameof(randomBetween2Value))]
    public float lowercooldownValue = 0;


    public bool TriggerOnStart = false;

    public bool m_ActivateTimeString = false;

    public UnityEvent<string> m_OnStringUpdated;

    float _cooldown = -1f;

    bool _IsOver = false;

    public float m_TimeReachedForEvent = -1f;
    bool m_TimeReachedForEventFlag = true;

    public UnityEvent m_OnTimeReached;

    public void FinishNow()
    {
        _cooldown = 0.001f;
    }

    public bool IsOver()
    {
        return _IsOver;
    }

    void Start()
    {
        if(TriggerOnStart)
            Trigger();
    }
    public void Trigger()
    {
        if(randomBetween2Value)
        {
            _cooldown = MyRandom.GetRandomFloatRange(lowercooldownValue, cooldownValue);
        }
        else
        {
            _cooldown = cooldownValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;

            if(m_TimeReachedForEventFlag && _cooldown < m_TimeReachedForEvent)
            {
                m_TimeReachedForEventFlag = false;
                m_OnTimeReached.Invoke();
            }

            if (_cooldown < 0f)
            {
                OnCooldownOver.Invoke();
                _IsOver = true;
                if(m_ActivateTimeString)
                {
                    m_OnStringUpdated.Invoke(MinuteSecond(0f));
                }
            }

            if (m_ActivateTimeString)
            {
                m_OnStringUpdated.Invoke(MinuteSecond(_cooldown));
            }
        }
    }

    string MinuteSecond(float time)
    {
        int minute = (int)(time / 60);
        int second = (int)(time % 60);

        return minute.ToString("0") + ":" + second.ToString("00");
    }
}
