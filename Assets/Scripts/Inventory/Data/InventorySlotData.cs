using System;
using UnityEngine;

namespace Inventory.Data
{
    [Serializable]
    public class InventorySlotData
    {
        public Sprite Icon;
        public string ItemId;
        public int Amount;
    }
}