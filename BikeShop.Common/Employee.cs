namespace BikeShop.Common;

public class Employee : Person
{
    public string Position { get; set; }
    public decimal Salary { get; set; }
    public int ExperienceYears { get; set; }

    // конструктор
    public Employee(string fullName, string phoneNumber, string position, decimal salary, int experienceYears)
        : base(fullName, phoneNumber)
    {
        Position = position;
        Salary = salary;
        ExperienceYears = experienceYears;
    }

    // метод
    public override string GetContactInfo()
    {
        return $"Працівник: {FullName}, посада: {Position}, стаж: {ExperienceYears} років";
    }
}