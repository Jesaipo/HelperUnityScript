using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameDrop : MonoBehaviour
{
    private float lastTime;
    public float freezeTime = 0.1f;
    public float timeScale = 0.001f;
    public bool triggerOnStart = false;
    public float globalCooldown = 0.0f;
    private static float s_GlobalLastTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (triggerOnStart)
            Trigger();
    }

    public void Trigger()
    {
        if (this.enabled && this.gameObject.activeSelf)
        {
            if (Time.realtimeSinceStartup - s_GlobalLastTime >= globalCooldown)
            {
                lastTime = Time.realtimeSinceStartup;
                Time.timeScale = timeScale;
                s_GlobalLastTime = Time.realtimeSinceStartup;
            }
        }
    }

    public void Update()
    {
        if (Time.realtimeSinceStartup - lastTime >= freezeTime)
        {
            lastTime = float.MaxValue;
            Time.timeScale = 1f;
        }
    }
}
