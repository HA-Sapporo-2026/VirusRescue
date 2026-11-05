using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerView : MonoBehaviour
{
    public Animator animator;

    public Rigidbody2D rb;

    Vector2 mousePos;

    Camera cam;

    public PlayerAttack playerAttack;

    public InputAction inputAction;

    Vector2 moveDirection;

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        float VelocityX = rb.linearVelocityX;
        float VelocityY = rb.linearVelocityY;

        bool isRunning = (VelocityX != 0 || VelocityY != 0);

        animator.SetBool("isRun", isRunning);
        animator.SetBool("isAttack", playerAttack.IsCharged);

        Vector2 moveInput = inputAction.ReadValue<Vector2>();

        if (!(moveInput == Vector2.zero))
        {
            moveDirection = moveInput.normalized;

        }

        else
        {
            if (cam != null)
            {
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

                Vector2 direction = mousePos - (Vector2)gameObject.transform.position;

                moveDirection = new Vector2(direction.x, direction.y).normalized;
            }
        }
        animator.SetFloat("posX", moveDirection.x);
        animator.SetFloat("posY", moveDirection.y);





    }

}
