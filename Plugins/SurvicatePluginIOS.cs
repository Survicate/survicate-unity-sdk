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
    }
}

#endif