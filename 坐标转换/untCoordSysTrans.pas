unit untCoordSysTrans;

interface

uses
  SysUtils, Math, Types;

type
  TCoordSysParam = class(TObject)
  public
    CSName: WideString;
    //�߲������ã�ע���Թ���Ϊ����
    SDX: Double;          //181.508222;//Xƽ�ƣ��ף�
    SDY: Double;          //132.455046;//Yƽ�ƣ��ף�
    SDZ: Double;          //49.213417;//Zƽ�ƣ��ף�
    SQX: Double;          //0.442;//X����ת���룩
    SQY: Double;          //-0.336;//Y����ת���룩
    SQZ: Double;          //-5.886;//Z����ת���룩
    SScale: Double;       //0.000007794586;//�߶�ppm
    //�Ĳ������ã�ע���Թ���Ϊ����
    FDX: Double;          //-2531752.528456;//ƽ��X�����ף�
    FDY: Double;          //-374168.451418;//ƽ��Y�����ף�
    FScale: Double;       //0.999946022614282; //�߶�K
    FRotateAngle: Double; //-0.0048295512;//��ת�Ƕ�T(����)

    //ͶӰ���� ��ע����BJ54�����ݱ�������ϵΪ����
    PProjectionType: Integer;   //3��3�ȴ���6�ȴ�
    PCentralMeridian: Double;   //114������������
    PScale: Double;             //0���߶�
    PConstantX: Double;         //0��X����
    PConstantY: Double;         //500000��Y����
    PBenchmarkLatitude: Double; //0����׼γ��
    SemiMajor: Double;          //6378245.0�����򳤰���54:6378245.0,80:6378140.0,84:6378137
    Flattening: Double;         //1.0/298.3���������54��1.0 / 298.3�� 80��1.0 / 298.257;84:1.0/298.2572236
  end;

  TGps2Locale = class(TObject)
  private
  public
    ///�������򳤶�����������һƫ���ʣ�����������ƽ��ֵ
    function Cal_e(a, b: Double; Id: Integer): Double;
    ///��������ĳ��̰��ᣬ����γ��Bl����î��Ȧ���ʰ뾶
    function Cal_N(a1, b1, B: Double): Double;
    ///�������򳤰���ͱ��ʼ���̰���
    function Cal_b(a, f: Double): Double;
    ///�ȣ�С����ʽ��ת����
    function DvToRad(x: Double): Double;
    ///�ȷ���ת����
    function DMSToRad(d, m, s: Double): Double;
    ///�ȷ���ת�ȣ�С����ʽ��
    function DmsToDv(d, m, s: Double): Double;
    ///�������(B��L��H)ת�ռ�ֱ�����꣨X��Y��Z������a1��b1��Ϊ���򳤶���
    procedure BLH2XYZ(B, L, H, a1, b1: Double; out X, Y, Z: Double);
    ///�ռ�ֱ�����꣨X��Y��Z��ת�������(B��L��H)����a1��b1��Ϊ���򳤶��ᣬdBΪγ����������λ���룩���磺0.00000001
    procedure XYZ2BLH(X, Y, Z, a1, b1, dB: Double; out B, L, H: Double);
    ///���������ֵ��ȡ��λ�ǣ����ȣ�
    function Azimuth(dx, dy: Double): Double;
    ///���ݾ��ȣ�С����ʽ����ȡ���������߾��ȣ�С����ʽ��
    function LongOfCentralMeridian(longtitude: Double; Id: Integer; out centralLong: Double): Integer;
    ///���������߻�������γ����Ϊ������γ����Ϊ��
    function MeridianDis(B, a1, f: Double): Double;
    ///���ø�˹ͶӰ��BLת��Ϊxy���ǶȾ�Ϊ���ȣ�L0�����������ߣ�a�������峤���ᣬf��������ʣ�off_X,off_Y��ƽ�Ʋ���
    procedure BL2xy(B, L, H, L0, a, f, off_X, off_Y: Double; out x, y, z: Double);
    ///�ռ�ֱ������ת��gps2locale
    procedure CoordTran7(x, y, z: Double; params: TDoubleDynArray; out _x, _y, _z: Double);
    ///�ռ�ֱ������ת��locale2gps
    procedure RevCoordTran7(x, y, z: Double; params: TDoubleDynArray; out _x, _y, _z: Double);

    ///.WGS84 To Locale
    procedure Transform(B, L, H: Double; coordSysParam: TCoordSysParam;
      out localeX, localeY, localeZ: Double);
    ///.Locale To WGS84
    procedure RevTransform(localeX, localeY, localeZ: Double; coordSysParam: TCoordSysParam;
      B, L, H: Double);
  end;

