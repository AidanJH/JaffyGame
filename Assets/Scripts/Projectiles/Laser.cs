using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile
{
    public int stayTime;

    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(DestroyLaser());  
    }
 

    public override void Launch(float firedAngle)
    {   
        //Debug.Log("Projectile Fired");
    }

    IEnumerator DestroyLaser()
    {
        yield return new WaitForSeconds(stayTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(gameObject.layer != other.gameObject.layer)
        {
            Debug.Log("Laser has hit" + other.gameObject.name);
        } else {
            Debug.Log(gameObject.name + "hit an object in the same layer");
        }
    }
}
