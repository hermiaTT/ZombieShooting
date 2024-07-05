using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryItem itemPrefabs;

        [SerializeField]
        private RectTransform contentPanel;

        [SerializeField]
        private UIInventoryDescription itemDescription;

        [SerializeField]
        private GameInput input;


        [SerializeField]
        private MouseFollower mouseFollower;

        List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private int currentDraggedItemIndex = -1;

        public event Action<int> OnDescriptionRequested,
            OnItemActionRequested,
            OnStartDragging;

        public event Action<int, int> OnSwapItems;

        public event Action<int> OnEquipToHand; 

        [SerializeField]
        private ItemActionPanel actionPanel;

        private void Awake()
        {
            Hide();
            mouseFollower.Toggle(false);
            itemDescription.ResetDescription();
        }

        public void HandleEquipToHand(int ID)
        {
            
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefabs, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseButtonClick += HandleShowItemActions;
            }
        }

        internal void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
        {
            itemDescription.SetDescription(itemImage, name, description);
            DeSelectAllItems();
            listOfUIItems[itemIndex].Select();
            
        }


        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                ResetDraggedItem();
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }


        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
            {
                ResetDraggedItem();
                return;
            }

            OnSwapItems?.Invoke(currentDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currentDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currentDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }

        public void Show()
        {
            gameObject.SetActive(true);


            ResetSelection();
            
        }

        public void ResetSelection()
        {
            itemDescription.ResetDescription();
            DeSelectAllItems();
        }


        public void AddAction(string actionName, Action perfomAction)
        {
            actionPanel.AddButton(actionName, perfomAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
        }

        private void DeSelectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }

            actionPanel.Toggle(false);
        }

        public void Hide()
        {
            actionPanel.Toggle(false);
            this.gameObject.SetActive(false);
            ResetDraggedItem();
            
        }

        internal void ResetAllItems()
        {
            foreach(var item in listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }    
        }
    }
}
