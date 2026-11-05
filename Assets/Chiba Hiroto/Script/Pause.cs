using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{

    public InputAction PauseButton;

    public GameObject screen;
    public GameObject level;
    public bool Paused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PauseButton.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Paused)
        {
            // ポーズされていたら
            // ポーズ画面を表示
            screen.SetActive(true);
            // レベルを左上に表示
            level.SetActive(true);
            // 時を止める
            Time.timeScale = 0.0f;
        }
        else
        {
            // ポーズされていなかったら
            screen.SetActive(false);
            level.SetActive(false);
            Time.timeScale = 1.0f;
        }



        //if (PauseButton.IsPressed())
        //{
            if (PauseButton.WasPressedThisFrame()) { 
            
            if (Paused)
            {
                Paused = false;
                return;
            }
            if (!Paused)
            {
                Paused = true;
                return;
            }}
        //}
    }
}
