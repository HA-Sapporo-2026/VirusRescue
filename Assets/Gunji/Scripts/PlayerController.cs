using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 2.4f;

    // 物理移動用のコンポーネント
    public Rigidbody2D rigidbody;

    // 移動用の入力設定
    public InputAction moveAction;

    void OnEnable()
    {
        // 移動入力を有効化
        moveAction.Enable();
    }

    void OnDisable()
    {
        // 移動入力を無効化
        moveAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        

        // 移動入力値を取得
        Vector2 moveInput = moveAction.ReadValue<Vector2>();

        // Rigidbodyの移動方向と速度を扱う変数に、
        // 移動入力の値（方向）に速度をかけた値を渡す
        rigidbody.linearVelocity = moveInput * moveSpeed;
    }
}


