
using System;
using UnityEngine;

public class CountHumans : MonoBehaviour
{
    public int countHumans /*{ get; private set; }*/ = 0;
    public int maxHumans /*{ get; private set; }*/ = 20;

    [SerializeField] GameObject gauge;
    [SerializeField] GameObject gaugeBack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlusCount(int count)
    {
        countHumans += count;
        float average = countHumans * (maxHumans * 0.0025f);
        gauge.transform.localScale = new Vector3(average * gaugeBack.transform.localScale.x, gauge.transform.localScale.y, gauge.transform.localScale.z);
        gauge.transform.position = new Vector3(gauge.transform.localScale.x + (-1 + average) * gaugeBack.transform.localScale.x * 0.5f - gaugeBack.transform.localScale.x + transform.position.x, gauge.transform.position.y, 0);
    }
}
