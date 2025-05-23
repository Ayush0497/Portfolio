using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarkers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && this.gameObject.tag == "EllieMarker")
        {
            Events.FoundEllie.Invoke();
        }
        if (other.gameObject.tag == "Player" && this.gameObject.tag == "ZombieMarker")
        {
            Events.FoundZombies.Invoke();
        }
        if (other.gameObject.tag == "Player" && this.gameObject.tag == "map")
        {
            Events.PickedUpMap.Invoke();
        }
        if (other.gameObject.tag == "Player" && this.gameObject.tag == "TreasureMarker")
        {
            Events.FoundBackpack.Invoke();
        }
    }
}
