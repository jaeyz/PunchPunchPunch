using UnityEngine;
using System.Collections;

public class PathArrow : MonoBehaviour {

	public Transform destinationTransform;

	[SerializeField]
	private Transform directionIndicator;

	[SerializeField]
	private Transform pivotTransform;

	// Use this for initialization
	void Start () {
		
	}

	void Update () {
		directionIndicator.LookAt(destinationTransform);
	}
}
