using UnityEngine;
using System.Collections;
using UnityEditor.Rendering;
[System.Serializable]
public class LickingFrame
{
    public Sprite sprite;
    public Vector2 positionOffset = Vector2.zero; // transform compare to the original position
    public float rotationOffset = 0f;              // rotation
    public bool flipX = false;                      // flip(horizontal)
    public bool flipY = false;                       // flip(vertical)
}

public class MomAnimation : MonoBehaviour
{
    public enum MomAnimState
    {
        Frosting,
        Thinking,
        Distracted
    }
    [Header("Animation Speed")]
    public float minFrameDelay = 0.08f; // fastest gap between frames (seconds)
    public float maxFrameDelay = 0.3f;  // slowest gap between frames (seconds)

    [Header("Frosting Sprite")]
    public Sprite frostingSprite;

    [Header("Thinking Sprite")]
    public Sprite thinkingSprite;

    [Header("Distracted Sprite")]
    public Sprite distractedSprite;

    [Header("Animation Speed")]
    public float frameRate = 8f;

    [Header("Random")]
    public bool randomOrder = false; // false = loop in sequence, true = pick randomly

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Coroutine currentAnimCoroutine;
    private MomAnimState currentState;

    private bool stopAll = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        PlayState(MomAnimState.Frosting);
    }

    void Update()
    {
        if (stopAll)
        {
            StopAllCoroutines();
        }
    }

    private void PlayAnimation(IEnumerator animationCoroutine)
    {
        if (currentAnimCoroutine != null)
        {
            StopCoroutine(currentAnimCoroutine);
        }
        currentAnimCoroutine = StartCoroutine(animationCoroutine);
    }

    public void PlayState(MomAnimState newState)
    {
        // if the state is already being played dont restart
        if (currentState == newState && currentAnimCoroutine != null) return;

        currentState = newState;

        switch (newState)
        {
            case MomAnimState.Thinking:
                PlayAnimation(ThinkingAnimation());
                break;

            case MomAnimState.Distracted:
                PlayAnimation(DistractedAnimation());
                break;

            case MomAnimState.Frosting:
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


    public void ResetToFrosting()
    {
        spriteRenderer.sprite = frostingSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    public void SetStop(bool newStopValue)
    {
        stopAll = true;
    }
}