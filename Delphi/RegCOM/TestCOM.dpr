library TestCOM;

uses
  ComServ,
  TestCOM_TLB in 'TestCOM_TLB.pas',
  TestCOMImpl in 'TestCOMImpl.pas' {TestCOM: CoClass},
  AutoObjImpl in 'AutoObjImpl.pas' {AutoObj: CoClass};

exports
  DllGetClassObject,
  DllCanUnloadNow,
  DllRegisterServer,
  DllUnregisterServer;

{$R *.TLB}

{$R *.RES}

begin
end.
