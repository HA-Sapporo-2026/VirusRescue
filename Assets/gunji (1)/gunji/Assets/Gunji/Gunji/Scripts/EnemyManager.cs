using UnityEngine;
using System;

public enum EnemyType
{
    Zombi,
    Teketeke

}


public class EnemyManager : MonoBehaviour
{
    // プールに生成する最大敵数
    public int maxEnemies = 16;

    [Header("敵のプレハブ")]
    // 敵オブジェクトのプレハブ
    public EnemyController enemyPrefab;
    public EnemyController teketekePrefab;


    // 生成された敵オブジェクトを
    // 格納する配列 (オブジェクトプール)
    EnemyController[] enemies;

    // プレイヤーの Transform
    public Transform player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        int numEnemyType = Enum.GetNames(typeof(EnemyType)).Length;

        // 指定された最大敵数分の配列を生成する
        enemies = new EnemyController[maxEnemies * numEnemyType];

        EnemyController[] prefabs = { enemyPrefab, teketekePrefab };
        for (int j = 0; j < numEnemyType; j++)
        {

            EnemyController prefab = prefabs[j];

            for (int i = 0; i < maxEnemies; i++)
            {
                int index = i + (j * maxEnemies);

                // 敵オブジェクトを動的に生成する
                enemies[index] = Instantiate(prefab);

                // 生成後は非アクティブ状態に設定する
                enemies[index].gameObject.SetActive(false);
            }

        }
    }
    // 敵をスポーンさせる関数
    public void SpawnEnemy(EnemyType type)
    {
        // スポーン可能な敵の配列の中身を精査して検索する
        for (int i = 0; i < maxEnemies; i++)
        {
            int index = i + (int)type * maxEnemies;

            // 敵がアクティブ状態なら、すでにスポーンしている
            // ため、continueする(スキップする)
            if (enemies[index].gameObject.activeInHierarchy/*activeSelf*/)
            {
                continue;
            }


            Vector3 position = RandomEnemyPosition();

            // ここに到達するということは
            // スポーンしていない敵が見つかったので
            // 敵をアクティブ状態にする

            Vector2 direction = new Vector2(
           UnityEngine.Random.Range(-1.0f, 1.0f),
           UnityEngine.Random.Range(-0.4f, -1.0f));

            //// スポーンするＸ座標が画面端だった場合
            //if (Mathf.Abs(position.x) > 3.0f)
            //{
            //    // X座標の符号を1.0f or -1.0fで取得
            //    float sign = Mathf.Sign(position.x);

            //    // 取得した符号の逆の値をX軸として設定する
            //    direction.x = -sign;
            //}

            enemies[index].transform.position = position;
            enemies[index].direction = direction;
            enemies[index].gameObject.SetActive(true);
            break;
        }
    }

   


    public Vector3 RandomEnemyPosition()
    {
        // X座標は-3から3の範囲で
        // Y座標は3にところにスポーンさせる
        Vector3 position = new Vector3(
            UnityEngine.Random.Range(-2.5f, 2.5f),
            2.5f,
            0.0f);

        return position;

    }


}
