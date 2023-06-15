using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour, IPointerClickHandler
{
    ItemState _myItem;
    public ItemState MyItem { get => _myItem; set => _myItem = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        Evaluator evl = new();
        evl.SetPlayer(FindObjectOfType<PlayerController>());
        if (_myItem.Ability != null)
        {
            if (_myItem.Condition.All(x => x.Check(evl)))
            {
                _myItem.Ability.Use(evl);
                Inventory.Instance.ItemCountDown(_myItem.ItemID);
            }
        }
    }
    private void Awake()
    {
        _myItem = new ItemState(-1, default, 0, null);
    }
    void Update()
    {
        GetComponent<Image>().sprite = _myItem.ItemImage;
        GetComponentInChildren<Text>().text = _myItem.ItemCount.ToString();
    }
}
