using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class WeaponSystem : MonoBehaviour
{   
    public static WeaponSystem Instance { get; set; }
    //static int totalWeapons = 1;
    ////the weapon is currently hold by player
    private int currentWeaponIndex = -1;
    private int currentWeaponCount = 0;
    //public static WeaponSystem Instance { get; set; }

    [SerializeField]
    public List<GameObject> weaponBag;

    public GameObject activeWeaponSlot;

    public Transform spawnPosition;


    private void Awake()
    {
        if( Instance!=null &&  Instance!=this) Destroy(gameObject);
        else Instance = this;

    }

    private void Update()
    {
        if(currentWeaponIndex >= 0) {
            ActiveCurrentWeapon();
        }
    }


    private void AddWeaponIntoActiveBag(GameObject newWeapon)
    {
        var vacantIndex = currentWeaponIndex == 1 ? 0 : 1;
        activeWeaponSlot = weaponBag[vacantIndex]; 
        newWeapon.transform.SetParent(activeWeaponSlot.transform, false);
        newWeapon.transform.position = spawnPosition.position;
        currentWeaponIndex = vacantIndex;
        currentWeaponCount++;

    }
    private void SwapWeapon(GameObject newWeapon)
    {
        //drop current weapon
        DropCurrentWeapon();

        //add new weapon to current index
        newWeapon.transform.SetParent(activeWeaponSlot.transform, false);
        newWeapon.transform.position = spawnPosition.position;
  

    }
    private void ActiveCurrentWeapon()
    {
        GameObject weapon;
        WeaponController currentWeapon;
        //for loop 
        for (int i= 0; i < weaponBag.Count; i++)
        {
            if (weaponBag[i].transform && weaponBag[i].transform.childCount >0)
            {
                weapon = weaponBag[i].transform.GetChild(0).gameObject;
                //check if current slot has weapon
                //check if is current weapon
                if(i== currentWeaponIndex)
                {
                    //set active in element and in activeSlot
                    activeWeaponSlot = weaponBag[i];
                    weaponBag[i].SetActive(true);
                    //set is active in the weapon object
                    currentWeapon = weapon.GetComponent<WeaponController>();
                    currentWeapon.isActiveWeapon = true;

                }
                else
                {
                    weaponBag[i].SetActive(false);
                    currentWeapon = weapon.GetComponent<WeaponController>();
                    currentWeapon.isActiveWeapon = false;

                }
            }

        }

    }

    private void DropCurrentWeapon()
    {
        var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;
        WeaponController currentWeapon = weaponToDrop.GetComponent<WeaponController>();
        weaponToDrop.transform.SetParent(null);
        weaponToDrop.transform.position = GameObject.FindWithTag("Player").transform.position;
        currentWeapon.isActiveWeapon = false;
    }
    public void PickUpWeapon(GameObject newWeapon)
    {
        if (currentWeaponIndex != -1 && currentWeaponCount>= weaponBag.Count) {
            SwapWeapon(newWeapon);
        }
        else
        {
            AddWeaponIntoActiveBag(newWeapon);
        }

    }
    //only used for drop the current weapon that is holding
    public void DropWeapon()
    {
        if (activeWeaponSlot && activeWeaponSlot.transform && activeWeaponSlot.transform.childCount > 0)
        {

            DropCurrentWeapon();
              currentWeaponCount--;
            //if there is no weapon in player's hand, set currentWeaponIndex to negative and return -1 
            if (currentWeaponCount == 0) currentWeaponIndex = -1;
            else currentWeaponIndex = currentWeaponIndex == 1 ? 0 : 1;
        }

    }

    public void SwitchActiveSlot()
    {
        if(currentWeaponCount ==1)
        {
            return;
        }
        currentWeaponIndex = currentWeaponIndex == currentWeaponCount - 1 ? 0 : currentWeaponIndex + 1;

    }


}
