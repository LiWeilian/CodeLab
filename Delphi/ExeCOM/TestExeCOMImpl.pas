unit TestExeCOMImpl;

{$WARN SYMBOL_PLATFORM OFF}

interface

uses
  ComObj, ActiveX, execom_TLB, StdVcl, Dialogs;

type
  TTestExeCOM = class(TAutoObject, ITestExeCOM)
  protected
    procedure Method1; safecall;
    procedure Method2(const p1: WideString; out p2: WideString); safecall;

  end;

implementation

uses ComServ, FormMain, Forms;

procedure TTestExeCOM.Method1;
begin
  ShowMessage('Method1 OK.');
end;

procedure TTestExeCOM.Method2(const p1: WideString; out p2: WideString);
begin
  frmMain.Label1.Caption := p1;
  p2 := frmMain.Caption + ' ' + p1; 
end;

initialization
  TAutoObjectFactory.Create(ComServer, TTestExeCOM, Class_TestExeCOM,
    ciMultiInstance, tmApartment);
end.
