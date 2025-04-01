using System;
using UnityEngine;

namespace ExtractionArcade.Scripts.Inventory.ReadOnly
{
    public interface IReadOnlyInventorySlot
    {
        event Action<string> ItemIdChanged;
        event Action<int> ItemAmountChanged;
        event Action<Sprite> ItemIconChanged;

        Sprite Icon { get; }
        string ItemId { get; }
        int Amount { get; }
        bool IsEmpty { get; }
    }
}