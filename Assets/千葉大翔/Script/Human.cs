//using UnityEditor.Tilemaps;
using UnityEngine;

public class Human : MonoBehaviour
{
    // これは要救助者のスクリプトです。
    
    //既にマークの対象であるかどうか
    public bool isTargeted = false;

    // プレイヤーに連れていかれているかどうか
    public bool isEscorted = false;

    public HelpDisplayManager helpDisplayManager;

    public HumanManager humanManager;

    public GameObject player;
    
    public Rigidbody2D rigidbody;
    public Collider2D col;
    public Collider2D pl_col;

    // このオブジェクトの番号
    public int Num;

    public bool isHere = false;

    public AudioClip audioClip;
    public AudioSource audioSource;
    // Helpと呼ぶ頻度
    public float CallTime = 5;
    public float CallTimer = 0;

    public float infectTimer = 0;
    public float infectTimeSmallest = 15;
    public float infectTimeBiggest = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        pl_col = player.GetComponent<Collider2D>();
        humanManager = FindAnyObjectByType<HumanManager>();
        helpDisplayManager = FindAnyObjectByType<HelpDisplayManager>();
        helpDisplayManager.GetDisplay();
        infectTimer = Random.Range(infectTimeSmallest,infectTimeBiggest);
        CallTimer = Random.Range(1f, 3f);
    }
    private void OnDisable()
    {
        isHere = false;
        isTargeted = false;
        //gameObject.tag = "Human";
    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        if (!isTargeted)
        {
            helpDisplayManager.GetDisplay();
        }
        if (Vector2.Distance(player.transform.position, transform.position) != 0)
        {
            audioSource.volume = 3-Vector2.Distance(player.transform.position, transform.position)/ 5;
        }//audioClip.
        if (CallTimer <= 0)
        {
            audioSource.PlayOneShot(audioClip);
            CallTimer = CallTime;
        }
        else
        {
            CallTimer -= Time.deltaTime;
        }
        if (infectTimer > 0)
        {
            infectTimer -= Time.deltaTime;
        }
        else
        {
            // 生存者を消し、ゾンビを生成

        }
        //if (gameObject.tag == "EscortedHuman")
        //{
        //}
        // 今、当たり判定に触れているなら、ランダム(じゃなくてもよし)な方向に進み続ける
        //if (col./*IsTouchingLayers()*/)
        //{

        //}

        //if (thereIsWall)
        //{
        //    rigidbody.linearVelocity/*transform.position +*/= -transform.position.normalized;
        //    //    //col.isTrigger = false;
        //    //    col.enabled = false;
        //    //    humanManager.PositionSearch(Num);
        //    //    col.enabled = true;
        //}
        //else
        //{
        //    rigidbody.linearVelocity = Vector2.zero;
        //}

    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    gameObject.SetActive(false);
    //    //Debug.Log("There is wall");
    //    //thereIsWall = true;
    //}
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    gameObject.SetActive(false);
    //    //rigidbody.linearVelocity/*transform.position +*/= -transform.position.normalized;
    //    //Debug.Log("There is wall");
    //    //thereIsWall = true;
    //}
    //private void OnCollitionEnter2D(Collider2D collision)
    //{
    //    Debug.Log("There is wall");
    //    thereIsWall = true;
    //}
    //private void OnCollitionStay2D(Collider2D collision)
    //{
    //    transform.position += -transform.position.normalized;
    //    Debug.Log("There is wall");
    //    thereIsWall = true;
    //}
    //private void OnCollitionExit2D(Collider2D collision)
    //{
    //    Debug.Log("There is no wall");
    //    thereIsWall = false;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("There is no wall");
    //    thereIsWall = false;
    //}

    //private void OnCollisionEnter2D(Collider2D collision)
    //{
    //    Debug.Log("There is wall");
    //    thereIsWall = true;
    //}
    //private void OnCollisionStay2D(Collider2D collision)
    //{
    //    Debug.Log("There is wall");
    //    thereIsWall = true;
    //}
    //private void OnCollisionExit2D(Collider2D collision)
    //{
    //    Debug.Log("There is no wall");
    //    thereIsWall = false;
    //}
}
