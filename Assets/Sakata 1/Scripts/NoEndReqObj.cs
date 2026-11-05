using UnityEngine;

public class NoEndReqObj : MonoBehaviour
{
    public GameObject[] endRequestObj;

    public GameObject[] noEndReqObj;

    public static int endRequestNum = 0;

    void Start()
    {
        if (endRequestObj == null)
            return;

        for (int i = 0; i < endRequestObj.Length; i++)
        {
            // If the field references a prefab asset (not a scene instance) we need to instantiate
            // it into the scene first.
            if (endRequestObj[i] != null && !endRequestObj[i].scene.IsValid())
            {
                endRequestObj[i] = Instantiate(endRequestObj[i], transform);
            }

            if (endRequestObj[i] != null)
                endRequestObj[i].SetActive(false);
        }
    }

    // Toggle all end-request objects on/off and invert noEndReqObj visibility
    public void EndRequest(bool ifClick)
    {
        if (noEndReqObj != null)
        {
            for (int j = 0; j < noEndReqObj.Length; j++)
            {
                if (noEndReqObj[j] != null)
                    noEndReqObj[j].SetActive(!ifClick);
            }
        }

        if (endRequestObj != null)
        {
            for (int i = 0; i < endRequestObj.Length; i++)
            {
                if (endRequestObj[i] != null)
                    endRequestObj[i].SetActive(ifClick);
            }
        }
    }

    // Toggle a specific end-request object by index. If showOnly is true, hide others.
    public void EndRequest(bool ifClick, int typeNum, bool showOnly = true)
    {
        if (noEndReqObj != null)
        {
            for (int j = 0; j < noEndReqObj.Length; j++)
            {
                if (noEndReqObj[j] != null)
                    noEndReqObj[j].SetActive(!ifClick);
            }
        }

        if (endRequestObj == null || endRequestObj.Length == 0)
            return;

        // Clamp index
        if (typeNum < 0 || typeNum >= endRequestObj.Length)
        {
            Debug.LogWarning($"NoEndReqObj: typeNum {typeNum} is out of range.");
            return;
        }

        if (showOnly)
        {
            for (int i = 0; i < endRequestObj.Length; i++)
            {
                if (endRequestObj[i] != null)
                    endRequestObj[i].SetActive(i == typeNum ? ifClick : false);
            }
        }
        else
        {
            if (endRequestObj[typeNum] != null)
                endRequestObj[typeNum].SetActive(ifClick);
        }

        endRequestNum = typeNum;
    }

    // Helper to set a specific index active/inactive
    public void SetActiveByIndex(int typeNum, bool active)
    {
        if (endRequestObj == null || typeNum < 0 || typeNum >= endRequestObj.Length)
            return;
        if (endRequestObj[typeNum] != null)
            endRequestObj[typeNum].SetActive(active);
    }
}