implementation

uses DateUtils;

{ TGps2Locale }

function TGps2Locale.Azimuth(dx, dy: Double): Double;
var
  s: Double;
  t: Double;
  ang: Double;
begin
  t := Abs(dy);
  s := Sqrt(dx * dx + dy * dy);
  if s = 0 then
  begin
    // ��������㣬����һ������360�Ľǣ���ô�˷�λ����Ч��
    Result := Pi * 3;
    Exit;
  end;

  ang := Math.ArcSin(t / s);

  if (dy = 0) and (dx > 0) then
    ang := 0;
  if (dy = 0) and (dx < 0) then
    ang := Pi;
  if (dx >= 0) and (dy < 0) then
    ang := Pi * 2 - ang;
  if (dx < 0) and (dy > 0) then
    ang := Pi - ang;
  if (dx < 0) and (dy < 0) then
    ang := Pi + ang;

  Result := ang;
end;

procedure TGps2Locale.BL2xy(B, L, H, L0, a, f, off_X, off_Y: Double; out x,
  y, z: Double);
var
  l_, t, nt: Double;
  l2, l4, l6: Double;
  l3, l5: Double;
  t2, t4: Double;
  N, b1, e2: Double;
  X1: Double;
  cos3, cos5: Double;
begin
  b1 := Cal_b(a, f);
  N := Cal_N(a, b1, B);
  t := Tan(B);
  t2 := t * t;
  t4 := t2 * t2;
  e2 := Cal_e(a, b1, 2);
  nt := e2 * e2 * Cos(B) * Cos(B);
  X1 := MeridianDis(B, a, f);
  l_ := L - L0;
  l2 := l_ * l_;
  l3 := l2 * l_;
  l4 := l3 * l_;
  l5 := l4 * l_;
  l6 := l5 * l_;
  cos3 := Cos(B) * Cos(B) * Cos(B);
  cos5 := cos3 * Cos(B) * Cos(B);

  x := 0.5 * N * Sin(B) * Cos(B) * l2 + N / 24 * Sin(B)
				* cos3 * (5 - t2 + 9 * nt + 4 * nt * nt) * l4 + N / 720
				* Sin(B) * cos5 * (61 - 58 * t2 + t4) * l6;
  x := X1 + x;
  x := x + off_X;

  y := N * Cos(B) * l_ + N / 6 * cos3 * (1 - t2 + nt) * l3 + N / 120
				* cos5 * (5 - 18 * t2 + t4 + 14 * nt - 58 * nt * t2) * l5;
	y := y + off_Y;

  z := H;
  (*
		double l, t, nt;
		double l2, l4, l6;
		double l3, l5;
		double t2, t4;
		double N, b, e2;
		double X;
		double cos3, cos5;
		b = cal_b(a, f);
		N = cal_N(a, b, B);
		t = Math.tan(B);
		t2 = t * t;
		t4 = t2 * t2;
		e2 = cal_e(a, b, 2);
		nt = e2 * e2 * Math.cos(B) * Math.cos(B);
		X = meridianDis(B, a, f);
		l = L - L0;
		l2 = l * l;
		l3 = l2 * l;
		l4 = l3 * l;
		l5 = l4 * l;
		l6 = l5 * l;
		cos3 = Math.cos(B) * Math.cos(B) * Math.cos(B);
		cos5 = cos3 * Math.cos(B) * Math.cos(B);
		_X = 0.5 * N * Math.sin(B) * Math.cos(B) * l2 + N / 24 * Math.sin(B)
				* cos3 * (5 - t2 + 9 * nt + 4 * nt * nt) * l4 + N / 720
				* Math.sin(B) * cos5 * (61 - 58 * t2 + t4) * l6;
		_X = X + _X;
		// ����Xƫ��
		_X = _X + off_X;
		_Y = N * Math.cos(B) * l + N / 6 * cos3 * (1 - t2 + nt) * l3 + N / 120
				* cos5 * (5 - 18 * t2 + t4 + 14 * nt - 58 * nt * t2) * l5;
		_Y = _Y + off_Y;
		return true;
  *)
