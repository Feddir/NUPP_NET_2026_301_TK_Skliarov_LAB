namespace BikeShop.Common;

public abstract class Person : IEntity
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }

    // статичне поле
    public static int TotalPersons;

    // статичний конструктор
    static Person()
    {
        TotalPersons = 0;
    }

    // конструктор
    protected Person(string fullName, string phoneNumber)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        PhoneNumber = phoneNumber;
        TotalPersons++;
    }

    // метод
    public virtual string GetContactInfo()
    {
        return $"{FullName}, телефон: {PhoneNumber}";
    }

    // статичний метод
    public static int GetTotalPersons()
    {
        return TotalPersons;
    }
}