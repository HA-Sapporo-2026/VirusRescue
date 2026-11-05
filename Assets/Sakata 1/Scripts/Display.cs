using UnityEngine;

public class Display : MonoBehaviour
{
    public void DisplayDo(bool displayFlag)
    {
        gameObject.SetActive(displayFlag);
    }
}
