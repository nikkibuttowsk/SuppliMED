async function updateDashboard() {
    try {
        const response = await fetch('/api/inventory/dashboard-summary');
        const data = await response.json();

        console.log(data);

        document.getElementById('total-supply-count').innerText = data.totalCount;
        document.getElementById('low-stock-count').innerText = data.lowStockCount;
        document.getElementById('expired-count').innerText = data.expiredCount;

        document.getElementById('low-stock-badge').innerText = data.lowStockItems.length;
        document.getElementById('expiring-badge').innerText = data.expiringItems.length;

        const lowStockContainer = document.getElementById('low-stock-list');
        lowStockContainer.innerHTML = data.lowStockItems.map(item => {
            const percentage = Math.min((item.quantity / 20) * 100, 100);
            const barColor = percentage <= 15 ? "#931313" : "#E2B737";

            return `
                <div class="item-card">
                    <div class="item-info">
                        <strong>${item.name}</strong>
                        <p>${item.brand || 'I-Care'}</p>
                        <div class="progress-bar">
                            <div class="progress" style="width:${percentage}%; background:${barColor}"></div>
                        </div>
                    </div>
                    <div>${item.quantity}</div>
                </div>
            `;
        }).join('');

        // expiring soon list
        const expiringContainer = document.getElementById('expiring-list');
        expiringContainer.innerHTML = data.expiringItems.map(item => {
            const expiry = new Date(item.expiryDate);
            const daysLeft = Math.ceil((expiry - today) / (1000 * 60 * 60 * 24));

            const today = new Date();

            let label = "Expiring Soon";
            let labelClass = "yellow";

            if (daysLeft <= 7) {
                label = "Urgent";
                labelClass = "red";
            }

            return `
                <div class="expiring-item-row">
                    <div class="expiring-status-line ${labelClass}"></div> 
                    <div class="expiring-details">
                        <span class="expiring-name">${item.name}</span>
                        <span class="expiring-id">ID: ${item.id}</span>
                        <span class="expiring-label ${labelClass}">${label}</span>
                    </div>
                    <span class="expiring-date-badge">${item.expiryDate || "No Date"}</span>
                </div>
            `;
        }).join('');

    } catch (error) {
        console.error("Dashboard Sync Error:", error);
    }
}


document.addEventListener('DOMContentLoaded', () => {
    // Initial data load
    updateDashboard();
    setInterval(updateDashboard, 30000);
    // Button Listeners
    document.getElementById('btn-add').addEventListener('click', () => {
        ModalController.open('add'); //calling the modal controller
    });

    document.getElementById('btn-delete').addEventListener('click', () => {
        ModalController.open('delete');
    });

    document.getElementById('btn-update').addEventListener('click', () => {
        ModalController.open('update');
    });
});