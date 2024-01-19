#if UNITY_IOS

namespace Plugins.Survicate
{
    using UnityEngine;
    using System.Runtime.InteropServices;

    public class Survicate
    {
        [DllImport("__Internal")]
        private static extern void setWorkspaceKey(string key);

        public static void SetWorkspaceKey(string key)
        {
            setWorkspaceKey(key);
        }

        [DllImport("__Internal")]
        private static extern void initialize();

        public static void Initialize()
        {
            initialize();
        }

        [DllImport("__Internal")]
        private static extern void enterScreen(string screenKey);

        public static void EnterScreen(string screenKey)
        {
            enterScreen(screenKey);
        }

        [DllImport("__Internal")]
        private static extern void leaveScreen(string screenKey);

        public static void LeaveScreen(string screenKey)
        {
            leaveScreen(screenKey);
        }

        [DllImport("__Internal")]
        private static extern void invokeEvent(string eventName);

        public static void InvokeEvent(string eventName)
        {
            invokeEvent(eventName);
        }

        [DllImport("__Internal")]
        private static extern void setUserTrait(string traitKey, string traitValue);

        public static void SetUserTrait(string traitKey, string traitValue)
        {
            setUserTrait(traitKey, traitValue);
        }

        public static void SetUserTrait(UserTrait trait)
        {
            setUserTrait(trait.key, trait.value);
        }

        [DllImport("__Internal")]
        private static extern void reset();

        public static void Reset()
        {
            reset();
        }
    }
}

#endif
