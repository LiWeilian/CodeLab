using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OracleClient;

namespace DS.OPC.Client
{
    partial class FormDBConnectionSetting : Form
    {
        class AutoInc
        {
            public string Name { get; set; }
            public int Index { get; set; }
        }

        class DataType
        {
            public string Name { get; set; }
            public int Index { get; set; }
        }
        class IsEntityIdentity
        {
            public string Name { get; set; }
            public int Index { get; set; }
        }
        class OPCItem
        {
            public string Name { get; set; }
            public int Index { get; set; }
        }
        public OPCUtils.DatabaseType DatabaseType { get { return (OPCUtils.DatabaseType)cbDatabaseType.SelectedIndex; } }
        public string Server { get { return txtServer.Text.Trim(); } }
        public string Database { get { return txtDatabase.Text.Trim(); } }
        public string UserName { get { return txtUserName.Text.Trim(); } }
        public string Password { get { return txtPassword.Text.Trim(); } }
        public string TableName { get { return txtNewDataTableName.Text.Trim(); } }
        public string HistoryTableName { get { return txtHistoryDataTableName.Text.Trim(); } }

        public List<OPCClientDBFieldMapping> DBFieldMappings
        {
            get
            {
                List<OPCClientDBFieldMapping> ret = new List<OPCClientDBFieldMapping>();
                
                IEnumerable<OPCClientDBFieldMapping> fieldMappings =
                    ((List<OPCClientDBFieldMapping>)dvgDBFieldMapping.DataSource)
                        .Where(x => x.FieldName.Trim() != string.Empty);

                foreach (OPCClientDBFieldMapping fieldMapping in fieldMappings)
                {
                    ret.Add(fieldMapping.Clone());
                }
                return ret;
            }
        }
        public FormDBConnectionSetting(OPCUtils.DatabaseType dbType, string server, string database, 
            string userName, string password, string tableName, string hisTableName, List<OPCClientDBFieldMapping> dbFieldMappings)
        {
            InitializeComponent();
            cbDatabaseType.SelectedIndex = (int)dbType;
            txtServer.Text = server;
            txtDatabase.Text = database;
            txtUserName.Text = userName;
            txtPassword.Text = password;
            txtNewDataTableName.Text = tableName;
            txtHistoryDataTableName.Text = hisTableName;

            InitializeDataGridViewComboBox();
            DisplayDBFieldMappings(CloneFieldMappings(dbFieldMappings));
        }

