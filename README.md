# Surivcate mobile SDK unity wrapper

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

## Usage

On your C# script, import

```
using Plugins.Survicate;
```

SDK methods:

- **SetWorkspaceKey(string key)**: This method is used to set the workspace key.

```csharp
  Survicate.SetWorkspaceKey("your_workspace_key");
```

- **Initialize()**: This method is used to initialize the Survicate SDK.

```csharp
  Survicate.Initialize();
```

- **EnterScreen(string screenKey)**: This method is used to set the current screen.

```csharp
  Survicate.EnterScreen("your_screen_key");
```

- **LeaveScreen(string screenKey)**: This method is used when the user leaves the current screen.

```csharp
  Survicate.LeaveScreen("your_screen_key");
```

- **InvokeEvent(string eventName)**: This method is used to invoke an event.

```csharp
  Survicate.InvokeEvent("your_event_name");
```

- **SetUserTrait(string traitKey, string traitValue)**: This method is used to set user traits.

```csharp
  Survicate.SetUserTrait("your_trait_key", "your_trait_value");
```

- **Reset()**: This method is used to reset the Survicate SDK.

```csharp
  Survicate.Reset();
```
