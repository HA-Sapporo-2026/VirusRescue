using UnityEngine;

public class swordArea : MonoBehaviour
{

    public PlayerAttack playerAttack;

    public GameObject Renderer;

    public Sword sword;

    Vector3 InitializeScale = new Vector3(6.0f, 6.0f, 1.0f);

    [Header("範囲に対応する大きさの変化")]
    public Vector3 maxScale = new Vector3(16.0f, 16.0f, 1.0f);

    public Vector3 mediumScale = new Vector3(13.0f, 13.0f, 1.0f);

    public Vector3 smallScale = new Vector3(10.0f, 10.0f, 1.0f);

    bool max = false;

    public Transform tf;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Renderer.transform.localScale = InitializeScale;
        playerAttack.swordDistance = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        bool charged = playerAttack.IsCharged;

        if (charged)
        {


            if (playerAttack.ChargeTimer >= playerAttack.maxChargeTime)
            {
                Renderer.transform.localScale = maxScale;
                playerAttack.swordDistance = 0.7f;

                if (max)
                {
                    return;
                }

                max = true;
                Vector3 pos = tf.localPosition;

                pos.x += 1.0f;

                tf.localPosition = pos;

            }

            else if (playerAttack.ChargeTimer >= playerAttack.mediumChargeTime)
            {

                Renderer.transform.localScale = mediumScale;
                playerAttack.swordDistance = 0.7f;

            }

            else if (playerAttack.ChargeTimer >= playerAttack.smallChargeTime)
            {

                Renderer.transform.localScale = smallScale;
                playerAttack.swordDistance = 0.5f;

            }

            else
            {
                Renderer.transform.localScale = InitializeScale;
                playerAttack.swordDistance = 0.1f;


                if (max)
                {
                    max = false;
                    Vector2 pos = tf.localPosition;

                    pos.x -= 1.0f;

                    tf.localPosition = pos;
                }


            }

        }

        if (!sword.SwordSize && !playerAttack.IsCharged)
        {
            Renderer.transform.localScale = InitializeScale;
            playerAttack.swordDistance = 0.1f;
        }
    }


}
