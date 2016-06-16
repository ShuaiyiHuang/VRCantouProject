using UnityEngine;
using System.Collections;

using DVR.Engine;

public class eventtest : MonoBehaviour {




	// Use this for initialization
	void Start () {
		DvrManager.SDK.OnTouchMoveDown += test1;
		DvrManager.SDK.OnTouchMoveUp += test2;
		DvrManager.SDK.OnTouchMoveForward += test3;
		DvrManager.SDK.OnTouchMoveBackward += test4;

		obj = GameObject.Find ("Head");
	}


	public static float near=0;
	public static float far=0;
	float lastnear=0;
	float lastfar=0;

	Quaternion text=Quaternion.identity;
	float zeroy=0.0f;
	GameObject obj;

	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Escape)) {
			Debug.LogError ("back");
		} else if (Input.GetKeyDown (KeyCode.Mouse0)) {
			if(obj!=null){
				zeroy=-DvrManager.SDK.HeadPose.Orientation.eulerAngles.y;
				Debug.LogError ("touch"+zeroy);
			}


		}

		Vector3 axisY= Quaternion.Inverse(DvrManager.SDK.HeadPose.Orientation) * new Vector3(0, 1, 0);
		Quaternion zero = Quaternion.AngleAxis(zeroy, axisY);
		DvrManager.SDK.ZeroQuaternion=zero;
		GameObject objc = GameObject.Find ("DvrCamera");
		if (Input.GetKey (KeyCode.Mouse0)) {
			objc.GetComponent<Camera>().farClipPlane+=10f;
		}

		if(Input.GetKey(KeyCode.Escape)){
			objc.GetComponent<Camera>().farClipPlane-=10f;
		}
	
	
	}

	void LateUpdate(){
		GameObject objc = GameObject.Find ("DvrCamera");
		if (objc != null) {
			near = objc.GetComponent<Camera>().nearClipPlane;
			far = objc.GetComponent<Camera>().farClipPlane;
		}
		if (lastnear != near || lastfar != far) {
			DvrManager.SDK.UpdateConfigInfo();
			lastfar=far;
			lastnear=near;
		}

	}


	void OnGUI(){
		GUI.Label  (new UnityEngine.Rect (0,0,200,50), "near:"+near+",far:"+far);
	}

	void test1(){
		Debug.LogError ("test1");
	}

	void test2(){
		Debug.LogError ("test2");
	}
	void test3(){
		Debug.LogError ("test3");
	}
	void test4(){
		Debug.LogError ("test4");
	}
}
