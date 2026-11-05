using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public HumanManager humanManager;

    // カウントアップして今、要救助者を最大まで連れているかどうか確かめる
    public int nowEscorted = 0;

    // 今、連れている要救助者の人数
    public int EscortedHumanNum = 0;

    // 救助した人の現在Hp
    public int[] escorted_Hp;


    // 救助した人のHp初期化用変数
    public int human_Hp = 5;

    public int human_damage = 1;

    // レベルアップに使う経験値
    public int Experience = 0;

    // 要救助者を避難させて稼ぐスコア
    public int Score = 0;

    public int Saved = 0;
    // 要救助者一人当たりの経験値
    public int ExpFor_A_Human = 200;
    // 要救助者一人当たりのスコア
    public int ScoreFor_A_Human = 200;

    public StatusDisplay status_Display;

    // プレイヤーの現在レベル(最大レベル：９９)
    public int playerLevel = 1;

    // レベルが上がる経験値の量

    public int levelUpBorder = 500;

    // レベルボーダーの上昇量
    //public int BorderUpValue = 200;

    // 次のレベルが10の倍数だった時
    public int BorderOnTen = 1000;
    // 次のレベルが5の倍数だった時
    public int BorderOnFive = 800;
    // 次のレベルが上記に当てはまらなかったとき
    public int BorderOnOthers = 500;

    public bool Clear = false;

    public PlayerMove playerMove;
    //public PlayerAttack playerAttack;
    public Sword sword;
    public CameraMoving cameraMoving;
    public PlayerAttack playerAttack;
    public infectionManager infectionManager;

    public AudioSource audioSource;
    public AudioClip levelUp;
    //public SceneManager sceneManager;
    //public static bool GameOver = false;
    //public static PlayerStatus Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        humanManager = FindAnyObjectByType<HumanManager>();
        status_Display = FindAnyObjectByType<StatusDisplay>();
        cameraMoving = FindAnyObjectByType<CameraMoving>();
        escorted_Hp = new int[200/*humanManager.maxHuman * 6*/];
        for (int i = 0; i < escorted_Hp.Length; i++)
        {
            escorted_Hp[i] = -99;
        }

        //DontDestroyOnLoad(this);
        
    }


    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if(pause.Paused)    return;
        //スコア
        status_Display.score_Num.text = Score.ToString();
        //連れている要救助者の人数
        nowEscorted = 0;
        for (int i = 0; i < escorted_Hp.Length; i++)
        {
            if (escorted_Hp[i] > 0)
            {
                nowEscorted++;
            }
        }
        status_Display.human_Num.text =
            nowEscorted.ToString();
        EscortedHumanNum = nowEscorted;
        nowEscorted = 0;
        //レベル

        if ((playerLevel + 1) % 10 == 0)
        {
            levelUpBorder = BorderOnTen;
        }
        else if ((playerLevel + 1) % 5 == 0)
        {
            levelUpBorder = BorderOnFive;
        }
        else
        {
            levelUpBorder = BorderOnOthers;
        }
        if (Experience >= levelUpBorder)
        {
            playerLevel++;
            LevelUpStatusUp(playerLevel);

            if (playerLevel % 10 == 0)
            {
                BorderOnTen += 500;
                BorderOnFive += 500;
                BorderOnOthers += 500;
            }
            Experience -= levelUpBorder;
            //levelUpBorder += BorderUpValue;
        }
        status_Display.pl_level.text =
            playerLevel.ToString();
        // ゲームオーバー
        if (infectionManager.infectionLevel >= infectionManager.maxInfectionLevel)
        {

            //    GameOver = true;
            //}
            //if (GameOver)
            //{
            //    //Time.timeScale = 0;
            //    float time = 0.1f;
            //    //Instance = this;

            SceneManager.LoadScene("ResultScene");
        }
    }
    public void Escape(Collider2D tent)
    {
        for (int i = 0; i < escorted_Hp.Length; i++)
        {
            if (escorted_Hp[i] > 0)
            {
                escorted_Hp[i] = -99;
                Experience += ExpFor_A_Human;
                Score += ScoreFor_A_Human;
                Saved++;
            }
        }
    }
    public void Damage(Collider2D enemy)
    {
        for (int i = 0; i < escorted_Hp.Length; i++)
        {
            if (escorted_Hp[i] > 0)
            {
                escorted_Hp[i] -= human_damage;
            }
        }
    }
    public void Escort(Collider2D escortedHuman)
    {
        nowEscorted = 0;
        for (int i = 0; i < escorted_Hp.Length; i++)
        {
            if (escorted_Hp[i] <= 0)
            {
                escorted_Hp[i] = human_Hp;
                escortedHuman.gameObject.SetActive(false);
                return;
            }
            else
            {
                nowEscorted++;
            }
            if (nowEscorted >= humanManager.maxHuman/*escorted_Hp.Length*/)
            {
                Debug.Log("まんいんです");
            }
        }
    }
    public void LevelUpStatusUp(int currentLevel)
    {
        audioSource.PlayOneShot(levelUp);
        if (currentLevel == 5)
        {
            sword.attackAverage *= 1.1f;
        }
        if (currentLevel == 10)
        {
            // 血清の持てる数が4個になる
            infectionManager.serumCount = 4;
        }
        if (currentLevel == 15)
        {
            playerMove.moveSpeed *= 1.05f;
            cameraMoving.CameraSpeed *= 1.05f;
        }
        if (currentLevel == 20)
        {
            sword.attackAverage *= 1.1f;
        }
        if (currentLevel == 25)
        {
            sword.critical.critical += 1;
            sword.critical.dyingCritical += 1;
        }
        if ( currentLevel == 30)
        {
            // 毎秒受けるウィルスが2秒から4秒になる
            infectionManager.time = 4f;
        }
        if( currentLevel == 35)
        {
            sword.critical.critical += 2;
            sword.critical.dyingCritical += 2;
        }
        if (currentLevel ==40)
        {
            // 血清の回復量が10あがる
            infectionManager.serumRecover += 10;
        }
        if (currentLevel == 45)
        {
            sword.critical.critical += 1;
            sword.critical.dyingCritical += 1;
        }
        if (currentLevel == 50)
        {
            playerMove.moveSpeed *= 1.05f;
            cameraMoving.CameraSpeed *= 1.05f;
        }
        if (currentLevel == 55)
        {
            // 血清の回復量が5あがる
            infectionManager.serumRecover += 5;
        }
        if (currentLevel == 60)
        {
            playerMove.moveSpeed *= 1.05f;
            cameraMoving.CameraSpeed *= 1.05f;
        }
        if (currentLevel == 65)
        {
            playerAttack.maxChargeTime = 2;
            playerAttack.mediumChargeTime = playerAttack.maxChargeTime/3 *2;
            playerAttack.smallChargeTime = playerAttack.maxChargeTime / 3 * 1;
        }
        if (currentLevel == 70)
        {
            sword.attackAverage *= 1.1f;
        }
        if (currentLevel == 75)
        {
            playerMove.moveSpeed *= 1.05f;
            cameraMoving.CameraSpeed *= 1.05f;
        }
        if (currentLevel == 80)
        {
            // 血清の持てる数が4個になる
            infectionManager.serumCount = 5;
        }
        if (currentLevel == 85)
        {
            sword.attackAverage *= 1.1f;
        }
        if (currentLevel == 90)
        {
            sword.attackAverage *= 1.1f;
        }
        if (currentLevel == 95)
        {
            sword.attackAverage *= 1.1f;
        }
        if (currentLevel == 99)
        {
            // 完全耐性
            sword.critical.critical = 100;
        }
    }
}
