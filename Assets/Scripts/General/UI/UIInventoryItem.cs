using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;


namespace Inventory.UI
{
    public class UIInventoryItem : MonoBehaviour,IPointerClickHandler,
    IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        [SerializeField]
        private Image itemImage;

        [SerializeField]
        private TMP_Text quantityTXT;

        [SerializeField]
        private Image borderImage;

        //[SerializeField]
        //private InputReader input;
       

        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseButtonClick;

        private bool empty = true;

        private void Awake()
        {
            ResetData();
            Deselect();
            

        }
        private void Start()
        {
            //input.InventoryItemActionEvent += HandleRightMouseDoubleClick;
            //input.InventoryItemSelectEvent += HandleLeftMouseClick;

            
        }
        //private void DetectUIObject()
        //{
        //    Ray ray = mainCamera.ScreenPointToRay(gameInput.PlayerUI.MousePosition.ReadValue<Vector2>());
        //    RaycastHit hit;


        //}

        public void ResetData()
        {
            this.itemImage.gameObject.SetActive(false);
            this.empty = true;
        }

        public void Deselect()
        {
            this.borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quantity)
        {
            this.itemImage.gameObject.SetActive(true);
            this.itemImage.sprite = sprite;
            this.quantityTXT.text = quantity + "";
            this.empty = false;
        }

        public void Select()
        {
            borderImage.enabled = true;
            
        }
        

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (empty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PointerEventData pointerData = (PointerEventData)eventData;
            
            if(pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseButtonClick?.Invoke(this);
            }
            else if(pointerData.button == PointerEventData.InputButton.Left)
                OnItemClicked?.Invoke(this);
        }
    }
}

