using UnityEngine;
using UnityEngine.UI;

public class swordCharge : MonoBehaviour
{

    [SerializeField] Slider chargeSlider;

    [SerializeField] GameObject chargeGauge;

    [SerializeField] Image chargeImage;

    [SerializeField] Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);

    [Header("color")]

    [SerializeField] Color normalColor;
    [SerializeField] Color smallColor;
    [SerializeField] Color mediumColor;
    [SerializeField] Color maxColor;

    public float maxTime = 3.0f;


    public PlayerAttack playerAttack;

    GameObject player;

    Transform mainCamaraTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chargeSlider.maxValue = 1.0f;

        chargeSlider.value = 0.0f;

        chargeImage.color = normalColor;

        mainCamaraTransform = Camera.main.transform;

        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        bool chargeTime = playerAttack.IsCharged;

        if (chargeTime)
        {
            chargeGauge.SetActive(true);

            float currentGargeTime = playerAttack.ChargeTimer / maxTime;


            chargeSlider.value = currentGargeTime;

            chargeSlider.transform.position = player.transform.position + offset;

            chargeSlider.transform.LookAt(mainCamaraTransform);

            if (playerAttack.ChargeTimer >= playerAttack.maxChargeTime)
            {
                chargeImage.color = maxColor;

            }

            else if (playerAttack.ChargeTimer >= playerAttack.mediumChargeTime)
            {
                chargeImage.color = mediumColor;


            }

            else if (playerAttack.ChargeTimer >= playerAttack.smallChargeTime)
            {
                chargeImage.color = smallColor;

            }

            else
            {
                chargeImage.color = normalColor;


            }






        }

        else
        {
            chargeGauge.SetActive(false);

            chargeImage.color = normalColor;


        }



    }
}
