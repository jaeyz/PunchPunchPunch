using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SettingsController : MonoBehaviour {

	[SerializeField]
	private Transform panel;

	[SerializeField]
	private CPButton backButton;

	[SerializeField]
	private List<UICheckbox> options = new List<UICheckbox>();

	void Start () {
		ScreenSizeSupport.Instance.root = GetComponent<UIRoot> ();
		panel.localScale = ScreenSizeSupport.Instance.GetPrefabScale ();
		AddListeners ();
		if (PlayerPrefs.HasKey("Settings")) 
			SetCheckedSetting(PlayerPrefs.GetString("Settings"));
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
		foreach (UICheckbox cb in options) {
			if (cb.isChecked) {
				PlayerPrefs.SetString("Settings", cb.transform.parent.name);
				break;
			}
		}
		PlayerPrefs.Save ();
		Application.LoadLevel("MainMenu");
	}

	void SetCheckedSetting(string s) {
		foreach (UICheckbox cb in options) {
			if (cb.transform.parent.name.Equals(s)) {
				cb.isChecked = true;
				break;
			}
		}
	}
}
