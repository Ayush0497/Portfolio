using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    float vInput, hInput, movementSpeed;
    [SerializeField]
    GameObject model;
    [SerializeField]
    Image fogPanel;
    [SerializeField]
    bool fade, fadeOn;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject barrelLocation;
    [SerializeField] float bulletForce;
    [SerializeField] GameObject SaveExit;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        movementSpeed = 1.0f;
        textMeshProUGUI.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInfo.piInstance.playerHP == 0)
        {
            SaveExit.SetActive(false);
        }
        if (!fade)
        {
            vInput = Input.GetAxis("Vertical");
            hInput = Input.GetAxis("Horizontal");
        }
        else
        {
            vInput = 0;
            hInput = 0;
            if (fadeOn && fogPanel.color.a < 1)
            {
                fogPanel.color = new Color(fogPanel.color.r, fogPanel.color.g, fogPanel.color.b, fogPanel.color.a + Time.deltaTime);
                textMeshProUGUI.text = "Teleporting...";
                if (!particle.isPlaying)
                {
                    particle.Play();
                }
            }
            else if (!fadeOn && fogPanel.color.a > 0)
            {
                fogPanel.color = new Color(fogPanel.color.r, fogPanel.color.g, fogPanel.color.b, fogPanel.color.a - Time.deltaTime);
                textMeshProUGUI.text = "Teleporting...";
                if (!particle.isPlaying)
                {
                    particle.Play();
                }
            }
            else
            {
                fade = false;
                textMeshProUGUI.text = " ";
                if (particle.isPlaying)
                {
                    particle.Stop();
                }
            }
        }

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        transform.Translate(new Vector3(hInput * movementSpeed * Time.deltaTime, 0, vInput * movementSpeed * Time.deltaTime));
        transform.rotation = Quaternion.identity;


        if (hInput == 0 && vInput == 0)
        {
            anim.SetBool("Walk", false);
        }
        else
        {
            model.transform.LookAt(transform.position + new Vector3(hInput, 0, vInput).normalized);
            anim.SetBool("Walk", true);

            anim.speed = movementSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 2.0f;
        }
        else
        {
            movementSpeed = 1.0f;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
            Debug.Log("Shoot");
        }

        if(PlayerInfo.piInstance.playerHP == 0)
        {
            PlayerInfo.piInstance.LoadGame();
            SceneManager.LoadScene("GameOver");       
        }
    }

    public void Fade(bool on)
    {
        fade = true;
        fadeOn = on;
    }

    public bool Fading()
    {
        return fade;
    }

    private void shoot()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrelLocation.transform.position, barrelLocation.transform.rotation);
        Rigidbody rb = spawnedBullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(barrelLocation.transform.forward * bulletForce, ForceMode.Impulse);
        }
        StartCoroutine(DestroyAfter1Seconds(spawnedBullet));
    }

    private IEnumerator DestroyAfter1Seconds(GameObject bullet)
    {
        yield return new WaitForSeconds(1);
        Destroy(bullet);
    }
}
