; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{B8DDA10D-2B16-4FF8-8CF5-1CE2831C61BA}
AppName=Space Breaker
AppVersion=1.0
;AppVerName=Space Breaker 1.0
AppPublisher=Raphael DUCHOSSOY
AppPublisherURL=https://statoondeo.itch.io/space-breaker
AppSupportURL=https://statoondeo.itch.io/space-breaker
AppUpdatesURL=https://statoondeo.itch.io/space-breaker
DefaultDirName={autopf}\Space Breaker
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputBaseFilename=Space Breaker
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\rapha\source\repos\Breakout\Testjeux\bin\Release\netcoreapp3.1\Breakout.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\rapha\source\repos\Breakout\Testjeux\bin\Release\netcoreapp3.1\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\Space Breaker"; Filename: "{app}\Breakout.exe"
Name: "{autodesktop}\Space Breaker"; Filename: "{app}\Breakout.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\Breakout.exe"; Description: "{cm:LaunchProgram,Space Breaker}"; Flags: nowait postinstall skipifsilent

