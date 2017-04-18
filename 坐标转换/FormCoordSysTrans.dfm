object frmCoordSysTrans: TfrmCoordSysTrans
  Left = 308
  Top = 233
  Width = 760
  Height = 473
  BorderIcons = [biSystemMenu]
  Caption = #22352#26631#36716#25442
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -12
  Font.Name = #23435#20307
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  PixelsPerInch = 96
  TextHeight = 12
  object GroupBox1: TGroupBox
    Left = 8
    Top = 8
    Width = 281
    Height = 201
    Caption = #19971#21442#25968#35774#32622
    TabOrder = 0
    object ledtSDX: TLabeledEdit
      Left = 8
      Top = 32
      Width = 121
      Height = 20
      EditLabel.Width = 66
      EditLabel.Height = 12
      EditLabel.Caption = 'X'#24179#31227#65288#31859#65289
      TabOrder = 0
      Text = '181.508222'
    end
    object ledtSDY: TLabeledEdit
      Left = 8
      Top = 72
      Width = 121
      Height = 20
      EditLabel.Width = 66
      EditLabel.Height = 12
      EditLabel.Caption = 'Y'#24179#31227#65288#31859#65289
      TabOrder = 1
      Text = '132.455046'
    end
    object ledtSDZ: TLabeledEdit
      Left = 8
      Top = 112
      Width = 121
      Height = 20
      EditLabel.Width = 66
      EditLabel.Height = 12
      EditLabel.Caption = 'Z'#24179#31227#65288#31859#65289
      TabOrder = 2
      Text = '49.213417'
    end
    object ledtSQX: TLabeledEdit
      Left = 144
      Top = 32
      Width = 121
      Height = 20
      EditLabel.Width = 78
      EditLabel.Height = 12
      EditLabel.Caption = 'X'#36724#26059#36716#65288#31186#65289
      TabOrder = 3
      Text = '0.442'
    end
    object ledtSQY: TLabeledEdit
      Left = 144
      Top = 72
      Width = 121
      Height = 20
      EditLabel.Width = 78
      EditLabel.Height = 12
      EditLabel.Caption = 'Y'#36724#26059#36716#65288#31186#65289
      TabOrder = 4
      Text = '-0.336'
    end
    object ledtSQZ: TLabeledEdit
      Left = 144
      Top = 112
      Width = 121
      Height = 20
      EditLabel.Width = 78
      EditLabel.Height = 12
      EditLabel.Caption = 'Z'#36724#26059#36716#65288#31186#65289
      TabOrder = 5
      Text = '-5.886'
    end
    object ledtSScale: TLabeledEdit
      Left = 144
      Top = 152
      Width = 121
      Height = 20
      EditLabel.Width = 42
      EditLabel.Height = 12
      EditLabel.Caption = #23610#24230'ppm'
      TabOrder = 6
      Text = '0.000007794586'
    end
  end
  object GroupBox2: TGroupBox
    Left = 296
    Top = 8
    Width = 145
    Height = 201
    Caption = #22235#21442#25968#35774#32622
    TabOrder = 1
    object ledtFDX: TLabeledEdit
      Left = 8
      Top = 32
      Width = 121
      Height = 20
      EditLabel.Width = 66
      EditLabel.Height = 12
      EditLabel.Caption = 'X'#24179#31227#65288#31859#65289
      TabOrder = 0
      Text = '-2531752.528456'
    end
    object ledtFDY: TLabeledEdit
      Left = 8
      Top = 72
      Width = 121
      Height = 20
      EditLabel.Width = 66
      EditLabel.Height = 12
      EditLabel.Caption = 'Y'#24179#31227#65288#31859#65289
      TabOrder = 1
      Text = '-374168.451418'
    end
    object ledtFRotateAngle: TLabeledEdit
      Left = 8
      Top = 112
      Width = 121
      Height = 20
      EditLabel.Width = 90
      EditLabel.Height = 12
      EditLabel.Caption = #26059#36716#35282#24230'T('#24359#24230')'
      TabOrder = 2
      Text = '-0.0048295512'
    end
    object ledtFScale: TLabeledEdit
      Left = 8
      Top = 152
      Width = 121
      Height = 20
      EditLabel.Width = 30
      EditLabel.Height = 12
      EditLabel.Caption = #23610#24230'K'
      TabOrder = 3
      Text = '0.999946022614282'
    end
  end
  object GroupBox3: TGroupBox
    Left = 448
    Top = 8
    Width = 281
    Height = 201
    Caption = #25237#24433#21442#25968#35774#32622
    TabOrder = 2
    object ledtProjectType: TLabeledEdit
      Left = 8
      Top = 32
      Width = 121
      Height = 20
      EditLabel.Width = 72
      EditLabel.Height = 12
      EditLabel.Caption = '3'#24230#24102#12289'6'#24230#24102
      TabOrder = 0
      Text = '3'
    end
    object ledtCentralMeridian: TLabeledEdit
      Left = 8
      Top = 72
      Width = 121
      Height = 20
      EditLabel.Width = 60
      EditLabel.Height = 12
      EditLabel.Caption = #20013#22830#23376#21320#32447
      TabOrder = 1
      Text = '114'
    end
    object ledtProjectScale: TLabeledEdit
      Left = 8
      Top = 112
      Width = 121
      Height = 20
      EditLabel.Width = 24
      EditLabel.Height = 12
      EditLabel.Caption = #23610#24230
      TabOrder = 2
      Text = '0'
    end
    object ledtConstantX: TLabeledEdit
      Left = 8
      Top = 152
      Width = 121
      Height = 20
      EditLabel.Width = 30
      EditLabel.Height = 12
      EditLabel.Caption = 'X'#24120#25968
      TabOrder = 3
      Text = '0'
    end
    object ledtConstantY: TLabeledEdit
      Left = 144
      Top = 32
      Width = 121
      Height = 20
      EditLabel.Width = 30
      EditLabel.Height = 12
      EditLabel.Caption = 'Y'#24120#25968
      TabOrder = 4
      Text = '500000'
    end
    object ledtBenchmarkLatitude: TLabeledEdit
      Left = 144
      Top = 72
      Width = 121
      Height = 20
      EditLabel.Width = 48
      EditLabel.Height = 12
      EditLabel.Caption = #22522#20934#32428#24230
      TabOrder = 5
      Text = '0'
    end
    object ledtSemiMajor: TLabeledEdit
      Left = 144
      Top = 112
      Width = 121
      Height = 20
      EditLabel.Width = 60
      EditLabel.Height = 12
      EditLabel.Caption = #26925#29699#38271#21322#36724
      TabOrder = 6
      Text = '6378245.0'
    end
    object ledtFlattening: TLabeledEdit
      Left = 144
      Top = 152
      Width = 121
      Height = 20
      EditLabel.Width = 48
      EditLabel.Height = 12
      EditLabel.Caption = #26925#29699#25153#29575
      TabOrder = 7
      Text = '0.00335232986925913509889373114314'
    end
  end
  object btnTranslate: TButton
    Left = 328
    Top = 248
    Width = 75
    Height = 25
    Caption = #36716#25442' ->'
    TabOrder = 3
    OnClick = btnTranslateClick
  end
  object GroupBox4: TGroupBox
    Left = 8
    Top = 224
    Width = 281
    Height = 193
    Caption = 'WGS84'
    TabOrder = 4
    object ledtWGS84GcsB: TLabeledEdit
      Left = 8
      Top = 37
      Width = 121
      Height = 20
      EditLabel.Width = 24
      EditLabel.Height = 12
      EditLabel.Caption = #32428#24230
      TabOrder = 0
    end
    object ledtWGS84GcsL: TLabeledEdit
      Left = 8
      Top = 77
      Width = 121
      Height = 20
      EditLabel.Width = 24
      EditLabel.Height = 12
      EditLabel.Caption = #32463#24230
      TabOrder = 1
    end
    object ledtWGS84GcsH: TLabeledEdit
      Left = 8
      Top = 117
      Width = 121
      Height = 20
      EditLabel.Width = 24
      EditLabel.Height = 12
      EditLabel.Caption = #39640#24230
      TabOrder = 2
      Text = '0'
    end
  end
  object GroupBox5: TGroupBox
    Left = 448
    Top = 224
    Width = 281
    Height = 193
    Caption = #24179#38754#22352#26631
    TabOrder = 5
    object ledtLocaleX: TLabeledEdit
      Left = 8
      Top = 37
      Width = 121
      Height = 20
      EditLabel.Width = 6
      EditLabel.Height = 12
      EditLabel.Caption = 'X'
      TabOrder = 0
    end
    object ledtLocaleY: TLabeledEdit
      Left = 8
      Top = 77
      Width = 121
      Height = 20
      EditLabel.Width = 6
      EditLabel.Height = 12
      EditLabel.Caption = 'Y'
      TabOrder = 1
    end
    object ledtLocaleZ: TLabeledEdit
      Left = 8
      Top = 117
      Width = 121
      Height = 20
      EditLabel.Width = 6
      EditLabel.Height = 12
      EditLabel.Caption = 'Z'
      TabOrder = 2
      Text = '0'
    end
  end
  object btnRevTranslate: TButton
    Left = 328
    Top = 296
    Width = 75
    Height = 25
    Caption = '<- '#36716#25442
    TabOrder = 6
    OnClick = btnRevTranslateClick
  end
end
