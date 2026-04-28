using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeStartController : MonoBehaviour
{
    [Header("Referências")]
    public GameObject barrier;
    public GameObject canvasStart;

    [Header("Áudio")]
    public AudioSource audioSource;
    public AudioClip startGuideAudio;

    [Header("Estado")]
    public bool practiceStarted = false;

    public void StartPractice()
    {
        if (practiceStarted) return;

        practiceStarted = true;

        // 🔊 toca áudio
        if (audioSource != null && startGuideAudio != null)
        {
            audioSource.PlayOneShot(startGuideAudio);
        }

        // 🧾 esconde canvas
        if (canvasStart != null)
        {
            canvasStart.SetActive(false);
        }

        // 🧱 remove barreira
        if (barrier != null)
        {
            barrier.SetActive(false);
        }

        Debug.Log("Prática iniciada!");
    }
}