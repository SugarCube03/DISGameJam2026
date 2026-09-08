using UnityEngine;
using System.Collections;

public class MomAnimation : MonoBehaviour
{
    [Header("Frosting Sprite")]
    public Sprite frostingSprite;

    [Header("Thinking Sprite")]
    public Sprite thinkingSprite;

    [Header("Distracted Sprite")]
    public Sprite distractedSprite;

    [Header("Turning Back Sprite")]
    public Sprite turningBackSprite;

    [Header("Sounds")]
    public AudioSource mainSource; // need two others so that we can play sounds together
    public AudioSource creamSource;
    public AudioSource hummingSource;

    // first three clips for frosting state; fourth is for thinking; fifth is for distracted; sixth
    // is for turning back
    public AudioClip pipingBagClip;
    public AudioClip creamClip;
    public AudioClip hummingClip;
    public AudioClip hmClip;
    public AudioClip stirringClip;
    public AudioClip huhClip;


    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Coroutine currentAnimCoroutine;
    private Mom.State currentState;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Start()
    {
        PlayState(Mom.State.Frosting);
    }

    private void PlayAnimation(IEnumerator animationCoroutine)
    {
        if (currentAnimCoroutine != null)
        {
            StopCoroutine(currentAnimCoroutine);
        }
        currentAnimCoroutine = StartCoroutine(animationCoroutine);
    }

    public void PlayState(Mom.State newState)
    {
        // if the state is already being played dont restart
        if (currentState == newState && currentAnimCoroutine != null) return;

        currentState = newState;

        switch (newState)
        {
            case Mom.State.Thinking:
                PlayAnimation(ThinkingAnimation());
                break;

            case Mom.State.Distracted:
                PlayAnimation(DistractedAnimation());
                break;

            case Mom.State.TurningBack:
                PlayAnimation(TurningBackAnimation());
                break;

            case Mom.State.Frosting:
            default:
                PlayAnimation(FrostingAnimation());
                break;
        }
    }

    private IEnumerator ThinkingAnimation()
    {
        PlayLoop(mainSource, null);
        PlayOnce(mainSource, hmClip);
        PlayLoop(creamSource, null);
        PlayLoop(hummingSource, null);

        spriteRenderer.sprite = thinkingSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        yield break;
    }

    private IEnumerator DistractedAnimation()
    {
        PlayLoop(mainSource, stirringClip);
        PlayLoop(creamSource, null);
        PlayLoop(hummingSource, null);

        spriteRenderer.sprite = distractedSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        yield break;
    }

    private IEnumerator FrostingAnimation()
    {
        PlayLoop(mainSource, pipingBagClip);
        PlayLoop(creamSource, creamClip);
        PlayLoop(hummingSource, hummingClip);

        spriteRenderer.sprite = frostingSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        yield break;
    }

    private IEnumerator TurningBackAnimation()
    {
        PlayLoop(mainSource, null);
        PlayOnce(mainSource, huhClip);
        PlayLoop(creamSource, null);
        PlayLoop(hummingSource, null);

        spriteRenderer.sprite = turningBackSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        yield break;
    }

    public void StopAnimating()
    {
        StopAllCoroutines();
        currentAnimCoroutine = null;

        mainSource.Stop();
        creamSource.Stop();
        hummingSource.Stop();
    }

    // starts a looping sound, or stops playing if clip is empty
    private void PlayLoop(AudioSource source, AudioClip clip)
    {
        if (clip == null)
        {
            source.Stop();
            return;
        }

        source.clip = clip;
        source.loop = true;
        source.Play();
    }

    private void PlayOnce(AudioSource source, AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        source.PlayOneShot(clip);
    }
}