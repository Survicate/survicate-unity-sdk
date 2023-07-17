#if UNITY_ANDROID

namespace Plugins.Survicate
{
    using UnityEngine;
    using System.Collections.Generic;

    public class Survicate
    {
        static AndroidJavaObject jc = new AndroidJavaClass("com.survicate.surveys.Survicate");
        static AndroidJavaObject jw = new AndroidJavaClass("SurvicateNativeBridgeAndroid");

        static AndroidJavaClass sc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
        static AndroidJavaObject so = sc.GetStatic<AndroidJavaObject>("currentActivity");

        public static void SetWorkspaceKey(string key)
        {
            jc.CallStatic("setWorkspaceKey", key);
        }

        public static void Initialize()
        {
            jc.CallStatic("init", so);
        }
    }
}

#endif