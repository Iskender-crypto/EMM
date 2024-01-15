namespace EMM.Domain.Entities;

public class User : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? Patronymic { get; set; }
    private string _fullName { get; set; }
    public string FullName
    {
        get => $"{LastName} {FirstName} {Patronymic}";
        set => _fullName = value;
    }
    private int _age { get; set; }
    public int Age
    {
        get => DateTime.Now.Year-DateOfBirth.Year;
        set => _age = value;
    }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string GroupName { get; set; }
    public string Email { get; set; }
    public string Rating { get; set; }
    public int Payment { get; set; }
    public string Status { get; set; }
}