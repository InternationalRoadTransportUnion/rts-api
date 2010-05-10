using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace IRU.RTS.AdminClient
{
	/// <summary>
	/// Summary description for ImportSubsDB.
	/// </summary>
	public class ImportSubsDB : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.ProgressBar prgBar;
		
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.OpenFileDialog ofd1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtProgress;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ImportSubsDB()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		private void btnImport_Click(object sender, System.EventArgs e)
		{
			#region validate

			txtProgress.Text="";
			txtFilePath.Text=txtFilePath.Text.Trim();

			if (txtFilePath.Text=="")
			{
				errorProvider1.SetError(btnBrowse,"Select a file path to Import");
			}
			errorProvider1.SetError(btnBrowse,"");
		

		
			#endregion
		
			
			try
			{
				this.Cursor=Cursors.WaitCursor;				 
				ImportSubsDBFromFile();
			}
			catch (Exception exx)
			{
				MessageBox.Show(this, "Error occured processing request \r\n " + exx.Message , "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);

				MessageBox.Show(this, "Some tables may not be updated. \r\n " + exx.Message , "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);

				return;
			}
			finally
			{
			this.Cursor=Cursors.Default;	
			}
		
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnImport = new System.Windows.Forms.Button();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.prgBar = new System.Windows.Forms.ProgressBar();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.ofd1 = new System.Windows.Forms.OpenFileDialog();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtProgress = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtFilePath
			// 
			this.txtFilePath.Location = new System.Drawing.Point(104, 24);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.ReadOnly = true;
			this.txtFilePath.Size = new System.Drawing.Size(384, 20);
			this.txtFilePath.TabIndex = 0;
			this.txtFilePath.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 24);
			this.label1.TabIndex = 1;
			this.label1.Text = "Import Path";
			// 
			// btnImport
			// 
			this.btnImport.Location = new System.Drawing.Point(432, 64);
			this.btnImport.Name = "btnImport";
			this.btnImport.Size = new System.Drawing.Size(128, 24);
			this.btnImport.TabIndex = 1;
			this.btnImport.Text = "Import";
			this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(504, 24);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(40, 24);
			this.btnBrowse.TabIndex = 0;
			this.btnBrowse.Text = "...";
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.prgBar);
			this.groupBox1.Controls.Add(this.btnBrowse);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtFilePath);
			this.groupBox1.Controls.Add(this.btnImport);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(600, 128);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(184, 24);
			this.label2.TabIndex = 8;
			this.label2.Text = "Import Progress:";
			// 
			// prgBar
			// 
			this.prgBar.Location = new System.Drawing.Point(16, 64);
			this.prgBar.Name = "prgBar";
			this.prgBar.Size = new System.Drawing.Size(384, 16);
			this.prgBar.TabIndex = 5;
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// ofd1
			// 
			this.ofd1.Filter = "XML Files|*.xml";
			this.ofd1.Title = "Select File to Import.";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnCancel);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox2.Location = new System.Drawing.Point(0, 334);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(600, 48);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(24, 16);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(128, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			// 
			// txtProgress
			// 
			this.txtProgress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtProgress.Location = new System.Drawing.Point(0, 128);
			this.txtProgress.Multiline = true;
			this.txtProgress.Name = "txtProgress";
			this.txtProgress.ReadOnly = true;
			this.txtProgress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtProgress.Size = new System.Drawing.Size(600, 206);
			this.txtProgress.TabIndex = 3;
			this.txtProgress.Text = "";
			// 
			// ImportSubsDB
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(600, 382);
			this.Controls.Add(this.txtProgress);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "ImportSubsDB";
			this.Text = "ImportSubsDB";
			this.Load += new System.EventHandler(this.ImportSubsDB_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnBrowse_Click(object sender, System.EventArgs e)
		{
			DialogResult dr = ofd1.ShowDialog();
			if (dr==DialogResult.Cancel)
				return;
			txtFilePath.Text=ofd1.FileName;
			btnImport.Enabled=true;
			
			prgBar.Value=0;

			

			

			
			
		}

		private void ImportSubsDBFromFile()
		{
			CommonDBHelper dbHelper = new CommonDBHelper((string)frmMain.HTConnectionStrings["SubscriberDB"]);
			prgBar.Value=0;
			
			
			string[] aTableArray=new string[] {"WS_SUBSCRIBER","WS_SUBSCRIBER_SERVICE_METHOD","WS_SUBSCRIBER_SERVICES","COPY_TO_URLS","IRU_ENCRYPTION_KEYS","RTS_RIGHTS_MASTER","RTS_USER","RTS_USER_RIGHTS","SUBSCRIBER_ENCRYPTION_KEYS"};

			DataSet dsSubs = new DataSet();
			dsSubs.ReadXml(txtFilePath.Text,XmlReadMode.ReadSchema);
			int nImportTableCount=0;
			txtProgress.Text+= "\r\n" + dsSubs.DataSetName;
			
		

			try
			{
				dbHelper.ConnectToDB();
				dbHelper.BeginTransaction(); //all changes in one transaction

			foreach (string aTable in aTableArray)
			{
				if (dsSubs.Tables[aTable].Rows.Count==0)
				{
					
					txtProgress.Text+= "\r\n" + "No changes in Table " + aTable;
					nImportTableCount++;
					prgBar.Value=prgBar.Maximum/aTableArray.Length * nImportTableCount;
					continue;
				}
				

					ArrayList aSQLPKParams = new ArrayList();
				
					#region generate delete and insert commands

					SqlCommand sqlDeletecmd, sqlInsertCmd;

					sqlDeletecmd=new SqlCommand();
					sqlInsertCmd=new SqlCommand();

					string sDeleteSQL = "DELETE " + aTable + " WHERE ";

					

					#region Delete parameters
					foreach (DataColumn dc in dsSubs.Tables[aTable].PrimaryKey)
					{
						SqlParameter sParam = new SqlParameter();
						sParam.ParameterName="@"+dc.ColumnName;
						sParam.SourceColumn=dc.ColumnName;

						if ( dc.DataType==typeof(System.Int32))
						{
							sParam.DbType= DbType.Int32;
						}
						else if ( dc.DataType==typeof(System.String))
						{
							sParam.DbType=DbType.String;
						}	
						else
						{
							throw new ApplicationException("cannot be precessed primary key " + dc.ColumnName + "of table " + aTable + " of type " + dc.DataType.ToString());
							}

						//add to pkparams collection
						aSQLPKParams.Add(sParam);
						sqlDeletecmd.Parameters.Add(sParam);

						sDeleteSQL+= " " + dc.ColumnName + "=@" + dc.ColumnName;
						sDeleteSQL+= " AND ";
					}
				
					sDeleteSQL=sDeleteSQL.Substring(0,sDeleteSQL.Length-5); //get rid of last AND ;

					sqlDeletecmd.CommandText=sDeleteSQL;
					#endregion
					

					#region insert parameters
					string sInsertSQL = "INSERT " + aTable + " (";
					string sInsertValues = " VALUES (";

					foreach (DataColumn dc in dsSubs.Tables[aTable].Columns)
					{
						SqlParameter sParam = new SqlParameter();
						sParam.ParameterName="@"+dc.ColumnName;
						sParam.SourceColumn=dc.ColumnName;

						System.Diagnostics.Debug.WriteLine(dc.DataType.ToString());
						if ( dc.DataType==typeof(System.Int32))
						{
							sParam.DbType= DbType.Int32;
						}
						else if ( dc.DataType==typeof(System.String))
						{
							sParam.DbType=DbType.String;
						}
						else if ( dc.DataType==typeof(System.DateTime))
						{
							sParam.DbType=DbType.DateTime;
						}	
						else if ( dc.DataType==typeof(System.Boolean))
						{
							sParam.DbType=DbType.Boolean;
						}	
						else if ( dc.DataType==typeof(System.Byte[]))
						{
							sParam.DbType=DbType.Binary;
						}	
						else
						{
							throw new ApplicationException("cannot be precessed primary key " + dc.ColumnName + "of table " + aTable + " of type " + dc.DataType.ToString());
						}

						//add to Params collection
						
						sqlInsertCmd.Parameters.Add(sParam);

						sInsertSQL+= " " + dc.ColumnName + ",";
						sInsertValues+= "@" + dc.ColumnName + ",";
						
					}
				
					sInsertSQL=sInsertSQL.Substring(0,sInsertSQL.Length-1)+") " ; //get rid of last , ;
					sInsertValues=sInsertValues.Substring(0,sInsertValues.Length-1)+") " ; //get rid of last , ;

					sqlInsertCmd.CommandText=sInsertSQL + sInsertValues ;

					System.Diagnostics.Debug.WriteLine(sqlInsertCmd.CommandText);

					#endregion

					#endregion
				
					int nRowCounter;
					
					nRowCounter=1;
					
					int nTotalRowsToImport=dsSubs.Tables[aTable].Rows.Count;
					txtProgress.Text+="\r\n\r\n ::Starting Import of Table " + aTable;
					txtProgress.Text+="\r\nNo. of Rows to Import :" + nTotalRowsToImport.ToString() + "\r\n";

					foreach (DataRow dr in dsSubs.Tables[aTable].Rows)
					{
						#region delete row
						foreach (SqlParameter sPKParam in  sqlDeletecmd.Parameters)
						{
							sPKParam.Value=dr[sPKParam.SourceColumn,DataRowVersion.Current];
						};	

						foreach (SqlParameter sPKParam in  sqlInsertCmd.Parameters)
						{
							sPKParam.Value=dr[sPKParam.SourceColumn,DataRowVersion.Current];
						};	
					
						int nRowsAffected;
						nRowsAffected= dbHelper.ExecuteNonQuery(sqlDeletecmd);
						if (nRowsAffected==0)
						{
							txtProgress.Text+= "\r\n" +  " Attempted delete row #" + nRowCounter.ToString() + " of " + nTotalRowsToImport.ToString() ;
						}
						else
						{
							txtProgress.Text+= "\r\n" +" Deleted row #" + nRowCounter.ToString()+ " of " + nTotalRowsToImport.ToString() ;
						}
					

						
						dbHelper.ExecuteNonQuery(sqlInsertCmd);
						txtProgress.Text+= "\r\n" + " Inserted row #" + nRowCounter.ToString()+ " of " + nTotalRowsToImport.ToString() ;

						nRowCounter++;
						;

						#endregion
					}
					
				
					txtProgress.Text+="\r\n\r\n::Completed Table " + aTable+"\r\n";
					nImportTableCount++;
					prgBar.Value=prgBar.Maximum/aTableArray.Length * nImportTableCount;
					Application.DoEvents();
					}
				dbHelper.CommitTransaction();

			}	
			catch (SqlException exSQL)
			{
				dbHelper.RollbackTransaction();
				MessageBox.Show(this, "All changes have been rolled back. Error occured processing request \r\n " + exSQL.Message + "\r\n SQL Error No:" + exSQL.Number, "Admin Client Error:",MessageBoxButtons.OK,MessageBoxIcon.Error);
				txtProgress.Text+="\r\nChanges to all tables rolledback.";
				return;
			}
			finally
			{
				dbHelper.Close();
			}
	

			
						
				txtProgress.Text+="\r\nImport Complete";

			MessageBox.Show("Import completed", "Admin Client");
			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		

		private void ImportSubsDB_Load(object sender, System.EventArgs e)
		{
			btnImport.Enabled=false;
		}

		
	}
}
