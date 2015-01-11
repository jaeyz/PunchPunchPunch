using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private const float LEFT_THRESHOLD = -400f;
	private const float RIGHT_THRESHOLD = 400f;
	private const float UP_THRESHOLD = 400f;
	private const float DOWN_THRESHOLD = -400f;

	public Animator anim;

	private bool canAttack = true;

	private Vector2 initialPosition = Vector2.zero;
	private Vector2 lastPosition = Vector2.zero;
	private bool touched = false;

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
#if ( UNITY_ANDROID || UNITY_IPHONE ) && !UNITY_EDITOR
			if (IsSwipeLeftDown()) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("LeftJabBool", true);
					GameManager.Instance.playerState = BoxerState.LEFT_JAB_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_JAB_ATTACK);
					StartCoroutine(WaitToReset());
				}
			} else if (IsLeftSwipeRight()) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("LeftHookBool", true);
					GameManager.Instance.playerState = BoxerState.LEFT_HOOK_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_HOOK_ATTACK);
					StartCoroutine(WaitToReset(false, 1.2f));
				}
			} else if (IsLeftSwipeLeft()) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("DodgeLeftBool", true);
					GameManager.Instance.playerState = BoxerState.LEFT_DODGE;
					StartCoroutine(WaitToReset(true));
				}
			} else if (IsSwipeLeftUp()) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("LeftUppercutBool", true);
					GameManager.Instance.playerState = BoxerState.LEFT_UPPERCUT_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_UPPERCUT_ATTACK);
					StartCoroutine(WaitToReset(false, 1.5f));
				}
			} 	else if (IsRightSwipeDown()) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("RightJabBool", true);
					GameManager.Instance.playerState = BoxerState.RIGHT_JAB_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_JAB_ATTACK);
					StartCoroutine(WaitToReset());
				}
			} else if (IsRightSwipeLeft()) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("RightHookBool", true);
					GameManager.Instance.playerState = BoxerState.RIGHT_HOOK_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_HOOK_ATTACK);
					StartCoroutine(WaitToReset(false, 1.2f));
				}
			} else if (IsRightSwipe()) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("DodgeRightBool", true);
					GameManager.Instance.playerState = BoxerState.RIGHT_DODGE;
					StartCoroutine(WaitToReset(true));
				}
			} else if (IsRightSwipeUp()) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("RightUppercutBool", true);
					GameManager.Instance.playerState = BoxerState.RIGHT_UPPERCUT_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_UPPERCUT_ATTACK);
					StartCoroutine(WaitToReset(false, 1.5f));
				}
			} else if (IsBlock()) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("BlockBool", true);
					GameManager.Instance.playerState = BoxerState.BLOCK;
					StartCoroutine(WaitToReset(true, 1.5f));
				}
			}
