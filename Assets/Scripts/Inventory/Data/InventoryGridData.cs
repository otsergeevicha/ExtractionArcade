﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Data
{
    [Serializable]
    public class InventoryGridData
    {
        public string OwnerID;
        public Vector2Int Size;
        public Sprite DefaultIcon;
        public List<InventorySlotData> Slots;
    }
}