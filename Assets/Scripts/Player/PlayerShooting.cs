using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

  public List<WeaponAnchor> playerWeaponAnchors;
	private PlayerInputActions playerInputActions;
	private bool usingMouseAndKeyboard = false;

	private void Awake() 
	{
		playerInputActions = new PlayerInputActions();
		playerInputActions.Player.Enable();
		
		playerInputActions.Player.Shoot.performed += ctx => HandleShootInput(ctx);
	}

	private void Update()
    {
        if (Gamepad.current != null)
        {
            usingMouseAndKeyboard = false;
            Vector2 rightStickInput = Gamepad.current.rightStick.ReadValue();
            if (rightStickInput.sqrMagnitude > 0)
            {
                ShootDirection(rightStickInput);
            }
        }
        else
        {
            usingMouseAndKeyboard = true;
        }
    }
	
	private void HandleShootInput(InputAction.CallbackContext context) 
	{
		if (usingMouseAndKeyboard)
		{
			// Mouse and keyboard shooting logic
			Vector3 mousePosition = Mouse.current.position.ReadValue();
			Vector2 shootDirection = ((Vector2)mousePosition - (Vector2)Camera.main.WorldToScreenPoint(transform.position)).normalized;
			ShootDirection(shootDirection);
		}
		else
		{
			// Controller shooting logic
			Vector2 rightStickInput = Gamepad.current.rightStick.ReadValue();
			ShootDirection(rightStickInput);
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
		if (hit.collider != null)
		{
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("PlayerWeapons"))
			{
				Weapon weapon = hit.collider.gameObject.GetComponent<Weapon>();
				if (weapon != null)
				{
					//Vector2 weaponCenter = hit.collider.bounds.center;
					Vector2 weaponCenter = weapon.GetComponent<Collider2D>().bounds.center;
					float recalibratedAngle = RecalibrateFiringAngle(weaponCenter);
					//Debug.Log("Hit weapon: " + weapon.name);
					weapon.WeaponShoot(recalibratedAngle);
				}
			}
		}
	}
  
    private RaycastHit2D FindWeapon(float angle)
    {
        //Need to convert back to Radians from Degrees cause that's what unity uses.
        float radians = angle * Mathf.Deg2Rad;
        //This is math to convert the angle into the direction to be shot, might need to revise my Year 9 trigonometry
        Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        //Getting the radius of the circle so that raycast will only extend to the borders of the object.
        float playerRadius = this.GetComponent<Collider2D>().bounds.size.x * 0.5f;
        //Casting the ray from the center position of the player in the direction dictated.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, playerRadius, LayerMask.GetMask("PlayerWeapons"));
        
        //Check if weapon attached to player anchor points, this is a second check might replace with the layer check above.
        if(hit.collider != null)
        {
            foreach(WeaponAnchor weaponAnchor in playerWeaponAnchors)
            {
                if(hit.collider.gameObject.transform.IsChildOf(weaponAnchor.transform))
                {
                    return hit;
                }
            }
        }
        return hit;
    }

    private float RecalibrateFiringAngle(Vector2 weaponCentrePosition)
    {       
        Vector2 directionToPlayer = weaponCentrePosition - (Vector2)transform.position;
        float recalibratedAngle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        return recalibratedAngle;
    }

}
