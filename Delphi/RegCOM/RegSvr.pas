unit RegSvr;

interface

uses
  Windows, SysUtils, ActiveX, ComObj, RegConst, Dialogs, StrUtils;

type
  TRegType = (rtAxLib, rtTypeLib, rtExeLib, rtNoLib);
  TRegAction = (raReg, raUnreg);
  TRegProc = function : HResult; stdcall;
  TUnRegTlbProc = function (const libID: TGUID; wVerMajor, wVerMinor: Word;
    lcid: TLCID; syskind: TSysKind): HResult; stdcall;


const
  ProcName: array[TRegAction] of PChar = (
    'DllRegisterServer', 'DllUnregisterServer');
  ExeFlags: array[TRegAction] of string = (' /regserver', ' /unregserver');

var
  RegType: TRegType = rtAxLib;
  RegAction: TRegAction = raReg;
  QuietMode: Boolean = False;
  FileName: string;  //注册的文件名称
  Param: string;     //注册参数  '', /u, /t;
  RegProc: TRegProc;
  LibHandle: THandle;
  OleAutLib: THandle;
  UnRegTlbProc: TUnRegTlbProc;

  Function RegComSvr(RegFileName: string; const RegParam: string=''): Boolean;
  Function DelDirFiles(fn: string; IsDirectory: Boolean): Boolean;
  Function RegAllDll(const DllPath: String; const Reg: Boolean = True): String;
  procedure RegisterAxLib(axFileName: String); overload;

implementation

procedure OutputStr(S: string);
begin
  if not QuietMode then
  begin
    CharToOEM(PChar(S), PChar(S));
    Writeln(S);
  end;
end;

Function RegAllDll(const DllPath: String; const Reg: Boolean = True): String;
  function FormatDir(sDir: String): String;
  begin
    Result := ExpandFileName(sDir);
    if RightStr(Result, 1) <> '\' then
      Result := Result + '\';
  end;
var
  SearchRec: TSearchRec;
  sDirName: String;
begin
  Result := '组件注册信息：';
  sDirName := FormatDir(DllPath);
  FindFirst(sDirName + '*.dll', faAnyFile, SearchRec);
  repeat
    if Reg then
    begin
      if RegComSvr(sDirName + SearchRec.Name) then      //注册组件
        Result := Result + #13#10 + '注册成功：' + SearchRec.Name
      else
        Result := Result + #13#10 + '注册失败：' + SearchRec.Name;
    end
    else
    begin
      if RegComSvr(sDirName + SearchRec.Name, '/U') then   //反注册组件
        Result := Result + #13#10 + '反注册成功：' + SearchRec.Name
      else
        Result := Result + #13#10 + '反注册失败：' + SearchRec.Name;
    end;
  until (FindNext(SearchRec)<>0);
  FindClose(SearchRec);
end;

Function DelDirFiles(fn: string; IsDirectory: Boolean): Boolean;
  function FormatDir(sDir: String): String;
  begin
    Result := ExpandFileName(sDir);
    if RightStr(Result, 1) <> '\' then
      Result := Result + '\';
  end;
  function DeleteDirFile(fname: String): Boolean;
  begin
    if FileExists(fname) then
    begin
      FileSetAttr(fname, 0); //修改文件属性为普通属性值
      RegComSvr(fname, '/U');      //反注册组件
      Result := DeleteFile(fname); //删除文件
    end
    else
      Result := True;
  end;
var
  SearchRec: TSearchRec;
  sDirName: String;
begin
  if IsDirectory then
  begin
    sDirName := FormatDir(fn);
    FindFirst(sDirName + '*.*', faAnyFile, SearchRec);
    repeat
      if (SearchRec.Attr = faDirectory) and (SearchRec.Name <> '.') and ((SearchRec.Name <> '..')) then
      begin
        Result := DelDirFiles(sDirName + SearchRec.Name, True);
        if Result then
          Result := RemoveDir(sDirName + SearchRec.Name);
      end
      else
        Result := DeleteDirFile(sDirName + SearchRec.Name);
    until (FindNext(SearchRec)<>0);
    FindClose(SearchRec);
    Result := RemoveDir(sDirName);
  end
  else
    Result := DeleteDirFile(fn);
end;

function DecodeOptions: Boolean;
var
  //i: Integer;
  FileStart: Boolean;
  FileExt: string;
begin
  Result := False;
  //if ParamCount = 0 then Exit;
  if FileName = '' then
    Exit;
  //for i := 1 to ParamCount do
  //begin
    //Param := ParamStr(i);
    if Param = '' then
      Param := FileName;
    FileStart := not (Param[1] in ['-', '/']);
    if FileStart then
    begin
      //if FileName = '' then FileName := Param
      //else FileName := FileName + ' ' + Param;
      //strip open and/or close quote if present
      if (FileName[1] = '"') then
      begin
        if (FileName[Length(FileName)] = '"') then
          FileName := Copy(FileName, 2, Length(FileName) - 2)
        else if FileName[1] = '"' then
          Delete(FileName, 1, 1);
      end;
    end
    else
    begin
      if Length(Param) < 2 then Exit;
      case Param[2] of
        'U', 'u': RegAction := raUnreg;
        'Q', 'q': QuietMode := True;
        'T', 't': RegType := rtTypeLib;
      end;
    end;
  //end;
  FileExt := ExtractFileExt(FileName);
  if FileExt = '' then
    raise Exception.CreateFmt(SNeedFileExt, [FileName]);
  if RegType <> rtTypeLib then
  begin
    if CompareText(FileExt, '.TLB') = 0 then
      RegType := rtTypeLib
    else if CompareText(FileExt, '.EXE') = 0 then
      RegType := rtExeLib
    else if CompareText(FileExt, '.DLL') = 0 then
      RegType := rtAxLib
    else
      RegType := rtNoLib;
  end;
  Result := True;
