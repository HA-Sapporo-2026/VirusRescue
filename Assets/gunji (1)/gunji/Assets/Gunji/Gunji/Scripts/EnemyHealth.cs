using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    // 最大体力
    public float maxHealth = 100;

    // 現在の体力
    float health;

    public float CurrentHealth => health;

    public Rigidbody2D rigidbody;

    public GameObject blood1Prefab;
    public GameObject blood2Prefab;
    public GameObject blood3Prefab;


    public AudioSource audioSource;
    public AudioClip die;

    public Animator zombieAnimator;

    public bool isCriticalDeath = false;

    float time = 1.0f;
    float timer = 0.0f;

    public Collider2D hitCollider;

    void OnEnable()
    {
        health = maxHealth;
        hitCollider.enabled = true;

        // 座標のフリーズを解放する
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }


    public void TakeDamage(float damage)
    {
        // Max関数(２つの大きい方を返す)
        // を用いて体力を減らす
        health = Mathf.Max(0, health - damage);

        // ダメージを受けた後の体力が
        // ０より小さいか、等しい場合
        if (health <= 0)
        {
       
            StartCoroutine(DeathRoutine());

        }
    }

    IEnumerator DeathRoutine()
    {
        FindAnyObjectByType<infectionManager>().AddSerumProgress(5.0f);

        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isCriticalDeath)
        {
            Instantiate(blood3Prefab, transform.position, Quaternion.identity);
        }
        else
        {
            int r = Random.Range(0, 2); // 0 or 1
            if (r == 0)
                Instantiate(blood1Prefab, transform.position, Quaternion.identity);
            else
                Instantiate(blood2Prefab, transform.position, Quaternion.identity);

            zombieAnimator.SetTrigger("Die");
            audioSource.PlayOneShot(die);
        }

        yield return new WaitForSeconds(time);

        // 自身を非アクティブにする
        gameObject.SetActive(false);


    }

}