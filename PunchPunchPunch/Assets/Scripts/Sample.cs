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
		} else if (Input.GetKeyUp(KeyCode.A)) {
			anim.SetBool("IdleBool", false);
			anim.SetBool("LeftHookBool", true);
		} else if (Input.GetKeyUp(KeyCode.Z)) {
			anim.SetBool("IdleBool", false);
			anim.SetBool("LeftUppercutBool", true);
		}  else {
			anim.SetBool("IdleBool", true);
			anim.SetBool("LeftJabBool", false);
			anim.SetBool("LeftHookBool", false);
			anim.SetBool("LeftUppercutBool", false);
		}
	}


}
