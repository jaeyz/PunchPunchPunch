using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	private Animator anim;

	private bool canAttack = true;

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
		if (!GameManager.Instance.hasKO) {
			if (Input.GetKeyUp(KeyCode.Q)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("LeftJabBool", true);
					GameManager.Instance.playerState = BoxerState.LEFT_JAB_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_JAB_ATTACK);
					StartCoroutine(WaitToReset());
				}
			} else if (Input.GetKeyUp(KeyCode.A)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("LeftHookBool", true);
					GameManager.Instance.playerState = BoxerState.LEFT_HOOK_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_HOOK_ATTACK);
					StartCoroutine(WaitToReset());
				}
			} else if (Input.GetKeyUp(KeyCode.Z)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("LeftUppercutBool", true);
					GameManager.Instance.playerState = BoxerState.LEFT_UPPERCUT_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_UPPERCUT_ATTACK);
					StartCoroutine(WaitToReset());
				}
			} else if (Input.GetKeyUp(KeyCode.X)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("RightUppercutBool", true);
					GameManager.Instance.playerState = BoxerState.RIGHT_UPPERCUT_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_UPPERCUT_ATTACK);
					StartCoroutine(WaitToReset());
				}
			} else if (Input.GetKeyUp(KeyCode.S)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("RightHookBool", true);
					GameManager.Instance.playerState = BoxerState.RIGHT_HOOK_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_HOOK_ATTACK);
					StartCoroutine(WaitToReset());
				}
			} else if (Input.GetKeyUp(KeyCode.W)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("RightJabBool", true);
					GameManager.Instance.playerState = BoxerState.RIGHT_JAB_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_JAB_ATTACK);
					StartCoroutine(WaitToReset());
				}
			} else if (Input.GetKeyUp(KeyCode.E)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("BlockBool", true);
					GameManager.Instance.playerState = BoxerState.BLOCK;
					StartCoroutine(WaitToReset(true));
				}
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
		anim.SetBool ("BlockBool", false);
		GameManager.Instance.playerState = BoxerState.IDLE;
	}

	private bool IsIdle() {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.nameHash == Animator.StringToHash("Base Layer.IdleState");
	}

	private IEnumerator WaitToReset(bool reset = false) {
		yield return new WaitForSeconds(1f);
		canAttack = true;
		if (reset)
			Reset ();
	}

}
