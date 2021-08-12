using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform weaponHold;
    public Weapon startingWeapon;
    Weapon equippedWeapon;

    void Start()
    {
        if (startingWeapon != null)
        {
            EquipGun(startingWeapon);
        }
    }

    public void EquipGun(Weapon gunToEquip)
    {
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon.gameObject);
        }
        equippedWeapon = Instantiate(gunToEquip, weaponHold.position, weaponHold.rotation) as Weapon;
        equippedWeapon.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.Shoot();
        }
    }

}
