using System;
using Inventory.Items;
using UnityEngine;

namespace Inventory.Data
{
    [Serializable]
    public class InventorySlotData
    {
        public TypeItem Item;
        public int Amount;
        public Sprite Icon;

        public string ItemId
        {
            get => 
                Item == TypeItem.None 
                    ? string.Empty
                    : FormattedTypeItemId();

            set => 
                Enum.TryParse<TypeItem>(value, out Item);
        }

        private string FormattedTypeItemId() => 
            Item.ToString().Replace(InventoryConstants.OldValue, InventoryConstants.NewValue);
    }
}