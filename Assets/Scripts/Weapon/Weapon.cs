using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public bool reloaded = true;
    public float reloadTime;

    public virtual void WeaponShoot(float firingAngle)
    {
        if(reloaded)
        {
            Projectile firedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            firedProjectile.gameObject.layer = gameObject.layer;
            firedProjectile.Launch(firingAngle);
            //Debug.Log("Fired at angle " + firingAngle);
            reloaded = false;
            StartCoroutine(ReloadWeapon());
        } else {
            Debug.Log("Weapon not reloaded yet");
        }
    }

    IEnumerator ReloadWeapon()
    {
        yield return new WaitForSeconds(reloadTime);
        reloaded = true;
    }
}
