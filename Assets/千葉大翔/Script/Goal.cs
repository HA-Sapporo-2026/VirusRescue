//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Goal : MonoBehaviour
{
    // ゴール地点発生のためのタイマー
    public float timer;
    // ゴール地点発生までの時間
    public float time;
    // ヘリのライト
    public GameObject Heli;

    public Collider2D PlayerCollider;
    public Collider2D HeliCollider;
    public HumanManager HumanManager;
    public ResultChanger result;
    public AudioSource audio_Hey;
    public AudioSource audio_HeliGetDown;
    public AudioClip Hey;
    public AudioClip HeliGetDown;
    Vector3 position;
    public bool once;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HumanManager = GameObject.FindAnyObjectByType<HumanManager>();
        PlayerCollider = GameObject.FindAnyObjectByType<PlayerStatus>().GetComponent<Collider2D>();
        HeliCollider = Heli.GetComponent<Collider2D>();
        timer = time;
        once = false;
        audio_Hey.volume = 2.0f;
audio_HeliGetDown.volume = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        if (timer > 0)
        {
            Heli.SetActive(false);
            timer -= Time.deltaTime;
        }
        else
        {
            if (!once)
            {
            int Range = UnityEngine.Random.Range(0, HumanManager.RangeCanSelect);
                Range = UnityEngine.Random.Range(0, HumanManager.RangeCanSelect);
                Heli.transform.position = new Vector3(UnityEngine.Random.Range(HumanManager.HumanPositionRangeMinus[Range].x, HumanManager.HumanPositionRangePlus[Range].x), UnityEngine.Random.Range(HumanManager.HumanPositionRangeMinus[Range].y, HumanManager.HumanPositionRangePlus[Range].y), 0);
                once = true;
            Heli.SetActive(true);
                audio_Hey.PlayOneShot(Hey);
            audio_HeliGetDown.clip = HeliGetDown;
            audio_HeliGetDown.Play(/*OneShot(HeliGetDown*/);
            }
        }
        if (HeliCollider.IsTouching(PlayerCollider))
        {
            result.isClear = true;

            SceneManager.LoadScene("ResultScene");
            //Debug.Log("Goal!");
        }
    }

}
