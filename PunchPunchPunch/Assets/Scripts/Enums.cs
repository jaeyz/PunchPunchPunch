using UnityEngine;
using System.Collections;

public enum EnemyType {
	EASY,
	NORMAL,
	HARD,
	EXTREME
}

public enum BoxerState {
	IDLE,
	LEFT_JAB,
	LEFT_HOOK,
	LEFT_UPPERCUT,
	RIGHT_JAB,
	RIGHT_HOOK,
	RIGHT_UPPERCUT,
	LEFT_DAMAGE,
	RIGHT_DAMAGE,
	UPPERCUT_DAMAGE,
	BLOCK,
	LEFT_DODGE,
	RIGHT_DODGE
}