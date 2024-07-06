using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Model
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public Image slotImage;
        public EquippableItemSO equippedItem;

        public void EquipItem(EquippableItemSO item)
        {
            equippedItem = item;
            if (slotImage != null)
            {
                slotImage.sprite = item.ItemImage;
                slotImage.enabled = true;
            }
            else
            {
                Debug.LogError("slotImage is not assigned in EquipmentSlot.");
            }
        }

        public void ClearSlot()
        {
            equippedItem = null;
            if (slotImage != null)
            {
                slotImage.sprite = null;
                slotImage.enabled = false;
            }
            else
            {
                Debug.LogError("slotImage is not assigned in EquipmentSlot.");
            }
        }
    }
}
