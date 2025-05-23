using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction movementAction;
    [SerializeField] float forwardSpeed = 10.0f;
    [SerializeField] float rotationSpeed = 20.0f;
    [SerializeField] Vector2 movement;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = movementAction.ReadValue<Vector2>();
        if (movement.y != 0)
        {
            rb.AddForce(transform.forward * Time.fixedDeltaTime * forwardSpeed * movement.y, ForceMode.Impulse);
        }
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * movement.x);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void OnEnable()
    {
        movementAction.Enable();
    }

    void OnDisable()
    {
        movementAction.Disable();
    }
}
