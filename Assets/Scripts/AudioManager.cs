using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Fonte única de áudio")]
    public AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Play(AudioClip clip)
    {
        if (audioSource == null || clip == null)
            return;

        // interrompe qualquer áudio anterior
        audioSource.Stop();

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void Stop()
    {
        if (audioSource != null)
            audioSource.Stop();
    }
}