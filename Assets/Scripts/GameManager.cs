using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private InputAction lickingKey;
    private bool lickingTime;
    void Awake()
    {
        lickingKey = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        lickingTime = lickingKey.IsPressed();
        print(lickingTime);
    }
}
