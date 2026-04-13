namespace Sklad_project_app
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblCompany = new Label();
            panelLogin = new Panel();
            lblTitle = new Label();
            lblLoginName = new Label();
            txtLogin = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnRegister = new Button();
            panelTop.SuspendLayout();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(30, 100, 200);
            panelTop.Controls.Add(lblCompany);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(800, 35);
            panelTop.TabIndex = 0;
            // 
            // lblCompany
            // 
            lblCompany.AutoSize = true;
            lblCompany.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblCompany.ForeColor = Color.White;
            lblCompany.Location = new Point(10, 8);
            lblCompany.Name = "lblCompany";
            lblCompany.Size = new Size(463, 19);
            lblCompany.TabIndex = 0;
            lblCompany.Text = "ООО \"Птички-тупички\" - система управления складом";
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.FromArgb(30, 100, 200);
            panelLogin.Controls.Add(lblTitle);
            panelLogin.Controls.Add(lblLoginName);
            panelLogin.Controls.Add(txtLogin);
            panelLogin.Controls.Add(lblPassword);
            panelLogin.Controls.Add(txtPassword);
            panelLogin.Controls.Add(btnLogin);
            panelLogin.Controls.Add(btnRegister);
            panelLogin.Location = new Point(270, 120);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(260, 230);
            panelLogin.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(10, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(240, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ВХОД В СИСТЕМУ";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLoginName
            // 
            lblLoginName.AutoSize = true;
            lblLoginName.Font = new Font("Arial", 9F);
            lblLoginName.ForeColor = Color.White;
            lblLoginName.Location = new Point(10, 45);
            lblLoginName.Name = "lblLoginName";
            lblLoginName.Size = new Size(144, 17);
            lblLoginName.TabIndex = 1;
            lblLoginName.Text = "ФИО пользователя:";
            // 
            // txtLogin
            // 
            txtLogin.BackColor = Color.FromArgb(180, 200, 230);
            txtLogin.Location = new Point(10, 65);
            txtLogin.Name = "txtLogin";
            txtLogin.Size = new Size(240, 27);
            txtLogin.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Arial", 9F);
            lblPassword.ForeColor = Color.White;
            lblPassword.Location = new Point(10, 95);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(62, 17);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Пароль:";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(180, 200, 230);
            txtPassword.Location = new Point(10, 115);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(240, 27);
            txtPassword.TabIndex = 4;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(210, 220, 235);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Location = new Point(10, 150);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(80, 28);
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Войти";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.FromArgb(210, 220, 235);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Location = new Point(10, 188);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(108, 28);
            btnRegister.TabIndex = 6;
            btnRegister.Text = "Регистрация";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // LoginForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(800, 500);
            Controls.Add(panelTop);
            Controls.Add(panelLogin);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLoginName;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
    }
}