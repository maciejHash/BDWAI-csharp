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
    private Position _position;

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

            if (BirthDate.Date > today.Date.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }

    // zadanie 4.4 - błąd w samym int age = DateTime.Today.Year - employee.BirthDate.Year; 
    // jest taki że nie uwzględnia miesiąca i dnia
    public Position Position
    {
        get => _position;
        set
        {
            if (value == Position.Intern && _salary > 10000) // zadanie 4.3
            {
                throw new ArgumentException(
                    "Pracownik na stanowisku Intern nie może zarabiać więcej niż 10000 zl.",
                    nameof(value));
            }

            _position = value;
        }
    }

    public decimal Salary // zadanie 1.3
    {
        get => _salary;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Pensja nie moze byc ujemna.");
            }

            if (_position == Position.Intern && value > 10000)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(value),
                    "Pracownik na stanowisku Intern nie może zarabiać więcej niż 10000 zl.");
            }

            _salary = value;
        }
    }

    public override string ToString() // zadanie 1.5
    {
        return $"Imię: {FirstName} \nNazwisko: {LastName}, \nwiek: {Age}, \npensja: {Salary}, \nstanowisko: {Position}";
    }
}
