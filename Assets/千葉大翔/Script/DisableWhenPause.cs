using UnityEngine;
using UnityEngine.InputSystem;

public class DisableWhenPause : MonoBehaviour
{
    public Pause pause;

    public InputAction action1;
    public InputAction action2;
    public InputAction action3;
    public InputAction action4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        action1 = GetComponent<InputAction>();
        action2 = GetComponent<InputAction>();
        action3 = GetComponent<InputAction>();
        action4 = GetComponent<InputAction>();
        if (pause == null) return;

        if (pause.Paused)
        {
            action1.Disable();
            action2.Disable();
            action3.Disable();
            action4.Disable();
        }
        else
        {
            action1.Enable();
            action2.Enable();
            action3.Enable();
            action4.Enable();
        }
    }
}
