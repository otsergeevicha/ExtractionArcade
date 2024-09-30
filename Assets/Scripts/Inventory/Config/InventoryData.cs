using System;
using System.Collections.Generic;
using Inventory.Data;
using UnityEngine;

namespace Inventory.Config
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Config/Inventory", order = 1)]
    public class InventoryData : ScriptableObject
    {
        public List<InitConfigInventoryGrid> Inventories;
    }

    [Serializable]
    public class InitConfigInventoryGrid
    {
        public string OwnerID;
        public Vector2Int Size;
        public Sprite DefaultIcon;
        public List<InventorySlotData> Slots;
    }
}