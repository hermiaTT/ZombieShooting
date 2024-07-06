using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Inventory.UI
{
    public class UIEquipmentPage : MonoBehaviour
    {
        [SerializeField]
        private EquipmentSlot[] equipmentSlots;

        private void Start()
        {
            // Initialize all slots to be empty
            ClearAllSlots();
        }

        public void UpdateEquipmentSlot(EquippableItemSO equippableItem, int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= equipmentSlots.Length)
            {
                Debug.LogWarning("Invalid slot index");
                return;
            }

            equipmentSlots[slotIndex].EquipItem(equippableItem);
        }

        public void ClearAllSlots()
        {
            foreach (var slot in equipmentSlots)
            {
                slot.ClearSlot();
            }
        }
    }
}