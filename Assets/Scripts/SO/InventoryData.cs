using System.Collections.Generic;
using Inventory.Data;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Config/Inventory", order = 1)]
    public class InventoryData : ScriptableObject
    {
        public List<InventoryGridData> Inventories = new ();
    }
}