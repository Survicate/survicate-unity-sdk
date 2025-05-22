#if UNITY_IOS
using System;
using System.Collections.Generic;

namespace Plugins.Survicate
{
    using UnityEngine;
    using System.Runtime.InteropServices;

    public class Survicate
    {
        private static List<SurvicateEventListener> listeners = new List<SurvicateEventListener>();

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
        private static extern void invokeEvent(string eventName, string eventPropertiesJson);

        public static void InvokeEvent(string eventName)
        {
            InvokeEvent(eventName, new Dictionary<string, string>());
        }

        public static void InvokeEvent(string eventName, Dictionary<string, string> eventProperties)
        {
            invokeEvent(eventName, SurvicateSerializer.serializeDictionary(eventProperties));
        }

        [DllImport("__Internal")]
        private static extern void setUserTrait(string traitKey, string traitValue);

        [Obsolete("SetUserTrait(string, string) is deprecated, please use SetUserTrait(UserTrait) instead.")]
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

        [DllImport("__Internal")]
        private static extern void setLocale(string locale);
        
        public static void SetLocale(string locale)
        {
            if (locale != null)
            {
                setLocale(locale);
            } 
            else 
            {
                setLocale("");
            }
        }

        [DllImport("__Internal")]
        private static extern void addSurvicateEventListener();

        public static void AddSurvicateEventListener(SurvicateEventListener listener)
        {
            listeners.Add(listener);

            if(listeners.Count == 1)
            {
                addSurvicateEventListener();
                setSurveyDisplayedCallback(OnSurveyDisplayedCallback);
                setQuestionAnsweredCallback(OnQuestionAnsweredCallback);
                setSurveyClosedCallback(OnSurveyClosedCallback);
                setSurveyCompletedCallback(OnSurveyCompletedCallback);
            }    
        }

        [DllImport("__Internal")]
        private static extern void removeSurvicateEventListener();

        public static void RemoveSurvicateEventListener(SurvicateEventListener listener)
        {
            listeners.Remove(listener);

            if(listeners.Count == 0) 
            {
                removeSurvicateEventListener();
                setSurveyDisplayedCallback(null);
                setQuestionAnsweredCallback(null);
                setSurveyClosedCallback(null);
                setSurveyCompletedCallback(null);
            }
        }

        delegate void SurveyDisplayedCallback(string json);

        [AOT.MonoPInvokeCallback(typeof(SurveyDisplayedCallback))]
        static void OnSurveyDisplayedCallback(string json)
        {
            SurveyDisplayedEvent @event = JsonUtility.FromJson<SurveyDisplayedEvent>(json);
            listeners.ForEach(listener => listener.OnSurveyDisplayed(@event));
        }

        [DllImport("__Internal")]
        static extern void setSurveyDisplayedCallback(SurveyDisplayedCallback callback);

        delegate void QuestionAnsweredCallback(string json);

        [AOT.MonoPInvokeCallback(typeof(QuestionAnsweredCallback))]
        static void OnQuestionAnsweredCallback(string json)
        {
            QuestionAnsweredEvent @event = JsonUtility.FromJson<QuestionAnsweredEvent>(json);
            listeners.ForEach(listener => listener.OnQuestionAnswered(@event));
        }

        [DllImport("__Internal")]
        static extern void setQuestionAnsweredCallback(QuestionAnsweredCallback callback);

        delegate void SurveyClosedCallback(string json);

        [AOT.MonoPInvokeCallback(typeof(SurveyClosedCallback))]
        static void OnSurveyClosedCallback(string json)
        {
            SurveyClosedEvent @event = JsonUtility.FromJson<SurveyClosedEvent>(json);
            listeners.ForEach(listener => listener.OnSurveyClosed(@event));
        }

        [DllImport("__Internal")]
        static extern void setSurveyClosedCallback(SurveyClosedCallback callback);

        delegate void SurveyCompletedCallback(string json);

        [AOT.MonoPInvokeCallback(typeof(SurveyCompletedCallback))]
        static void OnSurveyCompletedCallback(string json)
        {
            SurveyCompletedEvent @event = JsonUtility.FromJson<SurveyCompletedEvent>(json);
            listeners.ForEach(listener => listener.OnSurveyCompleted(@event));
        }

        [DllImport("__Internal")]
        static extern void setSurveyCompletedCallback(SurveyCompletedCallback callback);
    }
}

#endif
