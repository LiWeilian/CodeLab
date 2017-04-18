unit FormCoordSysTrans;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, Math;

type
  TfrmCoordSysTrans = class(TForm)
    GroupBox1: TGroupBox;
    GroupBox2: TGroupBox;
    ledtSDX: TLabeledEdit;
    ledtSDY: TLabeledEdit;
    ledtSDZ: TLabeledEdit;
    ledtSQX: TLabeledEdit;
    ledtSQY: TLabeledEdit;
    ledtSQZ: TLabeledEdit;
    ledtSScale: TLabeledEdit;
    ledtFDX: TLabeledEdit;
    ledtFDY: TLabeledEdit;
    ledtFRotateAngle: TLabeledEdit;
    ledtFScale: TLabeledEdit;
    GroupBox3: TGroupBox;
    ledtProjectType: TLabeledEdit;
    ledtCentralMeridian: TLabeledEdit;
    ledtProjectScale: TLabeledEdit;
    ledtConstantX: TLabeledEdit;
    ledtConstantY: TLabeledEdit;
    ledtBenchmarkLatitude: TLabeledEdit;
    ledtSemiMajor: TLabeledEdit;
    ledtFlattening: TLabeledEdit;
    btnTranslate: TButton;
    GroupBox4: TGroupBox;
    ledtWGS84GcsB: TLabeledEdit;
    ledtWGS84GcsL: TLabeledEdit;
    ledtWGS84GcsH: TLabeledEdit;
    GroupBox5: TGroupBox;
    ledtLocaleX: TLabeledEdit;
    ledtLocaleY: TLabeledEdit;
    ledtLocaleZ: TLabeledEdit;
    btnRevTranslate: TButton;
    procedure btnTranslateClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure btnRevTranslateClick(Sender: TObject);
  private
    
  public
    { Public declarations }
  end;

var
  frmCoordSysTrans: TfrmCoordSysTrans;

implementation

{$R *.dfm}

uses
  untCoordSysTrans;

procedure TfrmCoordSysTrans.btnTranslateClick(Sender: TObject);
var
  g2l: TGps2Locale;
  x, y, z: Double;
  B, L, H: Double;
  coordSysParam: TCoordSysParam;
begin
  if (not TryStrToFloat(ledtWGS84GcsB.Text, B)) or (B > 90) or (B < -90) then
  begin
    ShowMessage('纬度数值无效');
    Exit;
  end;
  if (not TryStrToFloat(ledtWGS84GcsL.Text, L)) or (L > 180) or (L < -180) then
  begin
    ShowMessage('经度数值无效');
    Exit;
  end;
  if not TryStrToFloat(ledtWGS84GcsH.Text, H) then
  begin
    ShowMessage('高度数值无效');
    Exit;
  end;

  coordSysParam := TCoordSysParam.Create;
  coordSysParam.SDX := StrToFloat(ledtSDX.Text);
  coordSysParam.SDY := StrToFloat(ledtSDY.Text);
  coordSysParam.SDZ := StrToFloat(ledtSDZ.Text);
  coordSysParam.SQX := StrToFloat(ledtSQX.Text);
  coordSysParam.SQY := StrToFloat(ledtSQY.Text);
  coordSysParam.SQZ := StrToFloat(ledtSQZ.Text);
  coordSysParam.SScale := StrToFloat(ledtSScale.Text);
  coordSysParam.FDX := StrToFloat(ledtFDX.Text);
  coordSysParam.FDY := StrToFloat(ledtFDY.Text);
  coordSysParam.FRotateAngle := StrToFloat(ledtFRotateAngle.Text);
  coordSysParam.FScale := StrToFloat(ledtFScale.Text);
  coordSysParam.PProjectionType := StrToInt(ledtProjectType.Text);
  coordSysParam.PCentralMeridian := StrToFloat(ledtCentralMeridian.Text);
  coordSysParam.PScale := StrToFloat(ledtProjectScale.Text);
  coordSysParam.PBenchmarkLatitude := StrToFloat(ledtBenchmarkLatitude.Text);
  coordSysParam.PConstantX := StrToFloat(ledtConstantX.Text);
  coordSysParam.PConstantY := StrToFloat(ledtConstantY.Text);
  coordSysParam.SemiMajor := StrToFloat(ledtSemiMajor.Text);
  coordSysParam.Flattening := StrToFloat(ledtFlattening.Text);

  g2l := TGps2Locale.Create;
  g2l.Transform(B, L, H, coordSysParam,  x, y, z);
  ledtLocaleX.Text := FloatToStr(x);
  ledtLocaleY.Text := FloatToStr(y);
  ledtLocaleZ.Text := FloatToStr(z);
end;

procedure TfrmCoordSysTrans.Button1Click(Sender: TObject);
var
  g2l: TGps2Locale;
  //centralLong: Double;
  //n: Integer;
  x, y, z: Double;
  B, L, H: Double;
  coordSysParam: TCoordSysParam;
