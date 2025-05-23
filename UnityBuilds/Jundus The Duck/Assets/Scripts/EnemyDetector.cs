using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDetector : MonoBehaviour
{
	[SerializeField] private GameObject warningPrefab; // Warning indicator prefab
	[SerializeField] private GameObject spawnPrefab; // Enemy prefab to spawn
	[SerializeField] private Transform incomingTriggerArea; // Reference for x position
	[SerializeField] private float moveSpeed = 2f; // Speed of spawned object
	[SerializeField] private float warningDuration = 3f; // Adjustable timer for warning duration
	private List<float> recordedYPositions = new List<float>();

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("FlyTrigger"))
		{
			float yPosition = other.transform.position.y;
			recordedYPositions.Add(yPosition);
            //Destroy(other.gameObject); // Delete the FlyTrigger object
            SpriteRenderer spriteRenderer = other.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            StartCoroutine(HandleIncomingWarning(yPosition));
		}
	}

	private IEnumerator HandleIncomingWarning(float yPosition)
	{
		Vector3 warningPosition = new Vector3(incomingTriggerArea.position.x, yPosition, transform.position.z);
		GameObject warningInstance = Instantiate(warningPrefab, warningPosition, Quaternion.identity);

		// Attach warningInstance to incomingTriggerArea as a child
		warningInstance.transform.SetParent(incomingTriggerArea);

		yield return new WaitForSeconds(warningDuration);

		Destroy(warningInstance);
		SpawnEnemy(yPosition);
	}

	private void SpawnEnemy(float yPosition)
	{
		GameObject spawnedObject = Instantiate(spawnPrefab, new Vector3(transform.position.x, yPosition, transform.position.z), Quaternion.identity);
		spawnedObject.AddComponent<MoveLeft>().SetSpeed(moveSpeed);
	}
}

public class MoveLeft : MonoBehaviour
{
	private float speed;

	public void SetSpeed(float newSpeed)
	{
		speed = newSpeed;
	}

	private void Update()
	{
		transform.position += Vector3.left * speed * Time.deltaTime;
	}
}