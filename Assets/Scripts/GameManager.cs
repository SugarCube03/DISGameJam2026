using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private InputAction lickingKey;
    public Mom momManager;
    public KidAnimation kid;
    public ProgressManager progressBar;
    public TimerCode timer;

    public GameObject currentSceneCanvas; 

    public float lickPower = 0.01f;
    public float lickBoost = 1.2f;
    private float frostingProgress = 0f;
    private bool lickingTime;
    public float frostingTarget=1;
    private bool gameEnded = false;

    public float timeLimit = 30f;
    private float timeRemaining;
    public GameObject SceneRoot;


    private GameObject objectToUnhide;

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

        SceneManager.LoadSceneAsync("BeCaught",LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Win",LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Lose",LoadSceneMode.Additive);
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

        gameEnded = true;

        kid.SetStop(gameEnded);
        momManager.SetStop(gameEnded);

        StopAllCoroutines();
        

        if (lickCoroutine != null)
        {
            lickCoroutine = null;
        }

        currentSceneCanvas.SetActive(false);
        SceneRoot.SetActive(false);

        switch (reason)
        {
            case EndReason.Won:
                objectToUnhide = GetRootOfScene("Win");
                Debug.Log("[GameManager] --- WON --- ");
                break;
            case EndReason.Caught:
                objectToUnhide = GetRootOfScene("BeCaught");
                
                Debug.Log("[GameManager] --- CAUGHT --- ");
                break;
            case EndReason.TimedOut:
                objectToUnhide = GetRootOfScene("Lose");
                Debug.Log("[GameManager] --- TIMED OUT --- ");
                break;
        }

        objectToUnhide.SetActive(true);

    }

    private GameObject GetRootOfScene(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (!scene.IsValid())
        {
            Debug.LogWarning($"Scene '{sceneName}' isn't loaded.");
            return null;
        }

        GameObject[] roots = scene.GetRootGameObjects();

        if (roots.Length == 0)
        {
            Debug.LogWarning($"Scene '{sceneName}' has no root objects.");
            return null;
        }

        return roots[0]; // since all of the objects live in one root
    }

}