using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FadeOutPanel : MonoBehaviour
{
	[SerializeField] private float fadeDuration = 1f;

	private Image panelImage;
	private float fadeTimer = 0f;
	private Color originalColor;
	private bool fading = true;

	private void Awake()
	{
		panelImage = GetComponent<Image>();
		originalColor = panelImage.color;
	}

	private void Start()
	{
		// Start fading on Start
		fadeTimer = 0f;
		fading = true;
	}

	private void Update()
	{
		if (!fading) return;

		fadeTimer += Time.deltaTime;
		float alpha = Mathf.Lerp(originalColor.a, 0f, fadeTimer / fadeDuration);

		panelImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

		if (fadeTimer >= fadeDuration)
		{
			fading = false;
			panelImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f); // Ensure fully transparent
		}
	}
}
