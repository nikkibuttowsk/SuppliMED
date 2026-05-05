using AppCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace AppCore.Services;

public static class AuthService
{
    // The "Database" of users (assuming you already refactored this part as discussed)
    private static List<User> _users = new List<User>
    {
        new Admin { UserID = "1",Username = "admin", Password = "admin123" },
        new Staff { UserID = "2", Username = "staff", Password = "staff123" }
    };

    public static List<User> GetAllUsers() => _users;
    public static User? GetUserByUsername(string? username)
    {
        if (string.IsNullOrEmpty(username)) return null;
        return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    // The "Database" of login attempts
    private static List<LoginAttempt> _attempts = new List<LoginAttempt>();

    // Constant configurations
    private const int MaxFailedAttempts = 3;
    private const int LockoutDurationMinutes = 1;

    // The result of a login attempt
    public enum LoginResultStatus { Success, Failure, LockedOut }
    public record LoginResult(LoginResultStatus Status, User? User = null, int? TimeRemainingSeconds = null);


    public static LoginResult Login(string username, string password)
    {
        // 1. Get or create the attempt tracker for this username
        var attempt = _attempts.FirstOrDefault(a => a.Identifier == username);
        if (attempt == null)
        {
            attempt = new LoginAttempt { Identifier = username };
            _attempts.Add(attempt);
        }

        // 2. CHECK: Is the user already locked out?
        if (attempt.IsLockedOut)
        {
            var timeLeft = (int)(attempt.LockoutEnd!.Value - DateTime.UtcNow).TotalSeconds;
            // Ensure we don't return negative seconds due to timing
            return new LoginResult(LoginResultStatus.LockedOut, TimeRemainingSeconds: timeLeft > 0 ? timeLeft : 0);
        }

        // 3. TRY: Check the actual credentials
        var user = _users.FirstOrDefault(u => 
            u.Username == username && u.Password == password);

        if (user != null)
        {
            // SUCCESS: Reset the counter upon successful login
            attempt.FailedCount = 0;
            attempt.LockoutEnd = null;
            return new LoginResult(LoginResultStatus.Success, User: user);
        }
        else
        {
            // FAILURE: Increment the count
            attempt.FailedCount++;

            // CHECK: Should we initiate lockout?
            if (attempt.FailedCount >= MaxFailedAttempts)
            {
                attempt.LockoutEnd = DateTime.UtcNow.AddMinutes(LockoutDurationMinutes);
                return new LoginResult(LoginResultStatus.LockedOut, TimeRemainingSeconds: LockoutDurationMinutes * 60);
            }

            return new LoginResult(LoginResultStatus.Failure);
        }
    }
}