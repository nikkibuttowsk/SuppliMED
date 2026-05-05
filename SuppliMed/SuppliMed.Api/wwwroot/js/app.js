/* Core UI Logic */

let clockInterval;

// Define the missing startClock function
function startClock() {
    //  prevent memory leaks
    if (clockInterval) clearInterval(clockInterval);
    
    updateDateTime();
    
    // interval to update every second
    clockInterval = setInterval(updateDateTime, 1000);
}

function updateDateTime() {
    const now = new Date();
    const dateOptions = { day: '2-digit', month: 'long', year: 'numeric' };
    const timeOptions = { hour: '2-digit', minute: '2-digit', second: '2-digit', hour12: true };

    const formattedDate = now.toLocaleDateString('en-GB', dateOptions);
    const formattedTime = now.toLocaleTimeString('en-US', timeOptions);

    const ids = ["dashboard-date", "inventory-date", "audit-date"];
    ids.forEach(id => {
        const display = document.getElementById(id);
        if (display) display.textContent = `${formattedDate} | ${formattedTime}`;
    })
}

async function handleLogout() {
    if (confirm("Are you sure you want to end your session?")) {
        localStorage.clear();
        sessionStorage.clear();
        window.location.href = 'index.html';
    }
}

// 2. Main UI Controller
document.addEventListener('DOMContentLoaded', () => {
    const views = {
        dashboard: document.getElementById('view-dashboard'),
        inventory: document.getElementById('view-inventory'),
        audit: document.getElementById('view-audit')
    };

    function navigateTo(viewId) {
        Object.values(views).forEach(view => {
            if (view) view.classList.add('hidden');
        });

        const targetView = views[viewId];
        if (targetView) {
            targetView.classList.remove('hidden');
            // Refresh time immediately on navigation
            setTimeout(updateDateTime, 50);
        }
    }

    // click listener for nav items
    document.addEventListener('click', (e) => {
        const logoutBtn = e.target.closest('#btn-logout');
        // logout
        if (logoutBtn) {
            e.preventDefault();
            console.log("Logout triggered");
            handleLogout();
            return;
        }

        // for the nav tabs
        const navItem = e.target.closest('.nav-item');
        if (navItem) {
            const navItems = Array.from(document.querySelectorAll('.nav-item'));
            const index = navItems.indexOf(navItem);
            
            if (index === 0) navigateTo('dashboard');
            if (index === 1) navigateTo('inventory');
            if (index === 2) navigateTo('audit');
        }
    });

    startClock();
    navigateTo('dashboard'); 
});

function applyRoleRestrictions() {
    const role = localStorage.getItem('userRole');
    console.log("Applying role restrictions for role:", role);

    document.body.classList.add('is-staff');
    
    if (role === "staff") {
        // IDs of buttons only Admins should see
        const adminOnlyIds = [
            'btn-add',      // Dashboard Add
            'btn-delete',   // Dashboard Delete
            'btn-inv-add',  // Inventory Add
            'btn-inv-delete'// Inventory Delete
        ];

        adminOnlyIds.forEach(id => {
            const btn = document.getElementById(id);
            if (btn) btn.style.display = "none";
        });

    }
}