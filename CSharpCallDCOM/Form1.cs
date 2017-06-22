﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharpCallDCOM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            try
            {

                System.Guid guid = new Guid(txtClsID.Text);

                System.Type t = Type.GetTypeFromCLSID(guid, txtServerIP.Text, true);
                //System.Type t = Type.GetTypeFromProgID(txtClsID.Text, txtServerIP.Text);
                object COMobject = System.Activator.CreateInstance(t);
                //DCOMclass myclass = (DCOMclass)COMobject;
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
