using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Projectile : MonoBehaviour 
{
    public float projectileForce;
    public int projectileDamage;

    public virtual void Launch(float firedAngle)
    {
        Debug.Log("Default Projectile Fired");
        //Convert the angle to radians for Unity.
        float radians = firedAngle * Mathf.Deg2Rad;

        // Calculate the direction based on the angle, this is the same as PlayerShooting for the weapons.
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        
        //rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);
        this.GetComponent<Rigidbody2D>().AddForce(direction * projectileForce, ForceMode2D.Impulse);

        Debug.Log("Projectile Fired");
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Default projectile has hit" + other.gameObject.name);

    }
}
    
