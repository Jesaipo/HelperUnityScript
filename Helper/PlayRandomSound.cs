using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayRandomSound : MonoBehaviour
{
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
        audioSource.clip = sounds[Random.Range(0, sounds.Length)];
        if(useRandomPitch) audioSource.pitch = Random.Range(minPitch, maxPitch);
        if(useRandomVolume) audioSource.volume = Random.Range(minVolume, maxVolume);
        audioSource.Play();
    }

    public void SpawnAndPlay() {
        GameObject obj = Instantiate(gameObject, transform.position, transform.rotation);
        obj.GetComponent<PlayRandomSound>().Play();
        Destroy(obj, 4f);
    }
}
