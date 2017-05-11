unit OPCcustomClasses;

interface

uses
  SysUtils, ActiveX, Windows, Variants,
  untOPCPublic,
  OPCTypes,
  OPCDA;

type
  TOPCAdviseSink = class(TInterfacedObject, IAdviseSink)
  public
    procedure OnDataChange(const formatetc: TFormatEtc;
                            const stgmed: TStgMedium); stdcall;
    procedure OnViewChange(dwAspect: Longint; lindex: Longint); stdcall;
    procedure OnRename(const mk: IMoniker); stdcall;
    procedure OnSave; stdcall;
    procedure OnClose; stdcall;
  end;

  // class to receive IConnectionPointContainer data change callbacks
  TOPCDataCallback = class(TInterfacedObject, IOPCDataCallback)
  public
    function OnDataChange(dwTransid: DWORD; hGroup: OPCHANDLE;
      hrMasterquality: HResult; hrMastererror: HResult; dwCount: DWORD;
      phClientItems: POPCHANDLEARRAY; pvValues: POleVariantArray;
      pwQualities: PWordArray; pftTimeStamps: PFileTimeArray;
      pErrors: PResultList): HResult; stdcall;
    function OnReadComplete(dwTransid: DWORD; hGroup: OPCHANDLE;
      hrMasterquality: HResult; hrMastererror: HResult; dwCount: DWORD;
      phClientItems: POPCHANDLEARRAY; pvValues: POleVariantArray;
      pwQualities: PWordArray; pftTimeStamps: PFileTimeArray;
      pErrors: PResultList): HResult; stdcall;
    function OnWriteComplete(dwTransid: DWORD; hGroup: OPCHANDLE;
      hrMastererr: HResult; dwCount: DWORD; pClienthandles: POPCHANDLEARRAY;
      pErrors: PResultList): HResult; stdcall;
    function OnCancelComplete(dwTransid: DWORD; hGroup: OPCHANDLE):
      HResult; stdcall;
  end;

implementation

{ TOPCAdviseSink }

procedure TOPCAdviseSink.OnClose;
begin

end;

procedure TOPCAdviseSink.OnDataChange(const formatetc: TFormatEtc;
  const stgmed: TStgMedium);
var
  PG: POPCGROUPHEADER;
  PI1: POPCITEMHEADER1ARRAY;
  PI2: POPCITEMHEADER2ARRAY;
  PV: POleVariant;
  I: Integer;
  PStr: PWideChar;
  NewValue: string;
  WithTime: Boolean;
  ClientHandle: OPCHANDLE;
  Quality: Word;
begin
  // the rest of this method assumes that the item header array uses
  // OPCITEMHEADER1 or OPCITEMHEADER2 records,
  // so check this first to be defensive
  if (formatetc.cfFormat <> OPCSTMFORMATDATA) and
      (formatetc.cfFormat <> OPCSTMFORMATDATATIME) then Exit;
  // does the data stream provide timestamps with each value?
  WithTime := formatetc.cfFormat = OPCSTMFORMATDATATIME;

  PG := GlobalLock(stgmed.hGlobal);
  if PG <> nil then
  begin
    // we will only use one of these two values, according to whether
    // WithTime is set:
    PI1 := Pointer(PAnsiChar(PG) + SizeOf(OPCGROUPHEADER));
    PI2 := Pointer(PI1);
    if Succeeded(PG.hrStatus) then
    begin
      for I := 0 to PG.dwItemCount - 1 do
      begin
        if WithTime then
        begin
          PV := POleVariant(PAnsiChar(PG) + PI1[I].dwValueOffset);
          ClientHandle := PI1[I].hClient;
          Quality := (PI1[I].wQuality and OPC_QUALITY_MASK);
        end
        else begin
          PV := POleVariant(PAnsiChar(PG) + PI2[I].dwValueOffset);
          ClientHandle := PI2[I].hClient;
          Quality := (PI2[I].wQuality and OPC_QUALITY_MASK);
        end;
        if Quality = OPC_QUALITY_GOOD then
        begin
          // this test assumes we're not dealing with array data
          if TVarData(PV^).VType <> VT_BSTR then
          begin
            NewValue := VarToStr(PV^);
          end
          else begin
            // for BSTR data, the BSTR image follows immediately in the data
            // stream after the variant union;  the BSTR begins with a DWORD
            // character count, which we skip over as the BSTR is also
            // NULL-terminated
            PStr := PWideChar(PAnsiChar(PV) + SizeOf(OleVariant) + 4);
            NewValue := WideString(PStr);
          end;
          if WithTime then
          begin
            AddMessageToMemo(Format('New value for item %d advised:  %s (with timestamp)', [ClientHandle, NewValue]));

          end
          else begin
            AddMessageToMemo(Format('New value for item %d advised:  %s', [ClientHandle, NewValue]));
          end;
        end
        else begin
          AddMessageToMemo(Format('Advise received for item %d , but quality not good', [ClientHandle]));

        end;
      end;
    end;
    GlobalUnlock(stgmed.hGlobal);
  end;
end;

procedure TOPCAdviseSink.OnRename(const mk: IMoniker);
begin

end;

procedure TOPCAdviseSink.OnSave;
begin

end;

procedure TOPCAdviseSink.OnViewChange(dwAspect, lindex: Integer);
begin

end;

{ TOPCDataCallback }

function TOPCDataCallback.OnCancelComplete(dwTransid: DWORD;
  hGroup: OPCHANDLE): HResult;
begin
  // we don't use this facility
  Result := S_OK;
end;

function TOPCDataCallback.OnDataChange(dwTransid: DWORD; hGroup: OPCHANDLE;
  hrMasterquality, hrMastererror: HResult; dwCount: DWORD;
  phClientItems: POPCHANDLEARRAY; pvValues: POleVariantArray;
  pwQualities: PWordArray; pftTimeStamps: PFileTimeArray;
  pErrors: PResultList): HResult;
var
  ClientItems: POPCHANDLEARRAY;
  Values: POleVariantArray;
  Qualities: PWORDARRAY;
  I: Integer;
  NewValue: string;
begin
  Result := S_OK;
  ClientItems := POPCHANDLEARRAY(phClientItems);
  Values := POleVariantArray(pvValues);
  Qualities := PWORDARRAY(pwQualities);
  for I := 0 to dwCount - 1 do
  begin
    if Qualities[I] = OPC_QUALITY_GOOD then
    begin
      NewValue := VarToStr(Values[I]);
      AddMessageToMemo(Format('New callback for item %d received, value:  %s', [ClientItems[I], NewValue]));

    end
    else begin
      AddMessageToMemo(Format('Callback received for item %d , but quality not good', [ClientItems[I]]));

    end;
  end;
end;

function TOPCDataCallback.OnReadComplete(dwTransid: DWORD;
  hGroup: OPCHANDLE; hrMasterquality, hrMastererror: HResult;
  dwCount: DWORD; phClientItems: POPCHANDLEARRAY;
  pvValues: POleVariantArray; pwQualities: PWordArray;
  pftTimeStamps: PFileTimeArray; pErrors: PResultList): HResult;
begin 
  Result := OnDataChange(dwTransid, hGroup, hrMasterquality, hrMastererror,
    dwCount, phClientItems, pvValues, pwQualities, pftTimeStamps, pErrors);
end;

function TOPCDataCallback.OnWriteComplete(dwTransid: DWORD;
  hGroup: OPCHANDLE; hrMastererr: HResult; dwCount: DWORD;
  pClienthandles: POPCHANDLEARRAY; pErrors: PResultList): HResult;
begin 
  // we don't use this facility
  Result := S_OK;
end;

end.
