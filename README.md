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

For intializing a survicate SDK;

```
  Survicate.SetWorkspaceKey({{ WORKSPACE_KEY }});
  Survicate.Initialize();
```
