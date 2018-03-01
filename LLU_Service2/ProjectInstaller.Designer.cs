namespace LLU_Service2
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LLU_serviceProcessInstaller2 = new System.ServiceProcess.ServiceProcessInstaller();
            this.LLU_serviceInstaller2 = new System.ServiceProcess.ServiceInstaller();
            // 
            // LLU_serviceProcessInstaller2
            // 
            this.LLU_serviceProcessInstaller2.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.LLU_serviceProcessInstaller2.Password = null;
            this.LLU_serviceProcessInstaller2.Username = null;
            // 
            // LLU_serviceInstaller2
            // 
            this.LLU_serviceInstaller2.DisplayName = "LLU service2";
            this.LLU_serviceInstaller2.ServiceName = "LLU_Service2";
            this.LLU_serviceInstaller2.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.LLU_serviceProcessInstaller2,
            this.LLU_serviceInstaller2});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller LLU_serviceProcessInstaller2;
        private System.ServiceProcess.ServiceInstaller LLU_serviceInstaller2;
    }
}