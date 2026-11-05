using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.PlayerSettings;

public class Sword : MonoBehaviour
{

    [Header("攻撃")]

    [SerializeField] int normalAttack;
    [SerializeField] int smallAttack;
    [SerializeField] int mediumAttack;
    [SerializeField] int maxAttack;

    public float attackAverage = 1;
    float attackPower;

    public Critical critical;

    public AudioSource audioSource;
    public AudioClip criticalAttack;
    public AudioClip Attack;

    public AudioClip slash;
    public SlashEffect slashEffect;

    public GameObject bloodPrefab;


    // 剣を振る速度
    [SerializeField] float attackTime;

    // 剣を振る際の角度
    [SerializeField] float attackAngle;

    // 剣のコライダー
    [SerializeField] BoxCollider2D cl;

    // 今攻撃しているのか
    public bool isAttack = false;

    bool swordSize = false;
    public bool SwordSize => swordSize;

    public Vector2 swordVector;
    public Vector3 mousePos;
    public GameObject player;
    public Rigidbody2D rb;


    [Header("コライダーの大きさ")]

    [SerializeField] Vector2 normalSet = new Vector2(2.5f, 0.0f);
    [SerializeField] Vector2 normalSize = new Vector2(1.5f, 0.5f);


    [SerializeField] Vector2 smallSet = new Vector2(2.7f, 0.0f);
    [SerializeField] Vector2 smallSize = new Vector2(2.7f, 0.8f);

    [SerializeField] Vector2 mediumSet = new Vector2(3.0f, -0.1f);
    [SerializeField] Vector2 mediumSize = new Vector2(3.3f, 0.9f);


    [SerializeField] Vector2 maxSet = new Vector2(3.0f, -0.1f);
    [SerializeField] Vector2 maxSize = new Vector2(4.5f, 1.0f);


    public InputAction attackAction;

    Vector2 direction;

    Vector2 swordPosition;



    // public Vector2 IsPlayerPosition;

    private void OnEnable()
    {
        attackAction.Enable();
    }

    private void OnDisable()
    {
        attackAction.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cl.enabled = false;
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        //Debug.Log(attackAction);
        SlashTarget();

    }


    public void SlashTarget()
    {

        if (isAttack)
        {
            return;
        }


        Vector2 attackDirection = attackAction.ReadValue<Vector2>();

        if (!(attackDirection == Vector2.zero))
        {

            float targetAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = (mousePos - player.transform.position).normalized;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }


    }



    public void StartSlash(int number)
    {
        if (isAttack)
            return;

        // rb.WakeUp();
        swordSize = true;

        swordPosition = swordVector;

        switch (number)
        {
            case 1:
                cl.offset = normalSet;
                cl.size = normalSize;
                attackPower = normalAttack;
                break;

            case 2:
                cl.offset = smallSet;
                cl.size = smallSize;
                attackPower = smallAttack;
                break;

            case 3:
                cl.offset = mediumSet;
                cl.size = mediumSize;
                attackPower = mediumAttack;
                break;

            case 4:
                cl.offset = maxSet;
                cl.size = maxSize;
                attackPower = maxAttack;
                break;


        }

        if (gameObject.activeInHierarchy /*!= false*/)


            StartCoroutine(SwingRoutine());
    }


    IEnumerator SwingRoutine()
    {

        isAttack = true;
        cl.enabled = true;
        audioSource.PlayOneShot(slash);
        float baseAngle = transform.eulerAngles.z;

        float startAngle = baseAngle + (attackAngle / 2.0f);
        float lastAngle = baseAngle - (attackAngle / 2.0f);

        float timer = 0.0f;

        while (timer < attackTime)
        {
            timer += Time.deltaTime;
            float time = timer / attackTime;

            float currentAngle = Mathf.Lerp(startAngle, lastAngle, time);
            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            yield return null;


        }

        transform.rotation = Quaternion.Euler(0, 0, lastAngle);
        cl.enabled = false;

        transform.rotation = Quaternion.Euler(0, 0, baseAngle);



        isAttack = false;
        swordSize = false;

        gameObject.SetActive(false);





    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Enemy"))
            return;

        EnemyHealth health = collision.GetComponent<EnemyHealth>();

        CriticalHandler criticalHandler = collision.GetComponent<CriticalHandler>();

        bool isCritical = critical.CriticalProbability();

        if (isCritical)
        {
            health.isCriticalDeath = true;
            audioSource.PlayOneShot(criticalAttack);
            criticalHandler.PlayCritical(health);
        }

        else
        {
            float damage = attackPower * attackAverage;
            audioSource.PlayOneShot(Attack);
            health.TakeDamage(damage);
        }


        slashEffect.OnHitEnemy(collision.transform);

        Instantiate(bloodPrefab, collision.transform.position, Quaternion.identity);

        Debug.Log(health.CurrentHealth);

    }




}
