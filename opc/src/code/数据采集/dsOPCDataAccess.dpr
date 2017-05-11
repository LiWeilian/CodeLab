program dsOPCDataAccess;

uses
  Forms,
  FormOPCDataAccess in 'FormOPCDataAccess.pas' {frmOPCDataAccess},
  OPCcustomClasses in '..\..\public\OPCcustomClasses.pas',
  untOPCPublic in '..\..\public\untOPCPublic.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmOPCDataAccess, frmOPCDataAccess);
  Application.Run;
end.
