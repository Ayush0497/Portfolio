using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI hudLabel;
    public Grenade grenade;
    public LaunchProjectile launchProjectile;

    void Start()
    {
        hudLabel = GameObject.Find("HUD").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        hudLabel.text = "Initial Position:"+ launchProjectile.initialpositionX +","+launchProjectile.initialpositionY 
            +"\nLaunch Velocity X: " + Math.Round(launchProjectile.velocityX,6) + 
            "\nLaunch Velocity Y: " + Math.Round(launchProjectile.velocityY,6) + 
            "\nTime Between Launch & Collision on X: " + Math.Round(grenade.timeBetweenLaunchAndCollisionX,6) + 
            "\nTime Between Launch & Collision on Y: " + Math.Round(grenade.timeBetweenLaunchAndCollisionY,6)
            + "\nExpected Final Position on X: " + Math.Round(launchProjectile.velocityX * grenade.timeBetweenLaunchAndCollisionX + launchProjectile.initialpositionX,6) + 
            "\nExpected Final Position on Y: " + Math.Round((launchProjectile.velocityY * grenade.timeBetweenLaunchAndCollisionY + 0.5 * (Physics2D.gravity.y * Math.Pow(grenade.timeBetweenLaunchAndCollisionY, 2)))+launchProjectile.initialpositionY,6);
    }
}
