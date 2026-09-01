# Survicate mobile SDK unity wrapper

## Installation

Clone or download this repository, then copy all `.cs` files from the [`Plugins`](Plugins) directory to `Assets/Plugins` in your Unity project. `SurvicatePluginIOS.cs` and `SurvicatePluginAndroid.cs` are wrapped in `#if UNITY_IOS` / `#if UNITY_ANDROID`, so copying the whole set is safe even if you build for one platform only. The platform-specific native files come next.

### iOS

- Copy the files from the [`Plugins/iOS`](Plugins/iOS) directory (`SurvicateNativeBridgeIOS.mm`, `SurvicateNativeListener.h`, `SurvicateNativeListener.m`) to `Assets/Plugins/iOS` in your Unity project.
- Download the latest iOS SDK from [here](https://repo.survicate.com/latest/ios/Survicate.zip), unzip it and copy `Survicate.xcframework` to your exported Xcode project folder.

Inside your exported Xcode project, on **Build Phases -> Link Binary With Libraries**, add

- Survicate.xcframework

### Android

- Copy the files from the [`Plugins/Android`](Plugins/Android) directory (`SurvicateNativeBridgeAndroid.java`, `SurvicateNativeEventListener.java`) to `Assets/Plugins/Android` in your Unity project.
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
   Your `Info.plist` file should look like this:
   ![Info.plist example](https://developers.survicate.com/ios-infoplist.png)
2. Run `pod update` in your `ios` directory.

### Initialization

Initialize the SDK in your application using `Initialize()` method. Call this method only once, in the main script of your project.

---

## Usage

On your C# script, import

```csharp
using Plugins.Survicate;

// Initialization
Survicate.SetWorkspaceKey("your_workspace_key");
Survicate.Initialize();

// Events
Survicate.InvokeEvent("your_event_name");
Dictionary<string, string> eventProperties = new Dictionary<string, string>();
eventProperties.Add("property1", "value1");
eventProperties.Add("property2", "value2");
Survicate.InvokeEvent("your_event_name", eventProperties);

// Screens
Survicate.EnterScreen("your_screen_key");
Survicate.LeaveScreen("your_screen_key");

// User traits
Survicate.SetUserTrait(new UserTrait("name", "John"));
Survicate.SetUserTrait(new UserTrait("age", 25));
Survicate.SetUserTrait(new UserTrait("count", 0.1));
Survicate.SetUserTrait(new UserTrait("isActive", true));
Survicate.SetUserTrait(new UserTrait("birthDate", DateTime.Now));

// Locale
Survicate.SetLocale("en-US");

// Theme
Survicate.SetThemeMode(ThemeMode.Auto); /* ThemeMode.Auto, ThemeMode.Light, ThemeMode.Dark */

// Custom fonts
Survicate.SetFonts(new SurvicateFontSystem(
    "fonts/MyFont-Regular.ttf",
    "fonts/MyFont-RegularItalic.ttf",
    "fonts/MyFont-Bold.ttf",
    "fonts/MyFont-BoldItalic.ttf"
));

// Response attributes
Survicate.SetResponseAttribute(new ResponseAttribute("plan", "premium"));
Survicate.SetResponseAttributes(new List<ResponseAttribute> {
    new ResponseAttribute("plan", "premium", "crm"),
    new ResponseAttribute("isTrialExpired", false),
    new ResponseAttribute("seats", 5),
    new ResponseAttribute("renewalDate", DateTime.Now)
});

// Event listeners
SurvicateEventListener survicateEventListener = new SurvicateEventListener(
    (SurveyDisplayedEvent event) => /* implement action */,
    (QuestionAnsweredEvent event) => /* implement action */,
    (SurveyClosedEvent event) => /* implement action */,
    (SurveyCompletedEvent event) => /* implement action */
);
Survicate.AddSurvicateEventListener(survicateEventListener);
Survicate.RemoveSurvicateEventListener(survicateEventListener);

// Reset
Survicate.Reset();
```

## Issues

Got an Issue?

To make things more streamlined, we’ve transitioned our issue reporting to our customer support platform. If you encounter any bugs or have feedback, please reach out to our customer support team. Your insights are invaluable to us, and we’re here to help ensure your experience is top-notch!

Contact us via Intercom in the application, or drop us an email at: [support@survicate.com]

Thank you for your support and understanding!