unit FormRegisterCOM;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ActiveX, ComObj;

type
  TRegProc = function : HResult; stdcall;
  TUnRegTlbProc = function (const libID: TGUID; wVerMajor, wVerMinor: Word;
    lcid: TLCID; syskind: TSysKind): HResult; stdcall;

  TForm1 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    //LibHandle: THandle;
    function GetDLLFileName: String;
    function GetCOMFileName: String;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

  //LibHandle: THandle;
  //OleAutLib: THandle;

implementation

{$R *.dfm}

uses
  TestCOM_TLB;

procedure RegisterAxLib(dllFileName: String);
var
  RegProc: TRegProc;
  LibHandle: THandle;
begin
  LibHandle := LoadLibrary(PChar(dllFileName));
  if LibHandle = 0 then
    raise Exception.CreateFmt('Failed to load "%s"', [dllFileName]);

  try
    @RegProc := GetProcAddress(LibHandle, 'DllRegisterServer');
    if @RegProc = Nil then
      raise Exception.CreateFmt('%s procedure not found in "%s"', ['DllRegisterServer', dllFileName]);
    if RegProc <> 0 then
      raise Exception.CreateFmt('Call to %s failed in "%s"', ['DllRegisterServer', dllFileName]);
    ShowMessage('Reg OK.');
  finally
    FreeLibrary(libHandle);
  end;
end;

procedure UnregisterAxLib(dllFileName: String);
var
  RegProc: TRegProc;
  LibHandle: THandle;
begin
  LibHandle := LoadLibrary(PChar(dllFileName));
  if LibHandle = 0 then
    raise Exception.CreateFmt('Failed to load "%s"', [dllFileName]);

  try
    @RegProc := GetProcAddress(LibHandle, 'DllUnregisterServer');
    if @RegProc = Nil then
      raise Exception.CreateFmt('%s procedure not found in "%s"', ['DllUnregisterServer', dllFileName]);
    if RegProc <> 0 then
      raise Exception.CreateFmt('Call to %s failed in "%s"', ['DllUnregisterServer', dllFileName]);
    ShowMessage('Unreg OK.');
  finally
    FreeLibrary(libHandle);
  end;
end;
procedure TForm1.Button1Click(Sender: TObject);
var
  dllFileName: WideString;
  RegProc: TRegProc;
  UnRegTlbProc: TUnRegTlbProc;
  oleObj: Variant;
  guid: TGUID;
  comIntf: IUnknown;
  outParam: WideString;
begin
  dllFileName := GetDLLFileName;
  if not FileExists(dllFileName) then
  begin
    ShowMessage('DLL文件不存在');
    Exit;
  end;

  //ShowMessage(RegAllDll(ExtractFilePath(Application.ExeName), False));
  RegisterAxLib(dllFileName);

  try
    oleObj := CreateOleObject('AxLib.AutoObj'); //ProgID
    oleObj.Method1;
    oleObj.Method2(2, outParam);
    ShowMessage(outParam);
    oleObj := Unassigned;
  except
    on e: Exception do
    begin
      ShowMessage('Call TestCOM error: ' + e.Message);
    end;
  end;

  //{A7E786EA-C03E-4FC4-A05C-5C5444ED911C}
  //{1FDCE511-906B-490C-A9CE-4CBEECB7DB56}
  comIntf := CreateComObject(StringToGUID('AxLib.AutoObj'));

  UnregisterAxLib(dllFileName);
end;

function TForm1.GetDLLFileName: String;
begin
  Result := ExtractFilePath(Application.ExeName) + 'AxLib.dll';
end;

procedure TForm1.Button2Click(Sender: TObject);
var
  dllFileName: WideString;
  RegProc: TRegProc;
  UnRegTlbProc: TUnRegTlbProc;
  oleObj: Variant;
  guid: TGUID;
  comIntf: IUnknown;
  outParam: WideString;
  progID: PWideChar;
  autoObjName: String;
  comObjName: String;
  testCOM: ITestCOM;
  pv: IUnknown;
begin
  dllFileName := GetCOMFileName;
  if not FileExists(dllFileName) then
  begin
    ShowMessage('COM文件不存在');
    Exit;
  end;

  //ShowMessage(RegAllDll(ExtractFilePath(Application.ExeName), False));
  RegisterAxLib(dllFileName);

  autoObjName := 'TestCOM.AutoObj';
  CLSIDFromString(PWideChar(WideString(autoObjName)), guid);
  ShowMessage(autoObjName + ' CLSID From String: ' + GUIDToString(guid));
  guid := StringToGUID(autoObjName);
  ShowMessage(autoObjName + ' GUID From String: ' + GUIDToString(guid));
  CLSIDFromProgID(PWideChar(WideString(autoObjName)), guid);
  ShowMessage(autoObjName + ' CLSID From ProgID: ' + GUIDToString(guid));
  ProgIDFromCLSID(guid, progID);
  ShowMessage(autoObjName + ' ProgID From CLSID: ' + progID);

  try
    oleObj := CreateOleObject(autoObjName); //ProgID，必须是实现IDispatch的ole对象
    oleObj.Method1;
    oleObj.Method2(2, outParam);
    ShowMessage(outParam);
    oleObj := Unassigned;
  except
    on e: Exception do
    begin
      ShowMessage(Format('Call %s error: %s', [autoObjName, e.Message]));
    end;
  end;

  comObjName := 'TestCOM.TestCOM';
  CLSIDFromString(PWideChar(WideString(comObjName)), guid);
  ShowMessage(comObjName + ' CLSID From String: ' + GUIDToString(guid));
  guid := StringToGUID(comObjName);
  ShowMessage(comObjName + ' GUID From String: ' + GUIDToString(guid));
  CLSIDFromProgID(PWideChar(WideString(comObjName)), guid);
  ShowMessage(comObjName + ' CLSID From ProgID: ' + GUIDToString(guid));
  ProgIDFromCLSID(guid, progID);
  ShowMessage(comObjName + ' ProgID From CLSID: ' + progID);
  comIntf := CreateComObject(StringToGUID(comObjName));

  if comIntf.QueryInterface(IID_ITestCOM, testCOM) = S_OK then
  begin
    testCOM.Method1;

    testCOM := nil;
  end;
  comIntf := nil;

  CLSIDFromString(PWideChar(WideString(comObjName)), guid);
  CoCreateInstance(guid, nil, CLSCTX_INPROC_SERVER or
    CLSCTX_LOCAL_SERVER, IID_ITestCOM, pv);
  if pv.QueryInterface(IID_ITestCOM, testCOM) = S_OK then
  begin
    testCOM.Method1;

    testCOM := nil;
  end;
  pv := nil;

  UnregisterAxLib(dllFileName);
end;

function TForm1.GetCOMFileName: String;
begin
  Result := ExtractFilePath(Application.ExeName) + 'TestCOM.dll';
end;

end.
