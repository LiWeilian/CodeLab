unit untOPCPublic;

interface

uses
  SysUtils, Forms, Classes, StdCtrls, TypInfo, Dialogs;

  function GetOPCMainForm: TObject;
  function GetOPCMainMemo: TMemo;
  procedure AddMessageToMemo(msg: WideString);

implementation

function GetOPCMainForm: TObject;
var
  component: TComponent;
begin
  Result := Application.MainForm;
  if Result = nil then
  begin
    component := Application.FindComponent('frmOPCDataAccess');
    if (component <> nil) and (component.ClassName = 'TfrmOPCDataAccess') then
    begin
      Result := component;
    end;
  end;
end;

function GetOPCMainMemo: TMemo;
var
  mainFormObj: TObject;
  propInfo: PPropInfo;
  mmObj: TObject;
begin
  Result := nil;
  mainFormObj := GetOPCMainForm;
  if mainFormObj = nil then
  begin
    Exit;
  end else
  begin
    propInfo := GetPropInfo(mainFormObj, 'MainMessageMemo');
    if propInfo <> nil then
    begin
      mmObj := GetObjectProp(mainFormObj, 'MainMessageMemo');
      if mmObj <> nil then
      begin
        Result := mmObj as TMemo;
      end;
    end;
  end;
end;

procedure AddMessageToMemo(msg: WideString);
var
  mm: TMemo;
begin
  mm := GetOPCMainMemo;
  if mm <> nil then
  begin
    mm.Lines.Add(msg);
  end;
end;

end.
