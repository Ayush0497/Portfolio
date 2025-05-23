using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class MyEvents
{
    public static UnityEvent<int, string> Swap = new UnityEvent<int, string>();
    public static UnityEvent<string> ChangeChest = new UnityEvent<string>();
}
