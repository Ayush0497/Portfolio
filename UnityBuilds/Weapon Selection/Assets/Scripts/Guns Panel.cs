using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunsPanel : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject instructions;
    private bool isPlayerInTrigger;
    private void Start()
    {
        Panel.SetActive(false);
        text.text = "Walk Closer";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            text.text = "Press E to Select Weapon";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Reset when the player exits the trigger
        if (other.CompareTag("Player"))
        {
            instructions.SetActive(true);
            text.text = "Walk Closer";
            isPlayerInTrigger = false;
            Panel.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKey(KeyCode.E))
        {
            instructions.SetActive(false);
            Panel.SetActive(true);
        }
    }
}
