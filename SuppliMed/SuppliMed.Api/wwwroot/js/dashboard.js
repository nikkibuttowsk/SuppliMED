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

    // 3. Action Button Logic
    // We'll use a simple alert for now to show they work!
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