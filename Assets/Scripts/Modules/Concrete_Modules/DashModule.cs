using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashModule : IModule
{
    private float _boost;
    private float _duration;
    public DashModule(float boost, float duration){
        _boost = boost;
        _duration = duration;

    }

    public void ApplyEffect(PlayerController player){
        //Can add in extra effects here as a template
        player.SetDash(_boost, _duration);
    }
}
