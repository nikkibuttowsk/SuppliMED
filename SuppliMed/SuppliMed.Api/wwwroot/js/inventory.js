async function loadInventoryTable() {
    try {
        const response = await fetch('/api/inventory'); // Ensure your Controller has a GET all
        const supplies = await response.json();
        
        const tbody = document.getElementById('inventoryBody');
        tbody.innerHTML = ''; // Clear the "Loading..." text

        supplies.forEach(item => {
            const isLow = item.quantity <= (item.minimumStock || 10);
            const statusClass = isLow ? 'low' : 'good';
            const statusText = isLow ? 'Low Stock' : 'In Stock';

            const row = `
                <tr>
                    <td>${item.id}</td>
                    <td>${item.name}</td>
                    <td>${item.brand || item.serialNumber || 'N/A'}</td>
                    <td>${item.quantity}</td>
                    <td>${item.expirationDate}</td>
                    <td><span class="status-pill ${statusClass}">${statusText}</span></td>
                </tr>
            `;
            tbody.innerHTML += row;
        });
    } catch (error) {
        console.error("Error loading table:", error);
    }
}