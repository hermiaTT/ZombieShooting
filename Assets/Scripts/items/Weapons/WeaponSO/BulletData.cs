using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletData : ItemSO
{
    [field: SerializeField]
    public float bulletSpeed { get; set; }
    [field: SerializeField]
    public float bulletDamage { get; set; }
}