end;

procedure TGps2Locale.BLH2XYZ(B, L, H, a1, b1: Double; out X, Y,
  Z: Double);
var
  e, N: Double;
begin
  e := Cal_e(a1, b1, 1);
  N := Cal_N(a1, b1, B);
  X := (N + H) * Cos(B) * Cos(L);
  Y := (N + H) * Cos(B) * Sin(L);
  Z := (N * (1 - e * e) + H) * Sin(B);
end;

function TGps2Locale.Cal_b(a, f: Double): Double;
begin
  Result :=  a * (1 - f);
end;

function TGps2Locale.Cal_e(a, b: Double; Id: Integer): Double;
var
  tp: Double;
begin
  tp := 0;
  case Id of
    1:
      begin
        tp := Sqrt((a * a - b * b)/(a * a));
      end;
    2:
      begin
        tp := Sqrt((a * a - b * b)/(b * b));
      end;
  end;

  Result := tp;
end;

function TGps2Locale.Cal_N(a1, b1, B: Double): Double;
var
  tp, e1: Double;
begin
  e1 := Cal_e(a1, b1, 1);
  tp := 1 - e1 * e1 * Sin(B) * Sin(B);
  tp := Sqrt(tp);
  tp := a1 / tp;

  Result := tp;
end;

procedure TGps2Locale.CoordTran7(x, y, z: Double; params: TDoubleDynArray;
  out _x, _y, _z: Double);
var
  Rou: Double;
begin
  Rou := 180.0 / Pi * 3600.0;
  _x := params[0] + x * (1 + params[3]) + y * params[6] / Rou - z * params[5] / Rou;
  _y := params[1] + y * (1 + params[3]) - x * params[6] / Rou + z * params[4] / Rou;
  _z := params[2] + z * (1 + params[3]) + x * params[5] / Rou - y * params[4] / Rou;

{
		double PI;
		double Rou;
		PI = Math.atan(1) * 4.0;
		Rou = 180.0 / PI * 3600.0;
		_x = Para[0] + X0 * (1 + Para[3]) + Y0 * Para[6] / Rou - Z0 * Para[5]
				/ Rou;
		_y = Para[1] + Y0 * (1 + Para[3]) - X0 * Para[6] / Rou + Z0 * Para[4]
				/ Rou;
		_z = Para[2] + Z0 * (1 + Para[3]) + X0 * Para[5] / Rou - Y0 * Para[4]
				/ Rou;
}
end;

function TGps2Locale.DmsToDv(d, m, s: Double): Double;
begin

end;

function TGps2Locale.DMSToRad(d, m, s: Double): Double;
begin

end;

function TGps2Locale.DvToRad(x: Double): Double;
begin
  Result := x * Pi / 180.0;
end;

function TGps2Locale.LongOfCentralMeridian(longtitude: Double;
  Id: Integer; out centralLong: Double): Integer;
var
  n: Integer;
  long1: Double;
  div_t: Integer;
begin
  if Id = 6 then
  begin
    n := Floor(longtitude / 6);
    //ȡ������modֻ����������
    div_t := Floor(longtitude) div 6;
    long1 := longtitude - div_t * 6;
    if long1 > 0 then
      n := n + 1;
    centralLong := n * 6 - 3;
  end else
  begin
    n := Floor((longtitude - 1.5) / 3);
    div_t := Floor(longtitude) div 3;
    long1 := longtitude - div_t * 3;
    if long1 > 0 then
      n := n + 1;

    centralLong := n * 3;
  end;

  Result := n;
