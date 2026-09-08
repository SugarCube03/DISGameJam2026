using UnityEngine;

public class MomSound : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource audioSource;

    public AudioClip frostingClip;
    public AudioClip thinkingClip;
    public AudioClip distractedClip;
    public AudioClip turningBackClip;

    // mom's behavior loop calls this every time she changes state
    public void PlayState(Mom.State newState)
    {
        switch (newState)
        {
            case Mom.State.Thinking:
                PlayLoop(null);
                PlayOnce(thinkingClip);
                break;

            case Mom.State.Distracted:
                PlayLoop(distractedClip);
                break;

            case Mom.State.TurningBack:
                PlayLoop(null);
                PlayOnce(turningBackClip);
                break;

            case Mom.State.Frosting:
            default:
                PlayLoop(frostingClip);
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

        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }

    // fires a sound once, for short vocal bits that shouldn't repeat
    private void PlayOnce(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        audioSource.PlayOneShot(clip);
    }
}
