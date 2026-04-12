using System;
using System.Windows.Forms;
using AppCore.Models;
using AppCore.Services;

public partial class InventoryForm : Form
{
    private InventoryServices inventoryService;
    private User currentUser;

    public InventoryForm(InventoryService service, User user)
    {
        InitializeComponent();
        inventoryService = service;
        currentUser = user;
    }

    private void InventoryForm_Load(object sender, EventArgs e)
    {
        cmbCategory.Items.Add("Medicine");
        cmbCategory.Items.Add("Equipment");

        LoadData();

        // Restrict staff
        if (currentUser.GetRole() == "Staff")
        {
            btnAdd.Enabled = false;
        }
    }

    private void LoadData()
    {
        dataGridView1.Rows.Clear();

        foreach (var item in inventoryService.GetAllSupplies())
        {
            dataGridView1.Rows.Add(
                item.Id,
                item.Name,
                item.GetCategory(),
                item.CurrentStock,
                item.GetStatus()
            );
        }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            MedicalSupply supply;

            if (cmbCategory.Text == "Medicine")
            {
                supply = new Medicine
                {
                    Id = txtId.Text,
                    Name = txtName.Text,
                    CurrentStock = int.Parse(txtStock.Text),
                    MinimumStock = int.Parse(txtMinStock.Text),
                    ExpirationDate = dtpExpiration.Value
                };
            }
            else
            {
                supply = new Equipment
                {
                    Id = txtId.Text,
                    Name = txtName.Text,
                    CurrentStock = int.Parse(txtStock.Text),
                    MinimumStock = int.Parse(txtMinStock.Text),
                    LastMaintenanceDate = dtpMaintenance.Value
                };
            }

            inventoryService.AddSupply(supply);

            LoadData();

            MessageBox.Show("Item added!");
        }
        catch (Exception)
        {
            MessageBox.Show("Invalid input!");
        }
    }
}