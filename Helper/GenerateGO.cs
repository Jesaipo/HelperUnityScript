using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGO : MonoBehaviour
{
    public enum Mode
    {
        GenerateOnGlobal,
        GenerateOnParent
    }
    public Mode m_Mode = Mode.GenerateOnGlobal;

    public GameObject GOToGenerate;
    public float Cooldown = 2.0f;
    float CurrentCooldown = -1.0f;

    public bool DebugTriggerGeneration = false;

    public void GenerateGOIfPossible()
    {
        if(CurrentCooldown <= 0)
        {
            ForceGenerateGO();
        }
    }

    public void ForceGenerateGO()
    {
        CurrentCooldown = Cooldown;
        if (GOToGenerate)
        {
            if (m_Mode == Mode.GenerateOnGlobal)
            {
                Instantiate(GOToGenerate, this.transform.position, this.transform.rotation);
            }
            else if(m_Mode == Mode.GenerateOnParent)
            {
                Instantiate(GOToGenerate, this.transform.position, this.transform.rotation, this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(DebugTriggerGeneration)
        {
            DebugTriggerGeneration = false;
            ForceGenerateGO();
        }

        if(CurrentCooldown > 0)
        {
            CurrentCooldown -= Time.deltaTime;
        }
    }
}
