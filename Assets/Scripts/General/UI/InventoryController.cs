using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;
using System.Text;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InputReader input;

        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField]
        private WeaponSystem weaponSystem;

        [SerializeField]
        private UIEquipmentPage equipmentUI;

        private void Awake()
        {
            
        }

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();

            input.InventoryOpenEvent += HandleInventoryOpen;
            input.InventoryCloseEvent += HandleIncentoryClose;

            input.SwitchWeaponEvent += HandleSwitchWeapon;

            if (weaponSystem == null) Debug.LogError("WeaponSystem is not assigned.");
            if (equipmentUI == null) Debug.LogError("UIEquipmentPage is not assigned.");
            if (input == null) Debug.LogError("InputReader is not assigned.");

        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.isEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            this.inventoryUI.OnDescriptionRequested += HandelDescriptionRequest;
            this.inventoryUI.OnSwapItems += HandleSwapItems;
            this.inventoryUI.OnStartDragging += HandleDragging;
            this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
                return;

            

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroybleItem destroybleItem = inventoryItem.item as IDestroybleItem;
            if (destroybleItem != null)
            {
                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }

        }

        private void EquipWeapon(EquippableItemSO equippableItem, List<ItemParameter> itemState)
        {
            weaponSystem.SetWeapon(equippableItem, itemState);
            UpdateEquipmentUI(equippableItem); // Update the equipment UI
        }

        private void UpdateEquipmentUI(EquippableItemSO equippableItem)
        {
            equipmentUI.UpdateEquipmentSlot(equippableItem, weaponSystem.activeWeaponIndex);
        }

        public void HandleSwitchWeapon()
        {
            if (weaponSystem != null)
            {
                weaponSystem.SwitchWeapon();
            }
            else
            {
                Debug.LogError("WeaponSystem is not assigned.");
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
                return;

            IDestroybleItem destroybleItem = inventoryItem.item as IDestroybleItem;
            if (destroybleItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);

                if(inventoryData.GetItemAt(itemIndex).isEmpty)
                    inventoryUI.ResetSelection();
            }
        }



        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem =inventoryData.GetItemAt(itemIndex);
            if(inventoryItem.isEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandelDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryUI.UpdateDescription(itemIndex, 
                item.ItemImage, item.name, description);
           
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.parameterName} : " +
                    $"{inventoryItem.itemState[i].value} / " +
                    $"{inventoryItem.item.DefaultParametersList[i].value}");
            }
            return sb.ToString();
        }

        public void HandleInventoryOpen()
        {
            
            inventoryUI.Show();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key,
                    item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        }

        public void HandleIncentoryClose()
        {
            
            inventoryUI.Hide();
        }
    }
}





