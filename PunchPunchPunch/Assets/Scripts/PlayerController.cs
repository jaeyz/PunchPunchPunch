using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private Animator anim;

	private static PlayerController playerController;
	public static PlayerController Instance {
		get {
			if (playerController == null)
				playerController = (PlayerController) GameObject.FindObjectOfType(typeof(PlayerController));
			return playerController;
		}  
	}

	public Animator PlayerAnimator {
		get { return anim; }
	}
	                                        
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.Q)) {
			if (IsIdle()) {
				anim.SetBool("IdleBool", false);
				anim.SetBool("LeftJabBool", true);
				GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_JAB);
			}
		} else if (Input.GetKeyUp(KeyCode.A)) {
			if (IsIdle()) {
				anim.SetBool("IdleBool", false);
				anim.SetBool("LeftHookBool", true);
				GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_HOOK);
			}
		} else if (Input.GetKeyUp(KeyCode.Z)) {
			if (IsIdle()) {
				anim.SetBool("IdleBool", false);
				anim.SetBool("LeftUppercutBool", true);
				GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_UPPERCUT);
			}
		} else if (Input.GetKeyUp(KeyCode.X)) {
			if (IsIdle()) {
				anim.SetBool("IdleBool", false);
				anim.SetBool("RightUppercutBool", true);
				GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_UPPERCUT);
			}
		} else if (Input.GetKeyUp(KeyCode.S)) {
			if (IsIdle()) {
				anim.SetBool("IdleBool", false);
				anim.SetBool("RightHookBool", true);
				GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_HOOK);
			}
		} else if (Input.GetKeyUp(KeyCode.W)) {
			if (IsIdle()) {
				anim.SetBool("IdleBool", false);
				anim.SetBool("RightJabBool", true);
				GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_JAB);
			}
		} 
	}

	public void Reset() {
		anim.SetBool("RightJabBool", false);
		anim.SetBool("RightHookBool", false);
		anim.SetBool("RightUppercutBool", false);
		anim.SetBool("LeftUppercutBool", false);
		anim.SetBool("LeftHookBool", false);
		anim.SetBool("LeftJabBool", false);
	}

	private bool IsIdle() {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.nameHash == Animator.StringToHash("Base Layer.IdleState");
	}

}
