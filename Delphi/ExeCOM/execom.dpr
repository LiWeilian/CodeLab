program execom;

uses
  Forms,
  FormMain in 'FormMain.pas' {frmMain},
  execom_TLB in 'execom_TLB.pas',
  TestExeCOMImpl in 'TestExeCOMImpl.pas' {TestExeCOM: CoClass};

{$R *.TLB}

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmMain, frmMain);
  Application.Run;
end.
