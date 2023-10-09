using System.Collections.Generic;

namespace CoreRuntime.Weapons
{
    public class WeaponInventory : Inventory<Weapon>
    {
        public WeaponInventory(List<Weapon> itemList) : base(itemList)
        {
        }
    }
}