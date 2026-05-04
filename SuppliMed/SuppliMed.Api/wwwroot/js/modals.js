/* Modal controller logic*/

const ModalController = {
    modal: document.getElementById('action-modal'),
    form: document.getElementById('action-form'),
    fields: document.getElementById('form-fields'),
    title: document.getElementById('modal-title'),

    init() {
        // Elements
        this.modal = document.getElementById('action-modal');
        this.form = document.getElementById('action-form');
        this.fields = document.getElementById('form-fields');
        this.title = document.getElementById('modal-title');

        // Handle Form Submission
        if (this.form) {
            this.form.onsubmit = async (e) => {
                e.preventDefault();
                await this.handleSubmit();
            };
        }

        // Close on background click
        window.onclick = (event) => {
            if (event.target == this.modal) {
                this.close();
            }
        };
    },

    open(type) {
        if (!this.modal) this.modal = document.getElementById('action-modal');
            if (!this.fields) this.fields = document.getElementById('form-fields');
            if (!this.title) this.title = document.getElementById('modal-title');

            if (!this.modal) {
                console.error("Modal element not found in the DOM!");
                return;
            }
            
        this.modal.style.display = 'flex';
        this.fields.innerHTML = ''; // Reset

        switch(type) {
            case 'add':
                this.setupAddForm();
                break;
            case 'delete':
                this.setupDeleteForm();
                break;
            case 'update':
                this.setupUpdateForm();
                break;
        }
    },

    close() {
        this.modal.style.display = 'none';
        this.form.reset();
    },

    setupAddForm() {
        this.title.innerText = "Add New Medical Supply";
        this.fields.innerHTML = `
            <div class="form-group">
                <label>Category</label>
                <select id="field-category" onchange="ModalController.toggleAddFields(this.value)">
                    <option value="Equipment">Equipment</option>
                    <option value="Medicine">Medicine</option>
                </select>
            </div>
            <div class="form-group"><p><strong>ID:</strong> Auto-generated</p></div>
            <div class="form-group"><label>Name</label><input id="field-name" required></div>
            <div class="form-group"><label>Brand</label><input id="field-brand"></div>
            <div class="form-group"><label>Min Stock</label><input type="number" id="field-min" value="10"></div>
            <div id="dynamic-extra">
                <div class="form-group"><label>Serial Number</label><input id="field-serial"></div>
                <div class="form-group"><label>Initial Quantity</label><input type="number" id="field-qty" value="1"></div>
            </div>
        `;
    },

    toggleAddFields(val) {
        const extra = document.getElementById('dynamic-extra');
        if (val === 'Medicine') {
            extra.innerHTML = `
                <div class="form-group"><label>Initial Qty (Total)</label><input type="number" id="field-qty" value="100"></div>
                <div class="form-group"><label>Batch Number</label><input id="field-batch" value="B-001"></div>
                <div class="form-group"><label>Expiry Date</label><input type="date" id="field-expiry" required></div>
            `;
        } else {
            extra.innerHTML = `
                <div class="form-group"><label>Serial Number</label><input id="field-serial"></div>
                <div class="form-group"><label>Initial Quantity</label><input type="number" id="field-qty" value="1"></div>
            `;
        }
    },

    setupDeleteForm() {
        this.title.innerText = "Delete Supply";
        this.fields.innerHTML = `
            <div class="form-group">
                <label>Target Supply ID</label>
                <input id="field-id" placeholder="e.g. MED001" required>
            </div>
            <p style="color: #ff4d4d; font-size: 11px;">Note: This will permanently remove the record.</p>
        `;
    },

    setupUpdateForm() {
        this.title.innerText = "Update Stock";
        this.fields.innerHTML = `
            <div class="form-group"><label>Supply ID</label><input id="field-id" required></div>
            <div class="form-group">
                <label>Update Type</label>
                <select id="field-update-type">
                    <option value="1">Restock (Add)</option>
                    <option value="-1">Dispense (Remove)</option>
                </select>
            </div>
            <div class="form-group"><label>Quantity</label><input type="number" id="field-qty" required></div>
        `;
    },

    async handleSubmit() {
        // 1. Identify which mode we are in
        const title = this.title.innerText.toLowerCase();
        let endpoint = "";
        let method = "POST";
        let payload = null;

        // Common selectors
        const getVal = (id) => document.getElementById(id)?.value;

        try {
            if (title.includes("add")) {
                const existingIds = Array.from(document.querySelectorAll('.id-column')).map(el => el.innerText);
                if (existingIds.includes(getVal('field-id'))) {
                    alert("This ID already exists. Please use 'Update Stock' to add a new batch.");
                    return;
                }
    
                endpoint = "/api/inventory/add";
                payload = {
                    name: getVal('field-name'),
                    brand: getVal('field-brand'),
                    minimumStock: parseInt(getVal('field-min')),
                    category: getVal('field-category'),
                    quantity: parseInt(getVal('field-qty')),
                    serialNumber: getVal('field-serial') || "",
                    batchNumber: getVal('field-batch')|| "",
                    expiryDate: getVal('field-expiry') || null
                };
                const result = await response.json();
                alert(`Supply added! ID: ${result.id}`);
            } 
            else if (title.includes("delete")) {
                const deleteId = document.getElementById('field-id').value.trim(); 
                
                if (!deleteId) {
                    alert("Please enter a valid Supply ID.");
                    return;
                }

                if (!confirm(`Are you sure you want to delete ${deleteId}?`)) return;
                
                endpoint = `/api/inventory/${deleteId}`; // This matches the [HttpDelete("{id}")]
                method = "DELETE";
                payload = null;
            } 
            else if (title.includes("update")) {
                endpoint = "/api/inventory/update-stock";
                const multiplier = parseInt(getVal('field-update-type'));
                const baseQty = parseInt(getVal('field-qty'));
                payload = {
                    id: getVal('field-id'),
                    quantity: parseInt(getVal('field-qty')),
                    batchNumber: getVal('field-batch')
                };
            }

            // 2. Send the request
            const response = await fetch(endpoint, {
                method: method,
                headers: { 'Content-Type': 'application/json' },
                body: payload ? JSON.stringify(payload) : null
            });

            // 3. Handle Result
            if (response.ok) {
                console.log("Action Successful");
                this.close(); // Close Modal
                await updateDashboard(); // Refresh UI real-time
            } else {
                const errorText = await response.text();
                console.error("Server Error:", errorText);
                alert("Delete Failed: " + errorText);
            }
        } catch (err) {
            alert("Network Error: Could not connect to server.");
            console.error(err);
        }
    }
};

function closeModal() 
{
    ModalController.close();
}

// Initialize the modal logic
document.addEventListener('DOMContentLoaded', () => ModalController.init());