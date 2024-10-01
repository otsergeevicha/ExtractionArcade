using System;
using Plugins.MonoCache;
using UnityEngine;

namespace Inventory.Views
{
    public class InventoryScreenView : MonoCache
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private InventoryView _inventoryView;

        public event Action<string> UseItem;
        public event Action Closed;

        public InventoryView InventoryView =>
            _inventoryView;

        public void OnActive() =>
            _canvas.enabled = true;

        public void InActive()
        {
            Closed?.Invoke();
            _canvas.enabled = false;
        }

        public void Use(InventorySlotView slot)
        {
            if (slot.Amount > 0) 
                UseItem?.Invoke(slot.Title);
        }
    }
}