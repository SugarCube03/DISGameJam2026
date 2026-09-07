using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private InputAction lickingKey;
    public Mom momManager;
    public KidAnimation kid;
    public ProgressManager progressBar;
    public TimerCode timer;
    public float lickPower = 0.01f;
    public float lickBoost = 1.2f;
    private float frostingProgress = 0f;
    private bool lickingTime;
    public float frostingTarget=1;
    private bool gameEnded = false;

    public float timeLimit = 30f;
    private float timeRemaining;
    //public bool distracted = true;
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
        progressBar.SetProgress(frostingProgress);
        Debug.Log("[GameManager] frostingTarget ACTUAL RUNTIME VALUE = " + frostingTarget);

        Debug.Log("[GameManager] Awake complete. gameEnded=" + gameEnded + " distracted=" + momManager.IsDistracted());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded)
        {
            Debug.Log("[GameManager] Update() early-returning because gameEnded = true");
            return;
        }

        

        lickingTime = lickingKey.IsPressed();

        // ---- DEBUG: print every frame so you can see raw press state ----
        Debug.Log("[GameManager] Update tick | lickingTime=" + lickingTime + " | distracted=" +  momManager.IsDistracted()+ " | lickCoroutine running=" + (lickCoroutine != null));

        if (lickingTime)
        {
            if(momManager.IsDistracted()== true)
            //if (distracted)
            {
                Debug.Log("[GameManager] Space is down AND distracted=true -> calling HandleLick()");
                HandleLick();
            }
            else
            {
                Debug.Log("[GameManager] Space is down but distracted=false -> EndGame(Caught)");
                EndGame(EndReason.Caught);
            }
        }
        else
        {
            if (lickCoroutine != null)
            {
                Debug.Log("[GameManager] Space released -> stopping lick coroutine, switching kid to Studying");
                StopCoroutine(lickCoroutine); //stops the reference of the currently running lick coroutine
                kid.PlayState(KidAnimation.KidAnimState.Studying);
            }
            lickCoroutine = null;

            if (momManager.IsDistracted() == false) //not distracted
            //if (!distracted)
            {
                DecayProgress();
            }
        }
    }

    private void HandleLick()
    {
        if (lickCoroutine != null)
        {
            Debug.Log("[GameManager] HandleLick() called but lickCoroutine already running -> ignoring");
            return; // already running, do nothing
        }
        Debug.Log("[GameManager] HandleLick() starting InTheLickZone coroutine");
        lickCoroutine = StartCoroutine(InTheLickZone());
    }

    private void DecayProgress()
    {
        frostingProgress -= momManager.GetMomPower() * Time.deltaTime;
        progressBar.SetProgress(frostingProgress);
    }

    IEnumerator InTheLickZone()
    {
        Debug.Log("[GameManager] InTheLickZone() STARTED");
        while (true)
        {
            frostingProgress += lickPower * Time.deltaTime;
            progressBar.SetProgress(frostingProgress);

            Debug.Log("[GameManager] Licking... frostingProgress=" + frostingProgress + " / target=" + frostingTarget);

            if (frostingProgress >= frostingTarget)
            {
                Debug.Log("[GameManager] Target reached -> EndGame(Won)");
                EndGame(EndReason.Won);
                yield break;
            }

            Debug.Log("[GameManager] Calling kid.PlayState(Licking)");
            kid.PlayState(KidAnimation.KidAnimState.Licking);
            yield return null;
        }
    }

    private IEnumerator CountdownTimer()
    {
        while (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            timer.SetTime(timeRemaining);

            yield return null;
        }

        // loop ended cuz timer hit zero
        Debug.Log("[GameManager] Timer hit zero -> EndGame(TimedOut)");
        EndGame(EndReason.TimedOut);
    }

    private void EndGame(EndReason reason)
    {
        if (gameEnded)
        {
            Debug.Log("[GameManager] EndGame(" + reason + ") called but gameEnded already true -> ignoring");
            return;
        }

        Debug.LogWarning("[GameManager] EndGame(" + reason + ") FIRING NOW. gameEnded is about to become permanently true.");
        gameEnded = true;

        kid.SetStop(gameEnded);
        StopAllCoroutines();

        if (lickCoroutine != null)
        {
            lickCoroutine = null;
        }

        switch (reason)
        {
            case EndReason.Won:
                Debug.Log("[GameManager] --- WON --- (no UI hooked up yet, this is just the log)");
                break;
            case EndReason.Caught:
                Debug.Log("[GameManager] --- CAUGHT --- (no UI hooked up yet, this is just the log)");
                break;
            case EndReason.TimedOut:
                Debug.Log("[GameManager] --- TIMED OUT --- (no UI hooked up yet, this is just the log)");
                break;
        }
    }
}