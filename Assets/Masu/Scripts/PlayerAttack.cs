
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{


    [Header("攻撃用")]
    [SerializeField] InputAction attackAction;


    [Header("チャージ時間")]
    public float maxChargeTime = 2.1f;
    public float mediumChargeTime = 1.1f;
    public float smallChargeTime = 0.5f;

    float chargeTimer;

    public float ChargeTimer => chargeTimer;

    bool isCharged = false;

    public bool IsCharged => isCharged;

    [Header("剣")]
    [SerializeField] GameObject attackObject;

    public Sword sword;

    public float swordDistance = 1f;

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
        attackObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        Pause pause = FindAnyObjectByType<Pause>();
        if (pause.Paused) return;
        if (Camera.main != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        sword.swordVector = (mousePos - transform.position);

        }
        if (sword.attackAction.ReadValue<Vector2>() != Vector2.zero)
        {
            sword.swordVector = sword.attackAction.ReadValue<Vector2>();
        }

        sword.swordVector = sword.swordVector.normalized * swordDistance;
        if (!sword.isAttack)
        {
            if (attackAction.IsPressed() || Input.GetMouseButton(0))
            {
                isCharged = true;

                chargeTimer += Time.deltaTime;

                attackObject.SetActive(true);

            }

            attackObject.transform.position = (Vector2)transform.position + sword.swordVector;

            if (attackAction.WasReleasedThisFrame() || Input.GetMouseButtonUp(0))
            {
                float time = chargeTimer;
                chargeTimer = 0.0f;
                isCharged = false;


                if (time >= maxChargeTime)
                {

                    Debug.Log("maxCharge");
                    // ここに攻撃処理
                    sword.StartSlash(4);

                    return;

                }

                if (time >= mediumChargeTime)
                {

                    Debug.Log("mediumCharge");
                    sword.StartSlash(3);
                    return;
                }

                if (time >= smallChargeTime)
                {

                    Debug.Log("smallCharge");
                    sword.StartSlash(2);
                    return;
                }

                else
                {

                    Debug.Log("minCharge");
                    sword.StartSlash(1);
                    return;
                }



            }

        }
    }







}
