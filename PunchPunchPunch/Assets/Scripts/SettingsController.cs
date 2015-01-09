using UnityEngine;
using System.Collections;

public class SettingsController : MonoBehaviour {

	[SerializeField]
	private Transform panel;

	[SerializeField]
	private CPButton backButton;

	void Start () {
		ScreenSizeSupport.Instance.root = GetComponent<UIRoot> ();
		panel.localScale = ScreenSizeSupport.Instance.GetPrefabScale ();
		AddListeners ();
	}

	void OnDestroy() {
		RemoveListener ();
	}

	void AddListeners() {
		backButton.AddListeners (OnBack);
	}

	void RemoveListener() {
		backButton.RemoveListeners (OnBack);
	}

	void OnBack() {
		Application.LoadLevel("MainMenu");
	}
}
