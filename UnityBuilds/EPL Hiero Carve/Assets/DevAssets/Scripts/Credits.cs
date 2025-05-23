using UnityEngine;
using TMPro;

public class EndCredits : MonoBehaviour
{
	[System.Serializable]
	public class CreditGroup
	{
		public string title;
		[TextArea(3, 10)]
		public string names;
	}

	[Header("UI Text Components")]
	[SerializeField] private TextMeshProUGUI titleText;
	[SerializeField] private TextMeshProUGUI nameText;

	[Header("Credit Content")]
	[SerializeField] private CreditGroup[] creditGroups;

	[Header("Timing")]
	private float displayDuration = 3f;

	private int currentIndex = 0;
	private float timer = 0f;

	private void OnEnable()
	{
		currentIndex = 0;
		timer = 0f;
		SetCredit(currentIndex);
	}

	private void Update()
	{
		if (creditGroups.Length == 0) return;

		timer += Time.deltaTime;
		if (timer >= displayDuration)
		{
			timer = 0f;
			currentIndex = (currentIndex + 1) % creditGroups.Length;
			SetCredit(currentIndex);
		}
	}

	private void SetCredit(int index)
	{
		titleText.text = creditGroups[index].title;
		nameText.text = creditGroups[index].names;
	}
}
