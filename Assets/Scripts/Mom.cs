using UnityEngine;
using System.Collections;

public class Mom : MonoBehaviour
{
    // carries mom's current state
    private State currentState;
    public float momPower = 0.08f;

    private int declineCount = 0; // how many times she considered being distrcated and declined in a row

    public float baseChance = 0.15f;  // 15% of distracteuin on the very first thought
    public float chanceIncreasePerDecline = 0.15f;

    public float minFrostingTime = 2f;
    public float maxFrostingTime = 6f;
    public float minThinkingTime = 1f;
    public float maxThinkingTime = 2f;
    public float minDistractedTime = 1.5f; // make sure not too short
    public float maxDistractedTime = 4f;

<<<<<<< Updated upstream
    public MomAnimation momAnimation;
=======
    private bool stopAll = false;
>>>>>>> Stashed changes

    // options for state of mom
    public enum State
    {
        Frosting,
        Thinking,
        Distracted
    }

    // enters the game already frosting
    private void Start()
    {
        if (momAnimation == null)
        {
            momAnimation = GetComponent<MomAnimation>();
        }
        StartCoroutine(MomBehaviorLoop());
    }

<<<<<<< Updated upstream
    private bool RollForDistraction()
=======
    private void Update()
    {
        if (stopAll)
        {
            StopAllCoroutines();
        }
        
    }

   private bool RollForDistraction()
>>>>>>> Stashed changes
    {
        float chance = baseChance + (declineCount * chanceIncreasePerDecline);
        chance = Mathf.Clamp01(chance); // never let it exceed 100% or go negative

        float roll = Random.value; // 0.0 to 1.0

        if (roll < chance) //she committed to distraction, e.g. rolled 30% in a 60% chance
        {
            declineCount = 0;
            return true;
        }
        else
        {
            declineCount++;
            return false;
        }
    }

    private IEnumerator MomBehaviorLoop()
    {
        while (true)
        {
            // frosting
            currentState = State.Frosting;
            momAnimation.PlayState(State.Frosting);
            yield return new WaitForSeconds(Random.Range(minFrostingTime, maxFrostingTime));


            // thinking abt distraction
            currentState = State.Thinking;
            momAnimation.PlayState(State.Thinking);
            yield return new WaitForSeconds(Random.Range(minThinkingTime, maxThinkingTime));


            // decide what happens after considering
            if (RollForDistraction())
            {
                currentState = State.Distracted;
                momAnimation.PlayState(State.Distracted);
                yield return new WaitForSeconds(Random.Range(minDistractedTime, maxDistractedTime));
                // loop back to frosting
            }
        }
    }

    // getter function to see if mom is distracted
    public bool IsDistracted()
    {
        return currentState == State.Distracted;
    }

    // getter function to see mom's current state
    public State CurrentState()
    {
        return currentState;
    }

    public float GetMomPower()
    {
        return momPower;

    }

<<<<<<< Updated upstream
    public void StopBehavior()
    {
        StopAllCoroutines();
        momAnimation.StopAnimating();
=======
    public void SetStop(bool stopState)
    {
        stopAll = stopState;
>>>>>>> Stashed changes
    }
}