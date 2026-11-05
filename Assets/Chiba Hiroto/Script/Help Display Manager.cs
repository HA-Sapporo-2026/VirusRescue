using System;
using UnityEngine;

public class HelpDisplayManager : MonoBehaviour
{
    // このスクリプトはカメラ外のPlayerから一定距離にいる要救助者(Human)を、
    // 何らかのマークで表示するためのものです。

    // プレイヤーにオブジェクト追加し、そこにつけてもよいでしょう。

    public GameObject Player;

    // 表示されるマーク
    public HelpDisplay DisplayPrefab;

    public HelpDisplay[] displays;

    // マークを表示できる要救助者の距離

    // 表示するマークの最大数
    // HumanManagerのmaxHumanで調整します
    public int maxDisplay;

    public HumanManager humanManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxDisplay = humanManager.maxHuman * 6;
        // 指定された最大人数分の配列を生成する
        displays = new HelpDisplay[maxDisplay];

        // 要救助者用のプレハブを一つの配列に格納する
        HelpDisplay[] prefabs =
            { DisplayPrefab };
        for (int i = 0; i < 1; i++)
        {
            // このループで生成するプレハブを取得する
            DisplayPrefab = prefabs[i];

            for (int j = 0; j < maxDisplay; j++)
            {
                int index = j + (i * maxDisplay);

                // Object.Instantiateを使用して
                // 要救助者オブジェクトを自動的に生成する
                displays[index] = Instantiate(DisplayPrefab);

                // 要救助者オブジェクトを非アクティブにする
                displays[index].gameObject.SetActive(false);
            }
        }
    }

    public HelpDisplay GetDisplay()
    {
        for (int i = 0; i < displays.Length; i++)
        {
            if (humanManager.human[i] != null)
            if (humanManager.human[i].gameObject.activeInHierarchy/*enabled*/ &&
                !humanManager.human[i].isTargeted)
            {
                displays[i].gameObject.SetActive(true);
                displays[i].targetHuman = humanManager.human[i].gameObject;
                humanManager.human[i].isTargeted = true;
                displays[i].isTarget = true;
                return displays[i];
            }
            //else
            //{
            //    displays[i].isTarget = false;
            //    humanManager.human[i].isTargeted = false;
            //}
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        GetDisplay();
    }
}
