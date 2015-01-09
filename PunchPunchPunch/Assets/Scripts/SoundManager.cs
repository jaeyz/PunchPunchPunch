using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	[SerializeField]
	private AudioClip punchClip;

	[SerializeField]
	private AudioClip missClip;

	[SerializeField]
	private AudioClip blockClip;

	[SerializeField]
	private AudioClip bellClip;

	[SerializeField]
	private AudioClip countdownClip;

	private static SoundManager soundManager;
	public static SoundManager Instance {
		get {
			if (soundManager == null)
				soundManager = (SoundManager) GameObject.FindObjectOfType(typeof(SoundManager));
			return soundManager;
		}
	}

	void Awake() {
		DontDestroyOnLoad (this);
	}

	public void PlaySound(Sounds s) {
		switch (s) {
		case Sounds.PUNCH:
			audio.clip = punchClip;
			break;
		case Sounds.BLOCK:
			//audio.PlayOneShot(punchClip);
			break;
		case Sounds.DODGE:
			audio.clip = missClip;
			break;
		case Sounds.BELL:
			audio.clip = bellClip;
			break;
		case Sounds.COUNTDOWN:
			audio.clip = countdownClip;
			break;
		}
		audio.Play ();
	}

	public void PlayOnce(Sounds s) {
		switch (s) {
		case Sounds.PUNCH:
			audio.PlayOneShot(punchClip);
			break;
		case Sounds.BLOCK:
			//audio.PlayOneShot(punchClip);
			break;
		case Sounds.DODGE:
			audio.PlayOneShot(missClip);
			break;
		case Sounds.BELL:
			audio.PlayOneShot(bellClip);
			break;
		case Sounds.COUNTDOWN:
			audio.PlayOneShot(countdownClip);
			break;
		}
	}

	public void StopSound() {
		if (audio.isPlaying)
			audio.Stop();
	}
}
