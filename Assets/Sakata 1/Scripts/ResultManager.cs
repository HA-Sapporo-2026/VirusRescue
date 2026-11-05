using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public GameObject gameClearPrefab;
    public GameObject gameOverPrefab;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI humanScoreText;

    // Result画面のSelect
    public Select[] selects;
    public ResultChanger result;
    public bool Clear = false;

    void Start()
    {
        result = FindAnyObjectByType<ResultChanger>();
        //gameOver = PlayerStatus.GameOver;
        Clear = result.isClear;
        // 最初のResult用Select
        ChangeSelect(0);
        if (Clear)
        {
            gameClearPrefab.SetActive(true);
            gameOverPrefab.SetActive(false);
        }
        else
        {
            gameClearPrefab.SetActive(false);
            gameOverPrefab.SetActive(true);
        }

        scoreText.text = "Score:      " + result.score;
        humanScoreText.text = "助けた救助者：" + result.saved +"人";
    }


    // Selectを切り替える
    public void ChangeSelect(int index)
    {
        if (selects == null)
            return;

        for (int i = 0; i < selects.Length; i++)
        {
            if (selects[i] != null)
            {
                selects[i].enabled = false;
            }
        }

        if (index >= 0 && index < selects.Length)
        {
            if (selects[index] != null)
            {
                selects[index].gameObject.SetActive(true);
                selects[index].enabled = true;
                selects[index].ResetSelection();
            }
        }
    }
}