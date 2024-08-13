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
- Download the latest android SDK from [here](https://repo.survicate.com/latest/android/Survicate.aar). Place the downloaded AAR file inside Assets/Plugins/Android
- Make sure **Android** is selected as plugin platform on **Inspector window**
  > Add dependencies listed in developers documentations [here](https://developers.survicate.com/mobile-sdk/android/#installing-manually)

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

2. Add missing Survicate dependencies to your project `build.gradle` located under `android` directory.

```groovy {{title: "Project's build.gradle" }}
dependencies {
    // ...
    implementation files('libs/Survicate.aar')
    implementation "androidx.appcompat:appcompat:1.6.1"
    implementation "androidx.cardview:cardview:1.0.0"
    implementation "androidx.recyclerview:recyclerview:1.3.2"
    implementation "androidx.constraintlayout:constraintlayout:2.1.4"
    implementation "androidx.transition:transition:1.4.1"
    implementation "androidx.annotation:annotation:1.7.1"
    implementation 'org.jetbrains.kotlinx:kotlinx-coroutines-android:1.8.0"
    implementation "com.squareup.moshi:moshi:1.15.1"
    implementation "com.squareup.moshi:moshi-kotlin:1.15.1"

    // since SDK version 2.0.0
    implementation "io.coil-kt:coil-base:2.4.0"
    implementation "io.coil-kt:coil-gif:2.4.0"
    implementation "io.coil-kt:coil-svg:2.4.0"

    // only up to SDK version 3.0.0
    implementation "com.google.android.material:material:1.5.0"
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