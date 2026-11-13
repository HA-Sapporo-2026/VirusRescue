
using System;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class HumanManager : MonoBehaviour
{
    // このスクリプトは、要救助者を管理するためのものです。
    // ゲーム中、永続的に要救助者を生成するために使用します。

    public Human[] human;

    public Human[] HumanVariant;

    // 表示される要救助者
    public Human HumanPrefab;



    // 表示する要救助者の最大数
    public int maxHuman = 20;


    // 道路の大きさをVector2の座標でとって、
    // それを複数用意し、その中からランダムに
    // スポーンさせるアイデアも考えておこう



    public float SpawnLimitDistance = 7f;

    public Vector2[] HumanPositionRangePlus;

    public Vector2[] HumanPositionRangeMinus;

    public PlayerStatus playerStatus;
    public GameObject player;

    public int RangeCanSelect;
    public bool[] RangeDontSelect;
    public Vector2 position;

    public Camera mainCamera;

    public GameObject[] tents;

    public int HumanCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStatus = FindAnyObjectByType<PlayerStatus>();
        player = GameObject.FindWithTag("Player");
        mainCamera = FindAnyObjectByType<Camera>();

        // 指定された最大人数分の配列を生成する
        human = new Human[maxHuman * 6];

        // 要救助者用のプレハブを一つの配列に格納する
        Human[] prefabs =
            { HumanVariant[0],HumanVariant[1],HumanVariant[2],HumanVariant[3],HumanVariant[4],HumanVariant[5], };
        for (int i = 0; i < 6; i++)
        {
            // このループで生成するプレハブを取得する
            HumanPrefab = prefabs[i];

            for (int j = 0; j < maxHuman; j++)
            {
                int index = j + (i * maxHuman);

                // Object.Instantiateを使用して
                // 要救助者オブジェクトを自動的に生成する
                human[index] = Instantiate(HumanVariant[i]);
                human[index] = /*human[index].gameObject.GetComponent<Human>();*/FindAnyObjectByType<Human>();

                human[index].Num = index;
                human[index].gameObject.transform.localScale = new Vector3(2,2,0);
                // 要救助者オブジェクトを非アクティブにする
                human[index].gameObject.SetActive(false);
                //PositionSearch(index);
            }
        }
        RangeDontSelect = new bool[6];
    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;

        HumanCount = 0;
        if (human != null)
        {
            for (int i = 0; i < human.Length; i++)
            {
                //if (human[i].gameObject.activeInHierarchy)
                //{
                //    Debug.Log(Vector3.Distance(human[i].gameObject.transform.position, player/*Status.gameObject*/.transform.position));
                //}
                //if (GameObject.FindSceneObjectsOfType<Human>()  == null)
                if (human[i] != null)
                
                    if (human[i].gameObject.activeInHierarchy)
                {
                    HumanCount++;
                    //PositionSearch(i);
                    //human[i].gameObject.SetActive(true);
                }
            }

            if (HumanCount <= 0)
            {
                if (playerStatus.playerLevel >= 7)
                {
                    RangeCanSelect = 5;
                }
                else if (playerStatus.playerLevel >= 5)
                {
                    RangeCanSelect = 4;
                }
                else if (playerStatus.playerLevel >= 3)
                {
                    RangeCanSelect = 3;
                }
                else
                {
                    RangeCanSelect = 2;
                }
                float Per/*UnityEngine.Random.Range(0, 100f)*/;
                Per = UnityEngine.Random.Range(0, 100f);
                int k = 1;
                if (Per < 2)
                {
                    k = 4;
                }
                else if (Per < 8 + 2)
                {
                    k = 3;
                }
                else if (Per < 20 + 8 + 2)
                {
                    k = 2;
                }
                else if (Per < 70 + 20 + 8 + 2)
                {
                    k = 1;
                }
                for (; k > 1; k--)
                {
                    //int Variant = UnityEngine.Random.Range(0, 5);
                    //Debug.Log(i);

                    PositionSearch(k - 1 + (maxHuman /***/ /*Variant*/));
                    for (float i = 7; /*i > -20*/; i -= 0.000001f)
                    {
                        //if (i < -3)
                        //    i = -3;
                        for (int f = 0; f < RangeCanSelect; f++)
                        {
                            RangeDontSelect[f] = false;
                            if (playerStatus.gameObject.transform.position.x! >= HumanPositionRangePlus[f].x && playerStatus.gameObject.transform.position.x! <= HumanPositionRangeMinus[f].x && playerStatus.gameObject.transform.position.y! >= HumanPositionRangePlus[f].y && playerStatus.gameObject.transform.position.y! <= HumanPositionRangeMinus[f].y)
                            {
                                RangeDontSelect[f] = true;
                            }
                        }

                        if (Vector3.Distance(/*human[k].gameObject.transform.*/position, player/*Status.gameObject*/.transform.position) <= SpawnLimitDistance + i)
                        {
                            PositionSearch(k - 1 + (maxHuman /***/ /*Variant*/));
                        }
                        else break;
                    }
                    human[k - 1 + (maxHuman /***/ /*Variant*/)].isHere = true;
                    human[k - 1 + (maxHuman /***/ /*Variant*/)].gameObject.SetActive(true);
                }
            }
        }
        HumanCount = 0;

    }

    public void PositionSearch(int Num)
    {
        int Range = UnityEngine.Random.Range(0, RangeCanSelect);
        for (;!RangeDontSelect[Range]; )
        {
            if (RangeDontSelect[Range])
            {
                Range = UnityEngine.Random.Range(0, RangeCanSelect);
            }
            else
            {
                break;
            }
        }
        //Debug.Log(Num - 1 + (4 * Variant));
        //Debug.Log(Variant);

        //Debug.Log(Num);
        position = new Vector3(UnityEngine.Random.Range(HumanPositionRangeMinus[Range].x, HumanPositionRangePlus[Range].x), UnityEngine.Random.Range(HumanPositionRangeMinus[Range].y, HumanPositionRangePlus[Range].y), 0);
        human[Num].gameObject.transform.position = position;
    }
    
}

                        //int inRange = 0; 
                        //inRange=0;
                        //if (playerStatus.gameObject.transform.position.x !>= HumanPositionRangePlus[f].x)
                        //    inRange = 
                        //if (playerStatus.gameObject.transform.position.x! <= HumanPositionRangeMinus[f].x)
                        //if (playerStatus.gameObject.transform.position.y! >= HumanPositionRangePlus[f].y)
                        //if (playerStatus.gameObject.transform.position.y! <= HumanPositionRangeMinus[f].y)