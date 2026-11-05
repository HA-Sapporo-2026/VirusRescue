using UnityEngine;

public class StoppingBarrier : MonoBehaviour
{
    // このスクリプトは、レベルアップによって消える障壁につけます。

    public PlayerStatus status;

    // 障壁が消えるのに必要なレベル
    public int DestroyingLevel = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //status = GetComponent<PlayerStatus>();
        status = FindAnyObjectByType<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        if (status.playerLevel >=DestroyingLevel)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
