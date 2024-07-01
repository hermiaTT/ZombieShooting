using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "NewWeapon")]
public class WeaponData : ItemSO
{
    public ShotType shotType;

    [field: SerializeField]
    public float ShootSpeed { get; set; }

    [field: SerializeField]
    public int ClipCapacity { get; set; }

    [SerializeField]
    public GameObject bulletPrefab;

    

    public enum ShotType
    {
        Single,
        Auto
    }
}
