using System;
using UnityEngine;
using UnityEngine.UI;

public class CameraManger : MonoBehaviour
{

    private InputManager inputManager;
    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float sensitivty = 1.0f;

    private float playerDistance;

    private Vector3 offset;
    private Vector2 camerRot;


    private Vector3 currVelocity = Vector3.zero;

    private int currentCameraPos = 0;
    public CameraPosition[] camerapos;

    bool canMove = true;

    private void Start()
    {
        inputManager = InputManager.Instance;

        if (PlayerPrefs.GetFloat("Sens") == 0f)
            PlayerPrefs.SetFloat("Sens", sensitivty);
        else
        {
            slider.value = PlayerPrefs.GetFloat("Sens") /10f;
            sensitivty = PlayerPrefs.GetFloat("Sens");
        }


    }

    private void Awake()
    {
        playerDistance = Vector3.Distance(transform.position, target.position);
    }

    private void Update()
    {
        var input = inputManager.GetMouse();
        camerRot.x += input.y * sensitivty * Time.deltaTime;
        camerRot.y += input.x * sensitivty * Time.deltaTime;
        camerRot.x = Mathf.Clamp(camerRot.x, -0f, 60f);

    }

    private void LateUpdate()
    {
        if (canMove)
        {
            transform.eulerAngles = new Vector3(camerRot.x, camerRot.y, 0f);
            transform.position = target.position - transform.forward * playerDistance;
        }else
        {
            var targetPos = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currVelocity, 0.1f);
        }


        if (Input.GetKeyDown(KeyCode.C))
            ChangeCamerPosition();

    }

    public void ChangeCamerPosition()
    {
        if (camerapos.Length-1 == currentCameraPos)
            currentCameraPos = 0;
        else
            currentCameraPos++;

       
        transform.position = camerapos[currentCameraPos].transform.position;
        transform.rotation = camerapos[currentCameraPos].transform.localRotation;

        offset = transform.position - target.position;
        canMove = camerapos[currentCameraPos].CanMoveMouse;
    }

    public void setSensitivity()
    {
        sensitivty = slider.value * 10f;
        PlayerPrefs.SetFloat("Sens", sensitivty);
    }
}
[Serializable]
public struct CameraPosition
{
    public Transform transform;
    public bool CanMoveMouse;
}


