using Plugins.MonoCache;
using UnityEngine;

namespace Inventory.Views
{
    public class InventoryScreenView : MonoCache
    {
        [SerializeField] private InventoryView _inventoryView;

        public InventoryView InventoryView =>
            _inventoryView;
    }
}