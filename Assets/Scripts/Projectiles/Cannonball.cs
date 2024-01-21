using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : Projectile
{
    // Start is called before the first frame update
    public float explosionRadius;

    public override void Launch(float firedAngle)
    {
        Debug.Log("Cannonball Projectile Fired");
        //this.GetComponent<Rigidbody>().AddForce(firedDirection * this.projectileForce, ForceMode.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Cannonball projectile has hit" + other);
    }
}
