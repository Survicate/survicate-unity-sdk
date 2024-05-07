#import <SurvicateNativeListener.h>
#import <Survicate/Survicate-Swift.h>

@implementation SurvicateNativeListener {
    SurveyDisplayedCallback _didSurveyDisplay;
    QuestionAnsweredCallback _didQuestionAnswered;
    SurveyCompletedCallback _didSurveyCompleted;
    SurveyClosedCallback _didSurveyClosed;
    bool _hasListeners;
}

static SurvicateNativeListener *shared = nil;

+ (instancetype)shared {
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        shared = [[self alloc] init];
    });
    return shared;
}

- (instancetype)init {
    if (self = [super init]) {
        self->_hasListeners = false;
    }
    return self;
}

- (NSArray<NSString*> *)supportedEvents {
    return @[@"onQuestionAnswered", @"onSurveyClosed", @"onSurveyCompleted", @"onSurveyDisplayed"];
}

- (void)addListener {
    _hasListeners = true;
    [[SurvicateSdk shared] addListener:self];
}

- (void)removeListener {
    _hasListeners = false;
    [[SurvicateSdk shared] removeListener:self];
}

- (void)registerSurveyDisplayedCallback:(SurveyDisplayedCallback)callback {
    _didSurveyDisplay = callback;
}

- (void)registerQuestionAnsweredCallback:(QuestionAnsweredCallback)callback {
    _didQuestionAnswered = callback;
}

- (void)registerSurveyCompletedCallback:(SurveyCompletedCallback)callback {
    _didSurveyCompleted = callback;
}

- (void)registerSurveyClosedCallback:(SurveyClosedCallback)callback {
    _didSurveyClosed = callback;
}

- (void)surveyDisplayedWithEvent:(SurveyDisplayedEvent * _Nonnull)event {
    if (!_hasListeners || _didSurveyDisplay == nil) return;
    NSDictionary *dictionary = @{
        @"surveyId": event.surveyId
    };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:nil];
    if(jsonData) {
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        const char *message = [jsonString UTF8String];
        _didSurveyDisplay(message);
    }
}

- (void)questionAnswered:(QuestionAnsweredEvent * _Nonnull)event {
    if (!_hasListeners || _didQuestionAnswered == nil) return;
    NSDictionary *answerDictionary = @{
        @"value": event.answer.value ?: [NSNull null],
        @"idSerialized": event.answer.id ?: [NSNull null],
        @"type": event.answer.type ?: [NSNull null],
        @"ids": event.answer.ids ?: @[],
    };
    NSDictionary *dictionary = @{
        @"surveyId": event.surveyId,
        @"surveyName": event.surveyName,
        @"visitorUuid": event.visitorUUID,
        @"responseUuid": event.responseUUID,
        @"questionId": @(event.questionID),
        @"question": event.question,
        @"answer": answerDictionary,
        @"panelAnswerUrl": event.panelAnswerUrl,
    };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:nil];
    
    if(jsonData) {
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        const char *message = [jsonString UTF8String];
        _didQuestionAnswered(message);
    }
}

- (void)surveyCompletedWithEvent:(SurveyCompletedEvent * _Nonnull)event {
    if (!_hasListeners || _didSurveyCompleted == nil) return;
    NSDictionary *dictionary = @{
        @"surveyId": event.surveyId
    };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:nil];
    
    if(jsonData) {
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        const char *message = [jsonString UTF8String];
        _didSurveyCompleted(message);
    }
}

- (void)surveyClosedWithEvent:(SurveyClosedEvent * _Nonnull)event {
    if (!_hasListeners || _didSurveyClosed == nil) return;
    NSDictionary *dictionary = @{
        @"surveyId": event.surveyId
    };
    NSData *jsonData = [NSJSONSerialization dataWithJSONObject:dictionary options:0 error:nil];
    
    if(jsonData) {
        NSString *jsonString = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        const char *message = [jsonString UTF8String];
        _didSurveyClosed(message);
    }
}

@end
