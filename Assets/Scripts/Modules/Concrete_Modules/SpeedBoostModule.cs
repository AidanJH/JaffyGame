using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostModule : MonoBehaviour
{
    private float _speedBoostPercentage;
    private float _speedBoostFlat;
    public SpeedBoostModule(float speedBoostPercentage, float speedBoostFlat){
        _speedBoostFlat = speedBoostFlat;
        _speedBoostPercentage = speedBoostPercentage;
    }


    public void ApplyEffect(GameObject player){
        
    }
}
