using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 20.0f;
    private bool isInputDisabled = false;  // Flag to disable input
    [SerializeField] float hInput;
    [SerializeField] float vInput;
    [SerializeField] GameObject SpawnLocation;

    void Start()
    {
        MyEvents.disableInput.AddListener(DisableInput);
        MyEvents.enableInput.AddListener(EnableInput);
    }

    void Update()
    {
        if (isInputDisabled) return; // Stop movement if input is disabled

        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(hInput * movementSpeed * Time.deltaTime, 0, vInput * movementSpeed * Time.deltaTime));
    }

    void DisableInput()
    {
        gameObject.transform.position = SpawnLocation.transform.position;
        isInputDisabled = true; // Prevent Update() from reading input   
    }

    void EnableInput()
    {
        gameObject.transform.position = SpawnLocation.transform.position;
        isInputDisabled = false; // Prevent Update() from reading input
    }
}