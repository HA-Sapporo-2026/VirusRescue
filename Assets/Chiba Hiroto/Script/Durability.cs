using UnityEngine;

public class Durability : MonoBehaviour
{
    [SerializeField] float maxDurability;
    [SerializeField] float durability;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        durability = maxDurability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
