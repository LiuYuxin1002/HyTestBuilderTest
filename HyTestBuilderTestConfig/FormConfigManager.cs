﻿using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using HyTestRTDataService.ConfigMode.MapEntities;
using HyTestRTDataService.ConfigMode;

namespace HyTestBuilderTestConfig
{
    public partial class FormConfigManager : Form
    {
        public ConfigManager configManager;         //正经ConfigManager

        private bool isSavedConfig;

        public FormConfigManager()
        {
            InitializeComponent();
            InitializeIOmapDataGridView();

            configManager = new ConfigManager();

            ShowConfigOnForm();
        }

        #region method
        //初始化Datagridview2
        private void InitializeIOmapDataGridView()
        {
            DataGridViewTextBoxColumn ID = new DataGridViewTextBoxColumn();
            ID.DataPropertyName = "ID";
            ID.HeaderText = "ID";
            ID.Visible = true;
            this.dataGridView2.Columns.Add(ID);

            DataGridViewTextBoxColumn Name = new DataGridViewTextBoxColumn();
            Name.DataPropertyName = "变量名";
            Name.HeaderText = "变量名";
            Name.Visible = true;
            this.dataGridView2.Columns.Add(Name);

            DataGridViewComboBoxColumn VarType = new DataGridViewComboBoxColumn();
            VarType.DataPropertyName = "变量类型";
            VarType.HeaderText = "变量类型";
            VarType.DataSource = new string[]
            {
                "System.Int32",
                "System.Double",
                "System.Boolean",
                "System.String",
            };
            VarType.Visible = true;
            this.dataGridView2.Columns.Add(VarType);

            DataGridViewComboBoxColumn IOType = new DataGridViewComboBoxColumn();
            IOType.DataPropertyName = "IO类型";
            IOType.HeaderText = "IO类型";
            IOType.DataSource = new string[]
            {
                "DI",
                "DO",
                "AI",
                "AO",
            };
            IOType.Visible = true;
            this.dataGridView2.Columns.Add(IOType);

            DataGridViewTextBoxColumn Port = new DataGridViewTextBoxColumn();
            Port.DataPropertyName = "端口号";
            Port.HeaderText = "端口号";
            Port.Visible = true;
            this.dataGridView2.Columns.Add(Port);

            DataGridViewTextBoxColumn MaxValue = new DataGridViewTextBoxColumn();
            MaxValue.DataPropertyName = "变量上限";
            MaxValue.HeaderText = "变量上限";
            MaxValue.Visible = true;
            this.dataGridView2.Columns.Add(MaxValue);

            DataGridViewTextBoxColumn MinValue = new DataGridViewTextBoxColumn();
            MinValue.DataPropertyName = "变量下限";
            MinValue.HeaderText = "变量下限";
            MinValue.Visible = true;
            this.dataGridView2.Columns.Add(MinValue);
        }

        //判断配置文件是否存在
        private bool IsExist(string filePath)
        {
            return false;
        }

        //从xml文件读入config信息，写到config里面去，显示出来
        private void ReadXmlConfigInfoIfExist()
        {
            if (IsExist(configManager.ConfigFile))
            {
                configManager.LoadConfig();
                ShowConfigOnForm();
            }
        }

        //将Config显示出来
        private void ShowConfigOnForm()
        {
            //adapter
            this.dataGridView1.DataSource = configManager.GetAdapterTableNoRefresh();

            //device
            TreeNode tn = configManager.GetDeviceTreeNoRefresh();
            if(tn!=null)
            {
                this.treeView1.Nodes.Add(tn);
            }

            //iomap
            this.dataGridView2.DataSource = configManager.GetIOmapTableNoRefresh();
            
        }

        //配置一旦发生更改触发
        private void OnConfigChanged()
        {
            this.isSavedConfig = false;
            this.btn_SaveConfig.Enabled = true;
        }

        private void OnConfigSaved()
        {
            this.isSavedConfig = true;
            this.btn_SaveConfig.Enabled = false;
        }

        #endregion

        #region 事件

        private void btn_ScanAdapter_Click(object sender, EventArgs e)
        {
            DataTable adapterTable = null;
            try
            {
                adapterTable = configManager.GetAdapterTableWithRefresh();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            if(adapterTable!=null) this.dataGridView1.DataSource = adapterTable;
        }

        private void btn_SelectAdapter_Click(object sender, EventArgs e)
        {
            int selectedAdapter = this.dataGridView1.SelectedRows[0].Index;
            try
            {
                configManager.SaveAdapterConfig(selectedAdapter);
            }
            catch (System.Exception ex)
            {
            	
            }

            OnConfigChanged();
        }

        private void btn_ScanDevices_Click(object sender, EventArgs e)
        {
            TreeNode rootDeviceTree = configManager.GetDeviceTreeWithRefresh();
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(rootDeviceTree);
            for(int i=0; i<treeView1.Nodes.Count; i++)
            {
                treeView1.Nodes[i].Expand();
            }
        }

        private void btn_SaveDeviceConfig_Click(object sender, EventArgs e)
        {
            configManager.SaveDeviceConfig(treeView1.Nodes[0]);
            OnConfigChanged();
        }

        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            configManager.SaveConfig();
            OnConfigSaved();
        }

        private void btn_ReloadConfig_Click(object sender, EventArgs e)
        {

        }

        //导入变量表
        private void btn_ImportExcel_Click(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = configManager.GetIOmapWithRefresh();
        }
        //导出变量表
        private void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            configManager.SaveIOmapToExcel();
        }
        //保存映射文件的更改
        private void btn_SaveIOmapChange_Click(object sender, EventArgs e)
        {
            configManager.SaveIOmapConfig(this.dataGridView2.DataSource as DataTable);
            //OnConfigChanged();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (!isSavedConfig)
            {
                if (MessageBox.Show("配置还没有保存，确定关闭？", "Confirm Message", MessageBoxButtons.OKCancel) != DialogResult.OK)
                {
                    return;
                }
            }
            this.Close();
        }

        #endregion
    }
}
