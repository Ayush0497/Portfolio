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

    // Fire rate variables
    [SerializeField] float fireRate = 2.0f; // Seconds between shots
    private float nextFireTime = 0f; // Time when the next shot can be fired

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        movementSpeed = 2.5f;
        textMeshProUGUI.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInfo.piInstance.Level == 0)
        {
            fireRate = 2.0f;
        }
        else if(PlayerInfo.piInstance.Level == 1)
        {
            fireRate = 1.5f;
        }
        else if (PlayerInfo.piInstance.Level == 2)
        {
            fireRate = 1.0f;
        }
        else if (PlayerInfo.piInstance.Level == 3)
        {
            fireRate = 0.5f;
        }
        else if (PlayerInfo.piInstance.Level == 4)
        {
            fireRate = 0.25f;
        }
        else if (PlayerInfo.piInstance.Level == 5)
        {
            fireRate = 0.10f;
        }

        if (PlayerInfo.piInstance.playerHP == 0)
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
            movementSpeed = 4.0f;
        }
        else
        {
            movementSpeed = 2.5f;
        }

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Set the next time you can fire
            shoot();
        }

        if (PlayerInfo.piInstance.playerHP == 0)
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
        GameObject bulletAudio = GameObject.Find("ShootAudioSource");
        bulletAudio.GetComponent<AudioSource>().Play();

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
