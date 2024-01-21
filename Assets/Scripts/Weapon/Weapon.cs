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
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // if(Gamepad.current != null)
        // {
        //     Vector2 rightStickInput = Gamepad.current.rightStick.ReadValue();
        //     if (rightStickInput.sqrMagnitude > 0)
        //     {
        //         WeaponShoot();
        //     }
        // }
        // else
        // {
        //     Debug.Log("No gamepad connected");
        // }
    }

    public void WeaponShoot(float firedAngle)
    {
        if(reloaded)
        {
            Projectile firedProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            firedProjectile.Launch(firedAngle);
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
