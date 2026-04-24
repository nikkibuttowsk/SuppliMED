// <!DOCTYPE html>
// <html lang="en">
// <head>
//     <meta charset="UTF-8">
//     <title>SuppliMed Login</title>
//     <link rel="stylesheet" href="css/style.css">
//     </head>
// <body>

//     <div class="login-card">
//     <h1>WELCOME BACK</h1>
        
//         <div class="input-group">
//             <label>Username</label>
//             <input type="text" id="username" placeholder="Enter your username">
//         </div>

//         <div class="input-group">
//             <label>Password</label>
//             <input type="password" id="password" placeholder="Enter your password">
//         </div>

//         <div class="options-row">
//             <label><input type="checkbox"> Remember Me</label>
//             <a href="#">Forgot Password?</a>
//         </div>

//         <button onclick="handleLogin()">Login</button>
        
//         <p id="message"></p>

//         <p class="footer-text">Can't log-in? Kindly ask your system administrator</p>
//     </div>

//     <script>
//         let lockoutTimer = null; 

//         async function handleLogin() {
//             // If a timer is already running, do nothing until it's finished
//             if (lockoutTimer) return;

//             const userVal = document.getElementById('username').value;
//             const passVal = document.getElementById('password').value;
//             const msg = document.getElementById('message');
//             const loginButton = document.querySelector('button'); // Select your login button

//             // Quick validation
//             if (!userVal || !passVal) {
//                 msg.innerText = "Please enter both Username and Password.";
//                 msg.style.color = "orange";
//                 return;
//             }

//             try {
//                 const response = await fetch('/api/auth/login', {
//                     method: 'POST',
//                     headers: { 'Content-Type': 'application/json' },
//                     body: JSON.stringify({ username: userVal, password: passVal })
//                 });

//                 if (response.ok) {
//                     msg.style.color = "green";
//                     msg.innerText = "Login Successful! Redirecting...";
//                     // Optional: window.location.href = "dashboard.html";
//                 } 
//                 else if (response.status === 423) {
//                     // LOCKED OUT CASE (Handle the 1-minute freeze)
//                     const data = await response.json();
//                     startLockoutCountdown(data.secondsRemaining, msg, loginButton);
//                 } 
//                 else {
//                     // GENERIC FAILURE (Unauthorized)
//                     msg.style.color = "red";
//                     msg.innerText = "Invalid credentials. Please try again.";
//                 }
//             } catch (error) {
//                 console.error("Error:", error);
//                 msg.innerText = "A server error occurred.";
//             }
//         }

//         function startLockoutCountdown(seconds, msgElement, buttonElement) {
//             let timeLeft = seconds;

//             // Disable the button visually and functionally
//             buttonElement.disabled = true;
//             buttonElement.style.opacity = "0.5";
//             msgElement.style.color = "red";

//             // Start the interval timer
//             lockoutTimer = setInterval(() => {
//                 timeLeft--;
//                 msgElement.innerText = `Too many attempts. Please try again in ${timeLeft}s.`;

//                 // When time runs out
//                 if (timeLeft <= 0) {
//                     clearInterval(lockoutTimer); // Stop the timer
//                     lockoutTimer = null; // Reset the timer variable

//                     // Re-enable the button
//                     buttonElement.disabled = false;
//                     buttonElement.style.opacity = "1";
//                     msgElement.style.color = "green";
//                     msgElement.innerText = "System unfrozen. You may try logging in again.";
//                 }
//             }, 1000); // Run every second
//         }
//     </script>

// </body>
// </html>