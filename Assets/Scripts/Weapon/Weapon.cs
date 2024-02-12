using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon : MonoBehaviour
{
    public Projectile projectilePrefab;
    public bool reloaded = true;
    public float reloadTime;
    public AnimationClip reloadAnimationClip;
    public GameObject reloadPrefab;

    public virtual void WeaponShoot(float firingAngle)
    {
        if (reloaded)
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
        GameObject reloadInstance = Instantiate(reloadPrefab, transform.position, transform.rotation);
        Animator animator = reloadInstance.GetComponent<Animator>();
        reloadInstance.transform.SetParent(this.transform);

        if (animator != null)
        { 
            animator.Play(reloadAnimationClip.name);
            //Gets the length of the animation and adjust it to make it match the reload time.
            float animationDuration = reloadAnimationClip.length;
            float speedMultiplier = animationDuration / reloadTime;
            animator.speed = speedMultiplier;

            yield return new WaitForSeconds(reloadTime);
        }
        reloaded = true;
        Destroy(reloadInstance);
    }

}
