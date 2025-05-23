using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    static public UnityEvent OpenDoor = new();
    static public UnityEvent phaseThrough = new();
    static public UnityEvent CapturedBlueSpy = new();
    static public UnityEvent CapturedRedSpy = new();
}
