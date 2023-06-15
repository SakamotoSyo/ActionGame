using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField]
    List<ItemState> _item = new List<ItemState>();

    public List<ItemState> Item { get => _item; } 
}
