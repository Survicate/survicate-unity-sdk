#if UNITY_ANDROID
using System;
using System.Collections.Generic;

namespace Plugins.Survicate
{
    using UnityEngine;

    public class Survicate
    {
        static AndroidJavaObject survicate = new AndroidJavaClass("SurvicateNativeBridgeAndroid");
        static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        static AndroidJavaObject context = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        private static SurvicateListenerProxy proxyListener = new SurvicateListenerProxy();

        public static void SetWorkspaceKey(string key)
        {
            survicate.CallStatic("setWorkspaceKey", key);
        }

        public static void Initialize()
        {
            survicate.CallStatic("initialize", context);
        }

        public static void EnterScreen(string screenKey)
        {
            survicate.CallStatic("enterScreen", screenKey);
        }

        public static void LeaveScreen(string screenKey)
        {
            survicate.CallStatic("leaveScreen", screenKey);
        }

        public static void InvokeEvent(string eventName)
        {
            InvokeEvent(eventName, new Dictionary<string, string>());
        }

        public static void InvokeEvent(string eventName, Dictionary<string, string> eventProperties)
        {
            survicate.CallStatic("invokeEvent", eventName, SurvicateSerializer.serializeDictionary(eventProperties));
        }

        [Obsolete("SetUserTrait(string, string) is deprecated, please use SetUserTrait(UserTrait) instead.")]
        public static void SetUserTrait(string traitKey, string traitValue)
        {
            survicate.CallStatic("setUserTrait", traitKey, traitValue);
        }

        public static void SetUserTrait(UserTrait trait)
        {
            survicate.CallStatic("setUserTrait", trait.key, trait.value);
        }

        public static void Reset()
        {
            survicate.CallStatic("reset");
        }

        public static void AddSurvicateEventListener(SurvicateEventListener listener)
        {
            proxyListener.addListener(listener);
            if (proxyListener.getListenerCount() == 1)
            {
                survicate.CallStatic("addSurvicateEventListener", proxyListener);
            }
        }

        public static void RemoveSurvicateEventListener(SurvicateEventListener listener)
        {
            proxyListener.removeListener(listener);
            if (proxyListener.getListenerCount() == 0)
            {
                survicate.CallStatic("removeSurvicateEventListener", proxyListener);
            }
        }
    }

    class SurvicateListenerProxy : AndroidJavaProxy
    {
        private List<SurvicateEventListener> survicateListener = new List<SurvicateEventListener>();

        public SurvicateListenerProxy() : base("SurvicateNativeEventListener") { }

        public void addListener(SurvicateEventListener listener)
        {
            survicateListener.Add(listener);
        }

        public void removeListener(SurvicateEventListener listener)
        {
            survicateListener.Remove(listener);
        }

        public int getListenerCount()
        {
            return survicateListener.Count;
        }

        public void onSurveyDisplayed(String json)
        {
            foreach (var listener in survicateListener)
            {
                SurveyDisplayedEvent @event = JsonUtility.FromJson<SurveyDisplayedEvent>(json);
                listener.OnSurveyDisplayed(@event);
            }
        }

        public void onQuestionAnswered(String json)
        {
            foreach (var listener in survicateListener)
            {
                QuestionAnsweredEvent @event = JsonUtility.FromJson<QuestionAnsweredEvent>(json);
                listener.OnQuestionAnswered(@event);
            }
        }

        public void onSurveyClosed(String json)
        {
            foreach (var listener in survicateListener)
            {
                SurveyClosedEvent @event = JsonUtility.FromJson<SurveyClosedEvent>(json);
                listener.OnSurveyClosed(@event);
            }
        }

        public void onSurveyCompleted(String json)
        {
            foreach (var listener in survicateListener)
            {
                SurveyCompletedEvent @event = JsonUtility.FromJson<SurveyCompletedEvent>(json);
                listener.OnSurveyCompleted(@event);
            }
        }
    }
}

#endif
