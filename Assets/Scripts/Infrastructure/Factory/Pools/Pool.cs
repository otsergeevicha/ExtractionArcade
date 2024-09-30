using System;
using Inventory.Items;
using Plugins.MonoCache;
using Services.Factory;
using SO;

namespace Infrastructure.Factory.Pools
{
    public class Pool : MonoCache
    {
        public PoolItems PoolItems { get; private set; }

        public void Construct(IGameFactory factory, GameData gameData) => 
            PoolItems = new PoolItems(factory, (TypeItem[])Enum.GetValues(typeof(TypeItem)), gameData.CountItems);
    }
}