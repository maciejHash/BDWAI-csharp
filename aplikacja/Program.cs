var employees = new List<Employee> // zadanie 1.6
{
    new(1, "Anna", "Kowalska", new DateTime(2003, 4, 10), Position.Manager, 12500),
    new(2, "Krzysztos", "Rapacz", new DateTime(1985, 11, 3), Position.Developer, 9800),
    new(3, "Zofia", "Wiśniewska", new DateTime(2001, 7, 21), Position.Intern, 4200),
    new(4, "Piotr", "Wójcik", new DateTime(1995, 2, 18), Position.Manager, 7600),
    new(5, "Maria", "Rapacz", new DateTime(1988, 9, 30), Position.Developer, 10500),
    new(6, "Reobert", "Lewandowski", new DateTime(1999, 1, 8), Position.Tester, 6900),
    new(7, "Alicja", "Rapacz", new DateTime(2002, 12, 5), Position.Intern, 4500),
    new(8, "Piotr", "Szymański", new DateTime(1982, 6, 14), Position.Manager, 14200)
};

PrintEmployees(employees); // zadanie 2.1
Console.WriteLine($"Średnia pensja: {CalculateAverageSalary(employees)}"); // zadanie 2.2
Console.WriteLine($"Pensja pracownika o ID 1: {FindById(employees, 1)?.Salary ?? 0}"); // zadanie 2.3 oraz 2.4
WriteEmployeesToFile(employees); // zadanie 2.5
var textFormatter = new TextReportFormatter(); // zadanie 3.5
var csvFormatter = new CsvReportFormatter(); // zadanie 3.5

Console.WriteLine("Raport tekstowy:");
PrintReport(employees, textFormatter);

Console.WriteLine("\nRaport CSV:");
PrintReport(employees, csvFormatter);

void PrintEmployees(IEnumerable<Employee> employees) // zadanie 2.1
{
    ArgumentNullException.ThrowIfNull(employees);

    foreach (var employee in employees)
    {
        Console.WriteLine(employee);
        Console.WriteLine("--------------------\n");
    }
}

decimal CalculateAverageSalary(IEnumerable<Employee> employees) // zadanie 2.2
{
    ArgumentNullException.ThrowIfNull(employees);

    decimal totalSalary = 0;
    var employeeCount = 0;

    foreach (var employee in employees)
    {
        totalSalary += employee.Salary;
        employeeCount++;
    }

    return employeeCount == 0 ? 0 : totalSalary / employeeCount;
}

Employee? FindById(IEnumerable<Employee> employees, int id) // zadanie 2.3
{
    ArgumentNullException.ThrowIfNull(employees);

    foreach (var employee in employees)
    {
        if (employee.Id == id)
        {
            return employee;
        }
    }

    return null;
}

void WriteEmployeesToFile(IEnumerable<Employee> employees) // zadanie 2.5
{
    ArgumentNullException.ThrowIfNull(employees);

    var filePath = Path.GetFullPath( // aby zapisywalo w katalogu projektu, a nie w katalogu bin/Debug/net10.0
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "employees.txt"));
    using var writer = new StreamWriter(filePath, append: false);
    foreach (var employee in employees)
    {
        writer.WriteLine(employee);
        writer.WriteLine("--------------------");
    }

    Console.WriteLine($"Raport zapisano do: {filePath}");
}

void PrintReport(IEnumerable<Employee> employees, IReportFormatter formatter) // zadanie 3.4
{
    ArgumentNullException.ThrowIfNull(employees);
    ArgumentNullException.ThrowIfNull(formatter);

    foreach (var employee in employees)
    {
        Console.WriteLine(formatter.Format(employee));
    }
}
