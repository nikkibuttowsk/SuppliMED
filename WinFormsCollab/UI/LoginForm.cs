using System;
using System.Windows.Forms;
using System.Timers;

namespace UI
{
    public partial class LoginForm : Form
    {
        private int loginAttempts = 0;
        private const int maxAttempts = 3;
        private bool isLocked = false;
        private int lockTime = 60; // seconds
        private System.Timers.Timer lockTimer;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (isLocked)
            {
                MessageBox.Show($"Too many failed attempts. Try again in {lockTime} seconds.");
                return;
            }

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // default accounts
            var user = AuthService.Login(username, password);

            if (user != null)
            {
                MessageBox.Show($"Login successful! ({user.GetType().Name})");
                OpenMainForm(user.GetType().Name);
            }
            else
            {
                loginAttempts++;
                MessageBox.Show("Invalid username or password.");

                if (loginAttempts >= maxAttempts)
                {
                    StartLockout();
                }
            }
        }

        private void OpenMainForm(string role)
        {
            this.Hide();

            MainForm mainForm = new MainForm(role); // pass role
            mainForm.Show();
        }

        private void StartLockout()
        {
            isLocked = true;
            lockTime = 60;

            lockTimer = new System.Timers.Timer(1000); // 1 second
            lockTimer.Elapsed += OnTimedEvent;
            lockTimer.AutoReset = true;
            lockTimer.Enabled = true;

            MessageBox.Show("Too many failed attempts. Locked for 1 minute.");
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            lockTime--;

            if (lockTime <= 0)
            {
                lockTimer.Stop();
                isLocked = false;
                loginAttempts = 0;

                this.Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show("You can now try logging in again.");
                });
            }
        }
    }
}