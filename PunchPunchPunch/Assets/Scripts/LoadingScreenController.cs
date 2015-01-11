using UnityEngine;
using System.Collections;

public class LoadingScreenController : MonoBehaviour {

	[SerializeField]
	private Transform panel;

	[SerializeField]
	private CPButton button;

	[SerializeField]
	private GameObject tapToStart;

	// Use this for initialization
	void Start () {
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
		Application.targetFrameRate = 30;
#endif
		ScreenSizeSupport.Instance.root = GetComponent<UIRoot> ();
		panel.localScale = ScreenSizeSupport.Instance.GetPrefabScale ();
		SoundManager.Instance.PlaySound (Sounds.MAIN_CLIP, true);
		button.AddListeners (OnStartClick);
		iTween.FadeFrom (tapToStart, iTween.Hash("alpha", 0, "time", 1f, "looptype", iTween.LoopType.loop));
		if (!PlayerPrefs.HasKey("Settings")) {
			PlayerPrefs.SetString("Settings", "Easy");
			PlayerPrefs.Save();
		}
	}
	
	void OnDestroy() {
		button.RemoveListeners (OnStartClick);
	}

	void OnStartClick() {
		Application.LoadLevel("MainMenu");
	}
}