#else
		
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
					StartCoroutine(WaitToReset(false, 1.5f));
				}
			} else if (Input.GetKeyUp(KeyCode.Z)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("LeftUppercutBool", true);
					GameManager.Instance.playerState = BoxerState.LEFT_UPPERCUT_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.LEFT_UPPERCUT_ATTACK);
					StartCoroutine(WaitToReset(false, 2f));
				}
			} else if (Input.GetKeyUp(KeyCode.X)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("RightUppercutBool", true);
					GameManager.Instance.playerState = BoxerState.RIGHT_UPPERCUT_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_UPPERCUT_ATTACK);
					StartCoroutine(WaitToReset(false, 2f));
				}
			} else if (Input.GetKeyUp(KeyCode.S)) {
				if (canAttack) {
					canAttack = false;
					anim.SetBool("IdleBool", false);
					anim.SetBool("RightHookBool", true);
					GameManager.Instance.playerState = BoxerState.RIGHT_HOOK_ATTACK;
					GameManager.Instance.Damage(Boxers.ENEMY, BoxerState.RIGHT_HOOK_ATTACK);
					StartCoroutine(WaitToReset(false, 1.5f));
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
#endif
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
		anim.SetBool ("DodgeLeftBool", false);
		anim.SetBool ("DodgeRightBool", false);
		GameManager.Instance.playerState = BoxerState.IDLE;
	}

	private bool IsIdle() {
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		return stateInfo.nameHash == Animator.StringToHash("Base Layer.IdleState");
	}

	private IEnumerator WaitToReset(bool reset = false, float w = 1f) {
		yield return new WaitForSeconds(w);
		canAttack = true;
		if (reset)
			Reset ();
	}

	private bool IsLeftSwipeLeft() {
		if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.position.x < Screen.width / 2f) {
				switch (touch.phase) {
				case TouchPhase.Began:
					initialPosition = touch.position;
					touched = true;
					break;
				case TouchPhase.Ended:
					touched = false;
					lastPosition = touch.position;
					float x = lastPosition.x - initialPosition.x;
					float y = lastPosition.y - initialPosition.y;
					if (x <= LEFT_THRESHOLD && y < UP_THRESHOLD) {
						Debug.LogError("Left Swipe");
						return true;
					}
					break;
				case TouchPhase.Canceled:
					touched = false;
					initialPosition = Vector2.zero;
					lastPosition = Vector2.zero;
					break;
				}
			}
		}
		return false;
	}

	private bool IsLeftSwipeRight() {
		if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.position.x < Screen.width / 2f) {
				switch (touch.phase) {
				case TouchPhase.Began:
					initialPosition = touch.position;
					touched = true;
					break;
				case TouchPhase.Ended:
					touched = false;
					lastPosition = touch.position;
					float x = lastPosition.x - initialPosition.x;
					float y = lastPosition.y - initialPosition.y;
					if (x >= RIGHT_THRESHOLD && y < UP_THRESHOLD) {
						Debug.LogError("Left Swipe Right");
						return true;
					}
					break;
				case TouchPhase.Canceled:
					touched = false;
					initialPosition = Vector2.zero;
					lastPosition = Vector2.zero;
					break;
				}
			}
		}
		return false;
	}

	private bool IsSwipeLeftUp() {
		if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.position.x < Screen.width / 2f) {
				switch (touch.phase) {
				case TouchPhase.Began:
					initialPosition = touch.position;
					touched = true;
					break;
				case TouchPhase.Ended:
					touched = false;
					lastPosition = touch.position;
					float x = lastPosition.x - initialPosition.x;
					float y = lastPosition.y - initialPosition.y;
					if (y >= UP_THRESHOLD && x > LEFT_THRESHOLD && x < RIGHT_THRESHOLD) {
						Debug.LogError("SwipeLeftUp");
						return true;
					}
					break;
				case TouchPhase.Canceled:
					touched = false;
					initialPosition = Vector2.zero;
					lastPosition = Vector2.zero;
					break;
				}
			}
		}
		return false;
	}

	private bool IsSwipeLeftDown() {
		if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.position.x < Screen.width / 2f) {
				switch (touch.phase) {
				case TouchPhase.Began:
					initialPosition = touch.position;
					touched = true;
					break;
				case TouchPhase.Ended:
					touched = false;
					lastPosition = touch.position;
					float x = lastPosition.x - initialPosition.x;
					float y = lastPosition.y - initialPosition.y;
					if (y <= DOWN_THRESHOLD && x > LEFT_THRESHOLD && x < RIGHT_THRESHOLD) {
						Debug.LogError("SwipeLeftDown");
						return true;
					}
					break;
				case TouchPhase.Canceled:
					touched = false;
					initialPosition = Vector2.zero;
					lastPosition = Vector2.zero;
					break;
				}
			}
		}
		return false;
	}

	private bool IsRightTap() {
		if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.position.x > Screen.width / 2f)  {
				switch (touch.phase) {
				case TouchPhase.Began:
					if (initialPosition.x == 0) {
						touched = true;
						Debug.Log("Right Tap");
						return true;
					}
					break;
				}
			}
		}
		return false;
	}

	private bool IsRightSwipe() {
		if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.position.x > Screen.width / 2f) {
				switch (touch.phase) {
				case TouchPhase.Began:
					initialPosition = touch.position;
					touched = true;
					break;
				case TouchPhase.Ended:
					touched = false;
					lastPosition = touch.position;
					float x = lastPosition.x - initialPosition.x;
					float y = lastPosition.y - initialPosition.y;
					if (x >= RIGHT_THRESHOLD && y < UP_THRESHOLD) {
						Debug.LogError("Right Swipe");
						return true;
					}
					break;
				case TouchPhase.Canceled:
					touched = false;
					initialPosition = Vector2.zero;
					lastPosition = Vector2.zero;
					break;
				}
			}
		}
		return false;
	}

	private bool IsRightSwipeLeft() {
		if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.position.x > Screen.width / 2f) {
				switch (touch.phase) {
				case TouchPhase.Began:
					initialPosition = touch.position;
					touched = true;
					break;
				case TouchPhase.Ended:
					touched = false;
					lastPosition = touch.position;
					float x = lastPosition.x - initialPosition.x;
					float y = lastPosition.y - initialPosition.y;
					if (x <= LEFT_THRESHOLD && y < UP_THRESHOLD) {
						Debug.LogError("Right Swipe Left");
						return true;
					}
					break;
				case TouchPhase.Canceled:
					touched = false;
					initialPosition = Vector2.zero;
					lastPosition = Vector2.zero;
					break;
				}
			}
		}
		return false;
	}

	private bool IsRightSwipeUp() {
		if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.position.x > Screen.width / 2f) {
				switch (touch.phase) {
				case TouchPhase.Began:
					initialPosition = touch.position;
					touched = true;
					break;
				case TouchPhase.Ended:
					touched = false;
					lastPosition = touch.position;
					float x = lastPosition.x - initialPosition.x;
					float y = lastPosition.y - initialPosition.y;
					if (y > UP_THRESHOLD && x > LEFT_THRESHOLD && x < RIGHT_THRESHOLD) {
						Debug.LogError("SwipeRightUp");
						return true;
					}
					break;
				case TouchPhase.Canceled:
					touched = false;
					initialPosition = Vector2.zero;
					lastPosition = Vector2.zero;
					break;
				}
			}
		}
		return false;
	}

	private bool IsRightSwipeDown() {
		if (Input.touchCount == 1) {
			Touch touch = Input.touches[0];
			if (touch.position.x > Screen.width / 2f) {
				switch (touch.phase) {
				case TouchPhase.Began:
					initialPosition = touch.position;
					touched = true;
					break;
				case TouchPhase.Ended:
					touched = false;
					lastPosition = touch.position;
					float x = lastPosition.x - initialPosition.x;
					float y = lastPosition.y - initialPosition.y;
					if (y <= DOWN_THRESHOLD && x > LEFT_THRESHOLD && x < RIGHT_THRESHOLD) {
						Debug.LogError("SwipeRightDown");
						return true;
					}
					break;
				case TouchPhase.Canceled:
					touched = false;
					initialPosition = Vector2.zero;
					lastPosition = Vector2.zero;
					break;
				}
			}
		}
		return false;
	}

	private bool IsBlock() {
		if (Input.touchCount == 2) {
			Touch touch = Input.touches[0];
			Touch touch2 = Input.touches[1];
			if ((touch.position.x < Screen.width / 2 && touch2.position.x > Screen.width / 2) ||
			   (touch.position.x > Screen.width / 2 && touch2.position.x < Screen.width / 2))
				return true;
		}
		return false;
	}
}
