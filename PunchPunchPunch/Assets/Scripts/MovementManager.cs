using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MovementManager : MonoBehaviour {

	public Animator anim;

	private List<BoxerState> boxerStates = new List<BoxerState>();

	private static MovementManager movementManager;
	public static MovementManager Instance {
		get {
			if (movementManager == null)
				movementManager = (MovementManager) GameObject.FindObjectOfType(typeof(MovementManager));
			return movementManager;
		}   
	}

	void Start() {
		boxerStates = Enum.GetValues(typeof(BoxerState)).OfType<BoxerState>().ToList();
	}

	public void PerformRandomMove() {
		if (UnityEngine.Random.value > 0.5f) {
			int randomIndex = UnityEngine.Random.Range (1, 9);
			AnimateMove (boxerStates [randomIndex]);
		} else {
			int randomIndex = UnityEngine.Random.Range (0, boxerStates.Count - 1);
			AnimateMove (boxerStates [randomIndex]);
		}
	}

	private void AnimateMove(BoxerState boxerState) {
		GameManager.Instance.enemyState = boxerState;
		switch (boxerState) {
		case BoxerState.IDLE:
			anim.SetBool("IdleBool", true);
			break;
		case BoxerState.LEFT_JAB_ATTACK:
			anim.SetBool("LeftJabBool", true);
			break;
		case BoxerState.LEFT_HOOK_ATTACK:
			anim.SetBool("LeftHookBool", true);
			break;
		case BoxerState.LEFT_UPPERCUT_ATTACK:
			anim.SetBool("LeftUppercutBool", true);
			break;
		case BoxerState.RIGHT_JAB_ATTACK:
			anim.SetBool("RightJabBool", true);
			break;
		case BoxerState.RIGHT_HOOK_ATTACK:
			anim.SetBool("RightHookBool", true);
			break;
		case BoxerState.RIGHT_UPPERCUT_ATTACK:
			anim.SetBool("RightUppercutBool", true);
			break;
		case BoxerState.BLOCK:
			anim.SetBool("BlockBool", true);
			break;
		case BoxerState.LEFT_DODGE:
			anim.SetBool("DodgeLeftBool", true);
			break;
		case BoxerState.RIGHT_DODGE:
			anim.SetBool("DodgeRightBool", true);
			break;
		}
		GameManager.Instance.Damage(Boxers.PLAYER, boxerState);
		CheckAnimation ();
	}

	private void CheckAnimation() {
		StartCoroutine(Wait(.5f));
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		StartCoroutine(Wait(stateInfo.length));
	}

	IEnumerator Wait(float t) {
		yield return new WaitForSeconds (t);
		anim.SetBool("LeftJabBool", false);
		anim.SetBool("LeftHookBool", false);
		anim.SetBool("LeftUppercutBool", false);
		anim.SetBool("RightJabBool", false);
		anim.SetBool("RightHookBool", false);
		anim.SetBool("RightUppercutBool", false);
		anim.SetBool("BlockBool", false);
		anim.SetBool("DodgeLeftBool", false);
		anim.SetBool("DodgeRightBool", false);
		GameManager.Instance.enemyState = BoxerState.IDLE;
	}

}