        private List<OPCClientDBFieldMapping> CloneFieldMappings(List<OPCClientDBFieldMapping> dbFieldMappings)
        {
            List<OPCClientDBFieldMapping> cloneMappings = new List<OPCClientDBFieldMapping>();
            foreach (OPCClientDBFieldMapping dbFieldMapping in dbFieldMappings)
            {
                cloneMappings.Add(dbFieldMapping.Clone());
            }
            return cloneMappings;
        }
        private void InitializeDataGridViewComboBox()
        {
            List<AutoInc> autoIncDS = new List<AutoInc>();
            AutoInc autoIncItem = new AutoInc();
            autoIncItem.Name = OPCClientDBFieldMapping.EnumAutoInc.NO.ToString();
            autoIncItem.Index = (int)OPCClientDBFieldMapping.EnumAutoInc.NO;
            autoIncDS.Add(autoIncItem);
            autoIncItem = new AutoInc();
            autoIncItem.Name = OPCClientDBFieldMapping.EnumAutoInc.YES.ToString();
            autoIncItem.Index = (int)OPCClientDBFieldMapping.EnumAutoInc.YES;
            autoIncDS.Add(autoIncItem);
            colAutoInc.DataSource = autoIncDS;
            colAutoInc.ValueMember = "Index";
            colAutoInc.DisplayMember = "Name";

            List<DataType> dtDS = new List<DataType>();
            DataType dt = new DataType();
            dt.Name = OPCClientDBFieldMapping.EnumDataType.INT.ToString();
            dt.Index = (int)OPCClientDBFieldMapping.EnumDataType.INT;
            dtDS.Add(dt);
            dt = new DataType();
            dt.Name = OPCClientDBFieldMapping.EnumDataType.NUMERIC.ToString();
            dt.Index = (int)OPCClientDBFieldMapping.EnumDataType.NUMERIC;
            dtDS.Add(dt);
            dt = new DataType();
            dt.Name = OPCClientDBFieldMapping.EnumDataType.CHAR.ToString();
            dt.Index = (int)OPCClientDBFieldMapping.EnumDataType.CHAR;
            dtDS.Add(dt);
            dt = new DataType();
            dt.Name = OPCClientDBFieldMapping.EnumDataType.NCHAR.ToString();
            dt.Index = (int)OPCClientDBFieldMapping.EnumDataType.NCHAR;
            dtDS.Add(dt);
            dt = new DataType();
            dt.Name = OPCClientDBFieldMapping.EnumDataType.DATETIME.ToString();
            dt.Index = (int)OPCClientDBFieldMapping.EnumDataType.DATETIME;
            dtDS.Add(dt);
            dt = new DataType();
            dt.Name = OPCClientDBFieldMapping.EnumDataType.BINARY.ToString();
            dt.Index = (int)OPCClientDBFieldMapping.EnumDataType.BINARY;
            dtDS.Add(dt);
            colDataType.DataSource = dtDS;
            colDataType.ValueMember = "Index";
            colDataType.DisplayMember = "Name";

            List<OPCItem> opcItemDS = new List<OPCItem>();
            OPCItem opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.未设置.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.未设置;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_VALUE.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_VALUE;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_QUALITY.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_QUALITY;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_TIMESTAMP.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_TIMESTAMP;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_SERVERHANDLE.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_SERVERHANDLE;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_CLIENTHANDLE.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_CLIENTHANDLE;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_0.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_0;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_1.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_1;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_2.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_2;
            opcItemDS.Add(opcItem);
            opcItem = new OPCItem();
            opcItem.Name = OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_3.ToString();
            opcItem.Index = (int)OPCClientDBFieldMapping.EnumOPCItem.ITEM_ID_3;
            opcItemDS.Add(opcItem);
            colSourceOPCItem.DataSource = opcItemDS;
            colSourceOPCItem.ValueMember = "Index";
            colSourceOPCItem.DisplayMember = "Name";

            List<IsEntityIdentity> isEntityIdentityDS = new List<IsEntityIdentity>();
            IsEntityIdentity isi = new IsEntityIdentity();
            isi.Name = OPCClientDBFieldMapping.EnumIsEntityIdentity.NO.ToString();
            isi.Index = (int)OPCClientDBFieldMapping.EnumIsEntityIdentity.NO;
            isEntityIdentityDS.Add(isi);
            isi = new IsEntityIdentity();
            isi.Name = OPCClientDBFieldMapping.EnumIsEntityIdentity.YES.ToString();
            isi.Index = (int)OPCClientDBFieldMapping.EnumIsEntityIdentity.YES;
            isEntityIdentityDS.Add(isi);
            colIsEntityIdentity.DataSource = isEntityIdentityDS;
            colIsEntityIdentity.ValueMember = "Index";
            colIsEntityIdentity.DisplayMember = "Name";
        }
        public void DisplayDBFieldMappings(List<OPCClientDBFieldMapping> fieldMappings)
        {   
            dvgDBFieldMapping.DataSource = fieldMappings;
        }


