using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    ItemObject[] items;
    public Button itemButtonPrefab;
    private ItemDetails itemDetails;

    void Start()
    {
        itemDetails = GetComponentInParent<ItemDisplay>().itemDetails;
    }

    public void setItemList(ItemObject[] items)
    {
        this.items = items;
        // Clear children
        foreach (Button btn in GetComponentsInChildren<Button>())
        {
            Destroy(btn.gameObject);
        }

        // Add new buttons
        for (int i = 0; i < items.Length; i++)
        {
            Button btn = Instantiate(itemButtonPrefab, transform);
            btn.GetComponentInChildren<TextMeshProUGUI>().text = this.items[i].name;
            int index = i;
            btn.onClick.AddListener(() => setSelectedItem(index));
        }
    }

    void setSelectedItem(int index)
    {
        itemDetails.SetItem(items[index]);
    }
}
