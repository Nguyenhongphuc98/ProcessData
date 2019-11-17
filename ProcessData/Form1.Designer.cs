namespace ProcessData
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnChangeOutPutFolder = new System.Windows.Forms.Button();
            this.btnChangeNewDataFolder = new System.Windows.Forms.Button();
            this.btnChangeCheckFolder = new System.Windows.Forms.Button();
            this.tbOutPut = new System.Windows.Forms.TextBox();
            this.tbNewData = new System.Windows.Forms.TextBox();
            this.tbLinkCheck = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.addEffect = new System.Windows.Forms.TabPage();
            this.gvLogEffect = new System.Windows.Forms.DataGridView();
            this.stt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAddEffect = new System.Windows.Forms.Button();
            this.tbLsNameEffect = new System.Windows.Forms.TextBox();
            this.addMonter = new System.Windows.Forms.TabPage();
            this.btnAddMonster = new System.Windows.Forms.Button();
            this.tbMonsterName = new System.Windows.Forms.TextBox();
            this.gvLogMonster = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.addEffect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogEffect)).BeginInit();
            this.addMonter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogMonster)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnChangeOutPutFolder);
            this.groupBox1.Controls.Add(this.btnChangeNewDataFolder);
            this.groupBox1.Controls.Add(this.btnChangeCheckFolder);
            this.groupBox1.Controls.Add(this.tbOutPut);
            this.groupBox1.Controls.Add(this.tbNewData);
            this.groupBox1.Controls.Add(this.tbLinkCheck);
            this.groupBox1.Location = new System.Drawing.Point(161, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setup path info";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "New data:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Check folder:";
            // 
            // btnChangeOutPutFolder
            // 
            this.btnChangeOutPutFolder.Location = new System.Drawing.Point(561, 65);
            this.btnChangeOutPutFolder.Name = "btnChangeOutPutFolder";
            this.btnChangeOutPutFolder.Size = new System.Drawing.Size(75, 23);
            this.btnChangeOutPutFolder.TabIndex = 5;
            this.btnChangeOutPutFolder.Text = "Change";
            this.btnChangeOutPutFolder.UseVisualStyleBackColor = true;
            this.btnChangeOutPutFolder.Click += new System.EventHandler(this.btnChangeOutPutFolder_Click);
            // 
            // btnChangeNewDataFolder
            // 
            this.btnChangeNewDataFolder.Location = new System.Drawing.Point(561, 41);
            this.btnChangeNewDataFolder.Name = "btnChangeNewDataFolder";
            this.btnChangeNewDataFolder.Size = new System.Drawing.Size(75, 23);
            this.btnChangeNewDataFolder.TabIndex = 4;
            this.btnChangeNewDataFolder.Text = "Change";
            this.btnChangeNewDataFolder.UseVisualStyleBackColor = true;
            this.btnChangeNewDataFolder.Click += new System.EventHandler(this.btnChangeNewDataFolder_Click);
            // 
            // btnChangeCheckFolder
            // 
            this.btnChangeCheckFolder.Location = new System.Drawing.Point(561, 17);
            this.btnChangeCheckFolder.Name = "btnChangeCheckFolder";
            this.btnChangeCheckFolder.Size = new System.Drawing.Size(75, 23);
            this.btnChangeCheckFolder.TabIndex = 3;
            this.btnChangeCheckFolder.Text = "Change";
            this.btnChangeCheckFolder.UseVisualStyleBackColor = true;
            this.btnChangeCheckFolder.Click += new System.EventHandler(this.btnChangeCheckFolder_Click);
            // 
            // tbOutPut
            // 
            this.tbOutPut.Location = new System.Drawing.Point(117, 67);
            this.tbOutPut.Name = "tbOutPut";
            this.tbOutPut.ReadOnly = true;
            this.tbOutPut.Size = new System.Drawing.Size(425, 20);
            this.tbOutPut.TabIndex = 2;
            // 
            // tbNewData
            // 
            this.tbNewData.Location = new System.Drawing.Point(117, 43);
            this.tbNewData.Name = "tbNewData";
            this.tbNewData.ReadOnly = true;
            this.tbNewData.Size = new System.Drawing.Size(425, 20);
            this.tbNewData.TabIndex = 1;
            // 
            // tbLinkCheck
            // 
            this.tbLinkCheck.Location = new System.Drawing.Point(117, 19);
            this.tbLinkCheck.Name = "tbLinkCheck";
            this.tbLinkCheck.ReadOnly = true;
            this.tbLinkCheck.Size = new System.Drawing.Size(425, 20);
            this.tbLinkCheck.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.addEffect);
            this.tabControl1.Controls.Add(this.addMonter);
            this.tabControl1.Location = new System.Drawing.Point(12, 120);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(940, 370);
            this.tabControl1.TabIndex = 1;
            // 
            // addEffect
            // 
            this.addEffect.BackColor = System.Drawing.Color.Gainsboro;
            this.addEffect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.addEffect.Controls.Add(this.gvLogEffect);
            this.addEffect.Controls.Add(this.btnAddEffect);
            this.addEffect.Controls.Add(this.tbLsNameEffect);
            this.addEffect.Location = new System.Drawing.Point(4, 22);
            this.addEffect.Name = "addEffect";
            this.addEffect.Padding = new System.Windows.Forms.Padding(3);
            this.addEffect.Size = new System.Drawing.Size(932, 344);
            this.addEffect.TabIndex = 0;
            this.addEffect.Text = "Add effect";
            // 
            // gvLogEffect
            // 
            this.gvLogEffect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLogEffect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stt,
            this.time,
            this.message});
            this.gvLogEffect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gvLogEffect.Location = new System.Drawing.Point(3, 83);
            this.gvLogEffect.Name = "gvLogEffect";
            this.gvLogEffect.Size = new System.Drawing.Size(924, 256);
            this.gvLogEffect.TabIndex = 2;
            // 
            // stt
            // 
            this.stt.HeaderText = "STT";
            this.stt.Name = "stt";
            this.stt.Width = 50;
            // 
            // time
            // 
            this.time.HeaderText = "Time";
            this.time.Name = "time";
            this.time.Width = 200;
            // 
            // message
            // 
            this.message.HeaderText = "Message";
            this.message.Name = "message";
            this.message.Width = 630;
            // 
            // btnAddEffect
            // 
            this.btnAddEffect.Location = new System.Drawing.Point(587, 45);
            this.btnAddEffect.Name = "btnAddEffect";
            this.btnAddEffect.Size = new System.Drawing.Size(75, 23);
            this.btnAddEffect.TabIndex = 1;
            this.btnAddEffect.Text = "Start";
            this.btnAddEffect.UseVisualStyleBackColor = true;
            this.btnAddEffect.Click += new System.EventHandler(this.btnAddEffect_Click);
            // 
            // tbLsNameEffect
            // 
            this.tbLsNameEffect.Location = new System.Drawing.Point(228, 47);
            this.tbLsNameEffect.Name = "tbLsNameEffect";
            this.tbLsNameEffect.Size = new System.Drawing.Size(353, 20);
            this.tbLsNameEffect.TabIndex = 0;
            // 
            // addMonter
            // 
            this.addMonter.BackColor = System.Drawing.Color.Gainsboro;
            this.addMonter.Controls.Add(this.btnAddMonster);
            this.addMonter.Controls.Add(this.tbMonsterName);
            this.addMonter.Controls.Add(this.gvLogMonster);
            this.addMonter.Location = new System.Drawing.Point(4, 22);
            this.addMonter.Name = "addMonter";
            this.addMonter.Padding = new System.Windows.Forms.Padding(3);
            this.addMonter.Size = new System.Drawing.Size(932, 344);
            this.addMonter.TabIndex = 1;
            this.addMonter.Text = "Add monster";
            // 
            // btnAddMonster
            // 
            this.btnAddMonster.Location = new System.Drawing.Point(588, 36);
            this.btnAddMonster.Name = "btnAddMonster";
            this.btnAddMonster.Size = new System.Drawing.Size(75, 23);
            this.btnAddMonster.TabIndex = 5;
            this.btnAddMonster.Text = "Start";
            this.btnAddMonster.UseVisualStyleBackColor = true;
            this.btnAddMonster.Click += new System.EventHandler(this.btnAddMonster_Click);
            // 
            // tbMonsterName
            // 
            this.tbMonsterName.Location = new System.Drawing.Point(229, 38);
            this.tbMonsterName.Name = "tbMonsterName";
            this.tbMonsterName.Size = new System.Drawing.Size(353, 20);
            this.tbMonsterName.TabIndex = 4;
            // 
            // gvLogMonster
            // 
            this.gvLogMonster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvLogMonster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.gvLogMonster.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gvLogMonster.Location = new System.Drawing.Point(3, 85);
            this.gvLogMonster.Name = "gvLogMonster";
            this.gvLogMonster.Size = new System.Drawing.Size(926, 256);
            this.gvLogMonster.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "STT";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Time";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Message";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 630;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 502);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Ứng dụng chuyển dữ liệu";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.addEffect.ResumeLayout(false);
            this.addEffect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogEffect)).EndInit();
            this.addMonter.ResumeLayout(false);
            this.addMonter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvLogMonster)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChangeOutPutFolder;
        private System.Windows.Forms.Button btnChangeNewDataFolder;
        private System.Windows.Forms.Button btnChangeCheckFolder;
        private System.Windows.Forms.TextBox tbOutPut;
        private System.Windows.Forms.TextBox tbNewData;
        private System.Windows.Forms.TextBox tbLinkCheck;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage addEffect;
        private System.Windows.Forms.DataGridView gvLogEffect;
        private System.Windows.Forms.DataGridViewTextBoxColumn stt;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
        private System.Windows.Forms.DataGridViewTextBoxColumn message;
        private System.Windows.Forms.Button btnAddEffect;
        private System.Windows.Forms.TextBox tbLsNameEffect;
        private System.Windows.Forms.TabPage addMonter;
        private System.Windows.Forms.Button btnAddMonster;
        private System.Windows.Forms.TextBox tbMonsterName;
        private System.Windows.Forms.DataGridView gvLogMonster;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    }
}

