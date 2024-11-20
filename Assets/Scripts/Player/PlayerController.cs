using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private InputManager inputManager;

    [SerializeField]
    private MenuManager menuManager;

    private Vector3 playerVelocity;

    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    public TMP_Text pos;
    public TMP_Text rot;
    public TMP_Text speed;


    private float currVelocity;

    private Metrics metrics = new Metrics();


    private void Start()
    {
        inputManager = InputManager.Instance;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        updateGravity();

        Vector2 simpleMovment = inputManager.GetMovement();
        Vector3 moveDirection = new Vector3(simpleMovment.x, 0f, simpleMovment.y);


        controller.Move(moveDirection * Time.deltaTime * playerSpeed);

        if (moveDirection != Vector3.zero)
            updateVisual(simpleMovment);

        //Simple Gravity
        updateMetrics();

        // Quick test
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuManager.Esc(true);
            menuManager.Pause(true);
        }
    }
    private void LateUpdate()
    {
        if (inputManager.GetRestart())
            resetPlayer();
    }
    private void updateVisual(Vector2 Direction)
    {
        var targetAngle = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Rad2Deg;
        var angleDamp = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currVelocity, 0.05f);
        gameObject.transform.rotation = Quaternion.Euler(0f, angleDamp, 0f);
    }

    private void updateGravity()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    private void updateMetrics()
    {
        Vector3 horizontalVelocity = new Vector3(controller.velocity.x, 0, controller.velocity.z);

        metrics.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
        metrics.angleRot = Mathf.Round(gameObject.transform.eulerAngles.y);
        metrics.speed = Mathf.Round(horizontalVelocity.magnitude);

        pos.text = "X: " + metrics.position.y.ToString() + " Y: " + metrics.position.x.ToString();
        rot.text = "Rotation: " + metrics.angleRot.ToString();
        speed.text = "Speed: " + metrics.speed.ToString();
    }

    private void resetPlayer()
    {
        controller.enabled = false;
        
        transform.position = new Vector3(0, transform.position.y, 0);
        transform.rotation = Quaternion.identity;
        
        controller.enabled = true;
    }


}
[Serializable]
struct Metrics 
{
    public Vector2 position;
    public float angleRot;
    public float speed;
}

