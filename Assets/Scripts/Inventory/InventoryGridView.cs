using Inventory.ReadOnly;
using Plugins.MonoCache;
using UnityEngine;

namespace Inventory
{
    public class InventoryGridView : MonoCache
    {
        public void Setup(IReadOnlyInventoryGrid inventory)
        {
            IReadOnlyInventorySlot[,] slots = inventory.GetSlots();
            Vector2Int size = inventory.Size;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    var slot = slots[i, j];
                    Debug.Log($"Slot ({i}:{j}. Item: {slot.ItemId}, amount: {slot.Amount})");
                }
            }
        }
    }
}