#import <Survicate/Survicate-Swift.h>

typedef void (*SurveyDisplayedCallback)(const char* message);
typedef void (*QuestionAnsweredCallback)(const char* message);
typedef void (*SurveyClosedCallback)(const char* message);
typedef void (*SurveyCompletedCallback)(const char* message);

@interface SurvicateNativeListener : NSObject<SurvicateDelegate>
+ (instancetype)shared;
- (void)addListener;
- (void)removeListener;
- (void)registerSurveyDisplayedCallback:(SurveyDisplayedCallback)callback;
- (void)registerQuestionAnsweredCallback:(QuestionAnsweredCallback)callback;
- (void)registerSurveyClosedCallback:(SurveyClosedCallback)callback;
- (void)registerSurveyCompletedCallback:(SurveyCompletedCallback)callback;
@end
