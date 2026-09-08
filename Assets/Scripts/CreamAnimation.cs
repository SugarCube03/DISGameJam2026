using UnityEngine;

public class CreamManager : MonoBehaviour
{
    [Header("Cream Size Range")]
    public float maxScale = 1f;   // the size of cream when the progress is 0
    public float minScale = 0.2f; // the size of cream when the progress is about to reach 1(almost finish)

    // call this function and put the progress between 0-1
    // the progress closer to 1, smaller the cream; closer to 0, larger the cream
    public void SetProgress(float progress)
    {
        progress = Mathf.Clamp01(progress);//restrict the number between 0 and 1
        float currentScale = Mathf.Lerp(maxScale, minScale, progress);// linear interpolation
        transform.localScale = new Vector3(currentScale*1.30477000005f, currentScale, currentScale);
    }
}