using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction jump;
    [SerializeField] Vector2 playerMovement;
    [SerializeField] float playerJump;

    [SerializeField] float movementSpeed = 30f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] bool grounded = true;

    Rigidbody rbody;
    [SerializeField] float forceValue;

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerMovement = movement.ReadValue<Vector2>();
        playerJump = jump.ReadValue<float>();
        
        if (playerJump == 1.0f && grounded)
        {
            rbody.AddRelativeForce(Vector3.up * forceValue, ForceMode.Impulse);
            grounded = false;    
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * -playerMovement.y);
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * playerMovement.x);
    }

    private void OnCollisionEnter(Collision collision)
    {
        grounded = true;
    }

    void OnEnable()
    {
        movement.Enable();
        jump.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        jump.Disable();
    }
}


