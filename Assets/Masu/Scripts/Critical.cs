using UnityEngine;

public class Critical : MonoBehaviour
{
    public int critical = 2;

    public int dyingCritical = 15;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool CriticalProbability()
    {

        int number = Random.Range(1, 100);

        /*
        // 感染度が90％を超えたら
        if( >= 90)
        {
            critical += dyingCritical;
        }
        */

        if (critical >= number)
        {
            return true;
        }


        return false;


    }


}
