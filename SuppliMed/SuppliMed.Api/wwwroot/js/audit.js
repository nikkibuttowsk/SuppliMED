document.addEventListener('DOMContentLoaded', () => {
    // 1. Get the buttons
    const btnDashboard = document.getElementById('btn-nav-dashboard');
    const btnInventory = document.getElementById('btn-nav-inventory');
    const btnAudit = document.getElementById('btn-nav-audit'); // UPDATED HERE

    // 2. Get the views
    const viewDashboard = document.getElementById('view-dashboard');
    const viewInventory = document.getElementById('view-inventory');
    const viewAudit = document.getElementById('view-audit'); // UPDATED HERE

    // 3. Function to switch screens
    function switchPage(activeButton, activeView) {
        // Hide all views
        if(viewDashboard) viewDashboard.classList.add('hidden');
        if(viewInventory) viewInventory.classList.add('hidden');
        if(viewAudit) viewAudit.classList.add('hidden'); // UPDATED HERE

        // Remove active class from all buttons
        if(btnDashboard) btnDashboard.classList.remove('active');
        if(btnInventory) btnInventory.classList.remove('active');
        if(btnAudit) btnAudit.classList.remove('active'); // UPDATED HERE

        // Show selected
        if(activeView) activeView.classList.remove('hidden');
        if(activeButton) activeButton.classList.add('active');
    }

    // 4. Add Click Listeners
    if(btnDashboard) btnDashboard.addEventListener('click', () => switchPage(btnDashboard, viewDashboard));
    if(btnInventory) btnInventory.addEventListener('click', () => switchPage(btnInventory, viewInventory));
    if(btnAudit) btnAudit.addEventListener('click', () => switchPage(btnAudit, viewAudit));

});

document.addEventListener('DOMContentLoaded', () => {
    // Only handle data loading here
    loadAuditLogs();
    setInterval(loadAuditLogs, 30000);
});

async function loadAuditLogs() {
    try {
        const response = await fetch('/api/inventory/audit-logs');
        if (!response.ok) throw new Error("Backend unreachable");
        
        const logs = await response.json();
        const tbody = document.getElementById('auditBody');
        if (!tbody) return;

        tbody.innerHTML = '';

        logs.forEach(log => {
            const tr = document.createElement("tr");

            const shortId = log.logId.length > 8 ? log.logId.substring(0, 8) : log.logId;
            
            let actionStyle = "";
            if (log.action === "ADD") actionStyle = "color: #4ade80; font-weight: bold;";
            if (log.action === "RESTOCK") actionStyle = "color: #82a727; font-weight: bold;";
            if (log.action === "DISPENSE") actionStyle = "color: #b16b20; font-weight: bold;";
            if (log.action === "DELETE") actionStyle = "color: #f87171; font-weight: bold;";

            tr.innerHTML = `
                <td style="font-family: monospace; color: #9ca3af;" title="${log.logId}">
                    #${shortId}
                </td>
                <td>${new Date(log.dateTime).toLocaleString()}</td>
                <td>${log.user}</td>
                <td style="${actionStyle}">${log.action}</td>
                <td>${log.item}</td>
                <td style="color: #9ca3af; font-size: 0.85rem;">${log.details}</td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        console.error("Audit Sync Error:", error);
    }
}

function filterAuditTable() {
    // 1. Get the value the user typed in the search box
    let input = document.getElementById("auditSearch");
    let filter = input.value.toLowerCase();
    
    // 2. Get the table and all its rows
    let tbody = document.getElementById("auditBody");
    let tr = tbody.getElementsByTagName("tr");

    // 3. Loop through all table rows, and hide those who don't match the search query
    for (let i = 0; i < tr.length; i++) {
        // Gets all the text inside the current row
        let rowText = tr[i].textContent || tr[i].innerText;
        
        // If the row text contains the search term, show it. Otherwise, hide it.
        if (rowText.toLowerCase().indexOf(filter) > -1) {
            tr[i].style.display = "";
        } else {
            tr[i].style.display = "none";
        }
    }
}