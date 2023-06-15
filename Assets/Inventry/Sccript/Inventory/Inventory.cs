using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using JetBrains.Annotations;

public class Inventory : InstanceSystem<Inventory>
{
    [SerializeField] GameObject _inventory;
    [SerializeField] Text _moneyText;
    [SerializeField] int _money = 500; 
    ItemState[] _myItems;
    int _itemSlotCount;
    int[] _getItemIndex;

    public ItemState[] MyItems { get => _myItems; set => _myItems = value; }
    public int ItemSlotCount { get => _itemSlotCount; }
    public int Money { get => _money; set => _money = value; }

    private void Awake()
    {
        _itemSlotCount = _inventory.transform.childCount;
        _myItems = new ItemState[_itemSlotCount];
        List<ItemState> itemList = new List<ItemState>(Resources.Load<ItemData>("ItemBase").Item);
        for(int i = 0; i < ItemSlotCount; i++)
        {
            if(i < itemList.Count)
            {
                if (itemList[i].ItemCount > 0)
                {
                    _myItems[i] = itemList[i];
                }
                else
                {
                    _myItems[i] = new ItemState(-1, default, 1, null);
                }
            }
            else
            {
                _myItems[i] = new ItemState(-1, default, 1, null);
            }
        }
        SetItem();
        _inventory.SetActive(false);
    }
    public void SetMoney()
    {
        _moneyText.text = $"${_money}";
    }
    public void SetItem()
    {
        for(int i = 0; i < _itemSlotCount; i++)
        {
            var viewItemParent = _inventory.transform.GetChild(i);
            var viewItem = viewItemParent.transform.GetChild(0).GetComponent<ItemButton>();
            if (_myItems[i].ItemID != -1)
            {
                viewItem.MyItem = _myItems[i];
            }
            else
            {
                viewItem.MyItem = new ItemState(-1, default, 0, null);
            }
        }
    }

    public void ItemCountDown(int id)
    {
        for(int i = 0; i < _itemSlotCount; i++)
        {
            if (_myItems[i].ItemID == id)
            {
                if (_myItems[i].ItemCount > 0)
                {
                    _myItems[i].ItemCount--;
                    if (_myItems[i].ItemCount == 0)
                    {
                        _myItems[i] = new ItemState(-1, default, 0, null);
                        SetItem();
                    }
                    break;
                }
            }
        }
    }
}
