using UnityEngine;
using System.Collections;

public class LoadingScreenController : MonoBehaviour {

	[SerializeField]
	private Transform panel;

	[SerializeField]
	private CPButton button;

	// Use this for initialization
	void Start () {
		ScreenSizeSupport.Instance.root = GetComponent<UIRoot> ();
		panel.localScale = ScreenSizeSupport.Instance.GetPrefabScale ();
		SoundManager.Instance.PlaySound (Sounds.MAIN_CLIP, true);
		button.AddListeners (OnStartClick);
	}
	
	void OnDestroy() {
		button.RemoveListeners (OnStartClick);
	}

	void OnStartClick() {
		Application.LoadLevel("MainMenu");
	}
}
