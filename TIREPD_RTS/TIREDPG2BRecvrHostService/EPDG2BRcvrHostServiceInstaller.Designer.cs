namespace TIREDPG2BRecvrHostService
{
    partial class EPDG2BRcvrHostServiceInstaller
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
            this.B2GRecvrServiceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            // 
            // B2GRecvrServiceInstaller1
            // 
            this.B2GRecvrServiceInstaller1.DisplayName = "TIR EPD G2B Receiver Host Service";
            this.B2GRecvrServiceInstaller1.ServiceName = "EPD-G2BRecvrHostService";
            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // EPDG2BRcvrHostServiceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.B2GRecvrServiceInstaller1});

        }

        #endregion

        private System.ServiceProcess.ServiceInstaller B2GRecvrServiceInstaller1;
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
    }
}