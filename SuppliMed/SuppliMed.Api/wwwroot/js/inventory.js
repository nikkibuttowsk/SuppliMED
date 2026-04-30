document.addEventListener("DOMContentLoaded", () => {
    let selectedRow = null;

    loadInventoryTable();

    // BUTTON EVENTS (make sure IDs exist in HTML)
    const addBtn = document.getElementById("btn-add");
    const deleteBtn = document.getElementById("btn-delete");
    const updateBtn = document.getElementById("btn-update");

    if (addBtn) 
        {
            addBtn.onclick = () => 
            {
                if (typeof ModalController !== 'undefined') 
                {
                    ModalController.open('add');
                } 
                else 
                {
                    console.error("ModalController not loaded");
                }
            };
        }
        
    if (deleteBtn) deleteBtn.onclick = () => deleteSelected();
    if (updateBtn) 
        {
            updateBtn.onclick = () => 
                {
                if (typeof ModalController !== 'undefined') 
                    {
                        ModalController.open('update');
                    }
                };
        }

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
                const expiryDate = new Date(item.expirationDate);

                let statusClass = 'green';

                if (expiryDate < today) {
                    statusClass = 'red';
                } else if (item.quantity <= (item.minimumStock || 10)) {
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

    // DELETE SELECTED ROW
    function deleteSelected() {
        if (!selectedRow) {
            alert("Select a row first");
            return;
        }

        selectedRow.remove();
        selectedRow = null;

        updateInventoryStats();
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
});