using UnityEngine;

public class MomAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;

    private void FrostingAnimation()
    {
        // plays the bomb animation, then explodes
        /*private IEnumerator ExplodeAfterDelayRoutine()
        {
            float frameDelay = explosionDelay / frames.Length;

            foreach (Sprite frame in frames)
            {
                sr.sprite = frame;
                yield return new WaitForSeconds(frameDelay);
            }

            Explode();
        } */
    }

    private void ThinkingAnimation()
    {

    }

    private void DistractedAnimation()
    {

    }
}
