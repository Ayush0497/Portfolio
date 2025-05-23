using UnityEngine;

public class ThemeChange : MonoBehaviour
{
	[SerializeField] private GameObject rightExitActiveObject;
	[SerializeField] private GameObject leftExitActiveObject;
	[SerializeField] private GameObject[] inactiveObjects;

	private Vector3 objectPosition;

	private void Start()
	{
		objectPosition = transform.position;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.position.x > objectPosition.x)
		{
			SetActiveTheme(rightExitActiveObject);
		}
		else if (other.transform.position.x < objectPosition.x)
		{
			SetActiveTheme(leftExitActiveObject);
		}
	}

	private void SetActiveTheme(GameObject activeObject)
	{
		if (activeObject != null)
		{
			activeObject.SetActive(true);
		}

		foreach (GameObject obj in inactiveObjects)
		{
			if (obj != null && obj != activeObject)
			{
				obj.SetActive(false);
			}
		}
	}
}