using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class infectionManager : MonoBehaviour
{
    // 現在の感染度
    public float infectionLevel = 0.0f;
    public float serumRecover = 40f;

    // 感染度の上限
    public float maxInfectionLevel = 100.0f;

    float timer = 0.0f;
    public float time = 2.0f;

    // 血清の手持ち数
    public int serumCount = 0;

    // 血清の完成度
    public float serumProgress = 0.0f;

    // 血清アイコンのprefab
    public GameObject serum;

    // 血清アイコンを並べるcanvas
    public Transform serumUIContainer;

    public TextMeshProUGUI text;

    public TextMeshProUGUI serumText;

    List<GameObject> serumIcons = new List<GameObject>();

    public InputAction useSerumAction;

    void OnEnable()
    {
        useSerumAction.Enable();
        useSerumAction.performed += OnUseSerum;
    }

    void OnDisable()
    {
        useSerumAction.performed -= OnUseSerum;
        useSerumAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > time)
        {
            infectionLevel += 0.5f;
            text.text = infectionLevel.ToString("F2"); 
            timer = 0.0f;
            Debug.Log("感染度: " + infectionLevel);
        }

        UpdateSerumUI();
    }


    public void AddSerumProgress(float amount)
    { 
        // 血清の完成度を上げる
        serumProgress += amount;

        if(serumProgress >= 100.0f)
        {
            serumProgress = 0.0f;

            if(serumCount <= 3)
            {
                serumCount++;
                Debug.Log("血清を1つ作成 現在の所持数: " + serumCount);
            }
            else
            {
                Debug.Log("血清をこれ以上持てません！");
            }

        }
    }

    void OnUseSerum(InputAction.CallbackContext context)
    {
        UseSerum();
    }

    void UseSerum()
    {
        if(serumCount > 0)
        {
            serumCount--;

            infectionLevel = Mathf.Max(0,infectionLevel - serumRecover/*40.0f*/);
            text.text = infectionLevel.ToString("F2");
            Debug.Log("血清使用！現在感染度: " + infectionLevel);
        }
        else
        {
            Debug.Log("血清がありません！");
        }


    }
    void UpdateSerumUI()
    {

        serumText.text = serumProgress.ToString();

        // 既存アイコンを削除
        foreach (var icon in serumIcons)
            Destroy(icon);

        serumIcons.Clear();

        // serumCount の数だけアイコンを生成
        for (int i = 0; i < serumCount; i++)
        {
            GameObject icon = Instantiate(serum, serumUIContainer);
            serumIcons.Add(icon);
        }
    }
}
