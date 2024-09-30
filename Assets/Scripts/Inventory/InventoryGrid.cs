using System;
using System.Collections.Generic;
using Inventory.Data;
using Inventory.ReadOnly;
using Inventory.Structs;
using UnityEngine;

namespace Inventory
{
    public class InventoryGrid : IReadOnlyInventoryGrid
    {
        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;

            Vector2Int size = data.Size;

            for (int i = 0; i < size.x; i++)
            {
                for (int j = 0; j < size.y; j++)
                {
                    int index = i * size.y + j;
                    var slotData = data.Slots[index];
                    var slot = new InventorySlot(slotData);
                    var position = new Vector2Int(i, j);

                    _slotsMap[position] = slot;
                }
            }
        }

        public event Action<string, int> ItemsAdded;
        public event Action<string, int> ItemsRemoved;
        public event Action<Vector2Int> SizeChanged;

        public string OwnerId =>
            _data.OwnerID;
        
        public Sprite GetDefaultIcon 
            => _data.DefaultIcon;

        public AddItemsToInventoryGridResult AddItems(string itemId, Sprite icon, int amount = 1)
        {
            var remainingAmount = amount;
            
            var itemsAddedToSlotsWithSameItemsAmount =
                AddToSlotsWithSameItems(itemId, remainingAmount, out remainingAmount);

            if (remainingAmount <= 0)
                return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedToSlotsWithSameItemsAmount);

            var itemsAddedToAvailableSlotAmount =
                AddToFirstAvailableSlots(itemId, remainingAmount, icon, out remainingAmount);
            
            var totalAddedItemsAmount = itemsAddedToSlotsWithSameItemsAmount + itemsAddedToAvailableSlotAmount;

            return new AddItemsToInventoryGridResult(OwnerId, amount, totalAddedItemsAmount);
        }

        public AddItemsToInventoryGridResult AddItems(Vector2Int slotCoordinate, string itemId, Sprite icon, int amount = 1)
        {
            var slot = _slotsMap[slotCoordinate];
            int newValue = slot.Amount + amount;
            int itemsAddedAmount = 0;

            if (slot.IsEmpty)
                slot.ItemId = itemId;

            if (slot.Icon == null) 
                slot.Icon = icon;

            int itemSlotCapacity = GetItemSlotCapacity(itemId);

            if (newValue > itemSlotCapacity)
            {
                int remainingItems = newValue - itemSlotCapacity;
                int itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.Amount = itemSlotCapacity;

                var result = AddItems(itemId, icon, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newValue;
            }

            return new AddItemsToInventoryGridResult(OwnerId, amount, itemsAddedAmount);
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(string itemId, int amount = 1)
        {
            if (!Has(itemId, amount))
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);

            int amountToRemove = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var slotCoordinate = new Vector2Int(i, j);
                    var slot = _slotsMap[slotCoordinate];

                    if (slot.ItemId != itemId)
                        continue;

                    if (amountToRemove > slot.Amount)
                    {
                        amountToRemove -= slot.Amount;
                        RemoveItems(slotCoordinate, itemId, amountToRemove);
                    }
                    else
                    {
                        RemoveItems(slotCoordinate, itemId, amountToRemove);
                        return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
                    }
                }
            }

            throw new Exception("Something went wrong, couldn't remove some items");
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(Vector2Int slotCoordinate, string itemId, int amount = 1)
        {
            var slot = _slotsMap[slotCoordinate];

            if (slot.IsEmpty || slot.ItemId != itemId || slot.Amount < amount)
                return new RemoveItemsFromInventoryGridResult(OwnerId, amount, false);

            slot.Amount -= amount;

            if (slot.Amount == 0)
            {
                slot.ItemId = null;
                slot.Icon = _data.DefaultIcon;
            }

            return new RemoveItemsFromInventoryGridResult(OwnerId, amount, true);
        }

        public void SwitchSlots(Vector2Int slotCoordinateA, Vector2Int slotCoordinateB)
        {
            var slotA = _slotsMap[slotCoordinateA];
            var slotB = _slotsMap[slotCoordinateB];

            Sprite tempIcon = slotA.Icon;
            string tempSlotItemId = slotA.ItemId;
            int tempSlotItemAmount = slotA.Amount;

            slotA.Icon = slotB.Icon;
            slotA.ItemId = slotB.ItemId;
            slotA.Amount = slotB.Amount;

            slotB.Icon = tempIcon;
            slotB.ItemId = tempSlotItemId;
            slotB.Amount = tempSlotItemAmount;
        }
        
        public Vector2Int Size
        {
            get => _data.Size;
            set
            {
                if (_data.Size != value)
                {
                    _data.Size = value;
                    SizeChanged?.Invoke(value);
                }
            }
        }

        public int GetAmount(string itemId)
        {
            int amount = 0;
            List<InventorySlotData> slots = _data.Slots;

            foreach (InventorySlotData slot in slots)
            {
                if (slot.ItemId == itemId) 
                    amount += slot.Amount;
            }

            return amount;
        }

        public bool Has(string itemId, int amount)
        {
            int amountExist = GetAmount(itemId);
            return amountExist >= amount;
        }

        public IReadOnlyInventorySlot[,] GetSlots()
        {
            var array = new IReadOnlyInventorySlot[Size.x, Size.y];

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[position];
                }
            }

            return array;
        }

        private int GetItemSlotCapacity(string itemId) =>
            Constants.MaxCapacitySlot;

        private int AddToFirstAvailableSlots(string itemId, int amount, Sprite icon, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var coordinate = new Vector2Int(i, j);
                    var slot = _slotsMap[coordinate];

                    if (!slot.IsEmpty)
                        continue;

                    slot.ItemId = itemId;
                    slot.Icon = icon;
                    
                    int newValue = remainingAmount;
                    int slotItemCapacity = GetItemSlotCapacity(slot.ItemId);

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        int itemsToAddAmount = slotItemCapacity;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }

        private int AddToSlotsWithSameItems(string itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (int i = 0; i < Size.x; i++)
            {
                for (int j = 0; j < Size.y; j++)
                {
                    var coordinate = new Vector2Int(i, j);
                    var slot = _slotsMap[coordinate];

                    if (slot.IsEmpty)
                        continue;

                    int slotItemCapacity = GetItemSlotCapacity(slot.ItemId);

                    if (slot.Amount >= slotItemCapacity)
                        continue;

                    if (slot.ItemId != itemId)
                        continue;

                    int newValue = slot.Amount + remainingAmount;

                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;

                        if (remainingAmount == 0)
                            return itemsAddedAmount;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }

            return itemsAddedAmount;
        }
    }
}