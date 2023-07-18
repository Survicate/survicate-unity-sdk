import com.unity3d.player.UnityPlayer;
import com.survicate.surveys.Survicate;
import com.survicate.surveys.traits.UserTrait;
import android.content.Context;

public class SurvicateNativeBridgeAndroid {

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
}
