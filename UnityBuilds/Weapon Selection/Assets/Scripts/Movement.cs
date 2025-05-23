using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] Vector2 playerMovement;

    [SerializeField] InputAction lookAction;
    [SerializeField] Vector2 lookTarget;

    [SerializeField] InputAction fireAction;
    [SerializeField] InputAction reloadAction;

    [SerializeField] float mouseSensitivity = 100.0f;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] GameObject cam;

    [SerializeField] GameObject pistol;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject rifle;

    private float maxVerticalAngle = 30f;

    private float verticalRotation = 0f;

    [SerializeField] GameObject[] paintableObjects;

    private void Start()
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        rifle.SetActive(false);
    }
    private void Update()
    {
        playerMovement = movement.ReadValue<Vector2>();
        lookTarget = lookAction.ReadValue<Vector2>();
        if(fireAction.ReadValue<float>()==1)
        {
            BroadcastMessage("fire", SendMessageOptions.DontRequireReceiver);
        }

        if(reloadAction.ReadValue<float>()==1)
        {
            BroadcastMessage("reload", SendMessageOptions.DontRequireReceiver);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            foreach(GameObject a in paintableObjects)
            {
                a.GetComponent<Renderer>().material.color = Color.white;
            }
        }

        transform.Rotate(Vector3.up, mouseSensitivity * Time.deltaTime * lookTarget.x);

        verticalRotation -= mouseSensitivity * Time.deltaTime * lookTarget.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * playerMovement.y);
        transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * playerMovement.x);      
    }

    void OnEnable()
    {
        movement.Enable();
        lookAction.Enable();
        fireAction.Enable();
        reloadAction.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        lookAction.Disable();
        fireAction.Disable();
        reloadAction.Disable();
    }
}


