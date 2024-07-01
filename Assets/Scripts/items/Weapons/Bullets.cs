using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Bullets : MonoBehaviour
{
    [SerializeField]
    public BulletData bullet;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = this.transform.right * bullet.bulletSpeed;
    }
}
