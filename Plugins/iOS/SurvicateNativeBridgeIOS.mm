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

void invokeEvent(const char* eventName, const char* eventProperties)
{
    NSString *jsonString = [[NSString alloc] initWithUTF8String:eventProperties];
    if(jsonString == NULL || jsonString.length == 0) {
        [SurvicateSdk.shared invokeEventWithName:[NSString stringWithUTF8String:eventName] with:@{}];
    } else {
        NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
        NSError *error;
        NSDictionary *jsonDict = [NSJSONSerialization JSONObjectWithData:jsonData options:0 error:&error];
        if (!error) {
            [SurvicateSdk.shared invokeEventWithName:[NSString stringWithUTF8String:eventName] with:jsonDict];
        }
    }
}

void setUserTrait(const char* traitKey, const char* traitValue)
{
    [SurvicateSdk.shared setUserTraitWithName:[NSString stringWithUTF8String:traitKey] value:[NSString stringWithUTF8String:traitValue]];
}

void reset()
{
    [SurvicateSdk.shared reset];
}

void setLocale(const char* locale)
{
    [SurvicateSdk.shared setLocale:[NSString stringWithUTF8String:locale]];
}

void setThemeMode(const char* mode)
{
    NSString *modeString = [NSString stringWithUTF8String:mode];
    ThemeMode themeMode;

    if ([modeString isEqualToString:@"LIGHT"]) {
        themeMode = ThemeModeLight;
    } else if ([modeString isEqualToString:@"DARK"]) {
        themeMode = ThemeModeDark;
    } else {
        themeMode = ThemeModeAuto;
    }

    [SurvicateSdk.shared setThemeMode:themeMode];
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