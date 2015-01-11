using UnityEngine;
using System.Collections;

public class CountdownManager : MonoBehaviour {

	private const int TIME_LIMIT = 10; 

	public bool isEnemy = true;

	private bool beginCountdown = false;
	private bool getUp = false;

	public int tapCount = 0;

	private float timer = 0;

	private float enemyTimer = 80;
	private float playerTimer = 30;

	private float randomValue = 0.5f;

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
			float tapLimit = 0;
			if (isEnemy)
				tapLimit = enemyTimer;
			else
				tapLimit = playerTimer;
			if (timer < TIME_LIMIT) {
				if (!isEnemy) {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
					if (Input.GetMouseButtonUp(0)) {
						tapCount ++;
						if (!getUp) {
							getUp = true;
							GameManager.Instance.Getup(isEnemy);
						}
					}
#else
					if (Input.GetKeyUp (KeyCode.G)) {
						tapCount ++;
						if (!getUp) {
							getUp = true;
							GameManager.Instance.Getup(isEnemy);
						}
					}
#endif
				} else if (isEnemy) {
					if (Random.value >= randomValue)
						tapCount++;
					if (!getUp) {
						getUp = true;
						GameManager.Instance.Getup(isEnemy);
					}
				}
				if (tapCount >= tapLimit) {
					tapCount = 0;
					timer = 0;
					StopCountdown();
					getUp = false;
					GameManager.Instance.Rebox(isEnemy);
				}
			} else {
				StopCountdown();
				GameManager.Instance.GameOver(isEnemy);
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
		yield return null;
		beginCountdown = true;
		SoundManager.Instance.PlaySound (Sounds.COUNTDOWN);
	}

	public void UpdateTimer (bool isEnemy) {
		if (isEnemy) {
			randomValue += 0.2f;
		} else
			playerTimer += 30;
	}

}