end;

function TGps2Locale.MeridianDis(B, a1, f: Double): Double;
var
  A1_, B1_, C1, D1, E1_, F1: Double;
  e1, e12, e14, e16, e18, e10, e102: Double;
  b1: Double;
  sin2, sin4, sin6, sin8, sin10: Double;
begin
  b1 := Cal_b(a1, f);
  e1 := Cal_e(a1, b1, 1);
  e12 := e1 * e1;
  e14 := e12 * e12;
  e16 := e12 * e14;
  e18 := e14 * e14;
  e10 := e18 * e12;
  e102 := e12 * e10;

  A1_ := 1 + 0.75 * e12 + 45.0 / 64 * e14 + 175.0 /256 * e16 + 11025.0 / 16384 * e18
           + 43659.0 / 65536 * e102;
  B1_ := 0.75 * e12 + 15.0 / 16 * e14 + 525.0 / 512 * e16 + 2205.0 / 2048
				* e18 + 72765.0 / 65536 * e102;
  C1 := 15.0 / 64 * e14 + 105.0 / 256 * e16 + 2205.0 / 4096 * e18
				+ 10395.0 / 16384 * e102;
  D1 := 35.0 / 512 * e16 + 315.0 / 2048 * e18 + 31185.0 / 131072 * e102;
  E1_ := 315.0 / 16384 * e18 + 3465.0 / 65536 * e102;
  F1 := 693.0 / 131072 * e102;

  sin2 := Sin(2 * B);
  sin4 := Sin(4 * B);
  sin6 := Sin(6 * B);
  sin8 := Sin(8 * B);
  sin10 := Sin(10 * B);

  Result := a1
				* (1 - e12)
				* (A1_ * B - 0.5 * B1_ * sin2 + 0.25 * C1 * sin4 - D1 * sin6 / 6
						+ E1_ * sin8 / 8 - F1 * sin10 / 10);


(*
		double A1, B1, C1, D1, E1, F1;
		double e1, e12, e14, e16, e18, e10, e102;
		double b;
		double sin2, sin4, sin6, sin8, sin10;
		double s;

		b = cal_b(a, f);// z

		e1 = cal_e(a, b, 1);
		e12 = e1 * e1;
		e14 = e12 * e12;
		e16 = e12 * e14;
		e18 = e14 * e14;
		e10 = e18 * e12;
		e102 = e12 * e10;

		A1 = 1 + 0.75 * e12 + 45.0 / 64 * e14 + 175.0 / 256 * e16 + 11025.0
				/ 16384 * e18 + 43659.0 / 65536 * e102;
		B1 = 0.75 * e12 + 15.0 / 16 * e14 + 525.0 / 512 * e16 + 2205.0 / 2048
				* e18 + 72765.0 / 65536 * e102;
		C1 = 15.0 / 64 * e14 + 105.0 / 256 * e16 + 2205.0 / 4096 * e18
				+ 10395.0 / 16384 * e102;
		D1 = 35.0 / 512 * e16 + 315.0 / 2048 * e18 + 31185.0 / 131072 * e102;
		E1 = 315.0 / 16384 * e18 + 3465.0 / 65536 * e102;
		F1 = 693.0 / 131072 * e102;
		sin2 = Math.sin(2 * B);
		sin4 = Math.sin(4 * B);
		sin6 = Math.sin(6 * B);
		sin8 = Math.sin(8 * B);
		sin10 = Math.sin(10 * B);
		s = a
				* (1 - e12)
				* (A1 * B - 0.5 * B1 * sin2 + 0.25 * C1 * sin4 - D1 * sin6 / 6
						+ E1 * sin8 / 8 - F1 * sin10 / 10);
		return s;
*)
end;

procedure TGps2Locale.RevCoordTran7(x, y, z: Double;
  params: TDoubleDynArray; out _x, _y, _z: Double);
var
  Rou: Double;
  b0,b1,b2,b3,b4,b5,b6: Double;
