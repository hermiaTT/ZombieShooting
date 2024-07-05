using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonPrefab;

        public void AddButton(string name, Action OnclickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => OnclickAction());
            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }

        internal void Toggle(bool val)
        {
            if (val == true)
                RemoveOldButtons();
            gameObject.SetActive(val);
            
        }

        private void RemoveOldButtons()
        {
            foreach(Transform transformChildObject in transform)
            {
                Destroy(transformChildObject.gameObject);
            }
        }
    }
}

