using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Inventory.Model;

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

        private void Awake()
        {

        }

        private void Start()
        {
            PrepareUI();
            //inventoryData.Initialize();

            input.InventoryOpenEvent += HandleInventoryOpen;
            input.InventoryCloseEvent += HandleIncentoryClose;
        }

        public void Update()
        {

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

        }

        private void HandleDragging(int itemIndex)
        {

        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {

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
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }

        public void HandleInventoryOpen()
        {
            Debug.Log("conShow");
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
            Debug.Log("conHide");
            inventoryUI.Hide();
        }
    }
}
