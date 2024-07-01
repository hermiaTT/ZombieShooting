using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    public int selectedIndex = 0;

    public InputReader input;


    public void Start()
    {
        input.SwitchWeaponEvent += HandleSwitch;

        SetDefaultActiveIndex();
    }

    private void HandleSwitch()
    {
        DoSwitch();
    }


    //Activate current Weapon
    public void SelectWeapon()
    {

        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            GameObject weapon = this.transform.GetChild(i).gameObject;

            if (i == selectedIndex)
            {
                weapon.gameObject.SetActive(true);
                Debug.Log(selectedIndex);
            }
            else weapon.gameObject.SetActive(false);
        }
    }


    //Switch Weapon to next
    public void DoSwitch()
    {

        if (selectedIndex < transform.childCount - 1)
        {
            selectedIndex ++;
        }
        else
        {
            selectedIndex = 0;
        }

        for (int i = 0; i <= transform.childCount - 1; i++)
        {
            GameObject weapon = this.transform.GetChild(i).gameObject;

            if (i == selectedIndex)
            {
                weapon.gameObject.SetActive(true);
            }
            else weapon.gameObject.SetActive(false);
        }
    }


    //Set Active Index As [0] at beginning of the game
    public void SetDefaultActiveIndex()
    {
        for (int i = 0; i <= this.transform.childCount - 1; i++)
        {
            if (selectedIndex != i)
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}

