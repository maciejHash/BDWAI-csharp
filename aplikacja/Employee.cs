public enum Position // zadanie 1.2
{
    Intern = 1,
    Developer,
    Tester,
    Manager
}

public class Employee // zadanie 1.1
{
    private decimal _salary;
    // zadanie 1.3
    public Employee(int id, string firstName, string lastName, DateTime birthDate, Position position, decimal salary)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Position = position;
        Salary = salary;
    }

    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int Age // zadanie 1.4
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - BirthDate.Year;

            if (BirthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }

    public Position Position { get; set; }
    public decimal Salary // zadanie 1.3
    {
        get => _salary;
        set => _salary = value >= 0
            ? value
            : throw new ArgumentOutOfRangeException(nameof(value), "Pensja nie moze byc ujemna.");
    }

    public override string ToString() // zadanie 1.5
    {
        return $"Imię: {FirstName} \nNazwisko: {LastName}, \nwiek: {Age}, \npensja: {Salary}, \nstanowisko: {Position}";
    }
}
