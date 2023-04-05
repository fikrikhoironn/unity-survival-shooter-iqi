using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadMenu : MonoBehaviour
{
    public GameObject[] saveSlots = new GameObject[3];

    void Start()
    {
        for (int i = 0; i < saveSlots.Length; i++)
        {
            if (DataManager.instance.saves[i] != null)
            {
                GameObject saveSlot = saveSlots[i];
                saveSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataManager
                    .instance
                    .saves[i].saveName;
            }
            else
            {
                GameObject saveSlot = saveSlots[i];
                saveSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Empty";
            }
        }
    }

    public void LoadGame(int saveSlot)
    {
        if (DataManager.instance.saves[saveSlot] != null)
        {
            DataManager.instance.LoadGame(saveSlot);
        }
    }
}
