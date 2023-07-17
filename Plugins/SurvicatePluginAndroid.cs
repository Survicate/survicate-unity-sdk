#if UNITY_ANDROID

namespace Plugins.Survicate
{
    using UnityEngine;

    public class Survicate
    {
        static AndroidJavaObject survicate = new AndroidJavaClass("com.survicate.surveys.Survicate");
        static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        static AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        public static void SetWorkspaceKey(string key)
        {
            survicate.CallStatic("setWorkspaceKey", key);
        }

        public static void Initialize()
        {
            survicate.CallStatic("init", context);
        }

        public static void EnterScreen(string screescnKey)
        {
            survicate.CallStatic("enterScreen", screenKey);
        }

        public static void LeaveScreen(string screenKey)
        {
            survicate.CallStatic("leaveScreen", screenKey);
        }

        public static void InvokeEvent(string eventName)
        {
            survicate.CallStatic("invokeEvent", eventName);
        }

        public static void SetUserTrait(string traitKey, string traitValue)
        {
            survicate.CallStatic("setUserTrait", traitKey, traitValue);
        }

        public static void Reset()
        {
            survicate.CallStatic("reset");
        }
    }
}

#endif
