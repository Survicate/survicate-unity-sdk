using System;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Survicate
{
    [System.Serializable]
    public class SurveyDisplayedEvent
    {

        public string surveyId;

        public SurveyDisplayedEvent(string surveyId)
        {
            this.surveyId = surveyId;
        }
    }

    [Serializable]
    public class QuestionAnsweredEvent
    {
        public String surveyId;
        public String surveyName;
        public String visitorUuid;
        public String responseUuid;
        public long questionId;
        public String question;
        public SurvicateAnswer answer;
        public String panelAnswerUrl;

        public QuestionAnsweredEvent(string surveyId, string surveyName, string visitorUuid, string responseUuid, long questionId, string question, SurvicateAnswer answer, string panelAnswerUrl)
        {
            this.surveyId = surveyId;
            this.surveyName = surveyName;
            this.visitorUuid = visitorUuid;
            this.responseUuid = responseUuid;
            this.questionId = questionId;
            this.question = question;
            this.answer = answer;
            this.panelAnswerUrl = panelAnswerUrl;
        }
    }

    [Serializable]
    public class SurvicateAnswer : ISerializationCallbackReceiver
    {
        public String type;
        public String idSerialized;
        [System.NonSerialized]
        public long? id;
        public List<long> ids;
        public String value;

        public SurvicateAnswer(String type, long? id, List<long> ids, String value)
        {
            this.type = type;
            this.id = id;
            this.ids = ids;
            this.value = value;
        }

        // Empty implementation because we don't need to do anything before serialization
        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            if (string.IsNullOrEmpty(idSerialized))
            {
                id = null;
            }
            else
            {
                bool isParsed = long.TryParse(idSerialized, out long parsedValue);
                id = isParsed ? (long?)parsedValue : null;
            }
        }
    }

    [Serializable]
    public class SurveyClosedEvent
    {
        public string surveyId;

        public SurveyClosedEvent(string surveyId)
        {
            this.surveyId = surveyId;
        }
    }

    [Serializable]
    public class SurveyCompletedEvent
    {
        public string surveyId;

        public SurveyCompletedEvent(string surveyId)
        {
            this.surveyId = surveyId;
        }
    }
}
