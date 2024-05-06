import com.survicate.surveys.QuestionAnsweredEvent;
import com.survicate.surveys.SurveyClosedEvent;
import com.survicate.surveys.SurveyCompletedEvent;
import com.survicate.surveys.SurveyDisplayedEvent;
import com.survicate.surveys.SurvicateEventListener;
import com.unity3d.player.UnityPlayer;
import com.survicate.surveys.Survicate;
import com.survicate.surveys.traits.UserTrait;
import android.content.Context;

import androidx.annotation.NonNull;

import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class SurvicateNativeBridgeAndroid {

    private static SurvicateEventListener nativeListener;

    public static void setWorkspaceKey(String workspaceKey) {
        Survicate.setWorkspaceKey(workspaceKey);
    }

    public static void initialize(Context activity) {
        Survicate.init(activity);
    }

    public static void enterScreen(String screenKey) {
        Survicate.enterScreen(screenKey);
    }

    public static void leaveScreen(String screenKey) {
        Survicate.leaveScreen(screenKey);
    }

    public static void invokeEvent(String eventName) {
        Survicate.invokeEvent(eventName);
    }

    public static void setUserTrait(String traitKey, String traitValue) {
        UserTrait trait = new UserTrait(traitKey, traitValue);
        Survicate.setUserTrait(trait);
    }

    public static void reset() {
        Survicate.reset();
    }

    public static void addSurvicateEventListener(SurvicateNativeEventListener listener) {
        nativeListener = new SurvicateEventListener() {
            @Override
            public void onSurveyDisplayed(@NonNull SurveyDisplayedEvent event) {
                Map<String, String> map = new HashMap<>();
                map.put("surveyId", event.getSurveyId());
                JSONObject json = new JSONObject(map);

                listener.onSurveyDisplayed(json.toString());
            }

            @Override
            public void onQuestionAnswered(@NonNull QuestionAnsweredEvent event) {
                Map<String, Object> answerMap = new HashMap<>();
                answerMap.put("type", event.getAnswer().getType());
                answerMap.put("idSerialized", event.getAnswer().getId());
                answerMap.put("ids", event.getAnswer().getIds());
                answerMap.put("value", event.getAnswer().getValue());
                Map<String, Object> map = new HashMap<>();
                map.put("surveyId", event.getSurveyId());
                map.put("surveyName", event.getSurveyName());
                map.put("responseUuid", event.getResponseUuid());
                map.put("visitorUuid", event.getVisitorUuid());
                map.put("panelAnswerUrl", event.getPanelAnswerUrl());
                map.put("questionId", event.getQuestionId());
                map.put("question", event.getQuestionText());
                map.put("answer", answerMap);
                JSONObject json = new JSONObject(map);

                listener.onQuestionAnswered(json.toString());
            }

            @Override
            public void onSurveyClosed(@NonNull SurveyClosedEvent event) {
                Map<String, String> map = new HashMap<>();
                map.put("surveyId", event.getSurveyId());
                JSONObject json = new JSONObject(map);

                listener.onSurveyClosed(json.toString());
            }

            @Override
            public void onSurveyCompleted(@NonNull SurveyCompletedEvent event) {
                Map<String, String> map = new HashMap<>();
                map.put("surveyId", event.getSurveyId());
                JSONObject json = new JSONObject(map);

                listener.onSurveyCompleted(json.toString());
            }
        };

        Survicate.addEventListener(nativeListener);
    }

    public static void removeSurvicateEventListener() {
        if (nativeListener != null) {
            Survicate.removeEventListener(nativeListener);
        }
    }
}
