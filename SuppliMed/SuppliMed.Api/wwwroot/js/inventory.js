document.addEventListener("DOMContentLoaded", () => {
    let selectedRow = null;

    loadInventoryTable();

    // LOAD TABLE FROM API
    async function loadInventoryTable() {
        try {
            const response = await fetch('/api/inventory');
            const supplies = await response.json();

            const tbody = document.getElementById('inventoryBody');
            if (!tbody) return;

            tbody.innerHTML = '';

            supplies.forEach(item => {
                const today = new Date();
                const expiryDate = item.expirationDate 
                    ? new Date(item.expirationDate) 
                    : null;

                let statusClass = 'green';

                if (expiryDate && expiryDate < today) {
                    statusClass = 'red';
                } else if (expiryDate && expiryDate <= new Date(today.getTime() + 30 * 24 * 60 * 60 * 1000)) {
                    statusClass = 'red';
                }
                else if (item.quantity <= (item.minimumStock || 10)) {
                    statusClass = 'yellow';
                }

                const tr = document.createElement("tr");

                tr.innerHTML = `
                    <td>${item.id}</td>
                    <td>${item.name}</td>
                    <td>${item.brand || item.serialNumber || 'N/A'}</td>
                    <td>${item.quantity} ${item.unit || ''}</td>
                    <td>${item.expirationDate || 'N/A'}</td>
                    <td><div class="v3-status ${statusClass}"></div></td>
                `;

                attachRowClick(tr);
                tbody.appendChild(tr);
            });

            updateInventoryStats();

        } catch (error) {
            console.error("Error loading inventory:", error);
        }
    }

    // ROW CLICK (SELECT)
    function attachRowClick(row) {
        row.addEventListener("click", () => {
            document.querySelectorAll("#mainInventoryTable tbody tr")
                .forEach(r => r.classList.remove("selected"));

            row.classList.add("selected");
            selectedRow = row;
        });
    }

    // UPDATE STATS (top cards)
    function updateInventoryStats() {
        const tbody = document.getElementById('inventoryBody');
        if (!tbody) return;

        const rows = tbody.getElementsByTagName('tr');

        let total = rows.length;
        let low = 0;
        let expired = 0;

        for (let row of rows) {
            if (row.querySelector('.v3-status.yellow')) low++;
            else if (row.querySelector('.v3-status.red')) expired++;
        }

        const totalEl = document.getElementById('inv-total-count');
        const lowEl = document.getElementById('inv-low-count');
        const expiredEl = document.getElementById('inv-expired-count');

        if (totalEl) totalEl.innerText = total;
        if (lowEl) lowEl.innerText = low;
        if (expiredEl) expiredEl.innerText = expired;
    }

    // OPTIONAL: SEARCH FILTER
    window.filterTable = function () {
        const input = document.getElementById("inventorySearch");
        const filter = input.value.toLowerCase();
        const rows = document.querySelectorAll("#inventoryBody tr");

        rows.forEach(row => {
            const text = row.innerText.toLowerCase();
            row.style.display = text.includes(filter) ? "" : "none";
        });
    };

    // Inventory Action Buttons
    const invButtons = {
        add: document.getElementById('btn-inv-add'),
        delete: document.getElementById('id-inv-delete'),
        update: document.getElementById('btn-inv-update')
    };

    // Attach listeners only if the elements exist in the DOM
    if (invButtons.add) {
        invButtons.add.addEventListener('click', () => {
            console.log("Opening Add Modal from Inventory");
            ModalController.open('add');
        });
    }

    if (invButtons.delete) {
        invButtons.delete.addEventListener('click', () => {
            ModalController.open('delete');
        });
    }

    if (invButtons.update) {
        invButtons.update.addEventListener('click', () => {
            ModalController.open('update');
        });
    }

    const invDeleteBtn = document.getElementById('btn-inv-delete');

if (invDeleteBtn) {
    invDeleteBtn.addEventListener('click', () => {
        console.log("DELETE BUTTON CLICKED");
        ModalController.open('delete');
    });
}
});