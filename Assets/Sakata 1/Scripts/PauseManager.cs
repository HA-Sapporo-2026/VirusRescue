using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    public string TitleSceneName;

    public Select[] selects;


    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        ChangeSelect(0);
    }


    public void ChangeSelect(int index)
    {
        if (selects == null)
            return;

        for (int i = 0; i < selects.Length; i++)
        {
            if (selects[i] != null)
            {
                selects[i].enabled = false;
            }
        }

        if (index >= 0 && index < selects.Length)
        {
            if (selects[index] != null)
            {
                selects[index].gameObject.SetActive(true);
                selects[index].enabled = true;
                selects[index].ResetSelection();
            }
        }
    }
}