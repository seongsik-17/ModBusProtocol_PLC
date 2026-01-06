namespace ModBusProtocol_PLC
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(349, 12);
            button1.Name = "button1";
            button1.Size = new Size(211, 44);
            button1.TabIndex = 0;
            button1.Text = "viewSample";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(566, 12);
            button2.Name = "button2";
            button2.Size = new Size(211, 44);
            button2.TabIndex = 0;
            button2.Text = "데이터 받아오기";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 28);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(316, 28);
            comboBox1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 6);
            label1.Name = "label1";
            label1.Size = new Size(72, 20);
            label1.TabIndex = 6;
            label1.Text = "Ip 리스트";
            // 
            // button3
            // 
            button3.Location = new Point(783, 12);
            button3.Name = "button3";
            button3.Size = new Size(211, 44);
            button3.TabIndex = 7;
            button3.Text = "START";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1000, 12);
            button4.Name = "button4";
            button4.Size = new Size(211, 44);
            button4.TabIndex = 7;
            button4.Text = "STOP";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(1217, 12);
            button5.Name = "button5";
            button5.Size = new Size(211, 44);
            button5.TabIndex = 7;
            button5.Text = "CLEAR";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(14, 73);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1410, 645);
            flowLayoutPanel1.TabIndex = 8;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1436, 730);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "PLC_Test";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private ComboBox comboBox1;
        private Label label1;
        private Button button3;
        private Button button4;
        private Button button5;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
