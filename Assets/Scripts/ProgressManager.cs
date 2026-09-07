using UnityEngine;
using UnityEngine.UI;
using TMPro; //stands for text mesh pro
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    [Header("UI")]
    public Slider progressSlider;       // drag the slider
    public TextMeshProUGUI percentText; // drag percentage text

    [Header("Progress Percentage")]
    [Range(0f, 1f)] public float currentProgress = 0f;
    public float advanceSpeed = 0.3f;
    public float regressSpeed = 0.15f;

    [Header("Regress Setting")]
    public bool enableRandomRegress = true;
    [Range(0f, 1f)] public float regressChancePerSecond = 0.1f;

    [Header("Jump to Ending")]
    public string endingSceneName = "Ending";
    private bool hasTriggeredEnding = false;

    void Update()
    {
        if (hasTriggeredEnding) return;

        // when space is pressed return true, and this is different from GetKeyDown, which only return true at the frame key was pressed
        if (Input.GetKey(KeyCode.Space))
        {
            currentProgress += advanceSpeed * Time.deltaTime;
        }

        //this make sure the current progress is between 0 and 1, and call UpdateUI to synchronize the value to the interface
        currentProgress = Mathf.Clamp01(currentProgress);
        UpdateUI();
    }

    //use the progress speed to minus the regress speed
    void TriggerRegress()
    {
        currentProgress -= regressSpeed;
    }

    //display the number of currentProgress
    void UpdateUI()
    {   
        //check whether progress slider is null
        if (progressSlider != null)
        {
            progressSlider.value = currentProgress;
        }

        //check whether text is null
        if (percentText != null)
        {
            //round to the nearest whole number
            int percent = Mathf.RoundToInt(currentProgress * 100f);
            percentText.text = percent + "%";
        }
    }
}