using Unity.VisualScripting;
using UnityEngine;

public class Mom : MonoBehaviour
{
    // carries mom's current state
    private State currentState;
    private float stateTimer;
    private float currentStateDuration;
    public float momPower = 0.3f;
    private State previousState;
    private bool isDistracted;

    // has the game ended?
    private bool isGameOver;


    // options for state of mom
    public enum State
    {
        Frosting,
        Thinking,
        Distracted,
        CatchingAngry,
        LosingAngry
    }

    // enters the game already frosting
    private void Start()
    {
        EnterState(State.Frosting);
    }

    private void Update()
    {
        // game has ended
        if (isGameOver)
        {
            return;
        }

        // increment timer
        stateTimer += Time.deltaTime;

        // if our state time has ended, move onto the next state
        if (stateTimer >= currentStateDuration)
        {
            // EnterState(ChooseNextState());
        }
    }

    // private State ChooseNextState()
    // {

    // }

    // change what state mom is in
    private void EnterState(State next)
    {
        // transition from the state we are currently in to the next
        previousState = currentState;
        currentState = next;

        // check if mom is distracted or not
        if (next == State.Distracted)
        {
            isDistracted = true;
        }
        else
        {
            isDistracted = false;
        }

        // check if game should be over
        if (next == State.CatchingAngry || next == State.LosingAngry)
        {
            isGameOver = true;
        }
        else
        {
            isGameOver = false;
        }

        // reset timer
        stateTimer = 0f;

        // how long should we be in this state now
        // currentStateDuration = RollDuration(currentState);
    }


    // mom caught child
    public void Caught()
    {
        if (isGameOver)
        {
            return;
        }

        EnterState(State.CatchingAngry);
    }

    // mom lost
    public void Lose()
    {
        if (isGameOver)
        {
            return;
        }

        EnterState(State.LosingAngry);
    }

    // getter function to see if mom is distracted
    public bool DistractionStatus()
    {
        return isDistracted;
    }

    // getter function to see mom's current state
    public State CurrentState()
    {
        return currentState;
    }

    // mom should start frosting again
    private void ResetMom()
    {
        EnterState(State.Frosting);
    }
    public float GetMomPower()
    {
        return momPower;

    }
}