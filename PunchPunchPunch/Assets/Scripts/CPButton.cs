using UnityEngine;
using System.Collections;

public class CPButton : MonoBehaviour {

	private System.Action onClick;

	public void AddListeners(System.Action action) {
		onClick -= action;
		onClick += action;
	}

	public void RemoveListeners(System.Action action) {
		onClick -= action;
	}

	void OnClick() {
		if (onClick != null)
			onClick ();
	}
}
