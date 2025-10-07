using System;
using Audio;
using FMODUnity;
using UnityEngine;

public class Puff : MonoBehaviour
{
    private StudioEventEmitter emitter;
    private ParticleSystem particle;
    
    private AudioManager audioManager;
    private FMODEvents fmodEvents;

    private void Start() {
        audioManager = AudioManager.Instance;
        fmodEvents = FMODEvents.Instance;
        particle = GetComponent<ParticleSystem>();

        emitter = audioManager.InitializeEventEmitter(fmodEvents.sheepPoof, gameObject);
    }

    public void Play() {
        emitter.Play();
        particle.Play();
    }
}
