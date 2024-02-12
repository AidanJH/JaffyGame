using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class WeaponAnchor : MonoBehaviour
{
    public GameObject weaponPrefab;
    // Start is called before the first frame update
    void Start()
    {
        if(WeaponAttached())
        {
            BuildWeapons();
        } else {
            Debug.Log("No Weapon Attached");
        }   
    }

    private void BuildWeapons()
    {
        GameObject newWeapon = Instantiate(weaponPrefab, this.transform.position, this.transform.rotation);
        newWeapon.transform.SetParent(this.transform);
        newWeapon.layer = this.gameObject.layer;
        
    } 

    private bool WeaponAttached()
    {
        //Returns true if not null
        return weaponPrefab != null;
    } 
}
