namespace AppCore.Models;

public class LoginAttempt
{
    public required string Identifier { get; set; } // Can be Username or IP address
    public int FailedCount { get; set; } = 0;
    public DateTime? LockoutEnd { get; set; } = null; // When the lockout expires

    // Check if the user is currently locked out
    public bool IsLockedOut => LockoutEnd.HasValue && LockoutEnd.Value > DateTime.UtcNow;
}