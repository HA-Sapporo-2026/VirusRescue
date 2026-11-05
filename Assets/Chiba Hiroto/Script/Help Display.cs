using UnityEngine;

public class HelpDisplay : MonoBehaviour
{
    // こちらが要救助者を表示するためのマークです。これがプレイヤーの周りに表示され、
    // 要救助者がいる方向を示します。

    public GameObject targetHuman;

    public bool isTarget = false;

    public GameObject player;

    public Vector2 direction;

    // カメラ外の表示されてよい要救助者の距離
    public float displayRange = 20f;

    public float DisplayDistance = 10;
    public SpriteRenderer spriteRenderer;
    public HelpDisplayManager manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        manager = FindAnyObjectByType<HelpDisplayManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (targetHuman == null)
        {
            isTarget = false;
            //spriteRenderer.enabled = false;
        }
        if (!targetHuman.activeInHierarchy)
        {
            gameObject.SetActive(false);
            targetHuman = null;
        }
        if (!isTarget)
        {
            manager.GetDisplay();
        }
        if (targetHuman == null || player == null) return;
        // ここで一定距離離れたら表示を消したい
        if (Vector3.Distance(targetHuman.transform.position,player.transform.position) > displayRange)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
        direction = (targetHuman.transform.position - player.transform.position);
        direction = direction.normalized * DisplayDistance;

        transform.position = direction + (Vector2)player.transform.position;
        if (!targetHuman.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }
}
