/* SuppliMed Core UI Logic */

// 1. Helper Functions (Global Scope)
function startClock() {
    const clockElement = document.getElementById('current-date');
    if (!clockElement) return;
    const update = () => {
        const now = new Date();
        clockElement.textContent = now.toLocaleString('en-GB');
    };
    setInterval(update, 1000);
    update();
}

async function handleLogout() {
    console.log("Attempting logout...");
    if (confirm("Are you sure you want to end your session?")) {
        localStorage.clear();
        sessionStorage.clear();
        console.log("Memory cleared. Redirecting...");
        window.location.href = 'index.html';
    }
}

// 2. Main UI Controller
document.addEventListener('DOMContentLoaded', () => {
    const views = {
        dashboard: document.getElementById('view-dashboard'),
        inventory: document.getElementById('view-inventory'),
        transactions: document.getElementById('view-transactions')
    };

    function navigateTo(viewId) {
        Object.values(views).forEach(view => {
            if (view) view.classList.add('hidden');
        });

        const targetView = views[viewId];
        if (targetView) {
            targetView.classList.remove('hidden');
            if (viewId === 'dashboard' && typeof updateDashboard === 'function') updateDashboard();
            if (viewId === 'inventory' && typeof loadInventoryTable === 'function') loadInventoryTable();
        }
    }

    // Single Click Listener for everything (Nav + Logout)
    document.addEventListener('click', (e) => {
        // Handle Logout
        if (e.target.closest('#btn-logout')) {
            e.preventDefault();
            
            // Use the AuthController we defined in auth.js
            if (typeof AuthController !== 'undefined') {
                AuthController.logout();
            } else {
                // Fallback to your local function if AuthController isn't loaded
                handleLogout();
            }
            return;
        }

        // Handle Tabs
        const navItem = e.target.closest('.nav-item');
        if (navItem) {
            const navItems = Array.from(document.querySelectorAll('.nav-item'));
            const index = navItems.indexOf(navItem);
            
            if (index === 0) navigateTo('dashboard');
            if (index === 1) navigateTo('inventory');
            if (index === 2) navigateTo('transactions');
        }
    });

    startClock();
    navigateTo('dashboard'); 
});