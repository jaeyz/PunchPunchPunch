using UnityEngine;
using System.Collections;

public class CountdownManager : MonoBehaviour {
	
	public bool isEnemy = true;

	private bool beginCountdown = false;
	private bool getUp = false;

	public int tapCount = 0;

	private float timer = 0;

	private static CountdownManager countdownManager;
	public static CountdownManager Instance {
		get {
			if (countdownManager == null)
				countdownManager = (CountdownManager) GameObject.FindObjectOfType(typeof(CountdownManager));
			return countdownManager;
		}
	}

	void Update() {
		if (beginCountdown) {
			timer += Time.deltaTime;
			if (timer < 10) {
				if (Input.GetKeyUp (KeyCode.G)) {
					tapCount ++;
					if (!getUp) {
						getUp = true;
						GameManager.Instance.Getup(isEnemy);
					}
				} if (tapCount >= 30) {
					tapCount = 0;
					timer = 0;
					StopCountdown();
					getUp = false;
					GameManager.Instance.Rebox(isEnemy);
				}
			} else {
				if (getUp) 
					GameManager.Instance.GameOver(isEnemy);
				Debug.LogError("Game Finished");
			}
		}
	}

	public void StartCountdown() {
		StartCoroutine (BeginSounds ());
	}

	public void StopCountdown() {
		beginCountdown = false;
		SoundManager.Instance.StopSound ();
	}

	IEnumerator BeginSounds() {
		yield return new WaitForSeconds (2f);
		SoundManager.Instance.PlaySound (Sounds.BELL);
		yield return new WaitForSeconds (1f);
		beginCountdown = true;
		SoundManager.Instance.PlaySound (Sounds.COUNTDOWN);
	}

}
