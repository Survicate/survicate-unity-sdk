using System;
using System.Collections.Generic;

namespace Plugins.Survicate
{
    public class SurvicateEventListener
    {
        private readonly Action<SurveyDisplayedEvent> _onSurveyDisplayed;
        private readonly Action<QuestionAnsweredEvent> _onQuestionAnswered;
        private readonly Action<SurveyClosedEvent> _onSurveyClosed;
        private readonly Action<SurveyCompletedEvent> _onSurveyCompleted;

        public SurvicateEventListener(
            Action<SurveyDisplayedEvent> onSurveyDisplayed = null,
            Action<QuestionAnsweredEvent> onQuestionAnswered = null,
            Action<SurveyClosedEvent> onSurveyClosed = null,
            Action<SurveyCompletedEvent> onSurveyCompleted = null
        )
        {
            _onSurveyDisplayed = onSurveyDisplayed;
            _onQuestionAnswered = onQuestionAnswered;
            _onSurveyClosed = onSurveyClosed;
            _onSurveyCompleted = onSurveyCompleted;
        }

        public void OnSurveyDisplayed(SurveyDisplayedEvent surveyDisplayedEvent)
        {
            _onSurveyDisplayed?.Invoke(surveyDisplayedEvent);
        }

        public void OnQuestionAnswered(QuestionAnsweredEvent questionAnsweredEvent)
        {
            _onQuestionAnswered?.Invoke(questionAnsweredEvent);
        }

        public void OnSurveyClosed(SurveyClosedEvent surveyClosedEvent)
        {
            _onSurveyClosed?.Invoke(surveyClosedEvent);
        }

        public void OnSurveyCompleted(SurveyCompletedEvent surveyCompletedEvent)
        {
            _onSurveyCompleted?.Invoke(surveyCompletedEvent);
        }
    }
}
