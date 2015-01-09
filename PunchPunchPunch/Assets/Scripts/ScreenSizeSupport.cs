using UnityEngine;
using System.Collections;

public class ScreenSizeSupport : MonoBehaviour {
	
	public const float ASPECTRATIO_16_9 = 1.777777778f;
	public const float ASPECTRATIO_17_10 = 1.7f;
	public const float ASPECTRATIO_16_10 = 1.6f;
	public const float ASPECTRATIO_3_2 = 1.5f;
	public const float ASPECTRATIO_4_3 = 1.333333333f;

	public UIRoot root;
	
	private Vector3 prefabScale = Vector3.zero;
	
	public float baseManualHeight = 1080;
	private float fixedUIManualHeight = 0;
	
	private static ScreenSizeSupport screenSizeSupport;
	public static ScreenSizeSupport Instance {
		get {
			if (screenSizeSupport == null)
				screenSizeSupport = (ScreenSizeSupport) GameObject.FindObjectOfType(typeof(ScreenSizeSupport));
			return screenSizeSupport;
		}
	}

	void Awake() {
		DontDestroyOnLoad (this);
	}
	
	void Start() {
		Screen.orientation = ScreenOrientation.LandscapeRight;
	}
	
	public Vector3 GetPrefabScale() {
		SetManualHeight();
		float size = fixedUIManualHeight / baseManualHeight;
		
		return new Vector3(size,size,size);
	}
	
	private void SetManualHeight() {
		Vector2 aspectRatio = AspectRatio.GetAspectRatio(Screen.width, Screen.height);
		float aspect = aspectRatio.x / aspectRatio.y;
		
		if (aspect >= ASPECTRATIO_16_9) {
			fixedUIManualHeight = 1080;
		} else if (aspect >= ASPECTRATIO_17_10) {
			fixedUIManualHeight = 600;
		} else if (aspect >= ASPECTRATIO_16_10) {
			fixedUIManualHeight = 800;
		} else if (aspect >= ASPECTRATIO_3_2) {
			fixedUIManualHeight = 640;
		} else if (aspect >= ASPECTRATIO_4_3) {
			fixedUIManualHeight = 768;
		} else {
			fixedUIManualHeight = Screen.height;
		}
		
		root.manualHeight = (int) fixedUIManualHeight;
	}
	
	public float GetAspectRatioBasedOnHeight (){
		Vector2 aspectRatio = AspectRatio.GetAspectRatio(Screen.width, Screen.height);
		float aspect = aspectRatio.x / aspectRatio.y;
		
		if (aspect >= ASPECTRATIO_16_9) {
			return ASPECTRATIO_16_9;
		} else if (aspect >= ASPECTRATIO_17_10) {
			return ASPECTRATIO_17_10;
		} else if (aspect >= ASPECTRATIO_16_10) {
			return ASPECTRATIO_16_10;
		} else if (aspect >= ASPECTRATIO_3_2) {
			return ASPECTRATIO_3_2;
		} else if (aspect >= ASPECTRATIO_4_3) {
			return ASPECTRATIO_4_3;
		} else {
			return aspect;
		}
	}
}