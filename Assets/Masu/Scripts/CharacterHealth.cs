using UnityEngine;
using System;

public class CharacterHealth : MonoBehaviour
{

    public float maxHealth = 1;

    float currenthealth;

    public float CurrentHealth => currenthealth;


    void OnEnable()
    {

        currenthealth = maxHealth;

    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {

        currenthealth = Mathf.Max(0, currenthealth - damage);

        if (currenthealth <= 0)
        {
            gameObject.SetActive(false);

        }


    }



}
