namespace GuaranteeApp
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.btnExit = new System.Windows.Forms.Button();
			this.lblOutputFolder = new System.Windows.Forms.Label();
			this.lblInputFile = new System.Windows.Forms.Label();
			this.txtOutputFolder = new System.Windows.Forms.TextBox();
			this.txtInputFile = new System.Windows.Forms.TextBox();
			this.btnOutputFolder = new System.Windows.Forms.Button();
			this.btnInputFile = new System.Windows.Forms.Button();
			this.btnProceed = new System.Windows.Forms.Button();
			this.fbdOutputFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.ofdInputFile = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// btnExit
			// 
			this.btnExit.Location = new System.Drawing.Point(397, 226);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 23);
			this.btnExit.TabIndex = 0;
			this.btnExit.Text = "Выход";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_OnClick);
			// 
			// lblOutputFolder
			// 
			this.lblOutputFolder.AutoSize = true;
			this.lblOutputFolder.Location = new System.Drawing.Point(35, 78);
			this.lblOutputFolder.Name = "lblOutputFolder";
			this.lblOutputFolder.Size = new System.Drawing.Size(122, 13);
			this.lblOutputFolder.TabIndex = 1;
			this.lblOutputFolder.Text = "Папка с результатами";
			// 
			// lblInputFile
			// 
			this.lblInputFile.AutoSize = true;
			this.lblInputFile.Location = new System.Drawing.Point(12, 108);
			this.lblInputFile.Name = "lblInputFile";
			this.lblInputFile.Size = new System.Drawing.Size(145, 13);
			this.lblInputFile.TabIndex = 2;
			this.lblInputFile.Text = "Папка с исходным файлом";
			// 
			// txtOutputFolder
			// 
			this.txtOutputFolder.Location = new System.Drawing.Point(163, 75);
			this.txtOutputFolder.Name = "txtOutputFolder";
			this.txtOutputFolder.ReadOnly = true;
			this.txtOutputFolder.Size = new System.Drawing.Size(273, 20);
			this.txtOutputFolder.TabIndex = 3;
			// 
			// txtInputFile
			// 
			this.txtInputFile.Location = new System.Drawing.Point(163, 105);
			this.txtInputFile.Name = "txtInputFile";
			this.txtInputFile.ReadOnly = true;
			this.txtInputFile.Size = new System.Drawing.Size(273, 20);
			this.txtInputFile.TabIndex = 4;
			// 
			// btnOutputFolder
			// 
			this.btnOutputFolder.Location = new System.Drawing.Point(442, 73);
			this.btnOutputFolder.Name = "btnOutputFolder";
			this.btnOutputFolder.Size = new System.Drawing.Size(30, 23);
			this.btnOutputFolder.TabIndex = 5;
			this.btnOutputFolder.Text = "...";
			this.btnOutputFolder.UseVisualStyleBackColor = true;
			this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_OnClick);
			// 
			// btnInputFile
			// 
			this.btnInputFile.Location = new System.Drawing.Point(442, 103);
			this.btnInputFile.Name = "btnInputFile";
			this.btnInputFile.Size = new System.Drawing.Size(30, 23);
			this.btnInputFile.TabIndex = 6;
			this.btnInputFile.Text = "...";
			this.btnInputFile.UseVisualStyleBackColor = true;
			this.btnInputFile.Click += new System.EventHandler(this.btnInputFile_OnClick);
			// 
			// btnProceed
			// 
			this.btnProceed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnProceed.Location = new System.Drawing.Point(372, 143);
			this.btnProceed.Name = "btnProceed";
			this.btnProceed.Size = new System.Drawing.Size(100, 30);
			this.btnProceed.TabIndex = 7;
			this.btnProceed.Text = "Запустить";
			this.btnProceed.UseVisualStyleBackColor = true;
			this.btnProceed.Click += new System.EventHandler(this.btnProceed_OnClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 261);
			this.Controls.Add(this.btnProceed);
			this.Controls.Add(this.btnInputFile);
			this.Controls.Add(this.btnOutputFolder);
			this.Controls.Add(this.txtInputFile);
			this.Controls.Add(this.txtOutputFolder);
			this.Controls.Add(this.lblInputFile);
			this.Controls.Add(this.lblOutputFolder);
			this.Controls.Add(this.btnExit);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Guarantee";
			this.Load += new System.EventHandler(this.MainForm_OnLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lblOutputFolder;
		private System.Windows.Forms.Label lblInputFile;
		private System.Windows.Forms.TextBox txtOutputFolder;
		private System.Windows.Forms.TextBox txtInputFile;
		private System.Windows.Forms.Button btnOutputFolder;
		private System.Windows.Forms.Button btnInputFile;
		private System.Windows.Forms.Button btnProceed;
		private System.Windows.Forms.FolderBrowserDialog fbdOutputFolder;
		private System.Windows.Forms.OpenFileDialog ofdInputFile;
	}
}