begin
  Rou := 180.0 / Pi * 3600.0;

  b0 := x - params[0];
  b1 := y - params[1];
  b2 := z - params[2];
  b3 := 1 + params[3];
  b4 := params[4] / Rou;
  b5 := params[5] / Rou;
  b6 := params[6] / Rou;
		
  _x := ((b0*b3+b2*b5)*(b3*b3+b4*b4)-(b3*b6-b4*b5)*(b1*b3-b2*b4))/((b3*b3+b5*b5)*(b3*b3+b4*b4)+(b3*b6-b4*b5)*(b3*b6+b4*b5));
		
  _y := ((b0*b3+b2*b5)*(b3*b6+b4*b5)+(b3*b3+b5*b5)*(b1*b3-b2*b4))/((b3*b3+b5*b5)*(b3*b3+b4*b4)+(b3*b6-b4*b5)*(b3*b6+b4*b5));
		
  _z := (b4*_y-b5*_x+b2)/b3;
{
		double PI;
		double Rou;
		PI = Math.atan(1) * 4.0;
		Rou = 180.0 / PI * 3600.0;
		
		double b0,b1,b2,b3,b4,b5,b6;
		
		b0=X2-Para[0];
		b1=Y2-Para[1];
		b2=Z2-Para[2];
		b3=1+Para[3];
		b4=Para[4]/Rou;
		b5=Para[5]/Rou;
		b6=Para[6]/Rou;
		
		_x = ((b0*b3+b2*b5)*(b3*b3+b4*b4)-(b3*b6-b4*b5)*(b1*b3-b2*b4))/((b3*b3+b5*b5)*(b3*b3+b4*b4)+(b3*b6-b4*b5)*(b3*b6+b4*b5));
		
		_y = ((b0*b3+b2*b5)*(b3*b6+b4*b5)+(b3*b3+b5*b5)*(b1*b3-b2*b4))/((b3*b3+b5*b5)*(b3*b3+b4*b4)+(b3*b6-b4*b5)*(b3*b6+b4*b5));
		
		_z = (b4*_y-b5*_x+b2)/b3;
}
end;

procedure TGps2Locale.RevTransform(localeX, localeY, localeZ: Double;
      coordSysParam: TCoordSysParam; B, L, H: Double);
begin
  //�Ĳ�������

end;

procedure TGps2Locale.Transform(B, L, H: Double;
  coordSysParam: TCoordSysParam; out localeX, localeY, localeZ: Double);
var
  wgs84srcsX, wgs84srcsY, wgs84srcsZ: Double;
  localeSrcsX, localeSrcsY, localeSrcsZ: Double;
  localeGcsB, localeGcsL, localeGcsH: Double;
  localeX7, localeY7, localeZ7: Double;
  params: TDoubleDynArray;
  semiMajor, semiMinor: Double;
  id_Zone: Integer;
  cur_Zone: Integer;
  centralMeridian: Double;
  c, d, k, t: Double;
  a0, b0: Double;
  xg, yg: Double;
