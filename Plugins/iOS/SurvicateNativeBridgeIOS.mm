#import <Survicate/Survicate-Swift.h>
#import <SurvicateNativeListener.h>

extern "C"
{
void setWorkspaceKey(const char* key)
{
    [SurvicateSdk.shared setWorkspaceKey:[NSString stringWithUTF8String:key] error: nil];
}

void initialize()
{
    [SurvicateSdk.shared initialize];
    [SurvicateNativeListener.shared addListener];
}

void enterScreen(const char* screenKey)
{
    [SurvicateSdk.shared enterScreenWithValue:[NSString stringWithUTF8String:screenKey]];
}

void leaveScreen(const char* screenKey)
{
    [SurvicateSdk.shared leaveScreenWithValue:[NSString stringWithUTF8String:screenKey]];
}

void invokeEvent(const char* eventName)
{
    [SurvicateSdk.shared invokeEventWithName:[NSString stringWithUTF8String:eventName]];
}

void setUserTrait(const char* traitKey, const char* traitValue)
{
    [SurvicateSdk.shared setUserTraitWithName:[NSString stringWithUTF8String:traitKey] value:[NSString stringWithUTF8String:traitValue]];
}

void reset()
{
    [SurvicateSdk.shared reset];
}

void addSurvicateEventListener() 
{
    [SurvicateNativeListener.shared addListener];
}

void removeSurvicateEventListener() 
{
    [SurvicateNativeListener.shared removeListener];
}

void setSurveyDisplayedCallback(SurveyDisplayedCallback callback)
{
    [SurvicateNativeListener.shared registerSurveyDisplayedCallback:callback];
}

void setQuestionAnsweredCallback(QuestionAnsweredCallback callback)
{
    [SurvicateNativeListener.shared registerQuestionAnsweredCallback:callback];
}

void setSurveyClosedCallback(SurveyClosedCallback callback)
{
    [SurvicateNativeListener.shared registerSurveyClosedCallback:callback];
}

void setSurveyCompletedCallback(SurveyCompletedCallback callback)
{
    [SurvicateNativeListener.shared registerSurveyCompletedCallback:callback];
}
}