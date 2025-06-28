[Setup]
AppName=QuanLyHopDong
AppVersion=1.0
DefaultDirName=C:\Program Files\QuanLyHopDong
OutputDir=Output
OutputBaseFilename=Setup_QuanLyHopDong
PrivilegesRequired=admin
Compression=lzma
SolidCompression=yes
DisableProgramGroupPage=yes

[Files]
Source: "C:\Files\Builds\QuanLyHopDong\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Quản Lý Hợp Đồng"; Filename: "{app}\QuanLyVayVon.exe"
Name: "{group}\Gỡ Cài Đặt Quản Lý Hợp Đồng"; Filename: "{uninstallexe}"
Name: "{userdesktop}\Quản Lý Hợp Đồng"; Filename: "{app}\QuanLyVayVon.exe"

[Run]
Filename: "{app}\QuanLyVayVon.exe"; Description: "Chạy Quản Lý Hợp Đồng"; Flags: nowait postinstall skipifsilent

[Code]
// Kiểm tra .NET Runtime 8.0 có được cài không
function IsDotNet8Installed(): Boolean;
var
  regValue: string;
  success: Boolean;
begin
  success := RegQueryStringValue(HKLM64, 'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedhost', 'Version', regValue);
  if not success then
    success := RegQueryStringValue(HKLM32, 'SOFTWARE\dotnet\Setup\InstalledVersions\x64\sharedhost', 'Version', regValue);

  Result := success and (Copy(regValue, 1, 2) = '8.');
end;

function InitializeSetup(): Boolean;
begin
  if not IsDotNet8Installed() then
  begin
    MsgBox('Máy tính của bạn chưa cài .NET Desktop Runtime 8.0.' + #13#10 +
           'Tải tại: https://dotnet.microsoft.com/en-us/download/dotnet/8.0/runtime',
           mbError, MB_OK);
    Result := False;
  end
  else
    Result := True;
end;
