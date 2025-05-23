using UnityEngine;
using UnityEngine.Playables;

public class TimelineAftermath : MonoBehaviour
{
	public PlayableDirector timeline;
	public GameObject objectToActivate;

	void Start()
	{
		if (timeline != null)
		{
			timeline.stopped += OnTimelineFinished;
		}

		// Ensure the target object starts inactive
		if (objectToActivate != null)
		{
			objectToActivate.SetActive(false);
		}
	}

	void OnTimelineFinished(PlayableDirector director)
	{
		if (objectToActivate != null)
		{
			objectToActivate.SetActive(true);
		}
	}
}
