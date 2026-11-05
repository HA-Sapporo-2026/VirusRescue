using UnityEngine;
using UnityEngine.UI;

public class InfectionDisplay : MonoBehaviour
{
    public infectionManager infectionManager;
    public Sprite zero;
    public Sprite twenty;
    public Sprite fourty;
    public Sprite sixty;
    public Sprite eighty;
    public Sprite ninty;
    //public SpriteRenderer renderer;
    public Image Image;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (infectionManager == null) return;


        else if (infectionManager.infectionLevel >= 90)
        {
            Image.sprite = ninty;
        }
        else if (infectionManager.infectionLevel >= 80)
        {
            Image.sprite = eighty;
        }
        else if (infectionManager.infectionLevel >= 60)
        {
            Image.sprite = sixty;
        }
        else if (infectionManager.infectionLevel >= 40)
        {
            Image.sprite = fourty;
        }
        else if (infectionManager.infectionLevel >= 20)
        {
            Image.sprite = twenty;
        }
        else
        {
            Image.sprite = zero;
        }
    }
}
