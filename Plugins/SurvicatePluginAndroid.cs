#if UNITY_ANDROID

namespace Plugins.Survicate
{
    using UnityEngine;

    public class Survicate
    {
        static AndroidJavaObject jc = new AndroidJavaClass("com.survicate.surveys.Survicate");
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

        public static void EnterScreen(string screenKey)
        {
            jc.CallStatic("enterScreen", screenKey);
        }

        public static void LeaveScreen(string screenKey)
        {
            jc.CallStatic("leaveScreen", screenKey);
        }

        public static void InvokeEvent(string eventName)
        {
            jc.CallStatic("invokeEvent", eventName);
        }

        public static void SetUserTrait(string traitKey, string traitValue)
        {
            jc.CallStatic("setUserTrait", traitKey, traitValue);
        }

        public static void Reset()
        {
            jc.CallStatic("reset");
        }
    }
}

#endif
