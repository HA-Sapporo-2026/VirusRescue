using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 移動速度
    public float moveSpeed = 1.0f;

    // 物理移動用のコンポーネント
    public Rigidbody2D rigidbody;

    // プレイヤーの Transform
    EnemyManager enemyManager;

    EnemyHealth enemyHealth;

    // 敵の進行方向
    public Vector2 direction;

    // 1体分の距離
    public float attackRange = 1.0f;

    // 攻撃にかかる時間
    public float attackTime = 0.5f;

    // 攻撃のクールダウン
    public float attackCooldown = 3.0f;

    // 
    bool isAttacking = false;
    float attackTimer = 0.0f;
    float attackCooldownTimer = 0.0f;

    Animator animator;

    public AudioSource audioSource;
    public AudioSource loopSource;
    public AudioClip walk;
    public AudioClip voice;
    //public AudioClip die;
    [SerializeField] HumanManager humanManager;
    // プレイヤーにのみ距離に補正値をかける
    [SerializeField] float playerDistanceAverage = 5.0f;
    [SerializeField]GameObject target;
    [SerializeField]GameObject[] targetList;

    [SerializeField]GameObject player;
    [SerializeField]GameObject[] humans;
    [SerializeField] GameObject[] tents;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyManager = FindAnyObjectByType<EnemyManager>();
        enemyHealth = FindAnyObjectByType<EnemyHealth>();
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        humanManager = FindAnyObjectByType<HumanManager>();
        humans = new GameObject[humanManager.maxHuman*6];
        for (int i = 0; i < humanManager.maxHuman * 6; i++)
        {
            humans[i] = humanManager.human[i].gameObject;
        }
        tents = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            tents[i] = humanManager.tents[i];
        }
        targetList = new GameObject[1 + humanManager.maxHuman * 6 + 3];
        for (int i = 0; i < humanManager.maxHuman * 6; i++)
        {
            targetList[i] = humanManager.human[i].gameObject;
        }
        tents = new GameObject[3];
        for (int i = humanManager.maxHuman * 6; i < humanManager.maxHuman * 6 + 3; i++)
        {
            targetList[i] = humanManager.tents[i- humanManager.maxHuman * 6];
        }
        targetList[humanManager.maxHuman * 6 + 3] = player;
        target = player;
    }
    private void OnEnable()
    {
        //audioSource.volume = 0.4f;
        audioSource.PlayOneShot(voice);
    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        Vector2 enemyPos = transform.position;
        Vector2 targetPos = (Vector2)target.transform.position;

        // プレイヤーとの距離
        float distance = Vector2.Distance(enemyPos, targetPos);

        SetTarget();
        // クールタイム中ならタイマーを進める
        if (attackCooldownTimer > 0.0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        // 攻撃中の処理
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;

            // 1秒経ったら攻撃完了
            if (attackTimer >= attackTime)
            {
                // 座標のフリーズを解放する
                rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

                float finalDistance = Vector2.Distance(transform.position, targetPos);
                if (finalDistance <= attackRange)
                {
                    // プレイヤーに感染ダメージを与える
                    FindAnyObjectByType<infectionManager>().infectionLevel += 10.0f;
                }

                // 攻撃終了
                isAttacking = false;
                attackTimer = 0.0f;
                animator.SetBool("Atack", false);

                // クールタイム開始
                attackCooldownTimer = attackCooldown;
            }

            return;
        }


        // ゾンビの前方方向（移動方向）
        Vector2 forward = direction.normalized;

        // ゾンビ → プレイヤーの方向
        Vector2 toPlayer = (targetPos - enemyPos).normalized;

        // 前方判定（dot が 0.7 以上ならほぼ正面）
        float dot = Vector2.Dot(forward, toPlayer);

        // 攻撃可能なら攻撃開始（距離 + 前方）
        if (distance <= attackRange && dot > 0.7f && attackCooldownTimer <= 0.0f)
        {
            isAttacking = true;
            animator.SetBool("Atack", true);
            attackTimer = 0.0f;
            // Rigidbodyの制約を全てフリーズ状態に設定する
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            return;
        }

        direction = (targetPos - enemyPos).normalized;
        rigidbody.linearVelocity = direction * moveSpeed;
        loopSource.Play();

        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        animator.SetFloat("Speed", rigidbody.linearVelocity.magnitude);

    }

    void SetTarget()
    {
        float[] distance;
        //int distanceNum = 0;
        float minDistance = 99999;
        int minDistanceIndex = 0;
        distance = new float[1/*player*/+ humans.Length + tents.Length];
        for (int i = 0; i < targetList.Length; i++)
        {
            if(targetList[i].activeInHierarchy)
            {
                distance[i] = Vector2.Distance(targetList[i].transform.position, transform.position);
                if (targetList[i] == player)
                {
                    distance[i] *= playerDistanceAverage;
                }
            }
        }
        //for (int i = 0; i < humans.Length; i++)
        //{
        //    if (humans[i].gameObject.activeInHierarchy)
        //    distance[i] = Vector2.Distance(humans[i].transform.position, transform.position);
        //    //distanceNum++;
        //}
        //for (int i = 0; i < tents.Length; i++)
        //{
        //        distance[i+humans.Length] = Vector2.Distance(tents[i].transform.position, transform.position);
        //    //distanceNum++;
        //}
        //distance[humans.Length + tents.Length] = Vector2.Distance(player.transform.position, transform.position)*playerDistanceAverage;
        for (int i = 0; i < distance.Length; i++)
        {
            if(targetList[i].activeInHierarchy)
            if (distance[i] <minDistance)
            {
                minDistance = distance[i];
                minDistanceIndex = i;
            }
        }
        target = targetList[minDistanceIndex];
        //if (minDistanceIndex < humans.Length)
        //{
        //    target = humans[minDistanceIndex];
        //}
        //else if (minDistanceIndex < humans.Length + tents.Length)
        //{
        //    target = tents[minDistanceIndex - humans.Length];
        //}
        //else
        //{
        //    target = player;
        //}
    }
}
