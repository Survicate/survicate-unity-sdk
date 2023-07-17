import com.unity3d.player.UnityPlayer;
import com.survicate.surveys.Survicate;
import android.content.Context;

public class SurvicateNativeBridgeAndroid {
    public static void setWorkspaceKey(String workspaceKey) {
        Survicate.setWorkspaceKey(workspaceKey);
    }

    public static void initialize(Context activity) {
        Survicate.init(activity);
    }
}