using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using OPCAutomation;

namespace DS.OPC.Client
{
    public partial class frmMain : Form
    {
        #region OPC变量
        int m_OPCGroupIndex = 1;
        int m_OPCItemClientHandle = 1;

        OPCServer m_OPCServer = null;
        OPCBrowser m_OPCBrowser = null;

        #endregion

        #region 系统变量
        private List<OPCClientBLL> m_blls = new List<OPCClientBLL>();
        private bool m_currentNew = false;
        private OPCConfig m_opcConfig;
        //用于指定的OPC项映射
        private OPCItemMappingConfig m_OPCItemMappingConfig;
        #endregion
        public frmMain()
        {
            InitializeComponent();
            InitializeOPCItemMappingConfig();
        }

        #region 系统方法
        private void ShowErrorMessageBox(string errMsg)
        {
            MessageBox.Show(errMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private string GetLocalTimeFromUTC(object utcTime)
        {
            string localTime = string.Empty;
            if (utcTime != null)
            {
                try
                {
                    DateTime utc = Convert.ToDateTime(utcTime.ToString());
                    return TimeZoneInfo.ConvertTimeFromUtc(utc, TimeZoneInfo.Local).ToString();
                }
                catch
                {
                    return string.Empty;
                }

            }
            else
            {
                return string.Empty;
            }
        }

        private void InitializeOPCItemMappingConfig()
        {
            try
            {
                string configFile = Path.GetDirectoryName(Application.ExecutablePath) + "\\..\\config\\OPCItemMapping.xml";
                this.m_OPCItemMappingConfig = new OPCItemMappingConfig(configFile);
            }
            catch (Exception e)
            {
            }
            
        }
        #endregion

        #region OPC服务器连接
        private void ListServers(string serverIP)
        {
            try
            {
                m_OPCServer = new OPCServer();
                object serverList = m_OPCServer.GetOPCServers(serverIP);
                if (serverList == null)
                {
                    ShowErrorMessageBox("无法获取服务器");
                    return;
                } else
                {
                    foreach (string serverName in (Array)serverList)
                    {
                        cbServers.Items.Add(serverName);
                    }
                    cbServers.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("获取服务器出现错误：" + ex.Message);
                return;
            }
        }

        private void ClearServers()
        {   
            try
            {
                if (m_OPCServer != null)
                {
                    m_OPCServer.OPCGroups.GlobalDataChange -= OPCGroups_GlobalDataChange;
                    m_OPCServer.Disconnect();
                }
            }
            catch
            {
            }
            cbServers.Items.Clear();
            
        }

        private bool ConnectServer(string opcServerName, string serverIP)
        {
            try
            {
                if (m_OPCServer == null)
                {
                    return false;
                } else
                {
                    m_OPCServer.Connect(opcServerName, serverIP);
                    if (m_OPCServer.ServerState == (int)OPCServerState.OPCRunning)
                    {
                        tsslOPCServerState.Text = "已连接到：" + m_OPCServer.ServerNode + " - " + m_OPCServer.ServerName + "   ";

                        tsslOPCServerStartTime.Text = "服务器开始时间：" 
                            + GetLocalTimeFromUTC(m_OPCServer.StartTime) + "   ";
                        
                        //m_OPCServer.OPCGroups.GlobalDataChange += 
                        //    new DIOPCGroupsEvent_GlobalDataChangeEventHandler(OPCGroups_GlobalDataChange);
                        //m_OPCServer.OPCGroups.DefaultGroupUpdateRate = 1000;

                        m_OPCServer.ServerShutDown += 
                            new DIOPCServerEvent_ServerShutDownEventHandler(OPCServer_ServerShutDown);

                        return true;
                    }
                    else
                    {
                        tsslOPCServerState.Text = "连接失败，状态：" + m_OPCServer.ServerState.ToString();
                        tsslOPCServerStartTime.Text = string.Empty;
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("连接远程服务器出现错误：" + ex.Message);
                return false;
            }
        }

        void OPCServer_ServerShutDown(string Reason)
        {
            
        }

        private bool DisconnectServer()
        {
            try
            {
                if (m_OPCServer != null && m_OPCServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    m_OPCServer.Disconnect();
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("断开远程服务器出现错误：" + ex.Message);
                return false;
            }
        }
        #endregion

        #region OPC Group
        private OPCGroup CreateNewGroup(string groupName)
        {
            OPCGroup newGroup = null;
            try
            {
                if (m_OPCServer != null && m_OPCServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    newGroup = m_OPCServer.OPCGroups.Add(groupName);
                    if (newGroup != null)
                    {
                        try
                        {
                            newGroup.IsSubscribed = true;
                            newGroup.TimeBias = 2;
                        }
                        catch (Exception ex)
                        {
                            ShowErrorMessageBox("设置订阅模式出现错误：" + ex.Message);
                        }
                        newGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(OPCGroup_DataChange);
                        newGroup.AsyncReadComplete += new DIOPCGroupEvent_AsyncReadCompleteEventHandler(OPCGroup_AsyncReadComplete);
                        m_OPCGroupIndex++;
                    }   
                }                             
            }
            catch (Exception ex)
            {
                newGroup = null;
                ShowErrorMessageBox("添加组出现错误：" + ex.Message);
            }
            return newGroup;
        }

        private void CheckGroupUpdateStatus()
        {
            DateTime now = DateTime.Now;
            foreach (OPCClientBLL bll in this.m_blls)
            {
                if (bll.CheckIsTimeout(now))
                {
                    bll.BindGroup.IsActive = false;
                    Application.DoEvents();
                    bll.BindGroup.IsActive = true;
                    bll.LastUpdateTime = now;

                    //MessageBox.Show("Reactive Group : " + bll.BindGroup.Name);
                }
            }
        }

        private void RemoveGroup(OPCGroup group)
        {
            string groupName = group.Name;
            try
            {
                if (m_OPCServer != null && m_OPCServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    group.DataChange -= OPCGroup_DataChange;
                    m_OPCServer.OPCGroups.Remove(groupName);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("移除组 " + groupName + " 出现错误：" + ex.Message);
            }
        }

        private void ClearGroups()
        {
            lvGroupItems.Items.Clear();
            lvGroupItems.Tag = null;

            lvGroups.Items.Clear();

            lvGroups_SelectedIndexChanged(lvGroups, new EventArgs());
            DisplayGroupDataConnectionInfo(null);
        }

        private void DisplayGroupProperties(OPCGroup group)
        {
            if (group != null)
            {
                txtGPName.Text = group.Name;
                txtGPDeadBand.Text = group.DeadBand.ToString();
                txtGPUpdateRate.Text = group.UpdateRate.ToString();
                cbGPIsActive.SelectedIndex = Convert.ToInt32(group.IsActive);
                cbGPIsSubcribed.SelectedIndex = Convert.ToInt32(group.IsSubscribed);

                OPCClientBLL bll = GetGroupBindingBLL(group);
                chkCheckTimeout.Checked = bll != null && bll.AutoCheckUpdateStatus;
                txtGPUpdateTimeout.Text = bll != null && bll.AutoCheckUpdateStatus ? bll.TimeoutSetting.ToString() : string.Empty;
            }
            else
            {
                txtGPName.Text = string.Empty;
                txtGPDeadBand.Text = string.Empty;
                txtGPUpdateRate.Text = string.Empty;
                cbGPIsActive.SelectedIndex = 0;
                cbGPIsSubcribed.SelectedIndex = 0;
                chkCheckTimeout.Checked = false;
                txtGPUpdateTimeout.Enabled = false;
                txtGPUpdateTimeout.Text = string.Empty;
            }
        }

        private void DisplayGroupDataConnectionInfo(OPCGroup group)
        {
            if (group == null)
            {
                //chkAutoInsertToDB.Checked = false;
                tsslDBConnection.Text = string.Empty;
            }
            else
            {
                OPCClientBLL bll = GetGroupBindingBLL(group);
                //chkAutoInsertToDB.Checked = bll != null && bll.AutoInsert;
                tsslDBConnection.Text = string.Empty;
            }
        }

        private void SetGroupProperties(OPCGroup group, string groupName, bool isActive, bool isSubscried,
            int updateRate, float deadband)
        {
            try
            {
                group.Name = groupName;
                group.IsActive = isActive;
                group.IsSubscribed = isSubscried;
                group.UpdateRate = updateRate;
                group.DeadBand = deadband;
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("设置组属性时出现错误：" + ex.Message);
                return;
            }

            try
            {
                if (m_OPCServer != null && m_OPCServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    m_OPCServer.OPCGroups.DefaultGroupIsActive = isActive;
                    m_OPCServer.OPCGroups.DefaultGroupUpdateRate = updateRate;
                    m_OPCServer.OPCGroups.DefaultGroupDeadband = deadband;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("设置默认组属性时出现错误：" + ex.Message);
            }

        }

        private void SetGroupExtensionProperties(OPCGroup group, bool checkTimeout, int timeout)
        {
            OPCClientBLL bll = GetGroupBindingBLL(group);
            if (bll != null)
            {
                bll.AutoCheckUpdateStatus = checkTimeout;
                bll.TimeoutSetting = timeout >= 0 ? timeout : 0;
            }
        }

        private OPCClientBLL GetGroupBindingBLL(OPCGroup group)
        {
            foreach (OPCClientBLL bll in this.m_blls)
            {
                if (bll.BindGroup == group)
                {
                    return bll;
                }
            }
            return null;
        }

        private OPCGroup GetGroupByItemClientHandle(int itemClientHDL)
        {
            foreach (OPCGroup group in this.m_OPCServer.OPCGroups)
            {
                foreach (OPCItem item in group.OPCItems)
                {
                    if (item.ClientHandle == itemClientHDL)
                    {
                        return group;
                    }
                }

            }
            return null;
        }

        private void SetGroupControlStatus()
        {
            btnAddGroup.Enabled = m_OPCServer != null
                && m_OPCServer.ServerState == (int)OPCServerState.OPCRunning;
            btnRemoveGroup.Enabled = lvGroups.SelectedItems.Count > 0;
            btnGPApply.Enabled = lvGroups.SelectedItems.Count > 0;
            btnGPCancel.Enabled = lvGroups.SelectedItems.Count > 0;
        }
        #endregion

        #region 读取数据
        void OPCGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            //OPCUtils.LogDataChangeTime("数据刷新-开始");
            m_currentNew = true;
            //tsslDBConnection.Text = NumItems.ToString();
            OPCGroup group = GetGroupByItemClientHandle(int.Parse(ClientHandles.GetValue(1).ToString()));
            OPCClientBLL bll = GetGroupBindingBLL(group);
            if (bll != null)
            {
                bll.LastUpdateTime = DateTime.Now;
            }

            List<OPCItemData> itemsNew;

            //OPCUtils.LogDataChangeTime("数据刷新-刷新列表数据");
            AsyncRefreshGroupItems(NumItems, ClientHandles, ItemValues, Qualities, TimeStamps, out itemsNew);
            //OPCUtils.LogDataChangeTime("数据刷新-插入实时数据");            
            //InsertGroupItems(group, out itemsNew);
            UpdateNewGroupItems(group, itemsNew);
            //OPCUtils.LogDataChangeTime("数据刷新-插入历史数据");
            InsertDataChangedItems(bll, NumItems, ClientHandles, ItemValues, Qualities, TimeStamps, itemsNew);
            //OPCUtils.LogDataChangeTime("数据刷新-结束");
        }

        void OPCGroups_GlobalDataChange(int TransactionID, int GroupHandle, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            /*
            //lvGroupItems.Items.Clear();
            if (lvGroups.SelectedItems.Count == 1)
            {
                OPCGroup group = (OPCGroup)lvGroups.SelectedItems[0].Tag;
                if (group.ServerHandle == GroupHandle)
                {
                    for (int i = 1; i <= NumItems; i++)
                    {
                        ListViewItem listItem = lvGroupItems.Items.Add(ClientHandles.GetValue(i).ToString());
                        listItem.SubItems.Add(ItemValues.GetValue(i).ToString());
                        listItem.SubItems.Add(Qualities.GetValue(i).ToString());
                        listItem.SubItems.Add(TimeStamps.GetValue(i).ToString());
                    }
                }
            }
            */
        }
        void OPCGroup_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            this.m_currentNew = true;
            List<OPCItemData> items;
            AsyncRefreshGroupItems(NumItems, ClientHandles, ItemValues, Qualities, TimeStamps, out items);
        }

        private void AsyncRefreshGroupItems(int NumItems, Array ClientHandles, 
            Array ItemValues, Array Qualities, Array TimeStamps, out List<OPCItemData> items)
        {
            items = new List<OPCItemData>();
            //需判断是否当前组的数据，由于GlobalDataChange事件触发有问题，所以在这里判断
            for (int i = 1; i <= NumItems; i++)
            {
                for (int j = 0; j < lvGroupItems.Items.Count; j++)
                {
                    Application.DoEvents();
                    OPCItem item = (OPCItem)lvGroupItems.Items[j].Tag;
                    string clientHDL1 = ClientHandles.GetValue(i).ToString();
                    string clientHDL2 = item.ClientHandle.ToString();
                    if (clientHDL1 == clientHDL2)
                    {
                        lvGroupItems.Items[j].SubItems[1].Text = ItemValues.GetValue(i).ToString();
                        lvGroupItems.Items[j].SubItems[2].Text = Qualities.GetValue(i).ToString();
                        lvGroupItems.Items[j].SubItems[3].Text = GetLocalTimeFromUTC(TimeStamps.GetValue(i));

                        OPCItemData itemData = new OPCItemData();
                        itemData.ItemID = item.ItemID.ToString();
                        itemData.Value = item.Value.ToString();
                        itemData.Quality = item.Quality.ToString();
                        itemData.Timestamp = GetLocalTimeFromUTC(item.TimeStamp);
                        itemData.ServerHandle = item.ServerHandle.ToString();
                        itemData.ClientHandle = item.ClientHandle.ToString();

                        items.Add(itemData);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 同步读取组
        /// </summary>
        private void GroupSyncRead()
        {
            OPCGroup group = (OPCGroup)lvGroupItems.Tag;
            if (group != null)
            {
                int[] tempHDLs = new int[lvGroupItems.Items.Count + 1];
                tempHDLs[0] = 0;
                for (int i = 0; i < lvGroupItems.Items.Count; i++)
                {
                    OPCItem item = (OPCItem)lvGroupItems.Items[i].Tag;
                    tempHDLs[i + 1] = item.ServerHandle;
                }
                Array ServerHandles = (Array)tempHDLs;
                Array Values;
                Array Errors;
                object Qualities;
                object Timestamps;
                group.SyncRead((int)OPCAutomation.OPCDataSource.OPCCache, lvGroupItems.Items.Count,
                    ref ServerHandles, out Values, out Errors, out Qualities, out Timestamps);

                for (int i = 0; i < lvGroupItems.Items.Count; i++)
                {
                    lvGroupItems.Items[i].SubItems[1].Text = Values.GetValue(i + 1).ToString();
                    lvGroupItems.Items[i].SubItems[2].Text = ((Array)Qualities).GetValue(i + 1).ToString();
                    lvGroupItems.Items[i].SubItems[3].Text = GetLocalTimeFromUTC(((Array)Timestamps).GetValue(i + 1));
                }
                this.m_currentNew = true;
            }
        }

        /// <summary>
        /// 异步读取组，在AsyncReadComplete事件处理
        /// </summary>
        private void GroupAsyncRead()
        {
            OPCGroup group = (OPCGroup)lvGroupItems.Tag;
            if (group != null)
            {
                int[] tempHDLs = new int[lvGroupItems.Items.Count + 1];
                tempHDLs[0] = 0;
                for (int i = 0; i < lvGroupItems.Items.Count; i++)
                {
                    OPCItem item = (OPCItem)lvGroupItems.Items[i].Tag;
                    tempHDLs[i + 1] = item.ServerHandle;
                }
                Array ServerHandles = (Array)tempHDLs;
                Array Errors;
                int cancelID;

                group.AsyncRead(lvGroupItems.Items.Count, ref ServerHandles, out Errors, group.ServerHandle, out cancelID);
            }
        }

        /// <summary>
        /// 异步刷新组，在DataChange事件处理
        /// </summary>
        private void GroupAsyncRefresh()
        {
            OPCGroup group = (OPCGroup)lvGroupItems.Tag;
            if (group != null)
            {
                int cancelID;
                group.AsyncRefresh(1, group.ClientHandle, out cancelID);
            }
        }

        #endregion

        #region OPC Item
        private void DisplayServerBranches()
        {
            tvOPCBrowser.Nodes.Clear();
            //不判断OPCBrowser.Organization（平面型、树型）
            DisplayOPCBrowserBranches(null);   
        }

        private void DisplayBranchLeaves(string branchFullPath)
        {
            Array branches = branchFullPath.Split('\\');
            m_OPCBrowser.MoveTo(branches);
            m_OPCBrowser.ShowLeafs(true);
            foreach (object leaf in m_OPCBrowser)
            {
                lbItems.Items.Add(leaf.ToString());
            }
        }

        private void DisplayOPCBrowserBranches(TreeNode treeNode)
        {
            m_OPCBrowser.ShowBranches();
            if (m_OPCBrowser.Count == 0)
            {
                //DispalyOPCBrowserLeaves(treeNode);
            } else
            {
                foreach (object branch in m_OPCBrowser)
                {
                    string branchName = branch.ToString();
                    TreeNode subNode;
                    if (treeNode == null)
                    {
                        subNode = tvOPCBrowser.Nodes.Add(branchName);
                    }
                    else
                    {
                        subNode = treeNode.Nodes.Add(branchName);
                    }
                    m_OPCBrowser.MoveDown(branchName);
                    DisplayOPCBrowserBranches(subNode);
                    m_OPCBrowser.MoveUp();
                }
            }
            
        }

        private void DispalyOPCBrowserLeaves(TreeNode treeNode)
        {
            m_OPCBrowser.ShowLeafs(false);
            foreach (object leaf in m_OPCBrowser)
            {
                string leafName = leaf.ToString();
                TreeNode subNode;
                if (treeNode == null)
                {
                    subNode = tvOPCBrowser.Nodes.Add(leafName);
                }
                else
                {
                    subNode = treeNode.Nodes.Add(leafName);
                }
            }
        }

        private void ClearLeaves()
        {
            lbItems.Items.Clear();
        }

        private void ClearBrowser()
        {
            lbItems.Items.Clear();
            tvOPCBrowser.Nodes.Clear();
        }

        private void RecordItemsToFile()
        {
            string fileName = Application.StartupPath + "\\OPC_Records_" 
                + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
            FileStream fs;
            if (File.Exists(fileName))
            {
                fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            }

            StreamWriter sw = new StreamWriter(fs);
            //sw.WriteLine("\"ItemID\",\"Value\",\"Quality\",\"Timestamp\",\"ServerHandle\",\"Client\",\"DataType\"");
            for (int i = 0; i < lvGroupItems.Items.Count; i++)
            {
                string NET_INFO = string.Empty;

                string[] netInfos = lvGroupItems.Items[i].SubItems[0].Text.Split('.');
                foreach (string s in netInfos)
                {
                    NET_INFO = NET_INFO == string.Empty ? "\"" + s + "\"" : NET_INFO + ",\"" + s + "\"";
                }

                sw.WriteLine(NET_INFO + ",\""
                    + lvGroupItems.Items[i].SubItems[1].Text + "\",\""
                    + lvGroupItems.Items[i].SubItems[2].Text + "\",\""
                    + lvGroupItems.Items[i].SubItems[3].Text + "\",\""
                    + lvGroupItems.Items[i].SubItems[4].Text + "\",\""
                    + lvGroupItems.Items[i].SubItems[5].Text + "\"");
            }
            sw.Flush();
            sw.Close();
            fs.Close(); 
        }

        private OPCItem AddItemToGroup(OPCGroup group, string itemID)
        {
            OPCItem item;
            try
            {
                item = group.OPCItems.AddItem(itemID, m_OPCItemClientHandle);
                m_OPCItemClientHandle++;
                return item;
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("添加项时出现错误：" + ex.Message);
                return null;
            }
        }

        private bool RemoveItemFromGroup(OPCGroup group, OPCItem item)
        {
            try
            {
                Array serverHDLs = new int[2] { 0, item.ServerHandle };
                Array errors;
                group.OPCItems.Remove(1, ref serverHDLs, out errors);
                return true;
            }
            catch (Exception ex)
            {
                ShowErrorMessageBox("移除项时出现错误：" + ex.Message);
                return false;
            }
        }

        private void ListGroupItems(OPCGroup group)
        {
            lvGroupItems.Tag = group;
            lvGroupItems.Items.Clear();
            if (group != null)
            {
                foreach (OPCItem item in group.OPCItems)
                {
                    item.IsActive = true;
                    ListViewItem listItem = lvGroupItems.Items.Add(item.ItemID);
                    listItem.Tag = item;
                    listItem.SubItems.Add(string.Empty);
                    listItem.SubItems.Add(string.Empty);
                    listItem.SubItems.Add(string.Empty);
                    listItem.SubItems.Add(item.ServerHandle.ToString());
                    listItem.SubItems.Add(item.ClientHandle.ToString());

                    m_currentNew = true;
                }
            }

        }

        private void AppendItemToList(OPCItem item)
        {
            if (item != null)
            {
                item.IsActive = true;
                ListViewItem listItem = lvGroupItems.Items.Add(item.ItemID);
                listItem.Tag = item;
                listItem.SubItems.Add(string.Empty);
                listItem.SubItems.Add(string.Empty);
                listItem.SubItems.Add(string.Empty);
                listItem.SubItems.Add(item.ServerHandle.ToString());
                listItem.SubItems.Add(item.ClientHandle.ToString());
                listItem.SubItems.Add(item.CanonicalDataType.ToString());
            }
        }

        private void SetGroupItemControlStatus()
        {
            tsbAddItem.Enabled = tstbSelectedLeaf.Text.Trim() != string.Empty;
            tsbRemoveItem.Enabled = lvGroupItems.SelectedItems.Count > 0;
        }

        private void RefreshGroupItems()
        {
            object value, quality, timestamp;
            for (int i = 0; i < lvGroupItems.Items.Count; i++)
            {
                OPCItem item = (OPCItem)lvGroupItems.Items[i].Tag;
                try
                {
                    item.Read(1, out value, out quality, out timestamp);
                }
                catch
                {
                    value = null;
                    quality = null;
                    timestamp = null;
                }

                lvGroupItems.Items[i].SubItems[1].Text = value != null ? value.ToString() : string.Empty;
                lvGroupItems.Items[i].SubItems[2].Text = quality != null ? quality.ToString() : string.Empty;
                lvGroupItems.Items[i].SubItems[3].Text = GetLocalTimeFromUTC(timestamp);


                m_currentNew = true;
            }
        }
        #endregion

        #region 写入数据库
        private bool InsertToNewDataTable(OPCClientBLL bll, out List<OPCItemData> items, out string errMsg)
        {
            items = new List<OPCItemData>();
            /*
            for (int i = 0; i < lvGroupItems.Items.Count; i++)
            {
                OPCItemData opcItem = new OPCItemData();
                opcItem.ItemID = lvGroupItems.Items[i].SubItems[0].Text;
                opcItem.Value = lvGroupItems.Items[i].SubItems[1].Text;
                opcItem.Quality = lvGroupItems.Items[i].SubItems[2].Text;
                opcItem.Timestamp = lvGroupItems.Items[i].SubItems[3].Text;
                opcItem.ServerHandle = lvGroupItems.Items[i].SubItems[4].Text;
                opcItem.ClientHandle = lvGroupItems.Items[i].SubItems[5].Text;

                items.Add(opcItem);
            }
            */
            foreach (OPCItem item in bll.BindGroup.OPCItems)
            {
                Application.DoEvents();
                OPCItemData itemData = new OPCItemData();
                itemData.ItemID = item.ItemID.ToString();
                itemData.Value = item.Value.ToString();
                itemData.Quality = item.Quality.ToString();
                itemData.Timestamp = GetLocalTimeFromUTC(item.TimeStamp);
                itemData.ServerHandle = item.ServerHandle.ToString();
                itemData.ClientHandle = item.ClientHandle.ToString();

                items.Add(itemData);
            }
            Application.DoEvents();
            return bll.UpdateNewDataTable(items, out errMsg);
        }

        private bool UpdateNewDataTable(OPCClientBLL bll, List<OPCItemData> items, out string errMsg)
        {
            return bll.UpdateNewDataTable(items, out errMsg);
        }

        private bool InsertToHistoryDataTable(OPCClientBLL bll, int NumItems, Array ClientHandles, Array ItemValues, 
            Array Qualities, Array Timestamps, List<OPCItemData> itemsNew, out string errMsg)
        {
            List<OPCItemData> items = new List<OPCItemData>();
            for (int i = 1; i <= NumItems; i++)
            {
                Application.DoEvents();
                /*
                OPCItem opcItem = bll.GetOPCItemByClientHandle(int.Parse(ClientHandles.GetValue(i).ToString()));
                if (opcItem != null)
                {
                    OPCItemData itemData = new OPCItemData();
                    itemData.ItemID = opcItem.ItemID.ToString();
                    itemData.Value = opcItem.Value.ToString();
                    itemData.Quality = opcItem.Quality.ToString();
                    itemData.Timestamp = GetLocalTimeFromUTC(opcItem.TimeStamp);
                    itemData.ServerHandle = opcItem.ServerHandle.ToString();
                    itemData.ClientHandle = opcItem.ClientHandle.ToString();

                    items.Add(itemData);
                }
                */
                foreach (OPCItemData itemDataNew in itemsNew)
                {
                    if (itemDataNew.ClientHandle == ClientHandles.GetValue(i).ToString())
                    {
                        items.Add(itemDataNew);
                    }
                }
            }
            Application.DoEvents();
            //OPCUtils.LogDataChangeTime("数据刷新-插入历史数据-插入数据表-开始");
            return bll.InsertToHistoryDataTable(items, out errMsg);
            //OPCUtils.LogDataChangeTime("数据刷新-插入历史数据-插入数据表-结束");
        }

        private void InsertGroupItems(OPCGroup group, out List<OPCItemData> items)
        {
            items = null;
            OPCClientBLL bll = GetGroupBindingBLL(group);
            if (bll != null && bll.AutoInsert)
            {
                string errMsg = string.Empty;
                if (!InsertToNewDataTable(bll, out items, out errMsg))
                {
                    tsslDBConnection.Text = errMsg;
                }
                else
                {
                    m_currentNew = false;
                    tsslDBConnection.Text = string.Empty;
                }
            }
        }

        private void UpdateNewGroupItems(OPCGroup group, List<OPCItemData> items)
        {
            OPCClientBLL bll = GetGroupBindingBLL(group);
            if (bll != null && bll.AutoInsert)
            {
                string errMsg = string.Empty;
                if (!UpdateNewDataTable(bll, items, out errMsg))
                {
                    tsslDBConnection.Text = errMsg;
                } else
                {
                    m_currentNew = false;
                    tsslDBConnection.Text = string.Empty;
                }
            }
        }

        private void InsertDataChangedItems(OPCClientBLL bll, int NumItems, Array ClientHandles, Array ItemValues,
            Array Qualities, Array Timestamps, List<OPCItemData> itemsNew)
        {
            if (bll != null && bll.AutoInsert && itemsNew != null && itemsNew.Count > 0)
            {
                string errMsg = string.Empty;
                if (!InsertToHistoryDataTable(bll, NumItems, ClientHandles, ItemValues, Qualities, Timestamps, itemsNew, out errMsg))
                {
                    tsslDBConnection.Text = errMsg;
                }
                else
                {
                    m_currentNew = false;
                    tsslDBConnection.Text = string.Empty;
                }
            }
        }
        #endregion

        #region 存取设置
        private void LoadSettings(string configFileName)
        {
            OPCConfig opcConfig = new OPCConfig(configFileName);
            if (!opcConfig.IsValid)
            {
                return;
            }

            ClearLeaves();
            ClearBrowser();
            ClearGroups();

            txtServerHost.Text = opcConfig.ServerHost;
            ClearServers();
            ListServers(opcConfig.ServerHost);
            

            bool foundServer = false;
            for (int i = 0; i < cbServers.Items.Count; i++)
            {
                if (opcConfig.OPCServerName == cbServers.Items[i].ToString())
                {
                    cbServers.SelectedIndex = i;
                    foundServer = true;
                    break;
                }
            }
            if (!foundServer)
            {
                return;
            }

            try
            {
                btnConnectServer.Enabled = false;
                Application.DoEvents();

                tsslOPCServerState.Text = "";
                tsslOPCServerStartTime.Text = "";

                if (ConnectServer(opcConfig.OPCServerName, opcConfig.ServerHost))
                {
                    btnConnectServer.Tag = 1;
                    btnConnectServer.Text = "断开";
                    ClearGroups();
                    ClearBrowser();
                    SetGroupControlStatus();
                } else
                {
                    return;
                }
            }
            finally
            {
                btnConnectServer.Enabled = true;
            }
            
            foreach (OPCGroupConfig groupCfg in opcConfig.Groups)
            {
                OPCGroup group = CreateNewGroup(groupCfg.GroupName);
                if (group != null)
                {
                    //Group属性
                    group.IsActive = groupCfg.IsActive;
                    group.IsSubscribed = groupCfg.IsSubscribed;
                    group.UpdateRate = groupCfg.UpdateRate;
                    group.DeadBand = groupCfg.Deadband;
                    //OPCItems
                    foreach (string itemID in groupCfg.OPCItems)
                    {
                        try
                        {
                            OPCItem item = group.OPCItems.AddItem(itemID, m_OPCItemClientHandle);
                            m_OPCItemClientHandle++;
                        }
                        catch (Exception e)
                        {
                        }
                    }

                    OPCClientBLL bll = new OPCClientBLL();
                    bll.BindGroup = group;

                    //写入数据库设置
                    bll.AutoInsert = groupCfg.AutoInsert;
                    bll.TimeoutSetting = groupCfg.TimeoutSetting;
                    bll.AutoCheckUpdateStatus = groupCfg.TimeoutSetting > 0;

                    //数据库连接设置
                    bll.DatabaseType = Enum.IsDefined(typeof(OPCUtils.DatabaseType), groupCfg.DBConnection.DatabaseType)
                        ? (OPCUtils.DatabaseType)groupCfg.DBConnection.DatabaseType : OPCUtils.DatabaseType.MSSQLServer;
                    bll.ServerName = groupCfg.DBConnection.ServerName;
                    bll.DatabaseName = groupCfg.DBConnection.DatabaseName;
                    bll.UserName = groupCfg.DBConnection.UserName;
                    bll.Password = groupCfg.DBConnection.Password;
                    bll.NewDataTableName = groupCfg.DBConnection.NewDataTable;
                    bll.HistoryDataTableName = groupCfg.DBConnection.HistoryDataTable;
                    
                    //字段映射设置
                    foreach (OPCGroupDatabaseFieldConfig fieldCfg in groupCfg.DBConnection.Fields)
                    {
                        OPCClientDBFieldMapping fieldMapping = new OPCClientDBFieldMapping();
                        fieldMapping.FieldName = fieldCfg.FieldName;
                        fieldMapping.SourceOPCItem = fieldCfg.SourceOPCItem;
                        fieldMapping.SourceCustom = fieldCfg.SourceCustom;
                        fieldMapping.DataType = fieldCfg.DataType;
                        fieldMapping.SeqName = fieldCfg.SEQName;
                        fieldMapping.AutoInc = fieldCfg.AutoInc;
                        fieldMapping.IsEntityIdentity = fieldCfg.IsEntityIdentity;

                        bll.DBFieldMappings.Add(fieldMapping);
                    }

                    //特殊OPC项映射设置
                    //MessageBox.Show(txtServerHost.Text);
                    //MessageBox.Show(this.m_OPCServer.ServerName);
                    bll.OPCItemMapping = this.m_OPCItemMappingConfig.
                        GetCurrentOPCItemMappingConfig(txtServerHost.Text, 
                        this.m_OPCServer.ServerName);

                    m_blls.Add(bll);

                    //加入列表
                    ListViewItem listItem = lvGroups.Items.Add(group.Name);
                    listItem.Tag = group;
                    listItem.Selected = true;
                }
            }
        }

        private void SaveSettings(string saveConfigFileName)
        {
            OPCConfig opcConfig = new OPCConfig();
            opcConfig.ServerHost = txtServerHost.Text;
            opcConfig.OPCServerName = m_OPCServer.ServerName;

            foreach (OPCGroup group in m_OPCServer.OPCGroups)
            {
                OPCClientBLL bll = GetGroupBindingBLL(group);
                if (bll == null)
                {
                    continue;
                }

                OPCGroupConfig groupCfg = new OPCGroupConfig();
                //Group属性
                groupCfg.GroupName = group.Name;
                groupCfg.Deadband = group.DeadBand;
                groupCfg.UpdateRate = group.UpdateRate;
                groupCfg.IsActive = group.IsActive;
                groupCfg.IsSubscribed = group.IsSubscribed;
                groupCfg.TimeoutSetting = bll.AutoCheckUpdateStatus ? bll.TimeoutSetting : 0;
                groupCfg.AutoInsert = bll.AutoInsert;
                
                //OPCItems
                foreach (OPCItem opcItem in group.OPCItems)
                {
                    groupCfg.OPCItems.Add(opcItem.ItemID);
                }

                //数据连接设置
                groupCfg.DBConnection.DatabaseType = (int)bll.DatabaseType;
                groupCfg.DBConnection.ServerName = bll.ServerName;
                groupCfg.DBConnection.DatabaseName = bll.DatabaseName;
                groupCfg.DBConnection.UserName = bll.UserName;
                groupCfg.DBConnection.Password = bll.Password;
                groupCfg.DBConnection.NewDataTable = bll.NewDataTableName;
                groupCfg.DBConnection.HistoryDataTable = bll.HistoryDataTableName;

                //字段映射设置
                foreach (OPCClientDBFieldMapping fieldMapping in bll.DBFieldMappings)
                {
                    OPCGroupDatabaseFieldConfig fieldCfg = new OPCGroupDatabaseFieldConfig();
                    fieldCfg.FieldName = fieldMapping.FieldName;
                    fieldCfg.SourceOPCItem = fieldMapping.SourceOPCItem;
                    fieldCfg.SourceCustom = fieldMapping.SourceCustom;
                    fieldCfg.DataType = fieldMapping.DataType;
                    fieldCfg.SEQName = fieldMapping.SeqName;
                    fieldCfg.AutoInc = fieldMapping.AutoInc;
                    fieldCfg.IsEntityIdentity = fieldMapping.IsEntityIdentity;

                    groupCfg.DBConnection.Fields.Add(fieldCfg);
                }

                opcConfig.Groups.Add(groupCfg);
            }

            opcConfig.SaveConfiguration(saveConfigFileName);
        }
        #endregion

        private void btnRefreshServers_Click(object sender, EventArgs e)
        {
            ClearServers();
            ListServers(txtServerHost.Text);
        }

        private void btnConnectServer_Click(object sender, EventArgs e)
        {
            try
            {
                btnConnectServer.Enabled = false;
                Application.DoEvents();

                tsslOPCServerState.Text = "";
                tsslOPCServerStartTime.Text = "";
                if (btnConnectServer.Tag.ToString() == "0")
                {
                    if (ConnectServer(cbServers.Text, txtServerHost.Text))
                    {
                        btnConnectServer.Tag = 1;
                        btnConnectServer.Text = "断开";
                        ClearGroups();
                        ClearBrowser();
                        SetGroupControlStatus();
                    }
                }
                else
                {
                    if (DisconnectServer())
                    {
                        btnConnectServer.Tag = 0;
                        btnConnectServer.Text = "连接";
                        ClearGroups();
                        ClearBrowser();
                        SetGroupControlStatus();
                    }
                }
            }
            finally
            {
                btnConnectServer.Enabled = true;
            }
            
            
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            try
            {
                btnAddGroup.Enabled = false;
                Application.DoEvents();

                OPCGroup group = CreateNewGroup("Group_" + m_OPCGroupIndex);
                if (group != null)
                {
                    OPCClientBLL bll = new OPCClientBLL();
                    bll.BindGroup = group;
                    bll.DatabaseType = OPCUtils.DatabaseType.MSSQLServer;
                    bll.ServerName = "";
                    bll.DatabaseName = "";
                    bll.UserName = "";
                    bll.Password = "";
                    bll.NewDataTableName = "";
                    bll.HistoryDataTableName = "";
                    bll.AutoInsert = true;

                    //特殊OPC项映射设置
                    //bll.OPCItemMapping = this.m_OPCItemMappingConfig.GetCurrentOPCItemMappingConfig(txtServerHost.Text,
                    //    this.m_OPCServer.ServerName);

                    m_blls.Add(bll);

                    //加入列表
                    ListViewItem listItem = lvGroups.Items.Add(group.Name);
                    listItem.Tag = group;
                    listItem.Selected = true;
                }
            }
            finally
            {
                btnAddGroup.Enabled = true;
            }
        }

        private void btnRemoveGroup_Click(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count == 1)
            {
                int selectIndex = lvGroups.SelectedItems[0].Index;
                OPCGroup group = (OPCGroup)lvGroups.SelectedItems[0].Tag;

                foreach (OPCClientBLL bll in m_blls)
                {
                    if (bll.BindGroup == group)
                    {
                        m_blls.Remove(bll);
                        break;
                    }
                }

                RemoveGroup(group);
                lvGroups.SelectedItems[0].Remove();
                if (selectIndex < lvGroups.Items.Count)
                {
                    lvGroups.Items[selectIndex].Selected = true;
                } else
                {
                    if (lvGroups.Items.Count > 0)
                    {
                        lvGroups.Items[selectIndex - 1].Selected = true;
                    }
                }
            }
        }

        private void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            OPCGroup group = lvGroups.SelectedItems.Count > 0 
                ? (OPCGroup)lvGroups.SelectedItems[0].Tag : null;
            DisplayGroupProperties(group);
            ListGroupItems(group);
            DisplayGroupDataConnectionInfo(group);
            SetGroupControlStatus();
        }

        private void btnGPApply_Click(object sender, EventArgs e)
        {
            OPCGroup group;
            if (lvGroups.SelectedItems.Count == 0)
            {
                ShowErrorMessageBox("未选中组。");
                return;
            } else
            {
                group = (OPCGroup)lvGroups.SelectedItems[0].Tag;
            }

            //名称
            string groupName = txtGPName.Text.Trim();
            if (groupName == string.Empty)
            {
                ShowErrorMessageBox("组名称不可为空。");
                return;
            }
            try
            {
                if (groupName != group.Name &&
                m_OPCServer.OPCGroups.Item(groupName) != null)
                {
                    ShowErrorMessageBox("已存在为该名称的组，请修改组名称。");
                    return;
                }
            }
            catch
            { }
            

            //是否激活
            bool isActive = Convert.ToBoolean(cbGPIsActive.SelectedIndex);
            //是否订阅
            bool isSubscribed = Convert.ToBoolean(cbGPIsSubcribed.SelectedIndex);
            //更新周期
            int updateRate;
            if (!int.TryParse(txtGPUpdateRate.Text, out updateRate) || updateRate <= 0)
            {
                ShowErrorMessageBox("更新周期必须为大于0的正整数。");
                return;
            }
            //不敏感区
            float deadband;
            if (!float.TryParse(txtGPDeadBand.Text, out deadband) || deadband < 0)
            {
                ShowErrorMessageBox("不敏感区必须为不小于0的浮点数。");
                return;
            }

            int timeout = 0;
            if (chkCheckTimeout.Checked && (!int.TryParse(txtGPUpdateTimeout.Text.Trim(), out timeout) || timeout < 0))
            {
                ShowErrorMessageBox("超时时间必须为不小于0的整数。");
                return;
            }

            SetGroupProperties(group, groupName, isActive, isSubscribed, updateRate, deadband);

            SetGroupExtensionProperties(group, chkCheckTimeout.Checked, timeout);

            DisplayGroupProperties(group);
            lvGroups.SelectedItems[0].Text = groupName;
        }

        private void btnGPCancel_Click(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count > 0)
            {
                DisplayGroupProperties((OPCGroup)lvGroups.SelectedItems[0].Tag);
            }
        }


        private void tsbAddItem_Click(object sender, EventArgs e)
        {
            if (lvGroups.SelectedItems.Count == 1 && tstbSelectedLeaf.Text.Trim() != string.Empty)
            {
                try
                {
                    tsbAddItem.Enabled = false;
                    Application.DoEvents();

                    OPCGroup group = (OPCGroup)lvGroups.SelectedItems[0].Tag;
                    string[] itemIDs = tstbSelectedLeaf.Text.Split(';');
                    foreach (string itemID in itemIDs)
                    {
                        /*
                        if (itemID.Contains(".温度")
                            || itemID.Contains(".压力")
                            || itemID.Contains(".标况瞬时流量")
                            || itemID.Contains(".工况瞬时流量")
                            || itemID.Contains(".标况累计流量")
                            || itemID.Contains(".工况累计流量")
                            || itemID.Contains(".更新时间"))
                        {
                            Application.DoEvents();
                            OPCItem item = AddItemToGroup(group, itemID);
                            AppendItemToList(item);
                            m_currentNew = true;                            
                        }
                        */
                        Application.DoEvents();
                        OPCItem item = AddItemToGroup(group, itemID);
                        AppendItemToList(item);
                        m_currentNew = true; 
                    } 
                }
                finally
                {
                    tsbAddItem.Enabled = true;
                }
                               
            }
        }

        private void tsbRemoveItem_Click(object sender, EventArgs e)
        {
            if (lvGroupItems.SelectedItems.Count == 1)
            {
                OPCGroup group = (OPCGroup)lvGroupItems.Tag;
                OPCItem item = (OPCItem)lvGroupItems.SelectedItems[0].Tag;
                if(RemoveItemFromGroup(group, item))
                {
                    int selectIndex = lvGroupItems.SelectedItems[0].Index;

                    lvGroupItems.SelectedItems[0].Remove();

                    if (selectIndex < lvGroupItems.Items.Count)
                    {
                        lvGroupItems.Items[selectIndex].Selected = true;
                    }
                    else
                    {
                        if (lvGroupItems.Items.Count > 0)
                        {
                            lvGroupItems.Items[selectIndex - 1].Selected = true;
                        }
                    }
                }
            }
        }

        private void tsbOPCBrowserTree_Click(object sender, EventArgs e)
        {
            ClearLeaves();
            if (m_OPCServer != null && m_OPCServer.ServerState == (int)OPCServerState.OPCRunning)
            {
                try
                {
                    m_OPCBrowser = m_OPCServer.CreateBrowser();
                    m_OPCBrowser.Filter = tstbBranchFilter.Text.Trim();
                }
                catch (Exception ex)
                {
                    m_OPCBrowser = null;
                    ShowErrorMessageBox("获取服务器节点列表出现错误：" + ex.Message);
                    return;
                }
                DisplayServerBranches();
            }
        }

        private void tvOPCBrowser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ClearLeaves();
            if (tvOPCBrowser.SelectedNode != null && tvOPCBrowser.SelectedNode.Nodes.Count == 0)
            {
                DisplayBranchLeaves(tvOPCBrowser.SelectedNode.FullPath);
            } else
            {
                tstbSelectedLeaf.Text = string.Empty;
            }
            
        }

        private void lbItems_SelectedValueChanged(object sender, EventArgs e)
        {
            string selectedItems = string.Empty;
            foreach (object listItem in lbItems.SelectedItems)
            {
                selectedItems = selectedItems == string.Empty
                    ? listItem.ToString() : selectedItems + ";" + listItem.ToString();
            }
            tstbSelectedLeaf.Text = selectedItems;
        }

        private void tstbSelectedLeaf_TextChanged(object sender, EventArgs e)
        {
            SetGroupItemControlStatus();
        }

        private void lvGroupItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetGroupItemControlStatus();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            RefreshGroupItems();
            RecordItemsToFile();
        }

        private void tsbInsertToDatabase_Click(object sender, EventArgs e)
        {
            OPCClientBLL bll = GetGroupBindingBLL((OPCGroup)lvGroupItems.Tag);
            if (bll != null)
            {
                List<OPCItemData> items = new List<OPCItemData>();
                for (int i = 0; i < lvGroupItems.Items.Count; i++)
                {
                    OPCItemData opcItem = new OPCItemData();
                    opcItem.ItemID       = lvGroupItems.Items[i].SubItems[0].Text;
                    opcItem.Value        = lvGroupItems.Items[i].SubItems[1].Text;
                    opcItem.Quality      = lvGroupItems.Items[i].SubItems[2].Text;
                    opcItem.Timestamp    = lvGroupItems.Items[i].SubItems[3].Text;
                    opcItem.ServerHandle = lvGroupItems.Items[i].SubItems[4].Text;
                    opcItem.ClientHandle = lvGroupItems.Items[i].SubItems[5].Text;

                    items.Add(opcItem);
                }

                string errMsg = string.Empty;
                bll.UpdateNewDataTable(items, out errMsg);
            }
        }

        private void chkAutoInsertToDB_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (lvGroupItems.Tag != null)
            {
                OPCGroup group = (OPCGroup)lvGroupItems.Tag;
                OPCClientBLL bll = GetGroupBindingBLL(group);
                if (bll != null)
                {
                    //bll.AutoInsert = chkAutoInsertToDB.Checked;
                

                    if (bll.AutoInsert && m_currentNew)
                    {
                        string errMsg = string.Empty;
                        //实时数据
                        if (!InsertToNewDataTable(bll, out errMsg))
                        {
                            tsslDBConnection.Text = errMsg;
                        } else
                        {
                            m_currentNew = false;
                            tsslDBConnection.Text = string.Empty;
                        }

                        //历史数据
                        List<OPCItemData> items = new List<OPCItemData>();
                        for (int i = 0; i < lvGroupItems.Items.Count; i++)
                        {
                            OPCItem opcItem = (OPCItem)lvGroupItems.Items[i].Tag;
                            if (opcItem != null)
                            {
                                OPCItemData itemData = new OPCItemData();
                                itemData.ItemID = opcItem.ItemID.ToString();
                                itemData.Value = opcItem.Value.ToString();
                                itemData.Quality = opcItem.Quality.ToString();
                                itemData.Timestamp = GetLocalTimeFromUTC(opcItem.TimeStamp);
                                itemData.ServerHandle = opcItem.ServerHandle.ToString();
                                itemData.ClientHandle = opcItem.ClientHandle.ToString();

                                items.Add(itemData);
                            }

                        }

                        if(!bll.InsertToHistoryDataTable(items, out errMsg))
                        {
                            tsslDBConnection.Text = errMsg;
                        } else
                        {
                            m_currentNew = false;
                            tsslDBConnection.Text = string.Empty;
                        }

                    }
                }
            }
             * */
        }

        private void tsbSyncRead_Click(object sender, EventArgs e)
        {
            GroupSyncRead();
        }

        private void tsbAsyncRead_Click(object sender, EventArgs e)
        {
            GroupAsyncRead();
        }

        private void tsbAsyncRefresh_Click(object sender, EventArgs e)
        {
            GroupAsyncRefresh();
        }

        private void btnGroupDBConnSetting_Click(object sender, EventArgs e)
        {
            OPCClientBLL bll = GetGroupBindingBLL((OPCGroup)lvGroupItems.Tag);
            if (bll != null)
            {
                FormDBConnectionSetting frmDBConnSetting = new FormDBConnectionSetting(
                    dbType: bll.DatabaseType,
                    server: bll.ServerName,
                    database: bll.DatabaseName,
                    userName: bll.UserName,
                    password: bll.Password,
                    tableName: bll.NewDataTableName,
                    hisTableName: bll.HistoryDataTableName,
                    dbFieldMappings: bll.DBFieldMappings);
                if (frmDBConnSetting.ShowDialog() == DialogResult.OK)
                {
                    bll.DatabaseType = frmDBConnSetting.DatabaseType;
                    bll.ServerName = frmDBConnSetting.Server;
                    bll.DatabaseName = frmDBConnSetting.Database;
                    bll.UserName = frmDBConnSetting.UserName;
                    bll.Password = frmDBConnSetting.Password;
                    bll.NewDataTableName = frmDBConnSetting.TableName;
                    bll.HistoryDataTableName = frmDBConnSetting.HistoryTableName;

                    bll.DBFieldMappings.Clear();
                    foreach (OPCClientDBFieldMapping dbFieldMapping in frmDBConnSetting.DBFieldMappings)
                    {
                        bll.DBFieldMappings.Add(dbFieldMapping.Clone());
                    }
                    bll.RefreshDBConnectionSetting();
                }
            }
        }

        private void tmCheckGroupStatus_Tick(object sender, EventArgs e)
        {
            CheckGroupUpdateStatus();
        }

        private void chkCheckTimeout_CheckedChanged(object sender, EventArgs e)
        {
            txtGPUpdateTimeout.Enabled = chkCheckTimeout.Checked;
            int timeout = 0;
            if (txtGPUpdateTimeout.Text.Trim() == string.Empty 
                || !int.TryParse(txtGPUpdateTimeout.Text, out timeout)
                || timeout < 0)
            {
                txtGPUpdateTimeout.Text = "0";
            }
        }

        private void miLoadSettings_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "OPC Client 配置文件(*.xml)|*.xml";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                LoadSettings(openDlg.FileName);
            }            
        }

        private void miSaveSettings_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "OPC Client 配置文件(*.xml)|*.xml";
            saveDlg.DefaultExt = ".xml";
            saveDlg.OverwritePrompt = true;
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                SaveSettings(saveDlg.FileName);
            }
        }
    }
}
