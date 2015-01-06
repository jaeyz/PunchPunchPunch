using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public EnemyType enemyType = EnemyType.EASY;
	public int minTimeRange = 3; // ATTACK DECISION MAKING.
	public int maxTimeRange = 5; // 

	private float counter = 0;

	void Start () {
	
	}

	void Update () {
		counter += Time.deltaTime;
		if (counter > GetRandomValue()) {
			counter = 0;
		}
	}

	private int GetRandomValue() {
		return Random.Range(minTimeRange, maxTimeRange);
	}
}
