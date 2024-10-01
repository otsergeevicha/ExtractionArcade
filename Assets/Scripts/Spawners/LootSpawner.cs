using System.Collections.Generic;
using System.Linq;
using Inventory.Items;
using UnityEngine;

namespace Spawners
{
    public class LootSpawner
    {
        public LootSpawner(IReadOnlyList<PickUpItem> poolItems, Vector3 spawnPoint) => 
            Spawn(poolItems, spawnPoint);

        private void Spawn(IReadOnlyList<PickUpItem> poolItems, Vector3 spawnPoint) => 
            poolItems.FirstOrDefault(item => !item.isActiveAndEnabled)?.OnActive(spawnPoint);
    }
}