begin
  g2l := TGps2Locale.Create;
  //n := g2l.LongOfCentralMeridian(113.9, 3, centralLong);
  //ShowMessage('N: ' + IntToStr(n) + ', 中央子午线：' + FloatToStr(centralLong));

  //g2l.BL2xy(DegToRad(22.5), DegToRad(113.1), DegToRad(114), 6378245.0, 1.0 / 298.3, 0, 500000, x, y);
  //ShowMessage('x: ' + FloatToStr(x) + ', y: ' + FloatToStr(y));


  //g2l.BLH2XYZ(DegToRad(22.5), DegToRad(113.1), 11, 6378137,
	//			g2l.cal_b(6378137, 1 / 298.257224), x, y, z);
  //ShowMessage('x: ' + FloatToStr(x) + ', y: ' + FloatToStr(y) + ', z: ' + FloatToStr(z));

  coordSysParam := TCoordSysParam.Create;
  coordSysParam.SDX := StrToFloat(ledtSDX.Text);
  coordSysParam.SDY := StrToFloat(ledtSDY.Text);
  coordSysParam.SDZ := StrToFloat(ledtSDZ.Text);
  coordSysParam.SQX := StrToFloat(ledtSQX.Text);
  coordSysParam.SQY := StrToFloat(ledtSQY.Text);
  coordSysParam.SQZ := StrToFloat(ledtSQZ.Text);
  coordSysParam.SScale := StrToFloat(ledtSScale.Text);
  coordSysParam.FDX := StrToFloat(ledtFDX.Text);
  coordSysParam.FDY := StrToFloat(ledtFDY.Text);
  coordSysParam.FRotateAngle := StrToFloat(ledtFRotateAngle.Text);
  coordSysParam.FScale := StrToFloat(ledtFScale.Text);
  coordSysParam.PProjectionType := StrToInt(ledtProjectType.Text);
  coordSysParam.PCentralMeridian := StrToFloat(ledtCentralMeridian.Text);
  coordSysParam.PScale := StrToFloat(ledtProjectScale.Text);
  coordSysParam.PBenchmarkLatitude := StrToFloat(ledtBenchmarkLatitude.Text);
  coordSysParam.PConstantX := StrToFloat(ledtConstantX.Text);
  coordSysParam.PConstantY := StrToFloat(ledtConstantY.Text);
  coordSysParam.SemiMajor := StrToFloat(ledtSemiMajor.Text);
  coordSysParam.Flattening := StrToFloat(ledtFlattening.Text);

  g2l.Transform(23.125467917688, 113.210002615135, 11, coordSysParam,  x, y, z);
  ShowMessage('x: ' + FloatToStr(x) + ', y: ' + FloatToStr(y) + ', z: ' + FloatToStr(z));
  //23.062279545594, 113.322852104270
  //44029, 21815, 17.32

  //23.125467917688, 113.210002615135,
  //32469.3902181918, 28815.631675113

  g2l.RevTransform(28815.631675113, 32469.3902181918, 17.32, coordSysParam, B, L, H);
  ShowMessage('B: ' + FloatToStr(B) + ', L: ' + FloatToStr(L) + ', H: ' + FloatToStr(H));
end;

procedure TfrmCoordSysTrans.btnRevTranslateClick(Sender: TObject);
var
  g2l: TGps2Locale;
  x, y, z: Double;
  B, L, H: Double;
  coordSysParam: TCoordSysParam;
begin
  if not TryStrToFloat(ledtLocaleX.Text, X) then
  begin
    ShowMessage('X数值无效');
    Exit;
  end;
  if not TryStrToFloat(ledtLocaleY.Text, Y) then
  begin
    ShowMessage('Y数值无效');
    Exit;
  end;
  if not TryStrToFloat(ledtLocaleZ.Text, Z) then
  begin
    ShowMessage('Z数值无效');
    Exit;
  end;

  coordSysParam := TCoordSysParam.Create;
  coordSysParam.SDX := StrToFloat(ledtSDX.Text);
  coordSysParam.SDY := StrToFloat(ledtSDY.Text);
  coordSysParam.SDZ := StrToFloat(ledtSDZ.Text);
  coordSysParam.SQX := StrToFloat(ledtSQX.Text);
  coordSysParam.SQY := StrToFloat(ledtSQY.Text);
  coordSysParam.SQZ := StrToFloat(ledtSQZ.Text);
  coordSysParam.SScale := StrToFloat(ledtSScale.Text);
  coordSysParam.FDX := StrToFloat(ledtFDX.Text);
  coordSysParam.FDY := StrToFloat(ledtFDY.Text);
  coordSysParam.FRotateAngle := StrToFloat(ledtFRotateAngle.Text);
  coordSysParam.FScale := StrToFloat(ledtFScale.Text);
  coordSysParam.PProjectionType := StrToInt(ledtProjectType.Text);
  coordSysParam.PCentralMeridian := StrToFloat(ledtCentralMeridian.Text);
  coordSysParam.PScale := StrToFloat(ledtProjectScale.Text);
  coordSysParam.PBenchmarkLatitude := StrToFloat(ledtBenchmarkLatitude.Text);
  coordSysParam.PConstantX := StrToFloat(ledtConstantX.Text);
  coordSysParam.PConstantY := StrToFloat(ledtConstantY.Text);
  coordSysParam.SemiMajor := StrToFloat(ledtSemiMajor.Text);
  coordSysParam.Flattening := StrToFloat(ledtFlattening.Text);

  g2l := TGps2Locale.Create;
  g2l.RevTransform(Y, X, Z, coordSysParam, B, L, H);
  ledtWGS84GcsB.Text := FloatToStr(B);
  ledtWGS84GcsL.Text := FloatToStr(L);
  ledtWGS84GcsH.Text := FloatToStr(H);
end;

end.
