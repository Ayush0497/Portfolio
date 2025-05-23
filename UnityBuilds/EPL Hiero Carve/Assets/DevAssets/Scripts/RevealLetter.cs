using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RevealLetter : MonoBehaviour
{
	public List<Image> letterSlots;
	[SerializeField] private Sprite newLetterSprite;
	private int revealedIndex = 0;
	private LoadShape shapeLoader; // Reference to LoadShapeList

	private void Start()
	{
		// Find LoadShapeList dynamically if not assigned
		shapeLoader = FindObjectOfType<LoadShape>();
		if (shapeLoader == null)
		{
			Debug.LogError("LoadShapeList component not found in the scene.");
		}
	}

	/// <summary>
	/// Reveals the next letter in sequence and loads the next shape.
	/// </summary>
	public void RevealNextLetter()
	{
		if (revealedIndex >= letterSlots.Count)
		{
			Debug.Log("All letters have been revealed.");
			return;
		}

		Image slot = letterSlots[revealedIndex];
		if (slot != null && slot.transform.childCount > 0)
		{
			TextMeshProUGUI textComponent = slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			if (textComponent != null)
			{
				textComponent.text = slot.sprite.name.Substring(0, 1);
			}
			else
			{
				Debug.LogError("TextMeshProUGUI component not found in child of letter slot.");
			}
		}
		else
		{
			Debug.LogError("Letter slot has no child or is null.");
		}

		if (newLetterSprite != null)
		{
			slot.sprite = newLetterSprite;
		}
		else
		{
			Debug.LogError("New letter sprite is not assigned.");
		}

		revealedIndex++;
	}
}
