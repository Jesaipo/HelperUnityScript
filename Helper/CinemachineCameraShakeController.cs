using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineCameraShakeController : MonoBehaviour
{
    public string UniqueID;

    public AnimationCurve AmplitudeCurve;

    public AnimationCurve FrequencyCurve;

    CinemachineVirtualCamera _camera;
    float _maxDuration = 0f;
    float _currentTime = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        _camera = this.GetComponent<CinemachineVirtualCamera>();
        foreach(var key in AmplitudeCurve.keys)
        {
            if(key.time > _maxDuration)
            {
                _maxDuration = key.time;
            }
        }

        foreach (var key in FrequencyCurve.keys)
        {
            if (key.time > _maxDuration)
            {
                _maxDuration = key.time;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentTime < _maxDuration)
        {
            _currentTime += Time.deltaTime;
            EraseCameraValue();
        }
    }

    void EraseCameraValue()
    {
        var perlin =_camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = AmplitudeCurve.Evaluate(_currentTime);
        perlin.m_FrequencyGain = FrequencyCurve.Evaluate(_currentTime);
    }

    public void Shake()
    {
        _currentTime = 0.0f;
        EraseCameraValue();
    }

    static public void Shake(string ID)
    {
        CinemachineCameraShakeController[] components = GameObject.FindObjectsOfType<CinemachineCameraShakeController>();
        foreach(CinemachineCameraShakeController component in components)
        {
            if(component.UniqueID == ID)
            {
                component.Shake();
                return;
            }
        }
        Debug.LogError("CinemachineCameraShakeController:: static Shake have an invalid input ID : " + ID);
    }
}
