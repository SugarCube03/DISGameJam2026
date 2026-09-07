using UnityEngine;

public class Mom : MonoBehaviour
{
    // carries mom's current state
    private State currentState;

    private SpriteRenderer sr;

    // options for state of mom
    public enum State
    {
        Frosting,
        Thinking,
        Distracted,
        CatchingAngry,
        LosingAngry
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // change what state mom is in
    private void EnterState(State MomState)
    {
        currentState = MomState;
    }
}