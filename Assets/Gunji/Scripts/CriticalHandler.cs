using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class CriticalHandler : MonoBehaviour
{
    public Animator zombieAnimator;

    public Rigidbody2D rigidbody;

    public GameObject criticalBlood;

    public Transform neckPoint;

    public Collider2D hitCollider;

    public void PlayCritical(EnemyHealth health)
    {
        // ★ 向きに応じて首の位置を補正する
        Vector3 spawnPos = neckPoint.position;

        hitCollider.enabled = false;

        // 左向きなら X を反転
        if (transform.localScale.x < 0)
        {
            spawnPos.x = transform.position.x - Mathf.Abs(neckPoint.localPosition.x);
        }
        else
        {
            spawnPos.x = transform.position.x + Mathf.Abs(neckPoint.localPosition.x);
        }

        if (criticalBlood != null && neckPoint != null)
        {
            Instantiate(criticalBlood, neckPoint.position, Quaternion.identity);
        }
        zombieAnimator.SetTrigger("Critical");
        rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        

        StartCoroutine(DelayDamage(health));
    }
   

    private IEnumerator DelayDamage(EnemyHealth health)
    {
        yield return new WaitForSeconds(1.0f);
        
        health.TakeDamage(health.CurrentHealth);
    }
}