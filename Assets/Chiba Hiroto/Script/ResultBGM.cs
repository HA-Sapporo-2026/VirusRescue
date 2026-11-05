//using UnityEditor.TerrainTools;
using UnityEngine;

public class ResultBGM : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clear;
    public AudioClip failed;
    public ResultChanger changer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        changer = FindAnyObjectByType<ResultChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (changer.isClear)
        {
            audioSource.clip = clear;
        }
        else
        {
            audioSource.clip = failed;
        }
    }
    private void OnEnable()
    {
        changer = FindAnyObjectByType<ResultChanger>();
        if (changer.isClear)
        {
            audioSource.clip = clear;
        }
        else
        {
            audioSource.clip = failed;
        }
        audioSource.Play/*OneShot*/(/*audioSource.clip*/);
    }
}