begin
  //WGS84 ������� to �ռ�ֱ������
  BLH2XYZ(DegToRad(B), DegToRad(L), H, 6378137, Cal_b(6378137, 1 / 298.257224), wgs84srcsX, wgs84srcsY, wgs84srcsZ);

  SetLength(params, 7);
  params[0] := coordSysParam.SDX;
  params[1] := coordSysParam.SDY;
  params[2] := coordSysParam.SDZ;
  params[3] := coordSysParam.SScale;
  params[4] := coordSysParam.SQX;
  params[5] := coordSysParam.SQY;
  params[6] := coordSysParam.SQZ;

  //WGS84�ռ�ֱ������ to Ŀ��������ռ�ֱ������
  CoordTran7(wgs84srcsX, wgs84srcsY, wgs84srcsZ, params, localeSrcsX, localeSrcsY, localeSrcsZ);

  semiMajor := coordSysParam.SemiMajor;
  semiMinor := Cal_b(semiMajor, coordSysParam.Flattening);

  //Ŀ��������ռ�ֱ������ to �������
  XYZ2BLH(localeSrcsX, localeSrcsY, localeSrcsZ, semiMajor, semiMinor, 0.00000001,
    localeGcsB, localeGcsL, localeGcsH);

  BL2xy(localeGcsB, localeGcsL, localeGcsH, DegToRad(coordSysParam.PCentralMeridian),
    coordSysParam.SemiMajor, coordSysParam.Flattening,
    coordSysParam.PConstantX, coordSysParam.PConstantY,
    localeX7, localeY7, localeZ7);

  //�Ĳ���ת��
  if (coordSysParam.FDX = 0.0) and (coordSysParam.FDY = 0.0) then
  begin
    localeX := localeY7;
    localeY := localeX7;
    localeZ := localeZ7;
  end else
  begin
    c := coordSysParam.FDX;
    d := coordSysParam.FDY;
    k := coordSysParam.FScale;
    t := coordSysParam.FRotateAngle;

    a0 := k * Cos(t);
    b0 := k * Sin(t);
    xg := c + a0 * localeX7 - b0 * localeY7;
    yg := d + b0 * localeX7 + a0 * localeY7;

    if IsNan(yg) then
      localeX := 0
    else
      localeX := yg;

    if IsNan(xg) then
      localeY := 0
    else
      localeY := xg;

    if IsNan(localeZ7) then
      localeZ := 0
    else
      localeZ := localeZ7;
  end;
  {
  double c = -2531752.528456;// .getM_ITI_F_DX();
		double d = -374168.451418;// .getM_ITI_F_DY();
		double k = 0.999946022614282;// .getM_ITI_F_Scale();
		double t = -0.0048295512;// .getM_ITI_F_RotateAngle();


		double a = k * Math.cos(t);
		double b = k * Math.sin(t);
		double xg = c + a * gps2locale._X - b * gps2locale._Y;
		double yg = d + b * gps2locale._X + a * gps2locale._Y;


		gps2locale._dLocalX = yg == Double.NaN ? 0 : yg;
		gps2locale._dLocalY = xg == Double.NaN ? 0 : xg;
		gps2locale._dLocalZ = gps2locale._Z == Double.NaN ? 0 : gps2locale._Z;
  }
end;

procedure TGps2Locale.XYZ2BLH(X, Y, Z, a1, b1, dB: Double; out B, L,
  H: Double);
var
  N0: Double;
  B0: Double;
  e1: Double;
  Bi: Double;
  t1, t2: Double;
  p2: Double;
  times: Integer;

begin
  times := 0;

  if (X = 0) and (Y = 0) then
  begin
    if Z > 0 then
    begin
      B := Pi / 2;
      L := 0;
      H := Z - b1;
      Exit;
    end else if Z < 0 then
    begin
      B := Pi / -2;
      L := 0;
      H := Z - b1;
      Exit;
    end;
  end;

  if Z = 0 then
  begin
    L := Azimuth(X, Y);
    B := 0;
    H := Sqrt(X * X + Y * Y) - a1;
    Exit;
  end;

  t2 := Abs(Z);
  //�ȼ��㾭��
  L := Azimuth(X, Y);

  e1 := Cal_e(a1, b1, 1);
  t1 := Sqrt(X * X + Y * Y);

  // ��������
  B0 := ArcTan(t2 / t1);
  N0 := a1 / Sqrt(1 - e1 * e1 * Sin(B0) * Sin(B0));

  repeat
    Bi := t2 + N0 * e1 * e1 * Sin(B0);
    Bi := Bi / t1;
    Bi := ArcTan(Bi);
    p2 := Bi - B0;
    p2 := RadToDeg(p2) * 3600;

    Inc(times);

    B0 := Bi;
    N0 := a1 / Sqrt(1 - e1 * e1 * Sin(B0) * Sin(B0));

    if times > 1000 then
      Break;
  until (p2 <= dB);

  B := Abs(Bi);
  H := t2 / Sin(Bi) - N0 * (1 - e1 * e1);
end;

end.
