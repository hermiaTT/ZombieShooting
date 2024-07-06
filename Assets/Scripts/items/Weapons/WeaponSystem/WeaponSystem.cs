using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField]
    private Transform rightHandTransform;

    [SerializeField]
    private UIEquipmentPage equipmentUI;

    public int activeWeaponIndex = 0;

    [SerializeField]
    private EquippableItemSO[] equippedWeapons = new EquippableItemSO[2];

    private GameObject currentWeaponObject;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private List<ItemParameter> parametersToModify, itemCurrentState1, itemCurrentState2;

    private void FixedUpdate()
    {
        
    }


    public void SetWeapon(EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if (equippedWeapons[0] == null)
        {
            EquipWeapon(0, weaponItemSO, itemState);
        }
        else if (equippedWeapons[1] == null)
        {
            EquipWeapon(1, weaponItemSO, itemState);
        }
        else
        {
            Debug.LogWarning("Both weapon slots are full. Please remove a weapon before equipping a new one.");
        }
    }

    private void EquipWeapon(int slot, EquippableItemSO weaponItemSO, List<ItemParameter> itemState)
    {
        if (equippedWeapons[slot] != null)
        {
            inventoryData.AddItem(equippedWeapons[slot], 1, slot == 0 ? itemCurrentState1 : itemCurrentState2);
        }

        if (currentWeaponObject != null)
        {
            Destroy(currentWeaponObject);
        }

        currentWeaponObject = Instantiate(weaponItemSO.itemPrefab, rightHandTransform); // Instantiate the weapon in hand
        currentWeaponObject.transform.localPosition = rightHandTransform.transform.localPosition; // Reset position
        //currentWeaponObject.transform.localRotation = Quaternion.identity; // Reset rotation

        equippedWeapons[slot] = weaponItemSO;
        if (slot == 0)
        {
            itemCurrentState1 = new List<ItemParameter>(itemState);
        }
        else
        {
            itemCurrentState2 = new List<ItemParameter>(itemState);
        }
        ModifyParameters(slot);

        // Update the equipment UI
        equipmentUI.UpdateEquipmentSlot(weaponItemSO, slot);
    }

    private void ModifyParameters(int slot)
    {
        var itemCurrentState = slot == 0 ? itemCurrentState1 : itemCurrentState2;
        foreach (var parameter in parametersToModify)
        {
            if (itemCurrentState.Contains(parameter))
            {
                int index = itemCurrentState.IndexOf(parameter);
                float newValue = itemCurrentState[index].value + parameter.value;
                itemCurrentState[index] = new ItemParameter
                {
                    itemParameter = parameter.itemParameter,
                    value = newValue
                };
            }
        }
    }

    public void SwitchWeapon()
    {
        activeWeaponIndex = 1 - activeWeaponIndex;
        Debug.Log($"Switched to weapon slot {activeWeaponIndex + 1}");

        // Destroy the current weapon object
        if (currentWeaponObject != null)
        {
            Destroy(currentWeaponObject);
        }

        // Instantiate the new weapon object if it is not null
        EquippableItemSO newWeapon = equippedWeapons[activeWeaponIndex];
        if (newWeapon != null)
        {
            currentWeaponObject = Instantiate(newWeapon.itemPrefab, rightHandTransform);
            currentWeaponObject.transform.localPosition = Vector3.zero; // Reset position
            currentWeaponObject.transform.localRotation = Quaternion.identity; // Reset rotation

            // Update the equipment UI
            equipmentUI.UpdateEquipmentSlot(newWeapon, activeWeaponIndex);
        }
        else
        {
            Debug.LogWarning($"No weapon equipped in slot {activeWeaponIndex + 1}");
        }

    }

    public EquippableItemSO GetActiveWeaponSlot()
    {
        return equippedWeapons[activeWeaponIndex];
    }
}
