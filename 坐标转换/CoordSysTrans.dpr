program CoordSysTrans;

uses
  Forms,
  FormCoordSysTrans in 'FormCoordSysTrans.pas' {frmCoordSysTrans},
  untCoordSysTrans in 'untCoordSysTrans.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmCoordSysTrans, frmCoordSysTrans);
  Application.Run;
end.
