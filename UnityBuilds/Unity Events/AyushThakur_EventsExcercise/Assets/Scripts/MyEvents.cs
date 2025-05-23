using UnityEngine;
using UnityEngine.Events;
using System.Collections;

static public class MyEvents
{
    static public UnityEvent spawnCoins = new UnityEvent();
    static public UnityEvent<int> updateScore = new UnityEvent<int>();
    static public UnityEvent disableInput = new UnityEvent();
    static public UnityEvent enableInput = new UnityEvent();
}
