using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{

    [Header("移動用")]

    [SerializeField] InputAction moveAction;

    [SerializeField] Rigidbody2D rb;

    public float moveSpeed;



    void OnEnable()
    {
        moveAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        rb.linearVelocity = moveInput * moveSpeed;

    }




}
