using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSelectController : MonoBehaviour {

	[SerializeField]
	private List<GameObject> playerAvatars = new List<GameObject>();

	[SerializeField]
	private List<GameObject> enemyAvatars = new List<GameObject>();

	[SerializeField]
	private List<GameObject> leftStats = new List<GameObject> ();

	[SerializeField]
	private List<GameObject> rightStats = new List<GameObject> ();

	[SerializeField]
	private Transform panel;
	
	[SerializeField]
	private CPButton playerLeftButton;
	
	[SerializeField]
	private CPButton playerRightButton;

	[SerializeField]
	private CPButton enemyLeftButton;
	
	[SerializeField]
	private CPButton enemyRightButton;

	[SerializeField]
	private CPButton boxButton;

	private int playerAvatarIndex = 0;
	private int enemyAvatarIndex = 0;

	void Start () {
		ScreenSizeSupport.Instance.root = GetComponent<UIRoot> ();
		panel.localScale = ScreenSizeSupport.Instance.GetPrefabScale ();
		SoundManager.Instance.PlaySound (Sounds.SUB_MENU_CLIP, true);
		AddListeners ();
		ShowPlayerAvatars ();
		ShowEnemyAvatars ();
	}
	
	void OnDestroy() {
		RemoveListeners ();
	}
	
	void AddListeners() {
		playerLeftButton.AddListeners (OnPlayerLeftClick);
		playerRightButton.AddListeners (OnPlayerRightClick);
		enemyLeftButton.AddListeners (OnEnemyLeftClick);
		enemyRightButton.AddListeners (OnEnemyRightClick);
		boxButton.AddListeners (OnStartClick);
	}
	
	void RemoveListeners() {
		playerLeftButton.RemoveListeners (OnPlayerLeftClick);
		playerRightButton.RemoveListeners (OnPlayerRightClick);
		enemyLeftButton.RemoveListeners (OnEnemyLeftClick);
		enemyRightButton.RemoveListeners (OnEnemyRightClick);
		boxButton.RemoveListeners (OnStartClick);
	}

	void OnStartClick() {
		PlayerPrefs.SetString ("PlayerAvatar", GetPlayerAvatarName ());
		PlayerPrefs.SetString ("EnemyAvatar", GetEnemyAvatarName ());
		SoundManager.Instance.StopSound ();
		Application.LoadLevel("Game");
	}

	void ShowPlayerAvatars() {
		ResetPlayer ();
		playerAvatars [0].SetActive (true);
		leftStats [0].SetActive (true);
	}

	void ShowEnemyAvatars() {
		ResetEnemy ();
		enemyAvatars [0].SetActive (true);
		rightStats [0].SetActive (true);
	}
	
	void OnPlayerLeftClick() {
		ResetPlayer ();
		if (playerAvatarIndex <= 0)
			playerAvatarIndex = playerAvatars.Count - 1;
		else
			playerAvatarIndex --;
		playerAvatars [playerAvatarIndex].SetActive (true);
		leftStats [playerAvatarIndex].SetActive (true);
	}

	void OnPlayerRightClick() {
		ResetPlayer ();
		if (playerAvatarIndex >= playerAvatars.Count - 1)
			playerAvatarIndex = 0;
		else
			playerAvatarIndex++;
		playerAvatars [playerAvatarIndex].SetActive (true);
		leftStats [playerAvatarIndex].SetActive (true);
	}

	void OnEnemyLeftClick() {
		ResetEnemy ();
		if (enemyAvatarIndex <= 0)
			enemyAvatarIndex = enemyAvatars.Count - 1;
		else
			enemyAvatarIndex --;
		enemyAvatars [enemyAvatarIndex].SetActive (true);
		rightStats [enemyAvatarIndex].SetActive (true);
	}

	void OnEnemyRightClick() {
		ResetEnemy ();
		if (enemyAvatarIndex >= enemyAvatars.Count - 1)
			enemyAvatarIndex = 0;
		else
			enemyAvatarIndex++;
		enemyAvatars [enemyAvatarIndex].SetActive (true);
		rightStats [enemyAvatarIndex].SetActive (true);
	}

	void ResetPlayer() {
		foreach (GameObject go in playerAvatars)
			go.SetActive(false);
		foreach (GameObject go in leftStats)
			go.SetActive(false);
	}

	void ResetEnemy() {
		foreach (GameObject go in enemyAvatars)
			go.SetActive(false);
		foreach (GameObject go in rightStats)
			go.SetActive(false);
	}

	private string GetPlayerAvatarName() {
		foreach (GameObject go in playerAvatars) {
			if (go.activeSelf)
				return go.name;
		}
		return null;
	}

	private string GetEnemyAvatarName() {
		foreach (GameObject go in enemyAvatars) {
			if (go.activeSelf)
				return go.name;
		}
		return null;
	}

}
