using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHp = 100, currentHp = 100;
    [SerializeField] UnityEngine.UI.Image healthBar;
    [SerializeField] GameObject StartLocation;
    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    Mmovement Playerr1;
    Mmovement Playerr2;
    void Start()
    {
        currentHp = maxHp;
        if (Player1 != null)
        {
            Playerr1 = Player1.GetComponent<Mmovement>();
        }
        if (Player2 != null)
        {
            Playerr2 = Player2.GetComponent<Mmovement>();
        }
    }

    public void applyDamage(float value)
    {
        currentHp -= value;
        updateHealthBar();
        if (currentHp <= 0)
        {
            if (Player1 != null)
            {
                
                currentHp = 0;
                Playerr1.DeathLocation = gameObject.transform.position;
                Debug.Log(Playerr1.DeathLocation);
                gameObject.transform.position = StartLocation.transform.position;
                currentHp = 100;
                Playerr1.died();
                updateHealthBar();
            }
            if (Player2 != null)
            {
                currentHp = 0;
                Playerr2.DeathLocation = gameObject.transform.position;
                Debug.Log(Playerr2.DeathLocation);
                gameObject.transform.position = StartLocation.transform.position;
                currentHp = 100;
                Playerr2.died();
                updateHealthBar();
            }
             
        }
    }

    private void updateHealthBar()
    {
        healthBar.transform.localScale = new Vector3(currentHp / maxHp, 1f, 1f);
    }
}
