public interface SurvicateNativeEventListener {
    void onSurveyDisplayed(String json);
    void onQuestionAnswered(String json);
    void onSurveyClosed(String json);
    void onSurveyCompleted(String json);
}
