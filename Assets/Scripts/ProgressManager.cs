using UnityEngine;
using UnityEngine.UI;
using TMPro; //stands for text mesh pro
using UnityEngine.SceneManagement;

public class ProgressManager : MonoBehaviour
{
    [Header("UI")]
    public Slider progressSlider;       // drag the slider
    public TextMeshProUGUI percentText; // drag percentage text

    private float currentProgress = 0f;

    void Update()
    {
      
        UpdateUI();
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


    public void SetProgress(float newProgress)
    {
        currentProgress = Mathf.Clamp01(newProgress);
    }
}