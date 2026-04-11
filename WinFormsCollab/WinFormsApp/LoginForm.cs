using System;
using System.Windows.Forms;
using AppCore.Models;
using AppCore.Services;

public partial class LoginForm : Form
{
    public LoginForm()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object sender, EventArgs e)
    {
        var user = AuthService.Login(txtUsername.Text, txtPassword.Text);

        if (user != null)
        {
            MessageBox.Show("Login successful!");

            MainForm main = new MainForm(user);
            main.Show();

            this.Hide();
        }
        else
        {
            MessageBox.Show("Invalid credentials");
        }
    }
}