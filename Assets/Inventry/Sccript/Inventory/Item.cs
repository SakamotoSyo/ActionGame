using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] int _itemID;
    [SerializeField] int _money;

    private void Awake()
    {
        GetComponent<Image>().sprite = Resources.Load<ItemData>("ItemBase").Item[_itemID].ItemImage;
        GetComponentInChildren<Text>().text = $"{_money}";
    }
    public void Get()
    {
        if (Inventory.Instance.Money > _money)
        {
            Inventory.Instance.Money -= _money;
            Inventory.Instance.SetMoney();
            for (int i = 0; i < Inventory.Instance.ItemSlotCount; i++)
            {
                var myItems = Inventory.Instance.MyItems;
                if (myItems[i].ItemID == _itemID && myItems[i].ItemCount < 99)
                {
                    myItems[i].ItemCount++;
                    //Inventory.Instance.SetItem();
                    break;
                }
                if (myItems[i].ItemID == -1)
                {
                    myItems[i] = Resources.Load<ItemData>("ItemBase").Item[_itemID];
                    myItems[i].ItemCount++;
                    //Inventory.Instance.SetItem();
                    break;
                }
            }
        }
    }
}
