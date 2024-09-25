﻿using System.Collections.Generic;
using Inventory.Data;
using Inventory.ReadOnly;
using Inventory.Structs;
using UnityEngine;

namespace Inventory
{
    public class InventoryService
    {
        private readonly Dictionary<string, InventoryGrid> _inventoriesMap = new ();

        public InventoryGrid RegisterInventory(InventoryGridData inventoryData)
        {
            var inventory = new InventoryGrid(inventoryData);
            _inventoriesMap[inventory.OwnerId] = inventory;

            return inventory;
        }

        public AddItemsToInventoryGridResult AddItemsToInventory(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.AddItems(itemId, amount);
        }

        public AddItemsToInventoryGridResult AddItemsToInventory(string ownerId, Vector2Int slotCoordinate,
            string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.AddItems(slotCoordinate, itemId, amount);
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(itemId, amount);
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(string ownerId, Vector2Int slotCoordinate, string itemId,
            int amount)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.RemoveItems(slotCoordinate, itemId, amount);
        }

        public bool Has(string ownerId, string itemId, int amount = 1)
        {
            var inventory = _inventoriesMap[ownerId];
            return inventory.Has(itemId, amount);
        }

        public IReadOnlyInventoryGrid GetInventory(string ownerId) => 
            _inventoriesMap[ownerId];
    }
}