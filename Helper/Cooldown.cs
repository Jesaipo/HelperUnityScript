using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class Cooldown : MonoBehaviour
{
    public UnityEvent OnCooldownOver;

    public float cooldownValue;
    public bool TriggerOnStart = false;

    float _cooldown = -1f;

    bool _IsOver = false;


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
        _cooldown = cooldownValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (_cooldown > 0f)
        {
            _cooldown -= Time.deltaTime;
            if (_cooldown < 0f)
            {
                OnCooldownOver.Invoke();
                _IsOver = true;
            }
        }
    }
}
