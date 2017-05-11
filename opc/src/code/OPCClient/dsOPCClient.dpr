program dsOPCClient;

uses
  Forms,
  FormMain in 'FormMain.pas' {frmMain},
  untNetwork in 'untNetwork.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmMain, frmMain);
  Application.Run;
end.
