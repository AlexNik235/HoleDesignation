
[Setup]
AppName=RevitTestProject
AppId={{afbe6770-d756-459c-b391-2bb6fa02392a}
AppVersion=2.1.2.1650795526
DefaultDirName={userappdata}/Autodesk/Revit/Addins/2019\RevitTestProject
UsePreviousAppDir=no
PrivilegesRequired=lowest
OutputBaseFilename=RevitTestProject_2.1.2.1650795526
DisableDirPage=yes

[Files]
Source: "C:\Users\Undea\AppData\Local\Temp\RxBim_build_3a944880-d3c1-4c6c-b7d2-58e558602f83\bin\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs; 
Source: "C:\Users\Undea\AppData\Local\Temp\RxBim_build_3a944880-d3c1-4c6c-b7d2-58e558602f83\*"; DestDir: "{userappdata}/Autodesk/Revit/Addins/2019"; 

[Code]
function GetUninstallString(): String;
var
  sUnInstPath: String;
  sUnInstallString: String;
begin
  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#emit SetupSetting("AppId")}_is1');

  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;

function IsUpgrade(): Boolean;
begin
  Result := (GetUninstallString() <> '');
end;

function UnInstallOldVersion(): Integer;
var
  sUnInstallString: String;
  iResultCode: Integer;
begin
{ Return Values: }
{ 1 - uninstall string is empty }
{ 2 - error executing the UnInstallString }
{ 3 - successfully executed the UnInstallString }

  { default return value }
  Result := 0;

  { get the uninstall string of the old app }
  sUnInstallString := GetUninstallString();
  if sUnInstallString <> '' then begin
    sUnInstallString := RemoveQuotes(sUnInstallString);
    if Exec(sUnInstallString, '/SILENT /NORESTART /SUPPRESSMSGBOXES','', SW_HIDE, ewWaitUntilTerminated, iResultCode) then
      Result := 3
    else
      Result := 2;
  end else
    Result := 1;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep=ssInstall) then
  begin
    if (IsUpgrade()) then
    begin
      UnInstallOldVersion();
    end;
  end;
end;
