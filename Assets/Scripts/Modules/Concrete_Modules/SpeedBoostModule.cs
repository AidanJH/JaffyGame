using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostModule : IModule
{
    private int _speedBoost;
    public SpeedBoostModule(int speedBoost){
        _speedBoost = speedBoost;
    }


    public void ApplyEffect(PlayerController player){
        //Can add in extra effects here as a template
        player.ModifySpeed(_speedBoost);
    }
}
