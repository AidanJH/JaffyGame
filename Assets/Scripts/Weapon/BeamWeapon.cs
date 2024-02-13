using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeapon : Weapon
{
    public int chargeTime;
    private bool charged = false;

    public override void WeaponShoot(float firingAngle)
    {
        if (reloaded)
        {
            if (charged)
            {
                //I offset the beam by moving the rotation point in the sprite editor, wouldn't work here for some reason.
                Projectile firedProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
                firedProjectile.gameObject.layer = gameObject.layer;
                firedProjectile.transform.SetParent(transform);
                firedProjectile.Launch(firingAngle);
                //Debug.Log("Fired at angle " + firingAngle);
                reloaded = false;
                charged = false;
                StartCoroutine(ReloadWeapon());
            } else {
                StartCoroutine(ChargeWeapon());
            }
        } else {
            Debug.Log("Weapon not reloaded yet");
        }
    }

    IEnumerator ChargeWeapon()
    {
        Debug.Log("Charging Weapon");
        yield return new WaitForSeconds(chargeTime);

        charged = true;
    }
}
