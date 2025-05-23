using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CubePosition : MonoBehaviour
{
    Vector3 initialPosition;
    [SerializeField] InputAction reset;
    [SerializeField] float resetValue;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        resetValue = reset.ReadValue<float>();
        if (resetValue == 1)
        {
            
            transform.position = initialPosition;
        }
    }

    void OnEnable()
    {
        reset.Enable();
    }

    void OnDisable()
    {
        reset.Disable();
    }
}
