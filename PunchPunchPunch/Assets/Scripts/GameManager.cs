using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	private List<EnemyType> enemyTypes = new List<EnemyType>();
	private int enemyIndex = 0;

	private static GameManager gameManager;
	public static GameManager Instance {
		get {
			if (gameManager == null)
				gameManager = (GameManager) GameObject.FindObjectOfType(typeof(GameManager));
			return gameManager;
		}
	}

	void Start() {
		enemyTypes = Enum.GetValues(typeof(EnemyType)).OfType<EnemyType>().ToList();
	}

	public void NextLevel() {
		if ((enemyIndex + 1) < enemyTypes.Count)
			enemyIndex++;
		EnemyController.Instance.UpdateEnemyType(enemyTypes[enemyIndex]);
	}

	public EnemyType CurrentEnemyType {
		get {
			return enemyTypes[enemyIndex];
		}
	}

}
