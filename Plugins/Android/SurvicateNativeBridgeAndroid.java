import com.survicate.surveys.QuestionAnsweredEvent;
import com.survicate.surveys.SurveyClosedEvent;
import com.survicate.surveys.SurveyCompletedEvent;
import com.survicate.surveys.SurveyDisplayedEvent;
import com.survicate.surveys.SurvicateEventListener;
import com.unity3d.player.UnityPlayer;
import com.survicate.surveys.Survicate;
import com.survicate.surveys.traits.UserTrait;
import com.survicate.surveys.ThemeMode;
import android.content.Context;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Iterator;
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

    public static void invokeEvent(String eventName, String eventProperties) {
        try {
            Survicate.invokeEvent(eventName, convertJsonToMap(eventProperties));
        } catch (JSONException e) {
            // silent
        }
    }

    public static void setUserTrait(String traitKey, String traitValue) {
        UserTrait trait = new UserTrait(traitKey, traitValue);
        Survicate.setUserTrait(trait);
    }

    public static void reset() {
        Survicate.reset();
    }

    public static void setLocale(String locale) {
        Survicate.setLocale(locale);
    }

    public static void setThemeMode(String mode) {
        ThemeMode themeMode;
        switch (mode) {
            case "LIGHT":
                themeMode = ThemeMode.LIGHT;
                break;
            case "DARK":
                themeMode = ThemeMode.DARK;
                break;
            case "AUTO":
            default:
                themeMode = ThemeMode.AUTO;
                break;
        }
        Survicate.setThemeMode(themeMode);
    }

    public static void addSurvicateEventListener(SurvicateNativeEventListener listener) {
        nativeListener = new SurvicateEventListener() {
            @Override
            public void onSurveyDisplayed(SurveyDisplayedEvent event) {
                Map<String, String> map = new HashMap<>();
                map.put("surveyId", event.getSurveyId());
                JSONObject json = new JSONObject(map);

                listener.onSurveyDisplayed(json.toString());
            }

            @Override
            public void onQuestionAnswered(QuestionAnsweredEvent event) {
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
            public void onSurveyClosed(SurveyClosedEvent event) {
                Map<String, String> map = new HashMap<>();
                map.put("surveyId", event.getSurveyId());
                JSONObject json = new JSONObject(map);

                listener.onSurveyClosed(json.toString());
            }

            @Override
            public void onSurveyCompleted(SurveyCompletedEvent event) {
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

    private static Map<String, String> convertJsonToMap(String json) throws JSONException {
        if (json == null || json.isEmpty()) {
            return new HashMap<>();
        }

        Map<String, String> map = new HashMap<>();

        JSONObject jsonObject = new JSONObject(json);
        for (Iterator<String> it = jsonObject.keys(); it.hasNext(); ) {
            String key = it.next();
            map.put(key, jsonObject.getString(key));
        }

        return map;
    }
}
