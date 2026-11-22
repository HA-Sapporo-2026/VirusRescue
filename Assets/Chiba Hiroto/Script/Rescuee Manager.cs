using UnityEngine;
using UnityEngine.UIElements;
using VirusRescue;

public class RescueeManager :　SingletonMonoBehaviour<RescueeManager>
{
    [SerializeField] Rescuee[] rescuees;
    [SerializeField] Rescuee[] variants;
    [SerializeField] Rescuee prefab;

    // 種類ごとの要救助者の最大数
    [SerializeField] int maxRescueesPerType;
    // 要救助者がマップに現れるまでのタイマー変数
    [SerializeField] float appearTimer;
    [SerializeField] float appearTime;
    // マップ上に出せる要救助者の最大数
    [SerializeField] int maxRescueesCanAppear;
    // 一度に出てくる要救助者の数
    [SerializeField] int rescueesAppearNum;
    // 要救助者が出てくる確率
    [SerializeField] float appearProbability;
    // 要救助者が現れることができるマップの範囲    の   数
    [SerializeField] int rangeCanSelect;
    // そこに現れることができるかどうかの変数
    [SerializeField] bool[] rangeDontSelect;
    // 要救助者が現れてよいマップの範囲の右上部分
    [SerializeField] Vector2[] appearRangePlus;
    // 要救助者が現れてよいマップの範囲の左下部分
    [SerializeField] Vector2[] appearRangeMinus;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        // 指定された最大人数分の配列を生成する
        rescuees = new Rescuee[maxRescueesPerType * 6];

        // 要救助者用のプレハブを一つの配列に格納する
        Rescuee[] prefabs =
            { variants[0],variants[1],variants[2],variants[3],variants[4],variants[5], };
        for (int i = 0; i < 6; i++)
        {
                prefab = prefabs[i];
            for(int j = 0; j < maxRescueesPerType; j++)
            {
                int index = j + (i * maxRescueesPerType);
                rescuees[index] = Instantiate(prefab);
                rescuees[index].gameObject.SetActive(false);
            }
        }
        rangeDontSelect = new bool[6];
        appearTimer = appearTime;
    }
    // Update is called once per frame
    void Update()
    {
        int appearedRescuee = 0;

        for (int i = 0; i < rescuees.Length; i++)
        {
            if (rescuees[i].gameObject.activeInHierarchy) appearedRescuee++;
        }

        for (int f = 0; f < rangeDontSelect.Length; f++)
        {
            if(f<rangeCanSelect)
            {
                rangeDontSelect[f] = false;
            }
            else
            {
                rangeDontSelect[f] = true;
            }
        }
        rangeCanSelect = 2;

        if(appearedRescuee < maxRescueesCanAppear)
        {
            if (appearTimer <= 0)
            {
                float per = 0;
                if (appearedRescuee <= 0)
                {
                    per = 100;
                }
                else
                {
                    per = UnityEngine.Random.Range(0, 100f);
                }
                if (per >= 100 - appearProbability)
                {
                    int rescueesAppeared = 0;
                    for (int i = 0; i < rescuees.Length; i++)
                    {
                        if (!rescuees[i].gameObject.activeInHierarchy)
                        {
                            //int index = UnityEngine.Random.Range(0, rescuees.Length);
                            PositionSearch(i);
                            rescuees[i].gameObject.SetActive(true);
                            rescueesAppeared++;
                        }
                        if(rescueesAppeared >= rescueesAppearNum)
                        {
                            break;
                        }
                    }
                }
                appearTimer = appearTime;
            }
            else
            {
                appearTimer -= Time.deltaTime;
            }
        }
    }
    public void PositionSearch(int Num)
    {
        int Range = UnityEngine.Random.Range(0, rangeCanSelect);
        for (; !rangeDontSelect[Range];)
        {
            if (rangeDontSelect[Range])
            {
                Range = UnityEngine.Random.Range(0, rangeCanSelect);
            }
            else
            {
                break;
            }
        }
        Vector3 position = new Vector3(UnityEngine.Random.Range(appearRangeMinus[Range].x, appearRangePlus[Range].x), UnityEngine.Random.Range(appearRangeMinus[Range].y, appearRangePlus[Range].y), 0);
        rescuees[Num].gameObject.transform.position = position;
    }
}
