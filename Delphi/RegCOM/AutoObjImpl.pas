unit AutoObjImpl;

{$WARN SYMBOL_PLATFORM OFF}

interface

uses
  ComObj, ActiveX, TestCOM_TLB, StdVcl, Dialogs;

type
  TAutoObj = class(TAutoObject, IAutoObj)
  protected
    procedure Method1; safecall;
    procedure Method2(p1: Integer; out p2: WideString); safecall;

  end;

implementation

uses ComServ, SysUtils;

procedure TAutoObj.Method1;
begin
  ShowMessage('Method1 OK.');
end;

procedure TAutoObj.Method2(p1: Integer; out p2: WideString);
begin
  p2 := 'In = ' + IntToStr(p1);
  ShowMessage(p2);
end;

initialization
  TAutoObjectFactory.Create(ComServer, TAutoObj, Class_AutoObj,
    ciMultiInstance, tmApartment);
end.
