using UnityEngine;

public class ResultChanger : MonoBehaviour
{
    public/* static*/ bool isClear = false;

    public int score;
    public int saved;
    public PlayerStatus player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerStatus>();

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            score = player.Score;
            saved = player.Saved;
        }
    }
}
