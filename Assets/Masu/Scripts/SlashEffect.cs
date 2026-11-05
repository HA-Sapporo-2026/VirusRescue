using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    public GameObject[] effectPrefabs;

    int comboStep = 0;




    public void OnHitEnemy(Transform enemyTransform)
    {
        comboStep++;
        if (comboStep > 3)
            comboStep = 1;

        Vector3 swordPos = transform.position;
        Vector3 enemyPos = enemyTransform.position;
        Vector2 attackDirection = (enemyPos - swordPos).normalized;

        float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;

        Vector3 spawnPosition = enemyPos;
        Quaternion spownRotation = Quaternion.Euler(0, 0, angle - 110);

        Vector2 rightDirection = new Vector2(attackDirection.y, -attackDirection.x);

        switch (comboStep)
        {
            case 1:
                {
                    spawnPosition += (Vector3)(attackDirection * -0.2f + rightDirection * 0.3f);
                    break;
                }
            case 2:
                {
                    spawnPosition += (Vector3)(attackDirection * -0.2f + rightDirection * -0.3f);
                    break;
                }
            case 3:
                {
                    spawnPosition += (Vector3)(attackDirection * 0.1f);
                    break;
                }
        }

        int number = Random.Range(0, (effectPrefabs.Length));

        Instantiate(effectPrefabs[number], spawnPosition, spownRotation);

        Debug.Log(number);




    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
