using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationBoostModule : IModule
{
    private int _rotationBoost;
    public RotationBoostModule(int rotationBoost){
        _rotationBoost = rotationBoost;
    }

    public void ApplyEffect(PlayerController player){
        //Can add in extra effects here as a template
        player.ModifyRotation(_rotationBoost);
    }
}
