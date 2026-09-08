using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource audioSource;

    public AudioClip calmClip;  // plays while he is studying
    public AudioClip tenseClip; // plays while he is licking

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Start()
    {
        SetLicking(false);
    }

    // game manager calls this when the kid starts or stops licking
    public void SetLicking(bool isLicking)
    {
        AudioClip clip = calmClip;

        if (isLicking)
        {
            clip = tenseClip;
        }

        // already playing this one, dont restart it
        if (audioSource.clip == clip && audioSource.isPlaying)
        {
            return;
        }

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    // the round is over, everything goes quiet
    public void StopSound()
    {
        audioSource.Stop();
    }
}
