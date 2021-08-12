using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolver : Weapon
{
    public override void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(projectile, bulletSpawn.position, bulletSpawn.rotation) as Projectile;
            newProjectile.SetSpeed(muzzleVelocity);
        }
    }
}
