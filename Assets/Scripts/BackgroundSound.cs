using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource audioSource;

    public AudioClip calmClip;  // plays while he is studying
    public AudioClip tenseClip; // plays while he is licking
    private bool currentlyLicking = false;

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Start()
    {
        
        audioSource.loop = true;
        audioSource.clip  = calmClip;
        audioSource.Play();
        SetLicking(false);
    }

    // game manager calls this when the kid starts or stops licking
    public void SetLicking(bool isLicking)
    {

        if (isLicking && !currentlyLicking)
        {
            if (audioSource.clip == calmClip && audioSource.isPlaying)
            {
                 audioSource.Stop();
            audioSource.clip = tenseClip;
            audioSource.Play();
            currentlyLicking = true;
            }
            else
            {
                return;
            }
           

        }

        if(!isLicking && currentlyLicking)
        {
            if (audioSource.clip == tenseClip && audioSource.isPlaying)
            {
            audioSource.Stop();
            audioSource.clip = calmClip;
            audioSource.Play();
            currentlyLicking = false;
            }
            else
            {
                return;
            }
            }
        }
    

    // the round is over, everything goes quiet
    public void StopSound()
    {
        audioSource.Stop();
    }
}