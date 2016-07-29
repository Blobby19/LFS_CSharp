namespace LFS_CSharp
{
    partial class vJoyForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gpb_axes = new System.Windows.Forms.GroupBox();
            this.pgb_axeZ = new System.Windows.Forms.ProgressBar();
            this.pgb_axeY = new System.Windows.Forms.ProgressBar();
            this.pgb_axeX = new System.Windows.Forms.ProgressBar();
            this.gpb_Control = new System.Windows.Forms.GroupBox();
            this.gpb_buttons = new System.Windows.Forms.GroupBox();
            this.chk_button1 = new System.Windows.Forms.CheckBox();
            this.chk_button2 = new System.Windows.Forms.CheckBox();
            this.lbl_controller = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.gpb_axes.SuspendLayout();
            this.gpb_Control.SuspendLayout();
            this.gpb_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.gpb_axes);
            this.flowLayoutPanel1.Controls.Add(this.gpb_Control);
            this.flowLayoutPanel1.Controls.Add(this.gpb_buttons);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(449, 530);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // gpb_axes
            // 
            this.gpb_axes.Controls.Add(this.pgb_axeZ);
            this.gpb_axes.Controls.Add(this.pgb_axeY);
            this.gpb_axes.Controls.Add(this.pgb_axeX);
            this.gpb_axes.Location = new System.Drawing.Point(3, 3);
            this.gpb_axes.Name = "gpb_axes";
            this.gpb_axes.Size = new System.Drawing.Size(233, 100);
            this.gpb_axes.TabIndex = 0;
            this.gpb_axes.TabStop = false;
            this.gpb_axes.Text = "Axes";
            // 
            // pgb_axeZ
            // 
            this.pgb_axeZ.Location = new System.Drawing.Point(10, 60);
            this.pgb_axeZ.Name = "pgb_axeZ";
            this.pgb_axeZ.Size = new System.Drawing.Size(217, 16);
            this.pgb_axeZ.TabIndex = 2;
            // 
            // pgb_axeY
            // 
            this.pgb_axeY.Location = new System.Drawing.Point(10, 38);
            this.pgb_axeY.Name = "pgb_axeY";
            this.pgb_axeY.Size = new System.Drawing.Size(217, 16);
            this.pgb_axeY.TabIndex = 1;
            // 
            // pgb_axeX
            // 
            this.pgb_axeX.Location = new System.Drawing.Point(10, 16);
            this.pgb_axeX.Name = "pgb_axeX";
            this.pgb_axeX.Size = new System.Drawing.Size(217, 16);
            this.pgb_axeX.TabIndex = 0;
            // 
            // gpb_Control
            // 
            this.gpb_Control.Controls.Add(this.lbl_controller);
            this.gpb_Control.Location = new System.Drawing.Point(242, 3);
            this.gpb_Control.Name = "gpb_Control";
            this.gpb_Control.Size = new System.Drawing.Size(201, 100);
            this.gpb_Control.TabIndex = 1;
            this.gpb_Control.TabStop = false;
            this.gpb_Control.Text = "Controller";
            // 
            // gpb_buttons
            // 
            this.gpb_buttons.Controls.Add(this.chk_button2);
            this.gpb_buttons.Controls.Add(this.chk_button1);
            this.gpb_buttons.Location = new System.Drawing.Point(3, 109);
            this.gpb_buttons.Name = "gpb_buttons";
            this.gpb_buttons.Size = new System.Drawing.Size(446, 421);
            this.gpb_buttons.TabIndex = 2;
            this.gpb_buttons.TabStop = false;
            this.gpb_buttons.Text = "Bouttons";
            // 
            // chk_button1
            // 
            this.chk_button1.AutoSize = true;
            this.chk_button1.Location = new System.Drawing.Point(7, 20);
            this.chk_button1.Name = "chk_button1";
            this.chk_button1.Size = new System.Drawing.Size(72, 17);
            this.chk_button1.TabIndex = 0;
            this.chk_button1.Text = "Boutton 1";
            this.chk_button1.UseVisualStyleBackColor = true;
            // 
            // chk_button2
            // 
            this.chk_button2.AutoSize = true;
            this.chk_button2.Location = new System.Drawing.Point(6, 43);
            this.chk_button2.Name = "chk_button2";
            this.chk_button2.Size = new System.Drawing.Size(72, 17);
            this.chk_button2.TabIndex = 1;
            this.chk_button2.Text = "Boutton 2";
            this.chk_button2.UseVisualStyleBackColor = true;
            // 
            // lbl_controller
            // 
            this.lbl_controller.Location = new System.Drawing.Point(0, 16);
            this.lbl_controller.Name = "lbl_controller";
            this.lbl_controller.Size = new System.Drawing.Size(201, 84);
            this.lbl_controller.TabIndex = 0;
            // 
            // vJoyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 530);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "vJoyForm";
            this.Text = "vJoyForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.vJoyForm_FormClosing);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gpb_axes.ResumeLayout(false);
            this.gpb_Control.ResumeLayout(false);
            this.gpb_buttons.ResumeLayout(false);
            this.gpb_buttons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gpb_axes;
        private System.Windows.Forms.GroupBox gpb_Control;
        private System.Windows.Forms.GroupBox gpb_buttons;
        private System.Windows.Forms.ProgressBar pgb_axeZ;
        private System.Windows.Forms.ProgressBar pgb_axeY;
        private System.Windows.Forms.ProgressBar pgb_axeX;
        private System.Windows.Forms.CheckBox chk_button1;
        private System.Windows.Forms.CheckBox chk_button2;
        private System.Windows.Forms.Label lbl_controller;
    }
}