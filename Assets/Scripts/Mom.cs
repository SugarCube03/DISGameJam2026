using System.Threading;
using NUnit.Framework;
using UnityEngine;

public class Mom : MonoBehaviour
{
    // carries mom's current state
    private State currentState;
    private Timer stateTimer;
    private Timer currentStateDuration;
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
    }

    // change what state mom is in
    private void EnterState(State next)
    {
        // transition from the state we are currently in to the next
        previousState = currentState;
        currentState = next;

        if (next == State.Distracted)
        {
            isDistracted = true;
        }
        else
        {
            isDistracted = false;
        }

        // reset timer
        // stateTimer = Timer(0);
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

    public bool DistractionStatus()
    {
        return isDistracted;
    }
}