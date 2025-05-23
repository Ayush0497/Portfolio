using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScrip : MonoBehaviour
{
    [SerializeField] float movementSpeed = 3.0f;
    [SerializeField] float rotationSpeed = 90.0f;
    [SerializeField] GameObject turret;
    [SerializeField] GameObject barrel;

    [SerializeField] InputAction leftTreadAction;
    [SerializeField] InputAction rightTreadAction;
    [SerializeField] InputAction rotationAction;

    [SerializeField] Vector2 leftTread;
    [SerializeField] Vector2 rightTread;
    [SerializeField] Vector2 rotationValues;
    Vector3 barrelAngles;
    void Update()
    {

        //movement
        //if (Keyboard.current.wKey.isPressed)
        //{
        //    transform.Translate(Vector3.forward * movementSpeed / 2 * Time.deltaTime); //w Key - left forward
        //    transform.Rotate(0, rotationSpeed / 2 * Time.deltaTime, 0); //If the left tread is moving forward and the right is not moving the tank should rotate to the right at half speed.
        //}
        //if (Keyboard.current.sKey.isPressed)
        //{
        //    transform.Translate(Vector3.back * movementSpeed / 2 * Time.deltaTime); //S Key - left backward
        //    transform.Rotate(0, -rotationSpeed / 2 * Time.deltaTime, 0); //If the left tread is moving backward and the right is not moving the tank should rotate to the left at half speed.
        //}
        //if (Keyboard.current.iKey.isPressed)
        //{
        //    transform.Translate(Vector3.forward * movementSpeed / 2 * Time.deltaTime); //i Key - right forward
        //    transform.Rotate(0, -rotationSpeed / 2 * Time.deltaTime, 0); //If the right tread is moving forward and the left is not moving the tank should rotate to the left at half speed.
        //}
        //if (Keyboard.current.kKey.isPressed)
        //{
        //    transform.Translate(Vector3.back * movementSpeed / 2 * Time.deltaTime); //k Key - right backward
        //    transform.Rotate(0, rotationSpeed / 2 * Time.deltaTime, 0); //If the right tread is moving backward and the left is not moving the tank should rotate to the right at half speed.
        //}

        //if (Keyboard.current.wKey.isPressed && Keyboard.current.iKey.isPressed)
        //{
        //    transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime); //w and i Keys - foward at full speed
        //}

        //if (Keyboard.current.sKey.isPressed && Keyboard.current.kKey.isPressed) //s and k keys - backward at full speed
        //{
        //    transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        //}


        ////Rotation
        //if (Keyboard.current.iKey.isPressed && Keyboard.current.sKey.isPressed) //If the right tread is moving forward and the left is moving backward the tank should rotate to the left at full speed.
        //{
        //    transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        //}

        //if (Keyboard.current.kKey.isPressed && Keyboard.current.wKey.isPressed) //If the right tread is moving forward and the left is moving backward the tank should rotate to the left at full speed.
        //{
        //    transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        //}

        //if (Keyboard.current.dKey.isPressed) //rotate turret left
        //{
        //    turret.transform.Rotate(0, -rotationSpeed * Time.deltaTime, 0);
        //}

        //if (Keyboard.current.jKey.isPressed) //rotate turret right
        //{
        //    turret.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        //}


        //moveValues = moveAction.ReadValue<Vector2>();
        //rotationValues = rotationAction.ReadValue<Vector2>();

   
        leftTread = leftTreadAction.ReadValue<Vector2>();
        rightTread = rightTreadAction.ReadValue<Vector2>();
        rotationValues = rotationAction.ReadValue<Vector2>();

        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime * (leftTread.y + rightTread.y));
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * (leftTread.y - rightTread.y));

        turret.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * rotationValues.x);
        barrel.transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime * rotationValues.y);
        barrelAngles = barrel.transform.localRotation.eulerAngles;
        if(barrelAngles.x > 5 && barrelAngles.x < 180)
        {
            barrel.transform.localRotation = Quaternion.Euler(5.0f, 0, 0);
        }
        if (barrelAngles.x > 180 && barrelAngles.x < 355)
        {
            barrel.transform.localRotation = Quaternion.Euler(355.0f, 0, 0);
        }

        if(Keyboard.current.escapeKey.isPressed)
        {
            Application.Quit();
        }
    }

    void OnEnable()
    {
        leftTreadAction.Enable();
        rightTreadAction.Enable();
        rotationAction.Enable();
    }

    void OnDisable()
    {
        leftTreadAction.Disable();
        rightTreadAction.Disable();
        rotationAction.Disable();
    }


}
