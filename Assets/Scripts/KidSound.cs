using UnityEngine;

public class KidSound : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource audioSource;

    public AudioClip studyingClip;
    public AudioClip lickingClip;

    void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // kid should start by studying
    void Start()
    {
        PlayState(KidAnimation.KidAnimState.Studying);
    }

    // game manager calls this every time the kid changes state
    public void PlayState(KidAnimation.KidAnimState newState)
    {
        switch (newState)
        {
            case KidAnimation.KidAnimState.Licking:
                PlayLoop(lickingClip);
                break;

            case KidAnimation.KidAnimState.Studying:
            default:
                PlayLoop(studyingClip);
                break;
        }
    }

    // the round is over, everything goes quiet
    public void StopSound()
    {
        audioSource.Stop();
    }

    // starts a looping sound, or stops playing if the clip is empty
    private void PlayLoop(AudioClip clip)
    {
        if (clip == null)
        {
            audioSource.Stop();
            return;
        }

        // the lick state is set every frame, so dont restart what is already playing
        if (audioSource.clip == clip && audioSource.isPlaying)
        {
            return;
        }

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
