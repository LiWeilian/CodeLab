unit TestCOMImpl;

{$WARN SYMBOL_PLATFORM OFF}

interface

uses
  SysUtils, Windows, ActiveX, Classes, ComObj, TestCOM_TLB, StdVcl, Dialogs;

type
  TTestCOM = class(TTypedComObject, ITestCOM)
  protected
    function Method1: HResult; stdcall;
    function Method2(p1: Integer): HResult; stdcall;
    function Method3(p1: Integer; out p2: WideString): HResult; stdcall;
    {Declare ITestCOM methods here}
  end;

implementation

uses ComServ;

function TTestCOM.Method1: HResult;
begin
  ShowMessage('Method1');
end;

function TTestCOM.Method2(p1: Integer): HResult;
begin
  ShowMessage('Method2: ' + IntToStr(p1));
end;

function TTestCOM.Method3(p1: Integer; out p2: WideString): HResult;
begin
  ShowMessage('Method3 : ' + IntToStr(p1));
  p2 := IntToStr(p1);
end;

initialization
  TTypedComObjectFactory.Create(ComServer, TTestCOM, TestCOM_TLB.CLASS_TestCOM_,
    ciMultiInstance, tmApartment);
end.
