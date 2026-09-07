using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private InputAction lickingKey;
    public Mom momManager;
    public KidAnimation kid;
    public float lickPower = 0.3f;
    public float lickBoost = 1.2f;
    private float frostingProgress = 0f;
    private bool lickingTime;
    public float frostingTarget;
    private bool gameEnded = false;

    public float timeLimit = 30f;
    private float timeRemaining;
    public enum EndReason
    {
        Won,
        Caught,
        TimedOut
    }
    private Coroutine lickCoroutine;

    void Awake()
    {
        lickingKey = InputSystem.actions.FindAction("Jump");
        timeRemaining = timeLimit;
        StartCoroutine(CountdownTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            return;
        } 
        
        lickingTime = lickingKey.IsPressed();
        if (lickingTime)
        {
            if(momManager.DistractionStatus()== true){
                HandleLick();
            }
            else
            {
                EndGame(EndReason.Caught);
            }
        }else {
            if(lickCoroutine != null)
            {
                StopCoroutine(lickCoroutine);//stops the reference of the currently running lick coroutine
                //TODO: call the function to change the kid animation to studying
            }
            lickCoroutine = null;
            if (momManager.DistractionStatus() == false) //not distracted
            {
                DecayProgress();
            }
        }
            

    }

    private void HandleLick(){
        if (lickCoroutine != null) return; // already running, do nothing
        lickCoroutine = StartCoroutine(InTheLickZone());
    }
    private void DecayProgress()
    {
         frostingProgress -= momManager.GetMomPower() * Time.deltaTime;
    }

    IEnumerator InTheLickZone(){
        while (true){
            frostingProgress += lickPower * Time.deltaTime;
            if (frostingProgress >= frostingTarget)
            {
                EndGame(EndReason.Won);
                yield break;
            }
        //TODO: call kid animation
            yield return null;
        }
    }

    private IEnumerator CountdownTimer()
    {
        while (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            //TODO: update  UI timer 

            yield return null;
        }

        // loop ended cuz timer hit zero
        EndGame(EndReason.TimedOut);
    }

   private void EndGame(EndReason reason)
    {
        if (gameEnded) return;
        gameEnded = true;

        StopAllCoroutines();

        if (lickCoroutine != null)
        {
            lickCoroutine = null;
        }
        

        switch (reason)
        {
            case EndReason.Won:
                // show win elements
                break;
            case EndReason.Caught:
                // show caught elements
                break;
            case EndReason.TimedOut:
                // show timed-out elements
                break;
        }
    }
}
