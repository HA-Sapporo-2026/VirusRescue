//using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapScreen : MonoBehaviour
{
    
    public InputAction MapButton;

    public bool Map = false;
    public GameObject screen;
    public GameObject BackGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MapButton.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (MapButton.WasPressedThisFrame())
        {

            if (Map)
            {
                Map = false;
                return;
            }
            else if (!Map)
            {
                Map = true;
                return;
            }
        }
        if (Map)
        {
            screen.SetActive(true);
            BackGround.SetActive(true);
        }
        else if (!Map)
        {            
            screen.SetActive(false);
            BackGround.SetActive(false);
        }
    }
}
