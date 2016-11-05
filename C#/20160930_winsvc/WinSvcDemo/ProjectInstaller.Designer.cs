namespace WinSvcDemo
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.WinSvcDemoPI = new System.ServiceProcess.ServiceProcessInstaller();
            this.WinSvcDemoInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // WinSvcDemoPI
            // 
            this.WinSvcDemoPI.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.WinSvcDemoPI.Password = null;
            this.WinSvcDemoPI.Username = null;
            // 
            // WinSvcDemoInstaller
            // 
            this.WinSvcDemoInstaller.ServiceName = "WinSvcDemo";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WinSvcDemoPI,
            this.WinSvcDemoInstaller});

        }

        #endregion
        public System.ServiceProcess.ServiceInstaller WinSvcDemoInstaller;
        public System.ServiceProcess.ServiceProcessInstaller WinSvcDemoPI;
    }
}