let lockoutTimer = null;
const eyeOpen = "M12 4.5C7.3 4.5 3.3 8.3 2 12c1.3 3.7 5.3 7.5 10 7.5s8.7-3.8 10-7.5c-1.3-3.7-5.3-7.5-10-7.5zM12 17c-2.8 0-5-2.2-5-5s2.2-5 5-5 5 2.2 5 5-2.2 5-5 5z";
const eyeClosed = "M12,7c-2.76,0-5,2.24-5,5c0,0.65,0.13,1.26,0.36,1.82l2.92-2.92C10.74,10.13,11.35,10,12,10c2.76,0,5,2.24,5,5 c0,0.65-0.13,1.26-0.36,1.82l2.92-2.92C19.87,13.26,20,12.65,20,12C20,9.24,17.76,7,12,7z M2,4.27l2.28,2.28l0.46,0.46 C3.08,8.3,1.78,10.03,1,12c1.73,4.39,6,7.5,11,7.5c1.55,0,3.03-0.3,4.38-0.84l0.42,0.42L19.73,22L21,20.73L3.27,3L2,4.27z M7.53,9.8l1.55,1.55c-0.05,0.21-0.08,0.43-0.08,0.65c0,1.66,1.34,3,3,3c0.22,0,0.44-0.03,0.65-0.08l1.55,1.55 C12.67,16.67,11.93,16.87,11.14,16.87c-2.76,0-5-2.24-5-5C6.14,11.08,6.34,10.34,6.67,9.67L7.53,9.8z";

// Password Toggle logic
document.getElementById('togglePassword').addEventListener('click', function() {
    const passInput = document.getElementById('password');
    const eyePath = document.querySelector('#eyeIcon path');
    
    if (passInput.type === 'password') {
        passInput.type = 'text';
        eyePath.setAttribute('d', eyeClosed);
    } else {
        passInput.type = 'password';
        eyePath.setAttribute('d', eyeOpen);
    }
});

async function handleLogin() {
            // If a timer is already running, do nothing until it's finished
            if (lockoutTimer) return;

            const userVal = document.getElementById('username').value.trim();
            const passVal = document.getElementById('password').value;
            const msg = document.getElementById('message');
            const loginButton = document.querySelector('button'); // Select your login button

            // Quick validation
            if (!userVal || !passVal) {
                msg.innerText = "Please enter both Username and Password.";
                msg.style.color = "orange";
                return;
            }

            try {
                const response = await fetch('/api/auth/login', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ Username: userVal, Password: passVal })
                });

                if (response.ok) {
                    msg.style.color = "green";
                    msg.innerText = "Login Successful! Redirecting...";
                    const data = await response.json();
                    localStorage.setItem('userRole', data.role); 

                    // Redirect to the new file
                    window.location.href = '/dashboard.html';
                } 
                else if (response.status === 423) {
                    // LOCKED OUT CASE (Handle the 1-minute freeze)
                    const data = await response.json();
                    startLockoutCountdown(data.secondsRemaining, msg, loginButton);
                } 
                else {
                    // GENERIC FAILURE (Unauthorized)
                    msg.style.color = "red";
                    msg.innerText = "Invalid credentials. Please try again.";
                }
            } catch (error) {
                console.error("Error:", error);
                msg.innerText = "A server error occurred.";
            }
        }

function startLockoutCountdown(seconds, msgElement, buttonElement) {
    let timeLeft = seconds;
    buttonElement.disabled = true;
    buttonElement.style.opacity = "0.5";

    lockoutTimer = setInterval(() => {
        timeLeft--;
        msgElement.style.color = "red";
        msgElement.innerText = `Too many attempts. Try again in ${timeLeft}s.`;

        if (timeLeft <= 0) {
            clearInterval(lockoutTimer);
            lockoutTimer = null;
            buttonElement.disabled = false;
            buttonElement.style.opacity = "1";
            msgElement.style.color = "green";
            msgElement.innerText = "System unfrozen. You may login.";
        }
    }, 1000);
}