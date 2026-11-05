using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class SettingButton : MonoBehaviour
{
    public int NumberOfTimesPressed = 1;

    Button button;

    public InputAction settingOpen;

    public InputAction settingClose;

    public bool stopFlag;

    public Display triangleRenderer;

    Camera mainCamera;

    private void OnEnable()
    {
        settingOpen.Enable();

        settingClose.Enable();
    }

    private void OnDisable()
    {
        settingOpen.Disable();

        settingClose.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // マウス位置をワールド座標に変換
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = -mainCamera.transform.position.z;

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // マウスがColliderの範囲内にあるか
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit != null && hit.gameObject == gameObject)
        {
            triangleRenderer.DisplayDo(true);

            // 左クリック
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                if (NumberOfTimesPressed == 1)
                {
                    OpenSetting();
                }
                else
                {
                    CloseSetting();
                }

                NumberOfTimesPressed = (NumberOfTimesPressed + 1) % 2;
            }
        }
        else
        {
            // マウスが離れている
            triangleRenderer.DisplayDo(false);
        }

        if (settingOpen.WasPressedThisFrame() && NumberOfTimesPressed == 1)
        {

            OpenSetting();

            NumberOfTimesPressed = 0;
        }

        if (settingClose.WasPressedThisFrame() && NumberOfTimesPressed == 0)
        {
            CloseSetting();

            NumberOfTimesPressed = 1;
        }


    }

    public void OpenSetting()
    {
        stopFlag = true;

        SceneManager.LoadScene("Setting", LoadSceneMode.Additive);
    }

    public void CloseSetting()
    {
        stopFlag = false;

        SceneManager.UnloadSceneAsync("Setting");
    }
}