        private bool TestMSSQLConnection(out string errMsg)
        {
            errMsg = string.Empty;
            SqlConnectionStringBuilder sqlConnBuilder = new SqlConnectionStringBuilder();
            sqlConnBuilder.DataSource = this.Server;
            sqlConnBuilder.InitialCatalog = this.Database;
            sqlConnBuilder.UserID = this.UserName;
            sqlConnBuilder.Password = this.Password;
            OPCMSSQLHelper sqlHelper = new OPCMSSQLHelper(sqlConnBuilder.ConnectionString, out errMsg);
            try
            {
                sqlHelper.Connected = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
            
            string querClause = string.Format("SELECT TOP 1 * FROM {0}", TableName);
            DataTable dt = sqlHelper.QueryRecords(querClause, out errMsg);

            if (dt == null)
            {
                return false;
            }

            querClause = string.Format("SELECT TOP 1 * FROM {0}", HistoryTableName);
            dt = sqlHelper.QueryRecords(querClause, out errMsg);
            return dt != null;
        }

        private bool TestOracleConnection(out string errMsg)
        {
            errMsg = string.Empty;
            OracleConnectionStringBuilder oraConnBuilder = new OracleConnectionStringBuilder();
            oraConnBuilder.DataSource = this.Server;
            oraConnBuilder.UserID = this.UserName;
            oraConnBuilder.Password = this.Password;
            OPCOracleSQLHelper oraHelper = new OPCOracleSQLHelper(oraConnBuilder.ConnectionString, out errMsg);
            try
            {
                oraHelper.Connected = true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }

            string querClause = string.Format("SELECT * FROM {0} WHERE ROWNUM <= 1", TableName);
            DataTable dt = oraHelper.QueryRecords(querClause, out errMsg);

            if (dt == null)
            {
                return false;
            }

            querClause = string.Format("SELECT * FROM {0} WHERE ROWNUM <= 1", HistoryTableName);
            dt = oraHelper.QueryRecords(querClause, out errMsg);
            return dt != null;
        }

        private void AddDBField()
        {
            OPCClientDBFieldMapping newMapping = new OPCClientDBFieldMapping();
            newMapping.FieldName = string.Empty;
            newMapping.SourceOPCItem = (int)OPCClientDBFieldMapping.EnumOPCItem.未设置;
            newMapping.SourceCustom = string.Empty;
            newMapping.DataType = (int)OPCClientDBFieldMapping.EnumDataType.CHAR;
            newMapping.SeqName = string.Empty;
            newMapping.AutoInc = (int)OPCClientDBFieldMapping.EnumAutoInc.NO;
            newMapping.IsEntityIdentity = (int)OPCClientDBFieldMapping.EnumIsEntityIdentity.NO;

            //克隆数据，直接插入数据赋值回去会报“索引-1没有值”错误
            List<OPCClientDBFieldMapping> dbFieldMappings = 
                CloneFieldMappings((List<OPCClientDBFieldMapping>)dvgDBFieldMapping.DataSource);
            dbFieldMappings.Add(newMapping);
            //DataGridView必须这样更新数据
            dvgDBFieldMapping.DataSource = new List<OPCClientDBFieldMapping>();            
            dvgDBFieldMapping.DataSource = dbFieldMappings;

            dvgDBFieldMapping.CurrentCell = dvgDBFieldMapping.Rows[dvgDBFieldMapping.Rows.Count - 1].Cells[0];
        }
        private void RemoveField()
        {
            if (dvgDBFieldMapping.CurrentRow != null)
            {
                int index = dvgDBFieldMapping.CurrentRow.Index;
                List<OPCClientDBFieldMapping> dbFieldMappings = 
                    (List<OPCClientDBFieldMapping>)dvgDBFieldMapping.DataSource;
                dbFieldMappings.RemoveAt(index);
                dvgDBFieldMapping.DataSource = new List<OPCClientDBFieldMapping>();
                dvgDBFieldMapping.DataSource = dbFieldMappings;
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            switch (cbDatabaseType.SelectedIndex)
            {
                case 0:
                    if (TestMSSQLConnection(out errMsg))
                    {
                        MessageBox.Show("测试通过");
                    } else
                    {
                        MessageBox.Show(errMsg);
                    }
                    break;
                case 1:
                    if (TestOracleConnection(out errMsg))
                    {
                        MessageBox.Show("测试通过");
                    }
                    else
                    {
                        MessageBox.Show(errMsg);
                    }
                    break;
                default:
                    MessageBox.Show("未支持此类型的数据库");
                    break;
            }
        }

        private void cbDatabaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbDatabaseType.SelectedIndex)
            {
                case 0:
                    txtDatabase.Enabled = true;
                    break;
                case 1:
                    txtDatabase.Text = string.Empty;
                    txtDatabase.Enabled = false;
                    break;
                default:
                    break;
            }
        }

        private void btnAddDBField_Click(object sender, EventArgs e)
        {
            AddDBField();
        }

        private void btnRemoveDBField_Click(object sender, EventArgs e)
        {
            RemoveField();
        }

    }
}
