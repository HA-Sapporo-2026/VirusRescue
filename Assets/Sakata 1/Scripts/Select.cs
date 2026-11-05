using UnityEngine;
using UnityEngine.InputSystem;

public class Select : MonoBehaviour
{
    // 選択するButton
    public UnityEngine.UI.Button[] buttons;

    // 選択中の矢印
    public RectTransform arrow;

    // 上下移動用Input
    public InputAction selectMoveAction;

    // 決定用Input
    public InputAction decisionAction;

    // 現在選択しているButton番号
    public int selectIndex = 0;

    // 決定入力の連続実行防止
    bool decisionLock = true;


    // =========================
    // 有効になったとき
    // =========================
    void OnEnable()
    {
        selectMoveAction.Enable();
        decisionAction.Enable();

        selectMoveAction.performed += OnMove;

        // Selectが有効になった直後は決定を受け付けない
        // 前のSelectで押した決定キーを引き継がないため
        decisionLock = true;

        MoveArrow();
    }


    // =========================
    // 無効になったとき
    // =========================
    void OnDisable()
    {
        selectMoveAction.performed -= OnMove;

        selectMoveAction.Disable();
        decisionAction.Disable();
    }


    // =========================
    // 毎フレーム
    // =========================
    void Update()
    {
        // 決定キーを離したら
        // 次の決定を受け付けられるようにする
        if (!decisionAction.IsPressed())
        {
            decisionLock = false;
        }

        // 決定キーが押されていて、
        // まだ今回の入力を処理していなければ決定
        if (decisionAction.IsPressed() && !decisionLock)
        {
            decisionLock = true;

            Decision();
        }
    }


    // =========================
    // 決定処理
    // =========================
    void Decision()
    {
        if (buttons == null || buttons.Length == 0)
            return;

        if (selectIndex < 0 || selectIndex >= buttons.Length)
            return;

        if (buttons[selectIndex] == null)
            return;

        // 選択中ButtonのOn Clickを実行
        buttons[selectIndex].onClick.Invoke();
    }


    // =========================
    // 上下移動
    // =========================
    void OnMove(InputAction.CallbackContext context)
    {
        if (buttons == null || buttons.Length == 0)
            return;

        float input = context.ReadValue<float>();

        // 上
        if (input > 0.5f)
        {
            selectIndex--;

            if (selectIndex < 0)
            {
                selectIndex = buttons.Length - 1;
            }

            MoveArrow();
        }

        // 下
        else if (input < -0.5f)
        {
            selectIndex++;

            if (selectIndex >= buttons.Length)
            {
                selectIndex = 0;
            }

            MoveArrow();
        }
    }


    // =========================
    // 選択番号を0に戻す
    // =========================
    public void ResetSelection()
    {
        selectIndex = 0;

        // 新しいSelectとして開始するので
        // 決定入力を一度ロックする
        decisionLock = true;

        MoveArrow();
    }


    // =========================
    // 矢印を移動
    // =========================
    void MoveArrow()
    {
        if (arrow == null)
            return;

        if (buttons == null || buttons.Length == 0)
            return;

        if (selectIndex < 0 || selectIndex >= buttons.Length)
            return;

        if (buttons[selectIndex] == null)
            return;

        Vector3 pos = buttons[selectIndex].transform.position;

        arrow.position = new Vector3(
            pos.x - 250f,
            pos.y,
            arrow.position.z
        );
    }
}