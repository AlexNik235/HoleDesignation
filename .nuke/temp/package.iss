
[Setup]
AppName=HoleDesignation
AppId={{41670a3b-f468-42a2-96de-5103d4f339e7}
AppVersion=1.0.0.1650967716
DefaultDirName={userappdata}/Autodesk/Revit/Addins/\HoleDesignation
UsePreviousAppDir=no
PrivilegesRequired=lowest
OutputBaseFilename=HoleDesignation_1.0.0.1650967716
DisableDirPage=yes

[Files]
Source: "C:\Users\nikitenkoaa\AppData\Local\Temp\RxBim_build_7e84ed4c-9361-4f50-960f-1b3bede5e8b8\bin\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs; 
Source: "C:\Users\nikitenkoaa\AppData\Local\Temp\RxBim_build_7e84ed4c-9361-4f50-960f-1b3bede5e8b8\*"; DestDir: "{userappdata}/Autodesk/Revit/Addins/"; 

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
