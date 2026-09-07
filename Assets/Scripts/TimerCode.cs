
using UnityEngine;
using TMPro;


public class TimerCode : MonoBehaviour
{
    private TextMeshProUGUI timerText;
 
    void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
 
    public void SetTime(float secondsRemaining)
    {
        if (timerText == null) return;
 
        // never show negative time
        if (secondsRemaining < 0f) secondsRemaining = 0f;
 
        int minutes = Mathf.FloorToInt(secondsRemaining / 60f);
        int seconds = Mathf.FloorToInt(secondsRemaining % 60f);
 
        // "SS" part padded to 2 digits, minutes NOT padded (so 1:28, not 01:28)
        timerText.text = minutes + ":" + seconds.ToString("00");
    }

}
