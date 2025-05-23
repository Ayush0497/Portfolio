using UnityEngine;

public class Spinner : MonoBehaviour
{
	public enum Axis { X, Y, Z }
	public Axis rotationAxis = Axis.Y; // Default rotation axis
	public float rotationSpeed = 50f;

	void Update()
	{
		Vector3 rotationVector = Vector3.zero;

		switch (rotationAxis)
		{
			case Axis.X:
				rotationVector = new Vector3(rotationSpeed * Time.deltaTime, 0, 0);
				break;
			case Axis.Y:
				rotationVector = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
				break;
			case Axis.Z:
				rotationVector = new Vector3(0, 0, rotationSpeed * Time.deltaTime);
				break;
		}

		transform.Rotate(rotationVector);
	}
}
