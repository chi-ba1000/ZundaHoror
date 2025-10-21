using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    //このスクリプトは自機にアタッチして
    public List<Item> itemInventory = new List<Item>();

    public void itemPick(Item iPick)
    {
        itemInventory.Add(iPick);
    }

    public bool checkItem(int cItemID)
    {
        foreach (Item item in itemInventory)
        {
            if (item.Id == cItemID)
            {
                return true;
            }
        }
        return false;
    }
    public void delItem(int dItemID)
    {
        for (int i = itemInventory.Count - 1; i >= 0; i--)
        {
            if (itemInventory[i].Id == dItemID)
            {
                itemInventory.RemoveAt(i);
            }
        }
    }
}
