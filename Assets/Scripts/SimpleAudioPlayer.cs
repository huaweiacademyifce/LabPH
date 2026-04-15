using UnityEngine;

public class SimpleAudioPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    public void PlayAudio()
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}