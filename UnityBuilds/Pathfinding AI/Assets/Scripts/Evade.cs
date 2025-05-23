using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] Pathfinder blueSpy;
    [SerializeField] RedSpy redSpy;
    private void Start()
    {
        blueSpy.mystate = Pathfinder.state.RoamingAround;
        redSpy.mystate = RedSpy.state.RoamingAround;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Guard" && this.gameObject.tag == "red")
        {
            redSpy.mystate = RedSpy.state.Evading;
            SendMessageUpwards("run");
            StartCoroutine(ResetRed());
        }
        if (other.gameObject.tag == "Guard" && this.gameObject.tag == "blue")
        {
            blueSpy.mystate = Pathfinder.state.Evading;
            SendMessageUpwards("run");
            StartCoroutine(ResetBlue());
        }
    }

    IEnumerator ResetRed()
    {
        yield return new WaitForSeconds(1f);
        redSpy.mystate = RedSpy.state.RoamingAround;
    }
    IEnumerator ResetBlue()
    {
        yield return new WaitForSeconds(1f);
        blueSpy.mystate = Pathfinder.state.RoamingAround;
    }
}
