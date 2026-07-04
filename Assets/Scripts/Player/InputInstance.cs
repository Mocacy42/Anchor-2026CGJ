using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInstance : MonoBehaviour
{
    public static InputInstance Instance;
    public PlayerInput PInput;
    public bool IsInteractPressed  {get; set;}
    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
        }

        if(PInput == null) PInput = new PlayerInput();

        PInput.Player.INTERACT.performed += ctx => IsInteractPressed = true;
        PInput.Player.INTERACT.canceled += ctx => IsInteractPressed = false;

    }
}
