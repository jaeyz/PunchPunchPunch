using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {

	[SerializeField]
	private Transform panel;

	[SerializeField]
	private GameObject glovesTransform;

	[SerializeField]
	private CPButton playButton;

	[SerializeField]
	private CPButton settingsButton;
	
	void Start () {
		ScreenSizeSupport.Instance.root = GetComponent<UIRoot> ();
		panel.localScale = ScreenSizeSupport.Instance.GetPrefabScale ();
		AddListeners ();
		iTween.MoveTo (glovesTransform, iTween.Hash("position", new Vector3 (482.56f, 60, 0), 
		                                                "time", 1f, "islocal", true));
	}

	void OnDestroy() {
		RemoveListeners ();
	}
	
	void AddListeners() {
		playButton.AddListeners (OnClickPlay);
		settingsButton.AddListeners (OnClickSettings);
	}

	void RemoveListeners() {
		playButton.RemoveListeners (OnClickPlay);
		settingsButton.RemoveListeners (OnClickSettings);
	}

	void OnClickPlay() {
		SoundManager.Instance.SetVolume (1);
		Application.LoadLevel("CharacterSelect");
	}

	void OnClickSettings() {
		Application.LoadLevel("Settings");
	}
}
