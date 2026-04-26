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

        const expiringContainer = document.getElementById('expiring-list');
        expiringContainer.innerHTML = data.expiringItems.map(item => `
            <div class="item-card">
                <div class="item-info">
                    <strong>${item.name}</strong>
                    <p>ID: ${item.id}</p>
                </div>
                <div>${item.expiryDate}</div>
            </div>
        `).join('');

    } catch (error) {
        console.error("Dashboard Sync Error:", error);
    }
}

document.addEventListener('DOMContentLoaded', () => {
    updateDashboard();
    setInterval(updateDashboard, 30000);
});

document.addEventListener('DOMContentLoaded', () => {
    // Initial data load
    updateDashboard();

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