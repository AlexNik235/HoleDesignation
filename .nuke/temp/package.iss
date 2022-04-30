
[Setup]
AppName=HoleDesignation
AppId={{41670a3b-f468-42a2-96de-5103d4f339e7}
AppVersion=1.0.0.1651313064
DefaultDirName={userappdata}/Autodesk/Revit/Addins/2020\HoleDesignation
UsePreviousAppDir=no
PrivilegesRequired=lowest
OutputBaseFilename=HoleDesignation_1.0.0.1651313064
DisableDirPage=yes

[Files]
Source: "C:\Users\Undea\AppData\Local\Temp\RxBim_build_85c212fd-59cc-4002-99c7-253bc91964fb\bin\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs; 
Source: "C:\Users\Undea\AppData\Local\Temp\RxBim_build_85c212fd-59cc-4002-99c7-253bc91964fb\*"; DestDir: "{userappdata}/Autodesk/Revit/Addins/2020"; 

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
