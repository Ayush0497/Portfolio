using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mmovement : MonoBehaviour
{
    [SerializeField] Mmovement anotherPlayer;
    [SerializeField] InputAction movement;
    [SerializeField] Vector2 playerMovement;

    [SerializeField] InputAction lookAction;
    [SerializeField] Vector2 lookTarget;

    [SerializeField] InputAction jumpAction;
    Rigidbody rbody;
    [SerializeField] bool grounded = true;

    [SerializeField] float mouseSensitivity = 200.0f;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] GameObject cam;

    [SerializeField] InputAction fireAction;

    [SerializeField] GameObject WinPanel;
    public GameObject FlagPanel;

    [SerializeField] GameObject Pistol;
    [SerializeField] GameObject SMG;
    [SerializeField] GameObject Rifle;

    [SerializeField] GameObject SmgOnGround;
    [SerializeField] GameObject RifleOnGround;
    [SerializeField] GameObject PistolOnGround;

    [SerializeField] int Score;
    [SerializeField] TextMeshProUGUI ScoreText;
    bool UpdateScore = false;
    public Vector3 DeathLocation;


    private float maxVerticalAngle = 30f;

    private float verticalRotation = 0f;

    private void Start()
    {
        Cursor.visible = false;
        rbody = GetComponent<Rigidbody>();
        WinPanel.SetActive(false);
        FlagPanel.SetActive(false);
        Pistol.SetActive(true);
        Rifle.SetActive(false);
        SMG.SetActive(false);
    }

    private void Update()
    {
        playerMovement = movement.ReadValue<Vector2>();
        lookTarget = lookAction.ReadValue<Vector2>();

        transform.Rotate(Vector3.up, mouseSensitivity * Time.deltaTime * lookTarget.x);

        verticalRotation -= mouseSensitivity * Time.deltaTime * lookTarget.y;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);
        if(jumpAction.ReadValue<float>() == 1.0 && grounded)
        {
            rbody.AddRelativeForce(Vector3.up * 3, ForceMode.Impulse);
            grounded = false;
            StartCoroutine("ChangeGounded");
        }
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        if (fireAction.ReadValue<float>() == 1)
        {
            BroadcastMessage("fire", SendMessageOptions.DontRequireReceiver);
        }
        ScoreText.text = $"Score: {Score}";       
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

    void OnEnable()
    {
        movement.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        fireAction.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        fireAction.Disable();
    }

    private IEnumerator ChangeGounded()
    {
        yield return new WaitForSeconds(0.7f);
        grounded = true;
    }

    public void FlagInteraction()
    {
        FlagPanel.SetActive(true);
        UpdateScore = true;
        StartCoroutine(UpdateScoreWhileFlag());
    }

    private IEnumerator UpdateScoreWhileFlag()
    {
        while (UpdateScore)
        {
            Score++;
            yield return new WaitForSeconds(1f);
            if(Score==50)
            {
                UpdateScore = false;
                WinPanel.SetActive(true);
                movement.Disable();
                lookAction.Disable();
                jumpAction.Disable();
                fireAction.Disable();
                anotherPlayer.lookAction.Disable();
                anotherPlayer.fireAction.Disable();
                anotherPlayer.jumpAction.Disable();
                anotherPlayer.movement.Disable();
            }
        }
    }

    public void died()
    {
        UpdateScore = false;
        if(FlagPanel.activeInHierarchy)
        { 
            anotherPlayer.FlagInteraction();
            FlagPanel.SetActive(false);
        }        
        if (Pistol.activeInHierarchy)
        {
            PistolOnGround.transform.position = DeathLocation;
            PistolOnGround.SetActive(true);            
        }
        
        if (SMG.activeInHierarchy)
        {
            SmgOnGround.transform.position = DeathLocation;
            SmgOnGround.SetActive(true);
            SMG.SetActive(false);
        }
        if(Rifle.activeInHierarchy)
        {
            RifleOnGround.transform.position = DeathLocation;
            RifleOnGround.SetActive(true);
            Rifle.SetActive(false);
        }
        Pistol.SetActive(true);
    }

    public void ActivateRifle()
    {
        if(SMG.activeInHierarchy)
        {
            SmgOnGround.transform.position = transform.position;
            SmgOnGround.SetActive(true);
            SmgOnGround.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(ActivateCollider(SmgOnGround));
            SMG.SetActive(false);
        }
        if(Pistol.activeInHierarchy)
        {
            PistolOnGround.transform.position = transform.position;
            PistolOnGround.SetActive(true);
            PistolOnGround.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(ActivateCollider(PistolOnGround));
            Pistol.SetActive(false);
        }
        Rifle.SetActive(true);
    }

    public void ActivateSMG()
    {
        if (Pistol.activeInHierarchy)
        {
            PistolOnGround.transform.position = transform.position;
            PistolOnGround.SetActive(true);
            PistolOnGround.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(ActivateCollider(PistolOnGround));
            Pistol.SetActive(false);
        }
        if (Rifle.activeInHierarchy)
        {
            RifleOnGround.transform.position = transform.position;
            RifleOnGround.SetActive(true);
            RifleOnGround.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(ActivateCollider(RifleOnGround));
            Rifle.SetActive(false);
        }
        SMG.SetActive(true);
    }

    public void ActivatePistol()
    {
        if (SMG.activeInHierarchy)
        {
            SmgOnGround.transform.position = transform.position;
            SmgOnGround.SetActive(true);
            SmgOnGround.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(ActivateCollider(SmgOnGround));
            SMG.SetActive(false);
        }
        if (Rifle.activeInHierarchy)
        {
            RifleOnGround.transform.position = transform.position;
            RifleOnGround.SetActive(true);
            RifleOnGround.GetComponent<SphereCollider>().enabled = false;
            StartCoroutine(ActivateCollider(RifleOnGround));
            
            Rifle.SetActive(false);
        }
        Pistol.SetActive(true);
    }

    IEnumerator ActivateCollider(GameObject a)
    {
        yield return new WaitForSeconds(3);
        a.GetComponent<SphereCollider>().enabled = true;
    }
}
