using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomSound : MonoBehaviour
{
    public float m_DestroyTime = 4.0f;

    public AudioClip[] sounds;
    public bool playOnAwake = false;
    public bool useRandomPitch = false;
    public float minPitch;
    public float maxPitch;
    public bool useRandomVolume = false;
    public float minVolume;
    public float maxVolume;

    AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        if(playOnAwake) Play();
    }

    public void Play() {
        audioSource.clip = sounds[MyRandom.GetRandomIntRange(0, sounds.Length)];
        if(useRandomPitch) audioSource.pitch = MyRandom.GetRandomFloatRange(minPitch, maxPitch);
        if(useRandomVolume) audioSource.volume = MyRandom.GetRandomFloatRange(minVolume, maxVolume);
        audioSource.Play();
    }

    public void SpawnAndPlay() {
        GameObject obj = Instantiate(gameObject, transform.position, transform.rotation);
        obj.GetComponent<PlayRandomSound>().Play();
        Destroy(obj, m_DestroyTime);
    }
}
