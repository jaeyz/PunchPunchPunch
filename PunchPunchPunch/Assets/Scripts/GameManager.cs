using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private const float HEALTH_DEFAULT_VALUE = 30;

	[SerializeField]
	private GameObject koGameObject;

	[SerializeField]
	private GameObject itsOverGameObject;

	[SerializeField]
	private GameObject youLoseGameObject;

	[SerializeField]
	private GameObject youWinGameObject;

	public Animator player;
	public Animator enemy;
	
	private List<EnemyType> enemyTypes = new List<EnemyType>();
	private int enemyIndex = 0;

	public float playerHealth = 10f;
	public float enemyHealth = 10f;
	private float playerHealthHolder;
	private float enemyHealthHolder;

	public BoxerState playerState;
	public BoxerState enemyState;

	public bool hasKO = false;

	private float playerHealthMultiplier = 0.7f;
	private float enemyHealthMultiplier = 0.7f;

	private static GameManager gameManager;
	public static GameManager Instance {
		get {
			if (gameManager == null)
				gameManager = (GameManager) GameObject.FindObjectOfType(typeof(GameManager));
			return gameManager;
		}
	}

	void Start() {
		enemyTypes = Enum.GetValues(typeof(EnemyType)).OfType<EnemyType>().ToList();
		SetLevel ();
		playerHealthHolder = playerHealth;
		enemyHealthHolder = enemyHealth;
		UpdateHealth ();
	}

	void SetLevel() {
		EnemyType eType = EnemyType.EASY;
		switch (PlayerPrefs.GetString("Settings")) {
		case "Easy":
			eType = EnemyType.EASY;
			break;
		case "Medium":
			eType = EnemyType.NORMAL;
			break;
		case "Hard":
			eType = EnemyType.HARD;
			break;
		case "Extreme":
			eType = EnemyType.EXTREME;
			break;
		}
		EnemyController.Instance.UpdateEnemyType(eType);
	}

	public EnemyType CurrentEnemyType {
		get {
			return enemyTypes[enemyIndex];
		}
	}
	
	public void Damage(Boxers boxer, BoxerState boxerState) {
		if (AllowDamage(boxer)) {
			if (boxerState.ToString ().ToUpper().Contains ("JAB")) {
				if (boxer == Boxers.PLAYER)
					playerHealth -= 1;
				else
					enemyHealth -= 1;
			} else if (boxerState.ToString().ToUpper().Contains("HOOK")) {
				if (boxer == Boxers.PLAYER)
					playerHealth -= 2;
				else
					enemyHealth -= 2;
			} else if (boxerState.ToString().ToUpper().Contains("UPPERCUT")) {
				if (boxer == Boxers.PLAYER)
					playerHealth -= 3;
				else
					enemyHealth -= 3;
			}
			UpdateHealth();
			StartCoroutine(Delay(boxer, boxerState));
		}
		if (boxer == Boxers.ENEMY) 
			StartCoroutine(Reset());
	}

	private IEnumerator Reset() {
		yield return new WaitForSeconds(0.35f);
		PlayerController.Instance.Reset();
	}

	private void CheckAnimation(Animator anim) {
		StartCoroutine(CheckAnimCor(anim));
	}

	IEnumerator Delay(Boxers boxer, BoxerState boxerState) {
		Animator animatorHolder = null;

		if (boxer == Boxers.PLAYER)
			animatorHolder = player;
		else
			animatorHolder = enemy;

		yield return new WaitForSeconds(0.35f);

		if (boxer == Boxers.PLAYER) {
			if (boxerState.ToString().ToUpper().Contains("RIGHT")) {
				StartCoroutine(AnimateLeftBoom());
			} else if (boxerState.ToString().ToUpper().Contains("LEFT")) {
				StartCoroutine(AnimateRightBoom());
			}
		} else if (boxer == Boxers.ENEMY) {
			if (boxerState.ToString().ToUpper().Contains("RIGHT")) {
				StartCoroutine(AnimateRightBoom());
			} else if (boxerState.ToString().ToUpper().Contains("LEFT")) {
				StartCoroutine(AnimateLeftBoom());
			}
		}

		SoundManager.Instance.PlayOnce (Sounds.PUNCH);

		if (enemyHealth <= 0 || playerHealth <= 0) {
			if (enemyHealth <= 0) {
				enemy.SetBool("KnockoutBool", true);
				EnemyController.Instance.isDead = true;
				CountdownManager.Instance.isEnemy = true;
			} else if (playerHealth <= 0) {
				player.SetBool("KnockoutBool", true);
				CountdownManager.Instance.isEnemy = false;
			}
			hasKO = true;
			StartCoroutine(ShowKO());
		} else {
			//EnemyController.Instance.Counter -= 2f;
			if (boxerState.ToString().ToUpper().Contains("UPPERCUT")) {
				animatorHolder.SetBool("DamageUppercut", true);
			} else {
				if (boxerState == BoxerState.LEFT_JAB_ATTACK || boxerState == BoxerState.LEFT_HOOK_ATTACK) {
					animatorHolder.SetBool("DamageRight", true);
				} else if (boxerState == BoxerState.RIGHT_JAB_ATTACK || boxerState == BoxerState.RIGHT_HOOK_ATTACK)
					animatorHolder.SetBool("DamageLeft", true);
			}
			CheckAnimation(animatorHolder);
		}
	}
	
	 IEnumerator CheckAnimCor(Animator anim) {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		while (stateInfo.nameHash == Animator.StringToHash("Base Layer.IdleState")) {
			stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			yield return null;
		}
		yield return StartCoroutine(Wait(stateInfo.length, anim));
	}
	
	IEnumerator Wait(float t, Animator anim) {
		yield return null;
		if (anim != null) {
			anim.SetBool("DamageUppercut", false);
			anim.SetBool("DamageRight", false);
			anim.SetBool("DamageLeft", false);
			anim.SetBool("KnockoutBool", false);
		}
	}

	private int EnemyHash {
		get {
			AnimatorStateInfo stateInfo = enemy.GetCurrentAnimatorStateInfo(0);
			return stateInfo.nameHash;
		}
	}

	private void Reset (Animator anim) {
		if (anim != null) {
			anim.SetBool("DamageUppercut", false);
			anim.SetBool("DamageRight", false);
			anim.SetBool("DamageLeft", false);
			anim.SetBool("KnockoutBool", false);
		}
	}

	private bool AllowDamage(Boxers boxer) {
		if (boxer == Boxers.PLAYER) {
			if (enemyState.ToString().ToUpper().Contains("ATTACK")) {
				if (playerState == BoxerState.BLOCK)
					return false;
				if (enemyState.ToString().ToUpper().Contains("LEFT")) {
					if (playerState == BoxerState.LEFT_DODGE)
						return false;
				}
				if (enemyState.ToString().ToUpper().Contains("RIGHT")) {
					if (playerState == BoxerState.RIGHT_DODGE)
						return false;
				}
			} else {
				return false;
			}
		} else if (boxer == Boxers.ENEMY) {
			if (playerState.ToString().ToUpper().Contains("ATTACK")) {
				if (enemyState == BoxerState.BLOCK) 
					return false;
				if (playerState.ToString().ToUpper().Contains("LEFT")) {
					if (enemyState == BoxerState.LEFT_DODGE)
						return false;
				}
				if (playerState.ToString().ToUpper().Contains("RIGHT")) {
					if (enemyState == BoxerState.RIGHT_DODGE)
						return false;
				}
			} else {
				return false;
			}
		}
		return true;
	}

	private void StartCountdown() {
		CountdownManager.Instance.StartCountdown ();
	}

	public void Getup(bool isEnemy) {
		if (isEnemy) {
			enemy.SetBool("GetUpBool", true);
		} else {
			player.SetBool("GetUpBool", true);
		}
	}

	public void Rebox(bool isEnemy) {
		SoundManager.Instance.PlaySound (Sounds.IN_GAME);
		if (isEnemy) {
			enemy.SetBool("GetupBoolOk", true);
			enemyHealthMultiplier -= 0.2f;
			float lifeValue = HEALTH_DEFAULT_VALUE * enemyHealthMultiplier;
			enemyHealth = lifeValue;
		} else {
			player.SetBool("GetupBoolOk", true);
			playerHealthMultiplier -= 0.2f;
			float lifeValue = HEALTH_DEFAULT_VALUE * playerHealthMultiplier;
			playerHealth = lifeValue;
		}
		CountdownManager.Instance.UpdateTimer(isEnemy);
		UpdateHealth ();
		Reset (isEnemy);
	}

	public void GameOver(bool isEnemy) {
		if (isEnemy) {
			enemy.SetBool("DeadBool", true);
		} else {
			player.SetBool("DeadBool", true);
		}
		StartCoroutine (ShowItsOver (!isEnemy));
	}

	private void Reset(bool isEnemy) {
		if (isEnemy) {
			enemy.SetBool("KnockoutBool", false);
			enemy.SetBool("GetUpBool", false);
			EnemyController.Instance.Counter = 0;
		} else {
			player.SetBool("KnockoutBool", false);
			player.SetBool("GetUpBool", false);
		}
		EnemyController.Instance.isDead = false;
		StartCoroutine (RemoveGtup (isEnemy));
	}

	public void RemoveKnockout(bool isEnemy) {
		if (isEnemy) {
			enemy.SetBool("KnockoutBool", false);
		} else {
			player.SetBool("KnockoutBool", false);
		}
	}

	private IEnumerator RemoveGtup(bool isEnemy) {
		yield return new WaitForSeconds (2f);
		if (isEnemy) {
			enemy.SetBool("GetupBoolOk", false);
		} else {
			player.SetBool("GetupBoolOk", false);
		}
		yield return new WaitForSeconds (0.90f);
		hasKO = false;
	}

	private void UpdateHealth() {
		GameController.Instance.playerHealthSlider.sliderValue = playerHealth / playerHealthHolder;
		GameController.Instance.enemyHealthSlider.sliderValue = enemyHealth / enemyHealthHolder;
	}

	private IEnumerator ShowKO() {
		yield return new WaitForSeconds (2f);
		koGameObject.SetActive (true);
		iTween.ScaleTo (koGameObject, new Vector3(689f, 331f, 1), 1f);
		SoundManager.Instance.PlayOnce (Sounds.BELL);
		yield return new WaitForSeconds (3f);
		koGameObject.transform.localScale = Vector3.zero;
		koGameObject.SetActive (false);
		StartCountdown();
	}

	private IEnumerator ShowItsOver(bool isEnemy) {
		yield return new WaitForSeconds (3f);
		itsOverGameObject.SetActive(true);
		iTween.ScaleTo (itsOverGameObject, new Vector3(774f, 146f, 1), 1f);
		SoundManager.Instance.PlayOnce (Sounds.BELL);
		yield return new WaitForSeconds(1f);
		SoundManager.Instance.PlayOnce (Sounds.BELL);
		yield return new WaitForSeconds(1f);
		SoundManager.Instance.PlayOnce (Sounds.BELL);
		itsOverGameObject.transform.localScale = Vector3.zero;
		itsOverGameObject.SetActive (false);
		yield return StartCoroutine (ShowWinner (isEnemy));
	}

	private IEnumerator ShowWinner(bool isEnemy) {
		GameObject goHolder = null;
		if (isEnemy) 
			goHolder = youLoseGameObject;
		else
			goHolder = youWinGameObject;
		goHolder.SetActive(true);
		iTween.ScaleTo (goHolder, new Vector3(1000f, 282f, 1), 1f);
		yield return new WaitForSeconds (2f);
		goHolder.transform.localScale = Vector3.zero;
		goHolder.SetActive (false);
		GameController.Instance.OnBack ();
	}

	private IEnumerator AnimateRightBoom() {
		int ranVal = UnityEngine.Random.Range (0, GameController.Instance.RightEffects.Count - 1);
		GameObject go = GameController.Instance.RightEffects [ranVal];
		go.SetActive (true);
		iTween.ScaleFrom (go, Vector3.zero, 0.5f);
		yield return new WaitForSeconds(0.5f);
		go.SetActive (false);
	}

	private IEnumerator AnimateLeftBoom() {
		int ranVal = UnityEngine.Random.Range (0, GameController.Instance.LeftEffects.Count - 1);
		GameObject go = GameController.Instance.LeftEffects [ranVal];
		go.SetActive (true);
		iTween.ScaleFrom (go, Vector3.zero, 0.5f);
		yield return new WaitForSeconds(0.5f);
		go.SetActive (false);
	}
}
