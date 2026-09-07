using UnityEngine;

public class Mom : MonoBehaviour
{
    // carries mom's current state
    private State MomState;

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

    // state: mom is frosting cake
    private void Frosting()
    {
        MomState = State.Frosting;
    }

    // state: mom is thinking about a distraction
    private void Thinking()
    {
        MomState = State.Thinking;
    }

    // state: mom is distracted
    private void Distracted()
    {
        MomState = State.Distracted;
    }

    // state: mom caught kid and is angry
    private void Caught()
    {
        MomState = State.CatchingAngry;
    }

    private void Lose()
    {
        MomState = State.LosingAngry;
    }
}