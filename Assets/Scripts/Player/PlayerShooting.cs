using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public List<Weapon> playerWeapons;

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.current != null)
        {
            Vector2 rightStickInput = Gamepad.current.rightStick.ReadValue();
            if (rightStickInput.sqrMagnitude > 0)
            {
                Debug.Log("Player Shooting");
                ShootDirection(rightStickInput);
            }
        }
        else
        {
            Debug.Log("No gamepad connected");
        }
    }

    //0 degrees is exactly right and then it goes in a counter-clockwise direction. i.e up is 90 degrees etc.
    private void ShootDirection(Vector2 stickInput)
    {
        //Atan2 is measuring the x and y from the point of 0 to return the angle in radians, Rad2Deg converts those radians to degress.
        float angle = Mathf.Atan2(stickInput.y, stickInput.x) * Mathf.Rad2Deg;
        //Converts the angle to be within the range of 0,360. By turning it positive and finding the remainder.
        angle = (angle + 360) % 360;
        //Debug.Log("Stick Input is" + angle);
        RaycastHit2D hit = FindWeapon(angle);
         // Check if the ray hit something
        if (hit.collider != null)
        {
            // Check if the hit object is of the weapon layer
            if (hit.collider.gameObject.tag == "Weapon")
            {
                // The ray hit a weapon
                Weapon weapon = hit.collider.gameObject.GetComponent<Weapon>();
                if (weapon != null)
                {
                    // Do something with the weapon (e.g., return it)
                    Debug.Log("Hit weapon: " + weapon.name);
                    weapon.WeaponShoot(angle);
                    // Your additional logic here
                }
            }
        }
        // if(angle >= 340 && angle <= 360 || angle >= 0 && angle <= 20)
        // {
        //     //Debug.Log("Stick Pushed Right");
        // } else if(angle >= 25 && angle <= 65) {
        //     //Debug.Log("Stick Pushed Right-Up Diagonal");
        // } else if(angle >= 70 && angle <= 110){
        //     //Debug.Log("Stick Pushed Up ");
        // } else if(angle >= 115 && angle <= 155){
        //     //Debug.Log("Stick Pushed Left-Up Diagonal");
        // } else if (angle >= 160 && angle <= 200){
        //     //Debug.Log("Stick Pushed Left");
        // } else if (angle >= 205 && angle <= 245){
        //     //Debug.Log("Stick Pushed Left-Down Diagonal");
        // } else if (angle >= 250 && angle <= 290){
        //     //Debug.Log("Stick Pushed Down");
        // } else if (angle >= 295 && angle <= 235){
        //     //Debug.Log("Stick Pushed Right-Down Diagonal");
        // } else {
        //     //Debug.Log("Stick Input not a direction" + angle);
        // }
    }

    private RaycastHit2D FindWeapon(float angle)
    {
        //Need to convert back to Radians from Degrees cause that's what unity uses.
        float radians = angle * Mathf.Deg2Rad;
        //This is math to convert the angle into the direction to be shot, might need to revise my Year 9 trigonometry
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        //Getting the radius of the circle so that raycast will only extend to the borders of the object.
        float playerRadius = this.GetComponent<Collider2D>().bounds.size.x * 0.5f;
        //Casting the ray from the center position of the player in the direction dictated. Also only interact with Weapons layer.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, playerRadius, LayerMask.GetMask("Weapons"));
        return hit;
    }
}
