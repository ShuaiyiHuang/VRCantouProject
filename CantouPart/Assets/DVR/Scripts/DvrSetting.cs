
#if !UNITY_EDITOR
#if UNITY_ANDROID
#define ANDROID_DEVICE
#elif UNITY_IPHONE
#define IPHONE_DEVICE
#endif
#endif
using UnityEngine;
using System.Collections;
using DVR.Engine;

public class DvrSetting :MonoBehaviour{
	 static DvrSetting(){
		DvrLog.LogEnable = true;

//		 DvrManager.SDK.GetDevice()
#if UNITY_5
		DvrSdkConfig.isUnity5 = true;
#endif
		
#if UNITY_EDITOR
		DvrSdkConfig.DVR_PLATFORM=DvrSdkConfig.PLATFORM.DVR_UNITY_EDITOR; 
#elif ANDROID_DEVICE
		DvrSdkConfig.DVR_PLATFORM=DvrSdkConfig.PLATFORM.DVR_UNITY_ANDROID;
#else
		DvrSdkConfig.DVR_PLATFORM = DvrSdkConfig.PLATFORM.DVR_UNKOWN;
#endif
	}
}
