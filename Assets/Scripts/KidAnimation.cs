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

public class KidAnimation : MonoBehaviour
{
    public enum KidAnimState
    {
        Studying,
        Licking
    }
    [Header("Animation Speed")]
public float minFrameDelay = 0.08f; // fastest gap between frames (seconds)
public float maxFrameDelay = 0.3f;  // slowest gap between frames (seconds)

  [Header("Idle mode")]
    public Sprite studyingSprite;

    [Header("Licking Sprite")]
    public LickingFrame[] lickingFrames; // 5 different licking sprites, each frame can set transform/rotation/flip

    [Header("Animation Speed")]
    public float frameRate = 8f;

    [Header("Random")]
    public bool randomOrder = false; // false = loop in sequence, true = pick randomly

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Coroutine currentAnimCoroutine;
    private KidAnimState currentState;

    private bool stopAll = false;
     void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        PlayState(KidAnimState.Studying);
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

        public void PlayState(KidAnimState newState)
        {
            // if the state is already being played dont restart
            if (currentState == newState && currentAnimCoroutine != null) return;

            currentState = newState;

            switch (newState)
            {
                case KidAnimState.Licking:
                    PlayAnimation(LickingAnimation());
                    break;

                case KidAnimState.Studying:
                default:
                    PlayAnimation(StudyingAnimation());
                    break;
            }
        }

        private IEnumerator StudyingAnimation()
        {
            spriteRenderer.sprite = studyingSprite;
            spriteRenderer.flipX = false;
            spriteRenderer.flipY = false;
            transform.position = originalPosition;
            transform.rotation = originalRotation;

            yield break;
        }

        private IEnumerator LickingAnimation()
        {
            if (lickingFrames == null || lickingFrames.Length == 0)
                yield break;

            while (true)
            {
                if (randomOrder)
                    currentIndex = Random.Range(0, lickingFrames.Length);
                else
                    currentIndex = (currentIndex + 1) % lickingFrames.Length;

                ApplyFrame(lickingFrames[currentIndex]);

                 float delay = Random.Range(minFrameDelay, maxFrameDelay);
                yield return new WaitForSeconds(delay);
            }
        }

    
    private void ApplyFrame(LickingFrame frame)
    {
        spriteRenderer.sprite = frame.sprite;
        spriteRenderer.flipX = frame.flipX;
        spriteRenderer.flipY = frame.flipY;

        transform.position = originalPosition + new Vector3(frame.positionOffset.x, frame.positionOffset.y, 0f);
        transform.rotation = originalRotation * Quaternion.Euler(0f, 0f, frame.rotationOffset);
    }


    public void ResetToStudying()
    {
        spriteRenderer.sprite = studyingSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    public void SetStop(bool newStopValue)
    {
        stopAll= true;
    }
}