using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveMenu : MonoBehaviour
{
    public GameObject[] saveSlots = new GameObject[3];
    public TMP_InputField saveNameInput;

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

    public void SaveGame(int saveSlot)
    {
        if (DataManager.instance.currentSaveData.saveName != "")
        {
            DataManager.instance.SaveGameData(DataManager.instance.currentSaveData, saveSlot);
            GameObject saveSlotObj = saveSlots[saveSlot];
            saveSlotObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = DataManager
                .instance
                .saves[saveSlot].saveName;
        }
    }

    public void changeSaveName(string saveName)
    {
        DataManager.instance.currentSaveData.saveName = saveName;
    }
}
