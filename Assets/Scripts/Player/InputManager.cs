using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance
    {
        get { return instance; }
    }

    private PlayerControlerInputs playerControlerInputs;
    void Awake()
    {
        //Error Handler
        if(instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = this;

        playerControlerInputs = new PlayerControlerInputs();
    }
    void OnEnable()
    {
        playerControlerInputs.Enable();
    }
    void OnDisable()
    {
        playerControlerInputs.Disable();
    }

    public Vector2 GetMovement()
    {
        return playerControlerInputs.Player.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMouse()
    {
        return playerControlerInputs.Player.Mouse.ReadValue<Vector2>();
    }
    public bool GetRestart()
    {
        return playerControlerInputs.Player.Restart.triggered;
    }
}

