namespace AIS
{
    partial class AutentificationF
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
            this.tb_login = new System.Windows.Forms.TextBox();
            this.tb_password = new System.Windows.Forms.TextBox();
            this.lb_login = new System.Windows.Forms.Label();
            this.lb_pass = new System.Windows.Forms.Label();
            this.bt_sign_up = new System.Windows.Forms.Button();
            this.bt_reg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tb_login
            // 
            this.tb_login.Location = new System.Drawing.Point(62, 19);
            this.tb_login.Name = "tb_login";
            this.tb_login.Size = new System.Drawing.Size(187, 20);
            this.tb_login.TabIndex = 0;
            // 
            // tb_password
            // 
            this.tb_password.Location = new System.Drawing.Point(62, 46);
            this.tb_password.Name = "tb_password";
            this.tb_password.PasswordChar = '*';
            this.tb_password.Size = new System.Drawing.Size(187, 20);
            this.tb_password.TabIndex = 1;
            // 
            // lb_login
            // 
            this.lb_login.AutoSize = true;
            this.lb_login.Location = new System.Drawing.Point(22, 22);
            this.lb_login.Name = "lb_login";
            this.lb_login.Size = new System.Drawing.Size(38, 13);
            this.lb_login.TabIndex = 2;
            this.lb_login.Text = "Логин";
            // 
            // lb_pass
            // 
            this.lb_pass.AutoSize = true;
            this.lb_pass.Location = new System.Drawing.Point(15, 49);
            this.lb_pass.Name = "lb_pass";
            this.lb_pass.Size = new System.Drawing.Size(45, 13);
            this.lb_pass.TabIndex = 3;
            this.lb_pass.Text = "Пароль";
            // 
            // bt_sign_up
            // 
            this.bt_sign_up.Location = new System.Drawing.Point(62, 73);
            this.bt_sign_up.Name = "bt_sign_up";
            this.bt_sign_up.Size = new System.Drawing.Size(91, 23);
            this.bt_sign_up.TabIndex = 4;
            this.bt_sign_up.Text = "Авторизоваться";
            this.bt_sign_up.UseVisualStyleBackColor = true;
            this.bt_sign_up.Click += new System.EventHandler(this.bt_sign_up_Click);
            // 
            // bt_reg
            // 
            this.bt_reg.Location = new System.Drawing.Point(159, 73);
            this.bt_reg.Name = "bt_reg";
            this.bt_reg.Size = new System.Drawing.Size(90, 23);
            this.bt_reg.TabIndex = 5;
            this.bt_reg.Text = "Регистрация";
            this.bt_reg.UseVisualStyleBackColor = true;
            // 
            // AutentificationF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 110);
            this.Controls.Add(this.bt_reg);
            this.Controls.Add(this.bt_sign_up);
            this.Controls.Add(this.lb_pass);
            this.Controls.Add(this.lb_login);
            this.Controls.Add(this.tb_password);
            this.Controls.Add(this.tb_login);
            this.Name = "AutentificationF";
            this.Text = "AutentificationF";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_login;
        private System.Windows.Forms.TextBox tb_password;
        private System.Windows.Forms.Label lb_login;
        private System.Windows.Forms.Label lb_pass;
        private System.Windows.Forms.Button bt_sign_up;
        private System.Windows.Forms.Button bt_reg;
    }
}