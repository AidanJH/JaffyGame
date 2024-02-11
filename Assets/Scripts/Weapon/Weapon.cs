using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            reloaded = false;
            StartCoroutine(ReloadWeapon());
        } else {
            Debug.Log("Weapon not reloaded yet");
        }
    }

    protected virtual IEnumerator ReloadWeapon()
    {
        yield return new WaitForSeconds(reloadTime);
        reloaded = true;
    }
}
