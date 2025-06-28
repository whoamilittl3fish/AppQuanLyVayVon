[Setup]
AppName=GenKeyPortable
AppVersion=1.0
DefaultDirName={userdocs}\GenKey
OutputDir=Output
OutputBaseFilename=GenKey_Portable
CreateAppDir=yes
DisableProgramGroupPage=yes
Uninstallable=no
Compression=lzma
SolidCompression=yes
PrivilegesRequired=none

[Files]
Source: "C:\Users\khoan\source\repos\whoamilittl3fish\GenKey\bin\Debug\GenKey.exe"; DestDir: "{app}"; Flags: ignoreversion

[Run]
Filename: "{app}\GenKey.exe"; Description: "Chạy GenKey ngay"; Flags: nowait postinstall skipifsilent
