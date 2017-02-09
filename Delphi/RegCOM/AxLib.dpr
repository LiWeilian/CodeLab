library AxLib;

uses
  ComServ,
  AxLib_TLB in 'AxLib_TLB.pas',
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
