using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	[SerializeField]
	private Animator player;

	[SerializeField]
	private Animator enemy;
	
	private List<EnemyType> enemyTypes = new List<EnemyType>();
	private int enemyIndex = 0;

	public float playerHealth = 10f;
	public float enemyHealth = 10f;

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
	}

	public void NextLevel() {
		if ((enemyIndex + 1) < enemyTypes.Count)
			enemyIndex++;
		EnemyController.Instance.UpdateEnemyType(enemyTypes[enemyIndex]);
	}

	public EnemyType CurrentEnemyType {
		get {
			return enemyTypes[enemyIndex];
		}
	}
	
	public void Damage(Boxers boxer, BoxerState boxerState) {
		if (EnemyHash != Animator.StringToHash ("Base Layer.BlockBool")) {
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
			StartCoroutine(Delay(boxer, boxerState));
		}
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
		if (enemyHealth <= 0 || playerHealth <= 0)
			animatorHolder.SetBool ("KnockoutBool", true);
		else {
			EnemyController.Instance.Counter -= 2f;
			if (boxerState.ToString().ToUpper().Contains("UPPERCUT")) {
				animatorHolder.SetBool("DamageUppercut", true);
			} else {
				if (boxerState == BoxerState.LEFT_JAB || boxerState == BoxerState.LEFT_HOOK) {
					animatorHolder.SetBool("DamageRight", true);
				} else if (boxerState == BoxerState.RIGHT_JAB || boxerState == BoxerState.RIGHT_HOOK)
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
}
