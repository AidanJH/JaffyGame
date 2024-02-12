using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalModule : MonoBehaviour
{
    [SerializeField]
    private int health;
    private bool attachedToPlayer;
    public PlayerController playerController; // Assign this in the Inspector
    public IModule module;

    [SerializeField]
    private Rigidbody2D rigidbody;


    void Start()
    {
        module = new DashModule(8f, 2f);
        health = 5;
        rigidbody = GetComponent<Rigidbody2D>();
        Physics();
    }

    void TakeDamage()
    {
        if(attachedToPlayer){
            health--;

            if(health <= 0){
                Destroy();
            }
        }
        
    }


    void Physics()
    {
        //Rotate and slowly move throughout the air
        rigidbody.AddForce(transform.right * 1f * .25f * Time.deltaTime);
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ModuleAnchorPoint"))
        {
            Debug.Log("Attached to player anchor point");
            AttachToPlayer(collision.gameObject);
        }
    }

     private void AttachToPlayer(GameObject anchorPoint)
    {
        // Disable physics and collider
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

        // Optionally, set this part's parent to the player to follow the player's movement
        transform.SetParent(anchorPoint.transform);

        //TODO:  Set this position to be the anchorpoint
        //TODO: Create a Vector2.Lerp to rotate and position towards the player 
        Debug.Log("X: " + anchorPoint.transform.localPosition.x);
        Debug.Log("Y: " + anchorPoint.transform.localPosition.y);

        transform.localPosition = new Vector2(anchorPoint.transform.localPosition.x, anchorPoint.transform.localPosition.y);
        transform.localRotation = Quaternion.identity;

        module.ApplyEffect(playerController);
        attachedToPlayer = true;

    }


    private void Destroy(){
        
    }
}
