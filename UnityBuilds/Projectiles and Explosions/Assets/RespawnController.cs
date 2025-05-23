using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
	public GameObject Player;
	public GameObject respawnPoint;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Player.transform.position = respawnPoint.transform.position;
		}
	}

}
