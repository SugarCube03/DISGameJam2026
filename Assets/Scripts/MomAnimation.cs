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
        spriteRenderer.sprite = thinkingSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        yield break;
    }

    private IEnumerator DistractedAnimation()
    {
        spriteRenderer.sprite = distractedSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        yield break;
    }

    private IEnumerator FrostingAnimation()
    {
        spriteRenderer.sprite = frostingSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        yield break;
    }

    private IEnumerator TurningBackAnimation()
    {
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
    }
}