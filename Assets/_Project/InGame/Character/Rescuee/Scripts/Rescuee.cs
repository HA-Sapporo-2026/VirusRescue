using UnityEngine;

namespace VirusRescue
{
    public class Rescuee : MonoBehaviour
    {
        [SerializeField] float callTimer = 0;
        
        [SerializeField] float callTime = 5;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void OnEnable()
        {
            
        }
        // Update is called once per frame
        void Update()
        {
            if (callTimer <= 0)
            {
                // ここでボイス再生

                // ここでボイス再生
                callTimer = callTime;
            }
            else 
            {
                callTimer -= Time.deltaTime;
            }
        }
    }
}