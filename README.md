# Survicate mobile SDK unity wrapper

## Installation

### iOS

- Add content of the iOS directory to your Assets/Plugins/iOS
- Add SurvicatePluginIOS.cs and file inside your Assets/Plugins
- Download the latest iOS SDK from [here](https://repo.survicate.com/latest/ios/Survicate.zip). Copy this file to your xcode project folder.

Inside your exported Xcode project, on **Build Phases -> Link Binary With Libraries**, add

- survicate.xcframework

### Android

- Add content of the Android directory to your Assets/Plugins/Android
- Add SurvicatePluginAndroid.cs file inside your Assets/Plugins
- Define `https://repo.survicate.com` Maven repository in the project
- Add Survicate SDK dependency to your app's `build.gradle` file.

## Configuration

### Configuration for Android

1. Configure your *workspace key* in `AndroidManifest.xml` file.

```xml {{title: 'AndroidManifest.xml'}}
<application
    android:name=".MyApp"
>
    <!-- ... -->
    <meta-data android:name="com.survicate.surveys.workspaceKey" android:value="YOUR_WORKSPACE_KEY"/>
</application>
```

2. Define `https://repo.survicate.com` Maven repository in one of the following ways:

```groovy
// settingsTemplate.gradle
dependencyResolutionManagement {
    // ...
    repositories {
        // ...
        maven { url 'https://repo.survicate.com' }
    }
}
```

```groovy
// mainTemplate.gradle
allprojects {
    repositories {
        // ...
        maven { url 'https://repo.survicate.com' }
    }
}
```

3. Add Survicate SDK dependency to your app's `build.gradle` file.

```groovy
// mainTemplate.gradle
dependencies {
    // ...
    implementation 'com.survicate:survicate-sdk:latest.release'
}
```

### Configuration for iOS

1. Add workspace key to your `Info.plist` file.
   - Create `Survicate` *Dictionary*.
   - Define `WorkspaceKey` *String* in `Survicate` *Dictionary*.
   Your `Info.plist` file should looks like this:
   ![Info.plist example](/ios-infoplist.png)
2. Run `pod update` in your `ios` directory.

### Initialization

Initialize the SDK in your application using `initializeSdk()` method. Call this method only once, in the main script of your project.

---

## Usage

On your C# script, import

```csharp
using Plugins.Survicate;

Survicate.SetWorkspaceKey("your_workspace_key");
Survicate.Initialize();
Survicate.EnterScreen("your_screen_key");
Survicate.LeaveScreen("your_screen_key");
Survicate.InvokeEvent("your_event_name");
Dictionary<string, string> eventProperties = new Dictionary<string, string>();
eventProperties.Add("property1", "value1");
eventProperties.Add("property2", "value2");
Survicate.InvokeEvent("your_event_name", eventProperties);
Survicate.SetUserTrait(new UserTrait("name", "John"));
Survicate.SetUserTrait(new UserTrait("age", 25));
Survicate.SetUserTrait(new UserTrait("count", 0.1));
Survicate.SetUserTrait(new UserTrait("isActive", true));
Survicate.SetUserTrait(new UserTrait("birthDate", DateTime.Now));
Survicate.SetLocale("en-US")
Survicate.Reset();
SurvicateEventListener survicateEventListener = new SurvicateEventListener(
    (SurveyDisplayedEvent event) => /* implement action */,
    (QuestionAnsweredEvent event) => /* implement action */,
    (SurveyClosedEvent event) => /* implement action */,
    (SurveyCompletedEvent event) => /* implement action */
);
Survicate.AddSurvicateEventListener(survicateEventListener);
Survicate.RemoveSurvicateEventListener(survicateEventListener);
```