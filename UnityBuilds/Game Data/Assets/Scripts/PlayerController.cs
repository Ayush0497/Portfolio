﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    float vInput, hInput, movementSpeed, rotationSpeed;
    [SerializeField]
    GameObject model;
    [SerializeField]
    Image fogPanel;
    [SerializeField]
    bool fade, fadeOn;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] ParticleSystem particle;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        movementSpeed = 1.0f;
        rotationSpeed = 100.0f;
        textMeshProUGUI.text = " ";
    }

    // Update is called once per frame
    void Update()
    {
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
                if(!particle.isPlaying)
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
}
