var employees = new List<Employee> // zadanie 1.6
{
    new(1, "Anna", "Kowalska", new DateTime(2003, 4, 10), Position.Manager, 16500),
    new(2, "Krzysztos", "Rapacz", new DateTime(1985, 11, 3), Position.Developer, 9800),
    new(3, "Zofia", "Wiśniewska", new DateTime(2001, 7, 21), Position.Intern, 4500),
    new(4, "Piotr", "Wójcik", new DateTime(1995, 2, 18), Position.Manager, 7600),
    new(5, "Maria", "Rapacz", new DateTime(1988, 9, 30), Position.Developer, 10500),
    new(6, "Reobert", "Lewandowski", new DateTime(1999, 1, 8), Position.Tester, 6900),
    new(7, "Alicja", "Rapacz", new DateTime(2002, 12, 5), Position.Intern, 4500),
    new(8, "Piotr", "Szymański", new DateTime(1982, 6, 14), Position.Manager, 14200),
    new(9, "Maria", "Rapacz", new DateTime(1988, 9, 30), Position.Developer, 10500),
    new(10, "Reobert", "Lewandowski", new DateTime(1999, 1, 8), Position.Tester, 6900),
    new(11, "Wojciech", "Rapacz", new DateTime(2002, 12, 5), Position.Intern, 4500),
    new(12, "Krzysztos", "Rapacz", new DateTime(1985, 11, 3), Position.Developer, 15800)
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

Console.WriteLine($"\nTestowanie (zwraca null): {FindById(employees, 100)?.Salary ?? 0}"); // zadanie 4.5

try // zadanie 4.2
{
    var invalidEmployee = new Employee(9, "Invalid", "Employee", new DateTime(2004, 1, 13), Position.Developer, -1000);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"Błąd: {ex.Message}");
}


/*
---------------------------- LABORATORIUM 2 ----------------------------
*/

var highEarners = employees.Where(e => e.Salary > 10000).ToList(); // zadanie 2.1

Console.WriteLine("\nPracownicy zarabiający więcej niż 10000 zł: ");
PrintEmployees(highEarners);

var developers = employees.Where(e => e.Position == Position.Developer).ToList(); // zadanie 2.2

Console.WriteLine("\nPracownicy zatrudnieni na stanowisku Developer: ");
PrintEmployees(developers);

Console.Write("\nPodaj minimalną wartość pensji: "); // zadanie 2.3
if (decimal.TryParse(Console.ReadLine(), out var minSalary))
{
    var aboveMinSalary = employees.Where(e => e.Salary > minSalary).ToList();

    Console.WriteLine($"\nPracownicy zarabiający więcej niż {minSalary} zł: ");
    PrintEmployees(aboveMinSalary);
}
else
{
    Console.WriteLine("Niepoprawna wartość pensji.");
}

Console.Write($"\nPodaj stanowisko ({string.Join(", ", Enum.GetNames<Position>())}): "); // zadanie 2.4
if (Enum.TryParse<Position>(Console.ReadLine(), ignoreCase: true, out var position) && Enum.IsDefined(position))
{
    var onPosition = employees.Where(e => e.Position == position).ToList();

    Console.WriteLine($"\nPracownicy zatrudnieni na stanowisku {position}: ");
    PrintEmployees(onPosition);
}
else
{
    Console.WriteLine("Niepoprawne stanowisko.");
}

Console.Write("\nOryginalna lista employees po użyciu zapytania z użyciem where(): \n\n\n"); // zadanie 2.5
PrintEmployees(employees); // lista nie została zmodyfikowana, ponieważ metoda Where() zwraca nową kolekcję, a nie modyfikuje istniejącą

// --- zadanie 3 ---

var sortedByLastName = employees.OrderBy(e => e.LastName).ToList(); // zadanie 3.1

Console.WriteLine("\nPracownicy posortowani rosnąco według nazwiska: \n");
PrintEmployees(sortedByLastName);

var sortedBySalaryDesc = employees.OrderByDescending(e => e.Salary).ToList(); // zadanie 3.2

Console.WriteLine("\nPracownicy posortowani malejąco według pensji: \n");
PrintEmployees(sortedBySalaryDesc);

PrintEmployees(developers.OrderByDescending(e => e.Salary).ToList()); // zadanie 3.3

var sortedBySalaryThenLastName = employees.OrderByDescending(e => e.Salary).ThenBy(e => e.LastName).ToList(); // zadanie 3.4

Console.WriteLine("\nPracownicy posortowani malejąco według pensji, a przy równych pensjach rosnąco według nazwiska: \n");
PrintEmployees(sortedBySalaryThenLastName);



/*
---------------------------- LABORATORIUM 2 ----------------------------
*/


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
