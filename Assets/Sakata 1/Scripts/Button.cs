using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    TitleManager titleManager;
    PauseManager pauseManager;
    ResultManager resultManager;

    public NoEndReqObj noEndReq;
    Fade fade;
    public StartProcess startProcess;


    void Awake()
    {
        titleManager = FindAnyObjectByType<TitleManager>();
        pauseManager = FindAnyObjectByType<PauseManager>();
        resultManager = FindAnyObjectByType<ResultManager>();
    }


    // =========================
    // TitleScene
    // =========================

    // START
    public void TitleStart()
    {
        StartRequest();
    }

    // END確認画面を表示
    public void TitleEndRequest()
    {
        if (noEndReq != null)
        {
            noEndReq.EndRequest(true, 0);
        }

        if (titleManager != null)
        {
            titleManager.ChangeSelect(1);
        }
    }

    // END確認 → YES
    public void TitleEndYes()
    {
        SceneTransition(null);
    }

    // END確認 → NO
    public void TitleEndNo()
    {
        if (noEndReq != null)
        {
            noEndReq.EndRequest(false);
        }

        if (titleManager != null)
        {
            titleManager.ChangeSelect(0);
        }
    }


    // =========================
    // PauseScene
    // =========================

    // リスタート確認画面を表示
    public void PauseRestartRequest()
    {
        if (noEndReq != null)
        {
            noEndReq.EndRequest(true, 0);
        }

        if (pauseManager != null)
        {
            pauseManager.ChangeSelect(1);
        }
    }

    // リスタート確認 → YES
    public void PauseRestartYes()
    {
        // 必要なリスタート処理
    }

    // リスタート確認 → NO
    public void PauseRestartNo()
    {
        if (noEndReq != null)
        {
            noEndReq.EndRequest(false);
        }

        if (pauseManager != null)
        {
            pauseManager.ChangeSelect(0);
        }
    }

    public void PauseTitleRequest()
    {
        if (noEndReq != null)
        {
            noEndReq.EndRequest(true, 1);
        }

        if (pauseManager != null)
        {
            pauseManager.ChangeSelect(2);
        }
    }

    public void PauseTitleYes()
    {
        SceneTransition("TitleScene");
    }

    // タイトルへ戻る確認 → NO
    public void PauseTitleNo()
    {
        if (noEndReq != null)
        {
            noEndReq.EndRequest(false);
        }

        if (pauseManager != null)
        {
            pauseManager.ChangeSelect(0);
        }
    }


    // =========================
    // ResultScene
    // =========================

    // タイトルへ戻る確認画面
    public void ResultTitleRequest()
    {
        if (noEndReq != null)
        {
            noEndReq.EndRequest(true, 0);
        }

        if (resultManager != null)
        {
            resultManager.ChangeSelect(1);
        }
    }

    // タイトルへ戻る → YES
    public void ResultTitleYes()
    {
        if (fade != null && startProcess != null)
        {
            fade.FadeIn(startProcess.fadeTime);
        }

        SceneTransition("TitleScene");
    }

    // タイトルへ戻る → NO
    public void ResultTitleNo()
    {
        if (noEndReq != null)
        {
            noEndReq.EndRequest(false);
        }

        if (resultManager != null)
        {
            resultManager.ChangeSelect(0);
        }
    }


    // =========================
    // 共通
    // =========================

    void SceneTransition(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }


    // START処理
    public void StartRequest()
    {
        if (startProcess != null)
        {
            startProcess.StartFade();
        }
    }
}