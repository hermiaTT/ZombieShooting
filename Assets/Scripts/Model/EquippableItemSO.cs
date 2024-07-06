using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EquippableItemSO : ItemSO, IDestroybleItem, IItemAction
    {
        public string ActionName => "Equip";

        public AudioClip actionSFX { get; private set; }

        public GameObject itemPrefab;

        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            WeaponSystem weaponSystem = character.GetComponent<WeaponSystem>();
            if (weaponSystem != null) 
            {
                weaponSystem.SetWeapon(this, itemState == null ? DefaultParametersList : itemState);
                return true;
            }
            return false;

        }
    }
}





