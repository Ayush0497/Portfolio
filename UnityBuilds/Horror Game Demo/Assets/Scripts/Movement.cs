using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] Vector2 playerMovement;

    [SerializeField] InputAction lookAction;
    [SerializeField] Vector2 lookTarget;

    [SerializeField] float mouseSensitivity = 100.0f;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] GameObject cam;
    [SerializeField] private Animator animator;


    private float maxVerticalAngle = 30f;

    private float verticalRotation = 0f;

    private void Start()
    {
        animator.enabled = false;
        Cursor.visible = false;
    }

    private void Update()
    {
        playerMovement = movement.ReadValue<Vector2>();
        lookTarget = lookAction.ReadValue<Vector2>();

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

    private void SwitchPosition()
    {
        movement.Disable();
        lookAction.Disable();
        transform.position = new Vector3(-29.33f, 4.0f, -25.93f);
        transform.rotation = Quaternion.Euler(0.0f, -89.0f, 0.0f);
        cam.transform.localRotation = Quaternion.Euler(0.0f, -89.0f, 0f);
    }

    public void StartAnimation()
    {
        animator.enabled = true;
    }

    void OnEnable()
    {
        movement.Enable();
        lookAction.Enable();   
    }

    void OnDisable()
    {
        movement.Disable();
        lookAction.Disable();
    }
}


