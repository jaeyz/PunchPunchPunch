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

	private float playerHealth = 10f;
	private float enemyHealth = 10f;

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

	public void CheckAttack() {

	}

	public void DamageOpponent(BoxerState boxerState) {
		if (EnemyHash != Animator.StringToHash ("Base Layer.BlockBool")) {
			if (boxerState.ToString ().ToUpper().Contains ("JAB"))
				enemyHealth -= 1;
			else if (boxerState.ToString().ToUpper().Contains("HOOK"))
				enemyHealth -= 2;
			else if (boxerState.ToString().ToUpper().Contains("UPPERCUT"))
				enemyHealth -= 3;
			
			if (enemyHealth <= 0)
				enemy.SetBool ("KnockoutBool", true);
			else {
				if (boxerState.ToString().ToUpper().Contains("UPPERCUT")) {
					enemy.SetBool("DamageUppercut", true);
				} else {
					if (boxerState == BoxerState.LEFT_JAB || boxerState == BoxerState.LEFT_HOOK)
						enemy.SetBool("DamageRight", true);
					else if (boxerState == BoxerState.RIGHT_JAB || boxerState == BoxerState.RIGHT_HOOK)
						enemy.SetBool("DamageLeft", true);
				}
				CheckAnimation(enemy);
			}
		}
	}

	private void CheckAnimation(Animator anim) {
		// PROB: STATE DELAYED
		StartCoroutine(Wait(3f, null));
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		//Debug.LogError (stateInfo.nameHash == Animator.StringToHash("Base Layer.IdleState"));
		StartCoroutine(Wait(stateInfo.length, anim));
	}
	
	IEnumerator Wait(float t, Animator anim) {
		yield return new WaitForSeconds (t);
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

}
