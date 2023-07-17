#import <Foundation/Foundation.h>
#import <Survicate/Survicate-Swift.h>

extern "C" {
void setWorkspaceKey(const char* appKey){
    [SurvicateSdk.shared setWorkspaceKey:[NSString stringWithUTF8String:appKey], error: nil];
    [SurvicateSdk.shared initialize];
}
}
