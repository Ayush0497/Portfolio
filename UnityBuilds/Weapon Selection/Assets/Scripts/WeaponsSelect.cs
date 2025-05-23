using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSelect : MonoBehaviour
{
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject Rifle;
    
    public void SelectPistol()
    {
        pistol.SetActive(true);
        shotgun.SetActive(false);
        Rifle.SetActive(false);
    }

    public void SelectShotgun()
    {
        pistol.SetActive(false);
        shotgun.SetActive(true);
        Rifle.SetActive(false);
    }
    public void SelectRifle()
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        Rifle.SetActive(true);
    }
}
