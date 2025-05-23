using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraMovement : MonoBehaviour
{
    public GameObject playerCharacter;
	void Update()
    {
		if(playerCharacter != null)
		{
            transform.position = new Vector3(playerCharacter.transform.position.x, playerCharacter.transform.position.y, -10);
        }	
	}
}
