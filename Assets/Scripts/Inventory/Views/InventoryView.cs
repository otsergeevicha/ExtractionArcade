using Plugins.MonoCache;
using TMPro;
using UnityEngine;

namespace Inventory.Views
{
    public class InventoryView : MonoCache
    {
        [SerializeField] private InventorySlotView[] _slots;
        [SerializeField] private TMP_Text _textOwner;

        public string OwnerId
        {
            get => _textOwner.text;
            set => _textOwner.text = value;
        }

        public InventorySlotView GetInventorySlotView(int index) => 
            _slots[index];
    }
}