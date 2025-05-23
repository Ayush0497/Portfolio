using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioStartAtPoint : MonoBehaviour
{
	[SerializeField] private float startTime = 0f; // Time (in seconds) where the audio should start

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		if (audioSource.clip != null)
		{
			// Clamp to clip length to avoid issues
			startTime = Mathf.Clamp(startTime, 0f, audioSource.clip.length);

			audioSource.time = startTime;
			audioSource.Play();
		}
		else
		{
			Debug.LogWarning("AudioSource has no clip assigned.", this);
		}
	}
}
