unit FormDemo;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls,
  execom_TLB, ExtCtrls;

type
  TForm1 = class(TForm)
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    ledtRemoteServer: TLabeledEdit;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
  private
    { Private declarations }
    execom: ITestExeCOM;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
begin
  execom := nil;
  execom := CoTestExeCOM.Create;
  //execom.Method1;
  //execom := nil;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  if execom <> nil then
    execom.Method1;
end;

procedure TForm1.Button3Click(Sender: TObject);
var
  s: WideString;
begin
  if execom <> nil then
  begin
    execom.Method2('test', s);
    ShowMessage(s);
  end;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
  execom := nil;
  try
    execom := CoTestExeCOM.CreateRemote(ledtRemoteServer.Text);     
    ShowMessage('ok');
  except
    on e: Exception do
    begin
      ShowMessage(e.Message);
    end;
  end;
end;

end.
