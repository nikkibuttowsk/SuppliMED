/* Core UI Logic */

// 1. Helper Functions (Global Scope)
let clockInterval;

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

        // 🔥 ADD THIS LINE
        setTimeout(updateDateTime, 50);
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

function updateDateTime() {
    const now = new Date();

    // 24-hour format + readable date
    const formatted = now.toLocaleString('en-GB', {
        year: 'numeric',
        month: 'long',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit',
        hour12: false
    });

    const dashboard = document.getElementById("dashboard-date");
    if (dashboard) dashboard.textContent = formatted;

    const inventory = document.getElementById("inventory-date");
    if (inventory) inventory.textContent = formatted;

    const audit = document.getElementById('audit-date');
    if (audit) audit.textContent = formatted;
}

// run immediately
updateDateTime();

// update every second
setInterval(updateDateTime, 1000);

