using UnityEngine;
using System.Collections;

public class LoadingScreenController : MonoBehaviour {

	[SerializeField]
	private Transform panel;

	// Use this for initialization
	void Start () {
		ScreenSizeSupport.Instance.root = GetComponent<UIRoot> ();
		panel.localScale = ScreenSizeSupport.Instance.GetPrefabScale ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
