<script>
document.addEventListener("DOMContentLoaded", () => {

let mode = ""; // add, delete, update
let selectedRow = null;

// SELECT ROW
document.querySelectorAll("#mainInventoryTable tbody tr").forEach(row => {
    row.addEventListener("click", () => {
        document.querySelectorAll("#mainInventoryTable tbody tr")
            .forEach(r => r.classList.remove("selected"));

        row.classList.add("selected");
        selectedRow = row;
    });
});

// BUTTON EVENTS
document.getElementById("btn-add").onclick = () => openModal("add");
document.getElementById("btn-delete").onclick = () => openModal("delete");
document.getElementById("btn-update").onclick = () => openModal("update");

// OPEN MODAL
function openModal(action) {
    mode = action;
    document.getElementById("supplyModal").classList.remove("hidden");

    const title = document.getElementById("modalTitle");

    if (action === "add") {
        title.innerText = "Add New Supply";
        clearInputs();
    }

    if (action === "update") {
        if (!selectedRow) return alert("Select a row first");

        title.innerText = "Update Supply";

        const cells = selectedRow.children;
        document.getElementById("supplyId").value = cells[0].innerText;
        document.getElementById("genericName").value = cells[1].innerText;
        document.getElementById("brandName").value = cells[2].innerText;

        const stockParts = cells[3].innerText.split(" ");
        document.getElementById("stock").value = stockParts[0];
        document.getElementById("unit").value = stockParts[1];

        document.getElementById("expiry").value = cells[4].innerText;
    }

    if (action === "delete") {
        if (!selectedRow) return alert("Select a row first");
        title.innerText = "Delete Supply?";
    }
}

// CLOSE MODAL
function closeModal() {
    document.getElementById("supplyModal").classList.add("hidden");
}

// CLEAR INPUTS
function clearInputs() {
    document.querySelectorAll(".modal-box input").forEach(i => i.value = "");
}

// CONFIRM ACTION
document.getElementById("confirmBtn").onclick = () => {

    if (mode === "add") {
        const table = document.getElementById("inventoryBody");
        const row = document.createElement("tr");

        const id = supplyId.value;
        const name = genericName.value;
        const brand = brandName.value;
        const stockVal = stock.value + " " + unit.value;
        const expiryVal = expiry.value;

        row.innerHTML = `
            <td>${id}</td>
            <td>${name}</td>
            <td>${brand}</td>
            <td>${stockVal}</td>
            <td>${expiryVal}</td>
            <td><div class="v3-status green"></div></td>
        `;

        table.appendChild(row);
        attachRowClick(row);
    }

    if (mode === "delete") {
        selectedRow.remove();
        selectedRow = null;
    }

    if (mode === "update") {
        const cells = selectedRow.children;

        cells[0].innerText = supplyId.value;
        cells[1].innerText = genericName.value;
        cells[2].innerText = brandName.value;
        cells[3].innerText = stock.value + " " + unit.value;
        cells[4].innerText = expiry.value;
    }

    closeModal();
};

// REATTACH CLICK (for new rows)
function attachRowClick(row) {
    row.addEventListener("click", () => {
        document.querySelectorAll("#mainInventoryTable tbody tr")
            .forEach(r => r.classList.remove("selected"));

        row.classList.add("selected");
        selectedRow = row;
    });
}

function updateInventoryStats() {
    const tbody = document.getElementById('inventoryBody');
    if (!tbody) return; 

    const rows = tbody.getElementsByTagName('tr');
    
    let totalCount = rows.length; 
    let lowCount = 0;
    let expiredCount = 0;

    for (let i = 0; i < rows.length; i++) {
        if (rows[i].querySelector('.v3-status.yellow')) {
            lowCount++;
        }
        else if (rows[i].querySelector('.v3-status.red')) {
            expiredCount++;
        }
    }

    const totalEl = document.getElementById('inv-total-count');
    const lowEl = document.getElementById('inv-low-count');
    const expiredEl = document.getElementById('inv-expired-count');

    if (totalEl) totalEl.innerText = totalCount;
    if (lowEl) lowEl.innerText = lowCount;
    if (expiredEl) expiredEl.innerText = expiredCount;
}

function addNewSupply() {
    updateInventoryStats(); 
}

document.addEventListener('DOMContentLoaded', () => {
    updateInventoryStats();
});

</script>