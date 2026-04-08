try
{
    inventoryService.DeleteSupply(id, currentUser);
    MessageBox.Show("Deleted");
}
catch (Exception ex)
{
    MessageBox.Show(ex.Message);
}