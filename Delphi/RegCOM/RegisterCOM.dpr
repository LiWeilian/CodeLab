program RegisterCOM;

uses
  Forms,
  FormRegisterCOM in 'FormRegisterCOM.pas' {Form1},
  RegConst in 'RegConst.pas',
  RegSvr in 'RegSvr.pas',
  TestCOM_TLB in 'TestCOM_TLB.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TForm1, Form1);
  Application.Run;
end.
