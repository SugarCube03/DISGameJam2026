using UnityEngine;

public class Music : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource musicSource; // Audio SOurce

    [Header("Sound Clips")]
    public AudioClip musicClipOne; // First Sound
    public AudioClip musicClipTwo; // Second Sound

    // Play music, both of the music will play at the smae time
    public void PlayMusic()
    {
        if (musicSource == null) return;

        if (musicClipOne != null)
        {
            musicSource.PlayOneShot(musicClipOne);
        }

        if (musicClipTwo != null)
        {
            musicSource.PlayOneShot(musicClipTwo);
        }
    }
}