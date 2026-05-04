using UnityEngine;
using Oculus.Interaction;

public class GrabAudioOnSelect : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip grabSound;

    private Grabbable grabbable;

    private bool hasPlayedOnce = false; // 🔥 nunca mais reseta

    void Awake()
    {
        grabbable = GetComponent<Grabbable>();
    }

    void Update()
    {
        if (grabbable == null || hasPlayedOnce) return;

        // 👉 detecta se está sendo segurado
        if (grabbable.SelectingPointsCount > 0)
        {
            PlaySound();
            hasPlayedOnce = true; // 🔥 trava pra sempre
        }
    }

    void PlaySound()
    {
        if (audioSource != null && grabSound != null)
        {
            AudioManager.Instance.Play(grabSound);
        }
    }
}