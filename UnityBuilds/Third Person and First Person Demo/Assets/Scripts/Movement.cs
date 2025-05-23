using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //Movement and Jump
    [SerializeField] InputAction movement;
    [SerializeField] Vector2 playerMovement;
    [SerializeField] InputAction lookAction;
    [SerializeField] Vector2 lookTarget;
    [SerializeField] InputAction jumpAction;
    [SerializeField] float jumpForce;
    [SerializeField] float mouseSensitivity = 100.0f;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] private bool grounded;
    [SerializeField] GameObject position;

    //Camera Stuff
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;
    [SerializeField] GameObject cam3;
    [SerializeField] InputAction cameraChangeAction;
    [SerializeField] InputAction zoomIn;
    [SerializeField] InputAction zoomOut;
    private float maxVerticalAngle = 30f;
    private float verticalRotation = 0f;

    //Firing and Reloading
    [SerializeField] InputAction fireAction;
    [SerializeField] InputAction reloadAction;
    
    [SerializeField] GameObject[] zombies;

    //Animations
    private Animator animator;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        grounded = true;
        cam1.SetActive(true);
        cam2.SetActive(false);
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(cam3.activeInHierarchy)
        {
            movement.Disable();
            lookAction.Disable();
            fireAction.Disable();
            reloadAction.Disable();
            jumpAction.Disable();
            zoomIn.Disable();
            zoomOut.Disable();
            this.transform.position = position.transform.position;
            this.transform.rotation = position.transform.rotation;
        }
        if(!cam3.activeInHierarchy)
        {
            movement.Enable();
            lookAction.Enable();
            fireAction.Enable();
            reloadAction.Enable();
            jumpAction.Enable();
            zoomIn.Enable();
            zoomOut.Enable();
        }
        //movement
        playerMovement = movement.ReadValue<Vector2>();
        lookTarget = lookAction.ReadValue<Vector2>();
        if ((playerMovement.x != 0f || playerMovement.y != 0f) && grounded)
        {
            animator.SetTrigger("run");
        }
        if ((playerMovement.x == 0f && playerMovement.y == 0f) && grounded)
        {
            animator.SetTrigger("idle");
        }

        if (jumpAction.ReadValue<float>() == 1f && grounded)
        {
            transform.GetComponent<Rigidbody>().AddForce(transform.up * Time.fixedDeltaTime * jumpForce, ForceMode.Impulse);
            grounded = false;
        }

        //shoot and reload
        if (fireAction.ReadValue<float>() == 1)
        {
            BroadcastMessage("fire", SendMessageOptions.DontRequireReceiver);
        }

        if (reloadAction.ReadValue<float>() == 1)
        {
            BroadcastMessage("reload", SendMessageOptions.DontRequireReceiver);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //camera stuff
        if (cameraChangeAction.ReadValue<float>() == 1)
        {
            if(cam1.activeInHierarchy && !cam2.activeInHierarchy)
            {
                GameObject a = GameObject.FindGameObjectWithTag("Weapon");
                cam2.SetActive(true);
                cam1.SetActive(false);
                GameObject b = GameObject.FindGameObjectWithTag("TPV");
                a.transform.position = b.transform.position;
                a.transform.rotation = b.transform.rotation;
                a.transform.SetParent(b.transform);
            }
            else if(!cam1.activeInHierarchy && cam2.activeInHierarchy)
            {
                GameObject a = GameObject.FindGameObjectWithTag("Weapon");
                cam1.SetActive(true);
                cam2.SetActive(false);
                GameObject b = GameObject.FindGameObjectWithTag("FPV");
                a.transform.position = b.transform.position;
                a.transform.rotation = b.transform.rotation;
                a.transform.SetParent(b.transform);
            }
            cameraChangeAction.Disable();
            StartCoroutine(ChangedCamera());
        }

        if (zoomIn.ReadValue<float>() == 1 && cam2.GetComponent<Camera>().fieldOfView < 80.0f)
        {
            if (cam2.activeInHierarchy)
            {
                cam2.GetComponent<Camera>().fieldOfView = cam2.GetComponent<Camera>().fieldOfView + 0.1f;
            }
        }

        if (zoomOut.ReadValue<float>() == 1 && cam2.GetComponent<Camera>().fieldOfView > 60.0f)
        {
            if (cam2.activeInHierarchy)
            {
                cam2.GetComponent<Camera>().fieldOfView = cam2.GetComponent<Camera>().fieldOfView - 0.1f;
            }
        }


        //moving and rotating the player and cameras
        transform.Rotate(Vector3.up, mouseSensitivity * Time.deltaTime * lookTarget.x);

        verticalRotation -= mouseSensitivity * Time.deltaTime * lookTarget.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        cam1.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        cam2.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);


        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed * playerMovement.y);
        transform.Translate(Vector3.right * Time.deltaTime * movementSpeed * playerMovement.x);

    }
    private IEnumerator ChangedCamera()
    {
        yield return new WaitForSeconds(0.5f);
        cameraChangeAction.Enable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<TerrainCollider>() != null)
        {
            grounded = true;
        }
    }

    void OnEnable()
    {
        movement.Enable();
        lookAction.Enable();
        fireAction.Enable();
        reloadAction.Enable();
        jumpAction.Enable();
        cameraChangeAction.Enable();
        zoomIn.Enable();
        zoomOut.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        lookAction.Disable();
        fireAction.Disable();
        reloadAction.Disable();
        jumpAction.Disable();
        cameraChangeAction.Disable();
        zoomIn.Disable();
        zoomOut.Disable();
    }
}
