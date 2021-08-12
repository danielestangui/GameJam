using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("General")]
    public Transform bulletSpawn;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

   protected float nextShotTime;

    virtual public void Shoot()
    {

    }
}
