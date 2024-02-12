using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashButton : MonoBehaviour
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
        DashModule dashModule = new DashModule(8f, .5f); // Adds 5 rotation speed
        dashModule.ApplyEffect(playerController);
    }
}
