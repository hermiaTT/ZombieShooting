using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunSO : EquippableItemSO
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




