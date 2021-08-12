using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [Header("Shotgun config")]
    public int _projectiles = 5;
    public float _angle = 20f;

    public override void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            for (int i = 0; i < _projectiles;i++)
            {
                float realAngle = _angle / 2;
                Quaternion originalRotation = bulletSpawn.rotation;
                bulletSpawn.Rotate(0, 0, Random.Range(-realAngle,realAngle)) ;
                Projectile newProjectile = Instantiate(projectile, bulletSpawn.position,bulletSpawn.rotation) as Projectile;
                bulletSpawn.rotation = originalRotation;
                newProjectile.SetSpeed(muzzleVelocity);
            }
        }
    }
}
