using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeItem : MonoBehaviour
{
    public ItemDisplay itemDisplayScript;
    public ItemObject itemToChange;

    public void ChangeToDifferentITem()
    {
        itemDisplayScript.displayedItem = itemToChange;
        itemDisplayScript.Display();
    }
}
