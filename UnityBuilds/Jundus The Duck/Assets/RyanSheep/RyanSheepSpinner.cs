using UnityEngine;

public class RyanSheepSpinner : MonoBehaviour
{
	[SerializeField] private float speed = 100f; // Rotation speed in degrees per second
	[SerializeField] private bool clockwise = true; // Toggle for rotation direction

	void Update()
	{
		float direction = clockwise ? -1f : 1f; // Negative = clockwise in Unity's 2D space
		transform.Rotate(0f, 0f, direction * speed * Time.deltaTime);
	}
}
