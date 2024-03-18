using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputControl : MonoBehaviour
{

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;
    [Header("Input Action Asset")]
    [SerializeField] private string actionMapName = "Player";
    [Header("Input Action Asset")]
    [SerializeField] private string move = "Move";
         [SerializeField] private string run = "Run";

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