end;

procedure RegisterAxLib; overload;
begin
  LibHandle := LoadLibrary(PChar(FileName));
  if LibHandle = 0 then
    raise Exception.CreateFmt(SLoadFail, [FileName]);
  try
    @RegProc := GetProcAddress(LibHandle, ProcName[RegAction]);
    if @RegProc = Nil then
      raise Exception.CreateFmt(SCantFindProc, [ProcName[RegAction], FileName]);
    if RegProc <> 0 then
      raise Exception.CreateFmt(SRegFail, [ProcName[RegAction], FileName]);
      //OutputStr(Format(SRegSuccessful, [ProcName[RegAction]]))
  finally
    FreeLibrary(LibHandle);
  end;
end;

procedure RegisterAxLib(axFileName: String); overload;
begin
  LibHandle := LoadLibrary(PChar(axFileName));
  if LibHandle = 0 then
    raise Exception.CreateFmt(SLoadFail, [axFileName]);
  try
    @RegProc := GetProcAddress(LibHandle, ProcName[RegAction]);
    if @RegProc = Nil then
      raise Exception.CreateFmt(SCantFindProc, [ProcName[RegAction], axFileName]);
    if RegProc <> 0 then
      raise Exception.CreateFmt(SRegFail, [ProcName[RegAction], axFileName]);
      //OutputStr(Format(SRegSuccessful, [ProcName[RegAction]]))
  finally
    FreeLibrary(LibHandle);
  end;
end;

procedure RegisterTLB;
const
  RegMessage: array[TRegAction] of string = (SRegStr, SUnregStr);
var
  WFileName, DocName: WideString;
  TypeLib: ITypeLib;
  LibAttr: PTLibAttr;
  DirBuffer: array[0..MAX_PATH] of char;
begin
  if ExtractFilePath(FileName) = '' then
  begin
    GetCurrentDirectory(SizeOf(DirBuffer), DirBuffer);
    FileName := '\' + FileName;
    FileName := DirBuffer + FileName;
  end;
  if not FileExists(FileName) then
    raise Exception.CreateFmt(SFileNotFound, [FileName]);
  WFileName := FileName;
  OleCheck(LoadTypeLib(PWideChar(WFileName), TypeLib));
  //OutputStr(Format(STlbName, [WFileName]));
  OleCheck(TypeLib.GetLibAttr(LibAttr));
  try
    //OutputStr(Format(STlbGuid, [GuidToString(LibAttr^.Guid)]) + #13#10);
    if RegAction = raReg then
    begin
      OleCheck(TypeLib.GetDocumentation(-1, nil, nil, nil, @DocName));
      DocName := ExtractFilePath(DocName);
      OleCheck(RegisterTypeLib(TypeLib, PWideChar(WFileName), PWideChar(DocName)));
    end
    else
    begin
      OleAutLib := GetModuleHandle('OLEAUT32.DLL');
      if OleAutLib <> 0 then
        @UnRegTlbProc := GetProcAddress(OleAutLib, 'UnRegisterTypeLib');
      if @UnRegTlbProc = nil then
        raise Exception.Create(SCantUnregTlb);
      with LibAttr^ do
        OleCheck(UnRegTlbProc(Guid, wMajorVerNum, wMinorVerNum, LCID, SysKind));
    end;
  finally
    TypeLib.ReleaseTLibAttr(LibAttr);
  end;
  //OutputStr(Format(STlbRegSuccessful, [RegMessage[RegAction]]));
end;

procedure RegisterEXE;
var
  SI: TStartupInfo;
  PI: TProcessInformation;
  RegisterExitCode: BOOL;
begin
  FillChar(SI, SizeOf(SI), 0);
  SI.cb := SizeOf(SI);
  RegisterExitCode := Win32Check(CreateProcess(PChar(FileName),
                                 PChar(FileName + ExeFlags[RegAction]),
                                 nil, nil, True, 0, nil, nil, SI, PI));
  CloseHandle(PI.hThread);
  CloseHandle(PI.hProcess);
  {if RegisterExitCode then
    OutputStr(Format(SExeRegSuccessful, [PChar(FileName + ExeFlags[RegAction])]))
  else
    OutputStr(Format(SExeRegUnsuccessful, [PChar(FileName + ExeFlags[RegAction])]));}
end;

Function RegComSvr(RegFileName: string; const RegParam: string=''): Boolean;
begin
  FileName := RegFileName;
  Param := RegParam;
  try
    if not DecodeOptions then
      raise Exception.Create(SAbout + #13#10 + SUsage);
    //OutputStr(SAbout);
    if not FileExists(FileName) then
      raise Exception.CreateFmt(SFileNotFound, [FileName]);
    case RegType of
      rtAxLib: RegisterAxLib;
      rtTypeLib: RegisterTLB;
      rtExeLib: RegisterEXE;
    end;
    Result := True;
  except
    Result := False;
    //on E:Exception do OutputStr(E.Message);
  end;
end;

end.
