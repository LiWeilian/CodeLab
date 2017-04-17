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
    Button1: TButton;
    procedure btnTranslateClick(Sender: TObject);
    procedure Button1Click(Sender: TObject);
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
  coordSysParam: TCoordSysParam;
begin
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


end;

procedure TfrmCoordSysTrans.Button1Click(Sender: TObject);
var
  g2l: TGps2Locale;
  //centralLong: Double;
  //n: Integer;
  x, y, z: Double;

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

  g2l.Transform(23.062279545594, 113.322852104270, 11, coordSysParam,  x, y, z);
  ShowMessage('x: ' + FloatToStr(x) + ', y: ' + FloatToStr(y) + ', z: ' + FloatToStr(z));
end;

end.
