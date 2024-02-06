using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenerateSoundEmmitter : MonoBehaviour
{
    public GameObject SoundObject;
    public float Cooldown = 2.0f;
    float CurrentCooldown = -1.0f;
    public UnityEvent<GameObject> GameObjectGeneratedEvent;

    public void GenerateSoundEmmiter()
    {
        if(CurrentCooldown < 0)
        {
            CurrentCooldown += Cooldown;
            if (SoundObject)
            {
                var go = Instantiate(SoundObject, this.transform.position, this.transform.rotation);
                GameObjectGeneratedEvent.Invoke(go);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentCooldown >= 0)
        {
            CurrentCooldown -= Time.deltaTime;
        }
    }
}
