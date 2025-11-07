using System;
using System.ComponentModel.DataAnnotations;

namespace StaffClient.Models;

public partial class Staff
{
    [Key]
    public int StaffId { get; set; }

    [Required(ErrorMessage = "Staff name is required.")]
    public string StaffName { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number format (10-15 digits, optional +).")]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Starting date is required.")]
    public DateTime StartingDate { get; set; }

    public string? Photo { get; set; }
}