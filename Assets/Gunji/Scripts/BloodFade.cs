using UnityEngine;

public class BloodFade : MonoBehaviour
{
    public float lifeTime = 5f; // 何秒で消えるか

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}