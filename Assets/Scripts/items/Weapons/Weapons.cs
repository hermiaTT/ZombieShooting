using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

abstract public class Weapons : MonoBehaviour, IPickable
{
    [SerializeField]
    Transform muzzle;

    protected float nextTimeToFire = 0f;

    [SerializeField]
    public WeaponData weapon;

    public InputReader input;

    public bool isFire;

    [SerializeField]
    private Camera _Camera;

    private Vector2 mousePosition;
    private Vector2 direction;
    private float angle;

    public void Start()
    {

        input.FireEvent += HandleFire;
        input.FireCancelEvent += HandleCancelledFire;

        input.AimEvent += HandleAim;
    }


    #region Fire Method

    private void HandleFire()
    {
        isFire = true;
    }
    private void HandleCancelledFire()
    {
        isFire = false; 
    }

    private bool CheckShootSpeed()
    {
        if (Time.time > nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1f / weapon.ShootSpeed);
            return true;
        }
        return false;
    }

    //SpawnBullet
    public void SpawnBullet()
    {
        GameObject bullet = Instantiate(weapon.bulletPrefab,
            muzzle.position,
            muzzle.rotation);
    }

    virtual public void Fire()
    {
        if (isFire)
        {
            if (weapon.shotType == WeaponData.ShotType.Auto)
            {
                if(CheckShootSpeed())
                {
                    SpawnBullet();
                }
            }
            else if(weapon.shotType == WeaponData.ShotType.Single)
            {
                SpawnBullet();
                isFire = false;
            }
        }
    }
    #endregion


    #region Rotation Method
    private void HandleAim(Vector2 direction)
    {
        mousePosition = _Camera.ScreenToWorldPoint(direction);
    }
    public virtual void WeaponRotation()
    {
        direction = (mousePosition - (Vector2)this.transform.position).normalized;
        this.transform.right = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localscale = new Vector3(1, 1, 1);
        if (angle > 90 || angle < -90)
        {
            localscale.y = -1f;
            localscale.x = -1f;
        }
        else
        {
            localscale.y = 1f;
        }

        this.transform.localScale = localscale;
    }
    #endregion


}
