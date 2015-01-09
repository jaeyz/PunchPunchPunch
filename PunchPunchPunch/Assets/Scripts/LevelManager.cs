using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private int[] easyTimer = new int[] { 2, 4 };
	private int[] normalTimer = new int[] { 2, 3};
	private int[] hardTimer = new int[] { 1, 4};
	private int[] extremeTimer = new int[] { 1, 3};

	private static LevelManager levelManager;
	public static LevelManager Instance {
		get {
			if (levelManager == null)
				levelManager = (LevelManager) GameObject.FindObjectOfType(typeof(LevelManager));
			return levelManager;
		}
	}

	public int[] GetMinMaxValues(EnemyType enemyType) {
		switch (enemyType) {
		case EnemyType.EASY:
			return easyTimer;
		case EnemyType.NORMAL:
			return normalTimer;
		case EnemyType.HARD:
			return hardTimer;
		case EnemyType.EXTREME:
			return extremeTimer;
		}
		return null;
	}



}
