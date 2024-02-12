using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostModuleButton : MonoBehaviour
{
    public PlayerController playerController; // Assign this in the Inspector

    void Start()
    {
        // Get the Button component and add a click listener
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Create and apply the rotation speed module
        RotationBoostModule rotationSpeedModule = new RotationBoostModule(5); // Adds 5 rotation speed
        rotationSpeedModule.ApplyEffect(playerController);
    }
}
