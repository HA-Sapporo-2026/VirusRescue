using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance;

    SoundManager soundManager;

    // 0 = メイン画面
    // 1 = 終了確認画面
    public Select[] selects;

    public AudioClip bgmClip;

    //public bool clearTest = true;


    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();

        if (bgmClip != null && soundManager != null)
        {
            soundManager.PlayBgm(bgmClip);
        }

        // 最初はメイン画面
        ChangeSelect(0);
    }
    public void ChangeSelect(int index)
    {
        if (selects == null)
            return;

        // 全Selectを無効化
        for (int i = 0; i < selects.Length; i++)
        {
            if (selects[i] != null)
            {
                selects[i].enabled = false;
            }
        }

        // 指定したSelect
        if (index >= 0 && index < selects.Length)
        {
            if (selects[index] != null)
            {
                // SelectのGameObject自体を有効化
                selects[index].gameObject.SetActive(true);

                // Selectを有効化
                selects[index].enabled = true;

                // 選択位置をリセット
                selects[index].ResetSelection();
            }
        }
    }
}