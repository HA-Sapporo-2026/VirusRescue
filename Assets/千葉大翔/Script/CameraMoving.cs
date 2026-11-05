using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    // このスクリプトはメインカメラに付けてください。
    // PlayerというGameObjectを追従するようになります。

    // カメラの追従速度
    // (カメラがプレイヤーにぴったりついていくと、酔いやすくなってしまいます。)
    // (プレイヤーに"少し遅れて"ついていくようにしましょう)
    public float CameraSpeed = 1f;
    public GameObject player;

    public Vector3 vector;

    public float cameraZ = -10f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, cameraZ);
    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        vector = player.transform.position - transform.position;
        //vector.z = cameraZ;
        //Rigidbody.linearVelocity = vector * CameraSpeed * Time.deltaTime;
        transform.position += vector * CameraSpeed * Time.deltaTime;
        transform.position = transform.position + new Vector3(0, 0, cameraZ - transform.position.z);
    }
}
