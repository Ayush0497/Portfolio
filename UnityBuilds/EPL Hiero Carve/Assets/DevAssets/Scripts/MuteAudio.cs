using UnityEngine;
using UnityEngine.UI;

public class MuteAudio : MonoBehaviour
{
	private bool isMuted = false;
	[SerializeField] AudioSource audioSource;

	[SerializeField] private Button[] muteButtons; // Reference to the UI button image
	[SerializeField] private Sprite mutedSprite;    // Sprite when muted
	[SerializeField] private Sprite unmutedSprite;  // Sprite when unmuted

	private void Start()
	{
        UpdateButtonSprite();
	}

	/// <summary>
	/// Toggles audio mute state and updates button image.
	/// </summary>
	public void ToggleMute()
	{
        isMuted = audioSource.mute;
        isMuted = !isMuted;
		audioSource.mute = isMuted;
		UpdateButtonSprite();
	}

	/// <summary>
	/// Updates the button image based on mute state.
	/// </summary>
	private void UpdateButtonSprite()
	{
		if (muteButtons != null)
		{
			for(int i = 0; i < muteButtons.Length; i++)
			{
				muteButtons[i].image.sprite = isMuted ? mutedSprite : unmutedSprite;
            }
		}
	}
}
