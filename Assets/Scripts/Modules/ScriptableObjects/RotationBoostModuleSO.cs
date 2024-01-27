using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Todo, tie these to a menu item if need be
[CreateAssetMenu(fileName = "New RotationBoostModule", menuName = "Modules/Speed")]
public class RotationBoostModuleSO : ScriptableObject
{
   public int rotation;
}
