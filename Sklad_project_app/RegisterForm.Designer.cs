
namespace Sklad_project_app
{
    partial class RegisterForm
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
            panelReg = new Panel();
            lblTitle = new Label();
            lblFio = new Label();
            txtFio = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblChoose = new Label();
            chkAdmin = new CheckBox();
            chkStorekeeper = new CheckBox();
            btnRegister = new Button();
            panelTop.SuspendLayout();
            panelReg.SuspendLayout();
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
            // panelReg
            // 
            panelReg.BackColor = Color.FromArgb(30, 100, 200);
            panelReg.Controls.Add(lblTitle);
            panelReg.Controls.Add(lblFio);
            panelReg.Controls.Add(txtFio);
            panelReg.Controls.Add(lblPassword);
            panelReg.Controls.Add(txtPassword);
            panelReg.Controls.Add(lblChoose);
            panelReg.Controls.Add(chkAdmin);
            panelReg.Controls.Add(chkStorekeeper);
            panelReg.Controls.Add(btnRegister);
            panelReg.Location = new Point(270, 100);
            panelReg.Name = "panelReg";
            panelReg.Size = new Size(260, 260);
            panelReg.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial", 10F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(10, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(240, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "РЕГИСТРАЦИЯ В СИСТЕМЕ";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFio
            // 
            lblFio.AutoSize = true;
            lblFio.Font = new Font("Arial", 9F);
            lblFio.ForeColor = Color.White;
            lblFio.Location = new Point(10, 42);
            lblFio.Name = "lblFio";
            lblFio.Size = new Size(108, 17);
            lblFio.TabIndex = 1;
            lblFio.Text = "Введите ФИО:";
            // 
            // txtFio
            // 
            txtFio.BackColor = Color.FromArgb(180, 200, 230);
            txtFio.Location = new Point(10, 60);
            txtFio.Name = "txtFio";
            txtFio.Size = new Size(240, 27);
            txtFio.TabIndex = 2;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Arial", 9F);
            lblPassword.ForeColor = Color.White;
            lblPassword.Location = new Point(10, 90);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(122, 17);
            lblPassword.TabIndex = 3;
            lblPassword.Text = "Введите пароль:";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.FromArgb(180, 200, 230);
            txtPassword.Location = new Point(10, 108);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(240, 27);
            txtPassword.TabIndex = 4;
            // 
            // lblChoose
            // 
            lblChoose.AutoSize = true;
            lblChoose.Font = new Font("Arial", 9F);
            lblChoose.ForeColor = Color.White;
            lblChoose.Location = new Point(10, 138);
            lblChoose.Name = "lblChoose";
            lblChoose.Size = new Size(133, 17);
            lblChoose.TabIndex = 5;
            lblChoose.Text = "Выберите нужное:";
            // 
            // chkAdmin
            // 
            chkAdmin.AutoSize = true;
            chkAdmin.Font = new Font("Arial", 9F);
            chkAdmin.ForeColor = Color.White;
            chkAdmin.Location = new Point(10, 158);
            chkAdmin.Name = "chkAdmin";
            chkAdmin.Size = new Size(136, 21);
            chkAdmin.TabIndex = 6;
            chkAdmin.Text = "Администратор";
            // 
            // chkStorekeeper
            // 
            chkStorekeeper.AutoSize = true;
            chkStorekeeper.Font = new Font("Arial", 9F);
            chkStorekeeper.ForeColor = Color.White;
            chkStorekeeper.Location = new Point(10, 180);
            chkStorekeeper.Name = "chkStorekeeper";
            chkStorekeeper.Size = new Size(106, 21);
            chkStorekeeper.TabIndex = 7;
            chkStorekeeper.Text = "Кладовщик";
            // 
            // btnRegister
            // 
            btnRegister.BackColor = Color.FromArgb(210, 220, 235);
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.Location = new Point(10, 215);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(108, 28);
            btnRegister.TabIndex = 8;
            btnRegister.Text = "Регистрация";
            btnRegister.UseVisualStyleBackColor = false;
            btnRegister.Click += btnRegister_Click;
            // 
            // RegisterForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(800, 500);
            Controls.Add(panelTop);
            Controls.Add(panelReg);
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Регистрация";
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelReg.ResumeLayout(false);
            panelReg.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Panel panelReg;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFio;
        private System.Windows.Forms.TextBox txtFio;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblChoose;
        private System.Windows.Forms.CheckBox chkAdmin;
        private System.Windows.Forms.CheckBox chkStorekeeper;
        private System.Windows.Forms.Button btnRegister;
    }
}
