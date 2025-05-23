using UnityEngine;

public class CinemaCanvas : MonoBehaviour
{
	[Header("Panels")]
	[SerializeField] private RectTransform topPanel;
	[SerializeField] private RectTransform bottomPanel;

	[Header("Animation Settings")]
	[SerializeField] private float moveDistance = 200f; // Distance to move each panel
	[SerializeField] private float moveSpeed = 500f; // Speed of movement

	private Vector2 topTarget;
	private Vector2 bottomTarget;
	private bool activated = false;

	private void Start()
	{
		if (!activated)
		{
			// Set target positions
			topTarget = topPanel.anchoredPosition + new Vector2(0, moveDistance);
			bottomTarget = bottomPanel.anchoredPosition + new Vector2(0, -moveDistance);
			activated = true;
		}
	}

	private void Update()
	{
		if (!activated) return;

		topPanel.anchoredPosition = Vector2.MoveTowards(topPanel.anchoredPosition, topTarget, moveSpeed * Time.deltaTime);
		bottomPanel.anchoredPosition = Vector2.MoveTowards(bottomPanel.anchoredPosition, bottomTarget, moveSpeed * Time.deltaTime);
	}

	private void OnDrawGizmosSelected()
	{
		if (topPanel != null && bottomPanel != null)
		{
			Gizmos.color = Color.red;

			Vector3 topTargetWorld = topPanel.transform.position + Vector3.up * moveDistance;
			Vector3 bottomTargetWorld = bottomPanel.transform.position + Vector3.down * moveDistance;

			// Draw spheres to indicate where the panels will go
			Gizmos.DrawSphere(topTargetWorld, 10f);
			Gizmos.DrawSphere(bottomTargetWorld, 10f);

			// Draw lines from current to target positions
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(topPanel.transform.position, topTargetWorld);
			Gizmos.DrawLine(bottomPanel.transform.position, bottomTargetWorld);
		}
	}
}
