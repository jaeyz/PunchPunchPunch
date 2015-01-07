using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private int minTimeRange = 3; // ATTACK DECISION MAKING.
	private int maxTimeRange = 5; // 

	private float counter = 0;
	private EnemyType enemyType = EnemyType.EASY;

	public bool isDead = false;

	private static EnemyController enemyController;
	public static EnemyController Instance {
		get {
			if (enemyController == null)
				enemyController = (EnemyController) GameObject.FindObjectOfType(typeof(EnemyController));
			return enemyController;
		}
	}
	
	void Update () {
		if (!isDead && !GameManager.Instance.hasKO) {
			counter += Time.deltaTime;
			if (counter > GetRandomValue()) {
				MovementManager.Instance.PerformRandomMove();
				counter = 0;
			}
		}
	}

	public void UpdateEnemyType(EnemyType eType) {
		enemyType = eType;
	}

	private int GetRandomValue() {
		if (minTimeRange != LevelManager.Instance.GetMinMaxValues(enemyType)[0] &&
		    maxTimeRange != LevelManager.Instance.GetMinMaxValues(enemyType)[1]) {
			minTimeRange = LevelManager.Instance.GetMinMaxValues(enemyType)[0];
			maxTimeRange = LevelManager.Instance.GetMinMaxValues(enemyType)[1];
		}
		return Random.Range(minTimeRange, maxTimeRange);
	}

	public float Counter {
		get { return counter; }
		set { counter = value; }
	}

}
