using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	[SerializeField]
	private Transform panel;

	[SerializeField]
	private List<GameObject> playerAvatars = new List<GameObject>();

	[SerializeField]
	private List<GameObject> enemyAvatars = new List<GameObject>();

	[SerializeField]
	private GameObject backMenu;

	public UISlider playerHealthSlider;
	public UISlider enemyHealthSlider;

	private static GameController gameController;
	public static GameController Instance {
		get {
			if (gameController == null)
				gameController = (GameController) GameObject.FindObjectOfType(typeof(GameController));
			return gameController;
		}
	}

	void Start () {
		ScreenSizeSupport.Instance.root = GetComponent<UIRoot> ();
		panel.localScale = ScreenSizeSupport.Instance.GetPrefabScale ();
		SoundManager.Instance.PlaySound (Sounds.IN_GAME, true);
		ShowAvatars ();
	}

	public void OnBack() {
		SoundManager.Instance.StopSound ();
		Destroy (SoundManager.Instance.gameObject);
		Destroy (ScreenSizeSupport.Instance.gameObject);
		Application.LoadLevel("Loading");
	}

	void ShowAvatars() {
		ShowActivePlayerAvatar ();
		ShowActiveEnemyAvatar ();
	}

	void ShowActivePlayerAvatar() {
		foreach (GameObject go in playerAvatars) {
			if (go.name == PlayerPrefs.GetString("PlayerAvatar")) {
				go.SetActive(true);
				GameManager.Instance.player = go.GetComponent<Animator>();
				PlayerController.Instance.anim = go.GetComponent<Animator>();
			}
		}
	}

	void ShowActiveEnemyAvatar() {
		foreach (GameObject go in enemyAvatars) {
			if (go.name == PlayerPrefs.GetString("EnemyAvatar")) {
				go.SetActive(true);
				GameManager.Instance.enemy = go.GetComponent<Animator>();
				MovementManager.Instance.anim = go.GetComponent<Animator>();
			}
		}
	}
}
