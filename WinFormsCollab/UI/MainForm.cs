using System;
using System.Windows.Forms;
using AppCore.Models;
using AppCore.Services;

namespace UI
{
    public partial class MainForm : Form
    {
        private User currentUser;
        private InventoryServices inventoryService;

        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            inventoryService = new InventoryServices();

            lblRole.Text = "Role: " + currentUser.GetRole();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            InventoryForm form = new InventoryForm(inventoryService, currentUser);
            form.Show();
        }
    }
}