using UnityEngine;

public class HelpVoice : MonoBehaviour
{
    public GameObject player;
    //public HelpDisplayManager manager;
    public HumanManager manager;
    public GameObject[] Help;

    AudioSource audioSource;
    AudioClip audioClip;

    public float helpVolume = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        manager = FindAnyObjectByType<HumanManager>();
        for (int i = 0; i < manager.maxHuman; i++)
        {
            //Help[i] = manager.human[i].gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        for (int i = 0; i < manager.maxHuman; i++)
        {
            audioSource = manager.human[i].GetComponent<AudioSource>();
            audioSource.volume = helpVolume / Vector2.Distance(player.transform.position, manager.human[i].gameObject.transform.position);
            if (audioSource.volume >= 0.7f)
            {
                audioSource.volume = 0.7f;
            }
        }
    }
}
