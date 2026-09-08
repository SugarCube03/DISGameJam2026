using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    [Header("Sources")]
    public AudioSource calmSource;  // plays while he is studying
    public AudioSource tenseSource; // plays while he is licking

    [Header("Clips")]
    public AudioClip calmClip;
    public AudioClip tenseClip;

    [Header("Fade")]
    public float volume = 0.6f;    // how loud the music sits under everything else
    public float fadeSpeed = 4f;   // higher is a faster swap

    private bool licking = false;

    void Start()
    {
        // both tracks run the whole game, we only move the volume between them
        StartTrack(calmSource, calmClip, volume);
        StartTrack(tenseSource, tenseClip, 0f);
    }

    void Update()
    {
        float calmTarget = volume;
        float tenseTarget = 0f;

        if (licking)
        {
            calmTarget = 0f;
            tenseTarget = volume;
        }

        calmSource.volume = Mathf.MoveTowards(calmSource.volume, calmTarget, fadeSpeed * Time.deltaTime);
        tenseSource.volume = Mathf.MoveTowards(tenseSource.volume, tenseTarget, fadeSpeed * Time.deltaTime);
    }

    // game manager calls this when the kid starts or stops licking
    public void SetLicking(bool isLicking)
    {
        licking = isLicking;
    }

    // the round is over, everything goes quiet
    public void StopSound()
    {
        calmSource.Stop();
        tenseSource.Stop();
    }

    private void StartTrack(AudioSource source, AudioClip clip, float startVolume)
    {
        if (clip == null)
        {
            return;
        }

        source.clip = clip;
        source.loop = true;
        source.volume = startVolume;
        source.Play();
    }
}
