namespace ModBusProtocol_PLC
{
    partial class AutoBaseMonitorView
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(textBox1);
            splitContainer1.Size = new Size(441, 300);
            splitContainer1.SplitterDistance = 96;
            splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 2, 0);
            tableLayoutPanel1.Controls.Add(label4, 0, 1);
            tableLayoutPanel1.Controls.Add(label5, 1, 1);
            tableLayoutPanel1.Controls.Add(label6, 2, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(441, 96);
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			// 
			// label1
			// 
			label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("맑은 고딕", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 129);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(258, 40);
            label1.TabIndex = 0;
            label1.Text = "IP";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(267, 0);
            label2.Name = "label2";
            label2.Size = new Size(82, 40);
            label2.TabIndex = 0;
            label2.Text = "생산량";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(355, 0);
            label3.Name = "label3";
            label3.Size = new Size(83, 40);
            label3.TabIndex = 0;
            label3.Text = "가동상태";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(3, 40);
            label4.Name = "label4";
            label4.Size = new Size(258, 56);
            label4.TabIndex = 1;
            label4.Text = "Ip_addr";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(267, 40);
            label5.Name = "label5";
            label5.Size = new Size(82, 56);
            label5.TabIndex = 1;
            label5.Text = "total_cnt";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(355, 40);
            label6.Name = "label6";
            label6.Size = new Size(83, 56);
            label6.TabIndex = 1;
            label6.Text = "status";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(0, 0);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(441, 200);
            textBox1.TabIndex = 0;
            // 
            // AutoBaseMonitorView
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Name = "AutoBaseMonitorView";
            Size = new Size(441, 299);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}
