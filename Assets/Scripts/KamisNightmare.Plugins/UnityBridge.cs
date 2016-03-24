using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DapperApps.Plugins
{
	public class UnityBridge
	{
		public static void LoadAd()
		{
#if UNITY_ANDROID
            AndroidJNI.AttachCurrentThread();
			using(AndroidJavaClass jc = new AndroidJavaClass("com.dapperapps.admobplugin.InterstitialActivity"))
			using(AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("mContext"))
			{
				jo.Call("LoadInterstitial");
			}
#endif
		}
		
		public static void ShowAd()
		{
#if UNITY_ANDROID
            AndroidJNI.AttachCurrentThread();
			using(AndroidJavaClass jc = new AndroidJavaClass("com.dapperapps.admobplugin.InterstitialActivity"))
			using(AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("mContext"))
			{
				jo.Call("ShowInterstitial");
            }
#endif
        }
		
	}
}