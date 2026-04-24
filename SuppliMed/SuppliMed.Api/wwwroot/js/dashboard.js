async function updateDashboard() {
    try {
        const response = await fetch('/api/inventory/dashboard-summary');
        if (!response.ok) throw new Error("Failed to fetch dashboard data");
        
        const data = await response.json();

        // 1. Update Summary Cards
        document.getElementById('total-supply-count').innerText = data.totalCount;
        document.getElementById('low-stock-count').innerText = data.lowStockCount;
        document.getElementById('expired-count').innerText = data.expiredCount;

        // 2. Update Badges
        const lowBadge = document.getElementById('low-stock-badge');
        const expBadge = document.getElementById('expiring-badge');
        if(lowBadge) lowBadge.innerText = data.lowStockCount;
        if(expBadge) expBadge.innerText = data.expiringItems.length;

        // 3. Inject Low Stock List
        const lowStockList = document.getElementById('low-stock-list');
        if (lowStockList) {
            lowStockList.innerHTML = data.lowStockItems.map(item => `
                <div class="item-card">
                    <div class="item-info">
                        <strong>${item.name}</strong>
                        <p>${item.brand}</p>
                    </div>
                    <div class="item-qty">
                        <span>${item.quantity}</span>
                    </div>
                </div>
            `).join('');
        }

        // 4. Inject Expiring Soon List
        const expiringList = document.getElementById('expiring-list');
        if (expiringList) {
            expiringList.innerHTML = data.expiringItems.map(item => `
                <div class="item-card">
                    <div class="item-info">
                        <strong>${item.name}</strong>
                        <p>ID: ${item.id}</p>
                    </div>
                </div>
            `).join('');
        }

        // 5. Update Graph
        renderInventoryChart(data);

    } catch (error) {
        console.error("Dashboard Sync Error:", error);
    }
}

function renderInventoryChart(data) {
    const ctx = document.getElementById('inventoryChart').getContext('2d');
    
    if (window.myInventoryChart) {
        window.myInventoryChart.destroy();
    }

    window.myInventoryChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['Total', 'Low Stock', 'Expired'],
            datasets: [{
                label: 'Items',
                data: [data.totalCount, data.lowStockCount, data.expiredCount],
                backgroundColor: ['#4ade80', '#facc15', '#ef4444'],
                borderRadius: 8
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: { legend: { display: false } },
            scales: {
                y: { beginAtZero: true, grid: { color: 'rgba(255,255,255,0.1)' } },
                x: { grid: { display: false } }
            }
        }
    });
}

// Initialize on page load
document.addEventListener('DOMContentLoaded', updateDashboard);

document.addEventListener('DOMContentLoaded', () => {
    const updateClock = () => {
        const now = new Date();
        
        // Formatting options to match "19 April 2026 12:43:02"
        const options = { day: 'numeric', month: 'long', year: 'numeric' };
        const datePart = now.toLocaleDateString('en-GB', options);
        const timePart = now.toLocaleTimeString('en-GB', { hour12: false });
        
        document.getElementById('current-date').textContent = `${datePart} ${timePart}`;
    };

    setInterval(updateClock, 1000);
    updateClock(); 

    const navItems = document.querySelectorAll('.nav-item');
    navItems.forEach(item => {
        item.addEventListener('click', () => {
            // Remove active class from others
            navItems.forEach(nav => nav.classList.remove('active'));
            // Add to clicked
            item.classList.add('active');
        });
    });

    const actions = {
        add: document.querySelector('.action-btn.add'),
        delete: document.querySelector('.action-btn.delete'),
        update: document.querySelector('.action-btn.update')
    };

    actions.add.addEventListener('click', () => {
        console.log("Opening 'Add Supply' Modal...");
        alert("Redirecting to Add Supply Form");
    });

    actions.delete.addEventListener('click', () => {
        const confirmDelete = confirm("Are you sure you want to delete a supply record?");
        if(confirmDelete) console.log("Delete mode active");
    });

    actions.update.addEventListener('click', () => {
        console.log("Update mode triggered");
        alert("Select an item to update its details.");
    });

    // stock is below 15%, dynamically change bar colors
    const progressBars = document.querySelectorAll('.progress');
    progressBars.forEach(bar => {
        const width = parseInt(bar.style.width);
        if (width <= 15) {
            bar.style.backgroundColor = '#ef4444'; // Red for critical
        } else if (width <= 40) {
            bar.style.backgroundColor = '#facc15'; // Yellow for warning
        } else {
            bar.style.backgroundColor = '#4ade80'; // Green for healthy
        }
    });
});