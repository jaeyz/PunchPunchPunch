using UnityEngine;
using System.Collections;

public class Sample : MonoBehaviour {

	[SerializeField]
	private Animator anim;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Q)) {
			anim.SetBool("IdleBool", false);
			anim.SetBool("LeftJabBool", true);
			GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_JAB);
		} else if (Input.GetKeyUp(KeyCode.A)) {
			anim.SetBool("IdleBool", false);
			anim.SetBool("LeftHookBool", true);
			GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_HOOK);
		} else if (Input.GetKeyUp(KeyCode.Z)) {
			anim.SetBool("IdleBool", false);
			anim.SetBool("LeftUppercutBool", true);
			GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_UPPERCUT);
		} else if (Input.GetKeyUp(KeyCode.X)) {
			anim.SetBool("IdleBool", false);
			anim.SetBool("RightUppercutBool", true);
			GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_UPPERCUT);
		} else if (Input.GetKeyUp(KeyCode.S)) {
			anim.SetBool("IdleBool", false);
			anim.SetBool("RightHookBool", true);
			GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_HOOK);
		} else if (Input.GetKeyUp(KeyCode.W)) {
			anim.SetBool("IdleBool", false);
			anim.SetBool("RightJabBool", true);
			GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_JAB);
		} else {
			anim.SetBool("IdleBool", true);
			anim.SetBool("LeftJabBool", false);
			anim.SetBool("LeftHookBool", false);
			anim.SetBool("LeftUppercutBool", false);
			anim.SetBool("RightJabBool", false);
			anim.SetBool("RightHookBool", false);
			anim.SetBool("RightUppercutBool", false);
		}
	}

}
