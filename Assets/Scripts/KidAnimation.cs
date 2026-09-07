using UnityEngine;

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
    [Header("Idle mode")]
    public Sprite studyingSprite; // Study sprite

    [Header("Licking Sprite")]
    public LickingFrame[] lickingFrames; // 5 different licking sprites, every frame can set the transform, rotation and flip

    [Header("Animation Speed")]
    public float frameRate = 8f; // change the frame while pressing space

    [Header("Random")]
    public bool randomOrder = false; // false looping in sequence，true pick the sprite randomly

    private SpriteRenderer spriteRenderer;
    private int currentIndex = 0;
    private Vector3 originalPosition; // record the initial position
    private Quaternion originalRotation; //record the initial angle
    private bool isLicking = false; // whether is licking
    private float frameTimer = 0f; // timer for changing frame while press space

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = studyingSprite; // initially is the study sprite

        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // Every time press the space, switch to licking sprite
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLicking = true;
            frameTimer = 0f; // reset the timer every time we pressed
            ShowNextLickingSprite();
        }

        // release the space, switch back to study sprite
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isLicking = false;
            ResetToStudying(); // when release the space, reset the location angle and flip
        }

        // keep changing frames while pressing
        if (isLicking)
        {
            frameTimer += Time.deltaTime;
            if (frameTimer >= 1f / frameRate)
            {
                frameTimer = 0f;
                ShowNextLickingSprite();
            }
        }
    }

    void ShowNextLickingSprite()
    {
        if (lickingFrames == null || lickingFrames.Length == 0) return;

        if (randomOrder)
        {
            currentIndex = Random.Range(0, lickingFrames.Length);
        }
        else
        {
            currentIndex = (currentIndex + 1) % lickingFrames.Length; // switch licking sprite in sequence
        }

        LickingFrame frame = lickingFrames[currentIndex];

        spriteRenderer.sprite = frame.sprite;
        spriteRenderer.flipX = frame.flipX;
        spriteRenderer.flipY = frame.flipY;

        // add transform from original position only for this frame
        transform.position = originalPosition + new Vector3(frame.positionOffset.x, frame.positionOffset.y, 0f);

        // add rotation only for this frame
        transform.rotation = originalRotation * Quaternion.Euler(0f, 0f, frame.rotationOffset);
    }

    void ResetToStudying()
    {
        spriteRenderer.sprite = studyingSprite;
        spriteRenderer.flipX = false;
        spriteRenderer.flipY = false;
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}