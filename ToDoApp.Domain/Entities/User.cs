namespace ToDoApp.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Phone { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public DateTime? ConfirmationTokenExpiration {  get; set; }
    public string? ConfirmationToken { get; set; } = default!;
    public DateTime? ResetTokenExpiration { get; set; }
    public string? ResetToken { get; set; } = default!;
    public bool IsEmailConfirmed { get; set; }
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
    public List<Assignment> Assignments { get; set; } = default!; 
    public List<Roles> Roles{ get; set; } = default!;
}
