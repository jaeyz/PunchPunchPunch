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

	[SerializeField]
	private AudioClip mainClip;

	[SerializeField]
	private AudioClip subMenuClip;

	[SerializeField]
	private AudioClip clickClip;

	[SerializeField]
	private AudioClip fallGroundCollisionClip;

	[SerializeField]
	private AudioClip inGameClip;

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

	public void PlaySound(Sounds s, bool loop = false) {
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
		case Sounds.MAIN_CLIP:
			audio.clip = mainClip;
			break;
		case Sounds.SUB_MENU_CLIP:
			audio.clip = subMenuClip;
			break;
		case Sounds.IN_GAME:
			audio.clip = inGameClip;
			break;
		}
		audio.loop = loop;
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
		case Sounds.CLICK:
			audio.PlayOneShot(clickClip);
			break;
		case Sounds.GROUND:
			audio.PlayOneShot(fallGroundCollisionClip);
			break;
		}
	}

	public void StopSound() {
		if (audio.isPlaying)
			audio.Stop();
	}

	public void SetVolume(float t) {
		audio.volume = t;
	}
}
