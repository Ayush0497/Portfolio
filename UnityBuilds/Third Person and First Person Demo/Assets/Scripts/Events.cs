using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

static public class Events
{
    static public UnityEvent FoundEllie = new();
    static public UnityEvent FoundZombies = new();
    static public UnityEvent KilledZombies = new();
    static public UnityEvent PickedUpMap = new();
    static public UnityEvent FoundBackpack = new();
    static public UnityEvent<int> ReloadStuff = new();
}
