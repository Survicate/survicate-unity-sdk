#import <Survicate/Survicate-Swift.h>
#import <SurvicateNativeListener.h>
#import <CoreText/CoreText.h>

static NSString* registerFontAndGetPostScriptName(NSString *relativePath)
{
    // Build absolute path to the font file inside the app bundle
    NSString *fullPath = [[NSBundle mainBundle].bundlePath
        stringByAppendingPathComponent:[@"Data/Raw"
        stringByAppendingPathComponent:relativePath]];

    NSURL *fontURL = [NSURL fileURLWithPath:fullPath];

    // Read font descriptors from the file to extract metadata
    CFArrayRef descriptors = CTFontManagerCreateFontDescriptorsFromURL((__bridge CFURLRef)fontURL);
    if (!descriptors || CFArrayGetCount(descriptors) == 0) {
        if (descriptors) CFRelease(descriptors);
        return @"";
    }

    // Extract the PostScript name from the first font face in the file
    CTFontDescriptorRef descriptor = (CTFontDescriptorRef)CFArrayGetValueAtIndex(descriptors, 0);
    NSString *postScriptName = (__bridge_transfer NSString *)
        CTFontDescriptorCopyAttribute(descriptor, kCTFontNameAttribute);
    CFRelease(descriptors);

    return postScriptName ?: @"";
}

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

void setResponseAttributes(const char* attributesJson)
{
    NSString *jsonString = [NSString stringWithUTF8String:attributesJson];
    NSData *jsonData = [jsonString dataUsingEncoding:NSUTF8StringEncoding];
    NSError *error;
    NSArray *array = [NSJSONSerialization JSONObjectWithData:jsonData options:0 error:&error];
    if (error || !array) return;

    NSMutableArray<ResponseAttribute *> *list = [NSMutableArray array];
    for (NSDictionary *dict in array) {
        NSString *name = dict[@"name"];
        NSString *value = dict[@"value"];
        NSString *provider = dict[@"provider"];
        if ([provider length] == 0) provider = nil;
        ResponseAttribute *attr = [[ResponseAttribute alloc] initWithName:name value:value provider:provider];
        [list addObject:attr];
    }
    [[SurvicateSdk shared] setResponseAttributes:list];
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

void setFonts(const char* regular, const char* regularItalic, const char* bold, const char* boldItalic)
{
    NSString *regularName       = registerFontAndGetPostScriptName([NSString stringWithUTF8String:regular]);
    NSString *regularItalicName = registerFontAndGetPostScriptName([NSString stringWithUTF8String:regularItalic]);
    NSString *boldName          = registerFontAndGetPostScriptName([NSString stringWithUTF8String:bold]);
    NSString *boldItalicName    = registerFontAndGetPostScriptName([NSString stringWithUTF8String:boldItalic]);

    SurvicateFontSystem *fontSystem = [[SurvicateFontSystem alloc]
        initWithRegular:regularName
        regularItalic:regularItalicName
        bold:boldName
        boldItalic:boldItalicName];
    [SurvicateSdk.shared setFonts:fontSystem];
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