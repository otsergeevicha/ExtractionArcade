using Inventory.ReadOnly;
using Inventory.Views;
using UnityEngine;

namespace Inventory.Controllers
{
    public class InventorySlotController
    {
        private readonly InventorySlotView _view;
        private readonly Sprite _defaultIcon;

        public InventorySlotController(IReadOnlyInventorySlot slot, InventorySlotView view,
            Sprite defaultIcon)
        {
            _defaultIcon = defaultIcon;
            _view = view;

            slot.ItemIdChanged += OnSlotItemIdChanged;
            slot.ItemAmountChanged += OnSlotItemAmountChanged;
            slot.ItemIconChanged += OnSlotItemIconChanged;

            OnSlotItemIconChanged(slot.Icon);

            view.Title = slot.ItemId;
            view.Amount = slot.Amount;
        }

        private void OnSlotItemIconChanged(Sprite newSprite) =>
            _view.Icon = newSprite == null
                ? _defaultIcon
                : newSprite;

        private void OnSlotItemIdChanged(string newItemId) =>
            _view.Title = newItemId;

        private void OnSlotItemAmountChanged(int newAmount) =>
            _view.Amount = newAmount;
    }
}