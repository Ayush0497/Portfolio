using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
	[SerializeField] private GameObject targetObject;
	[SerializeField] private float fullyOpaqueRange = 1f; // Range in X where the object is fully opaque
	[SerializeField] private float maxDistanceX = 5f; // Maximum distance for transparency adjustment

	private SpriteRenderer spriteRenderer;
	private Color originalColor;
	private GameObject[] triggerObjects;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer != null)
		{
			originalColor = spriteRenderer.color;
		}

		// Find all objects tagged "ThemeChanger"
		triggerObjects = GameObject.FindGameObjectsWithTag("ThemeChanger");
	}

	void Update()
	{
		if (spriteRenderer == null || targetObject == null || triggerObjects == null) return;

		float closestDistanceX = float.MaxValue;

		foreach (var obj in triggerObjects)
		{
			if (obj == null) continue;
			float distanceX = Mathf.Abs(obj.transform.position.x - targetObject.transform.position.x);
			closestDistanceX = Mathf.Min(closestDistanceX, distanceX);
		}

		float alpha = closestDistanceX <= fullyOpaqueRange ? 0 : Mathf.Clamp01((closestDistanceX - fullyOpaqueRange) / (maxDistanceX - fullyOpaqueRange));

		Color newColor = originalColor;
		newColor.a = 1 - alpha;
		spriteRenderer.color = newColor;
	}
}
