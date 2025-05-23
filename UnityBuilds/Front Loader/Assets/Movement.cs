using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float rotationSpeed = 90.0f;
    [SerializeField] InputAction movementAction;
    [SerializeField] Vector2 movement;
    // Update is called once per frame
    void Update()
    {
        movement = movementAction.ReadValue<Vector2>();
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime * movement.y);
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
