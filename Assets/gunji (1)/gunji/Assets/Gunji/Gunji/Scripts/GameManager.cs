using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 敵がスポーンする間隔(秒)
    public float enemySpawnTime = 5.0f;

    float enemySpawnTimer;

    // 敵の生成・管理を行うコンポーネント
    EnemyManager enemyManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();

    }

    // Update is called once per frame
    void Update()
    {
        // 敵のスポーンタイマーが0.0fだったら
        if (enemySpawnTimer == 0.0f)
        {
            // 敵をスポーンさせる
            enemyManager.SpawnEnemy(enemyType());
        }

        // タイマーを加算する
        enemySpawnTimer += Time.deltaTime;

        // タイマーが指定した時間よりも大きかったら
        if (enemySpawnTimer > enemySpawnTime)
        {
            // タイマーをリセットする
            enemySpawnTimer = 0.0f;
        }

    }

    public EnemyType enemyType()
    {
        int number = UnityEngine.Random.Range(0, Enum.GetNames(typeof(EnemyType)).Length);

       switch (number)
        {
            case 0:
                {
                    return EnemyType.Teketeke;
                }
            case 1:
                {
                    return EnemyType.Zombi;
  
                }

        }

        return EnemyType.Zombi;

    }


}
