using System.Collections.Generic;
using ExtractionArcade.Scripts.Inventory.ReadOnly;
using ExtractionArcade.Scripts.Inventory.Views;
using UnityEngine;

namespace ExtractionArcade.Scripts.Inventory.Controllers
{
    public class InventoryGridController
    {
        private readonly List<InventorySlotController> _slotsController = new ();

        public InventoryGridController(IReadOnlyInventoryGrid inventory, InventoryView view)
        {
            Vector2Int size = inventory.Size;
            IReadOnlyInventorySlot[,] slots = inventory.GetSlots();
            int lineLength = size.y;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    int index = i * lineLength + j;
                    var slotView = view.GetInventorySlotView(index);
                    var slot = slots[i, j];
                    _slotsController.Add(new InventorySlotController(slot, slotView, inventory.GetDefaultIcon));
                }
            }

            view.OwnerId = inventory.OwnerId;
        }
    }
}