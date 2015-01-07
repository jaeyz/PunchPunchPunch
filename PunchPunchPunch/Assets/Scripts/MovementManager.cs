﻿using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MovementManager : MonoBehaviour {

	[SerializeField]
	private Animator anim;

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
		int randomIndex = UnityEngine.Random.Range (0, boxerStates.Count - 1);
		AnimateMove (boxerStates [randomIndex]);
	}

	private void AnimateMove(BoxerState boxerState) {
		switch (boxerState) {
		case BoxerState.IDLE:
			anim.SetBool("IdleBool", true);
			break;
		case BoxerState.LEFT_JAB:
			anim.SetBool("LeftJabBool", true);
			break;
		case BoxerState.LEFT_HOOK:
			anim.SetBool("LeftHookBool", true);
			break;
		case BoxerState.LEFT_UPPERCUT:
			anim.SetBool("LeftUppercutBool", true);
			break;
		case BoxerState.RIGHT_JAB:
			anim.SetBool("RightJabBool", true);
			break;
		case BoxerState.RIGHT_HOOK:
			anim.SetBool("RightHookBool", true);
			break;
		case BoxerState.RIGHT_UPPERCUT:
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
	}

}
