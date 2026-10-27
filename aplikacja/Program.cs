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
---------------------------- LABORATORIUM 2 początek ----------------------------
*/

// --- zadanie 2 ---

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

// --- zadanie 4 ---

var fullNames = employees.Select(e => new { e.FirstName, e.LastName }).ToList(); // zadanie 4.1

Console.WriteLine("\nImiona i nazwiska wszystkich pracowników: \n");
foreach (var name in fullNames)
{
    Console.WriteLine($"{name.FirstName} {name.LastName}");
}

var fullNamesWithPosition = employees.Select(e => new { e.FirstName, e.LastName, e.Position }).ToList(); // zadanie 4.2

Console.WriteLine("\nImiona, nazwiska i stanowiska wszystkich pracowników: \n");
foreach (var name in fullNamesWithPosition)
{
    Console.WriteLine($"{name.FirstName} {name.LastName} - {name.Position}");
}

var shortDescriptions = employees.Select(e => new { e.Id, e.FirstName, e.LastName, e.Position }).ToList(); // zadanie 4.3

foreach (var desc in shortDescriptions) // zadanie 4.4
{
    Console.WriteLine($"ID: {desc.Id}\n Imię: {desc.FirstName}\n Nazwisko: {desc.LastName}\n Stanowisko: {desc.Position}");
}

// --- zadanie 5 ---

Console.Write("\nPodaj Id pracownika: "); // zadanie 5.1
if (int.TryParse(Console.ReadLine(), out var employeeId))
{
    var foundEmployee = employees.FirstOrDefault(e => e.Id == employeeId);

    if (foundEmployee is null)
    {
        Console.WriteLine($"Nie znaleziono pracownika o Id {employeeId}"); // zadanie 5.2
    }
    else
    {
        Console.WriteLine($"\nZnaleziony pracownik: \n{foundEmployee}");
    }
}
else
{
    Console.WriteLine("Niepoprawne Id pracownika");
}

var doesVeryRichEmployeeExist = employees.Any(e => e.Salary > 15000); // zadanie 5.3

Console.WriteLine(doesVeryRichEmployeeExist
    ? "\nIstnieje pracownik zarabiający więcej niż 15000 zł."
    : "\nNie istnieje pracownik zarabiający więcej niż 15000 zł.");

var isThereAnyManager = employees.Any(e => e.Position == Position.Manager); // zadanie 5.4

Console.WriteLine(isThereAnyManager
    ? "\nIstnieje manager."
    : "\nNie istnieje manager.");

Console.Write("\nPodaj Id pracownika do sprawdzenia: "); // zadanie 5.5
if (int.TryParse(Console.ReadLine(), out var idToCheck))
{
    Console.WriteLine(employees.Any(e => e.Id == idToCheck)
        ? $"Istnieje pracownik o Id {idToCheck}."
        : $"Nie istnieje pracownik o Id {idToCheck}.");
}
else
{
    Console.WriteLine("Niepoprawne Id pracownika");
}

// --- zadanie 6 ---

var richDevelopers = employees
    .Where(e => e.Position == Position.Developer && e.Salary > 10000)
    .OrderByDescending(e => e.Salary)
    .ToList(); // zadanie 6.1

Console.WriteLine("\nDeveloperzy zarabiający więcej niż 10000 zł (malejąco według pensji): \n");
PrintEmployees(richDevelopers);

var topThreeEarners = employees
    .OrderByDescending(e => e.Salary)
    .Take(3)
    .Select(e => new { e.FirstName, e.LastName })
    .ToList(); // zadanie 6.2

Console.WriteLine("\nTrzech najlepiej zarabiających pracowników: \n");
foreach (var name in topThreeEarners)
{
    Console.WriteLine($"{name.FirstName} {name.LastName}");
}

Console.Write($"\nPodaj stanowisko ({string.Join(", ", Enum.GetNames<Position>())}): "); // zadanie 6.3
if (Enum.TryParse<Position>(Console.ReadLine(), ignoreCase: true, out var position1) && Enum.IsDefined(position1))
{
    if (employees.Any(e => e.Position == position1))
    {
        var allEmployeesOnPosition = employees.Where(e => e.Position == position1).OrderBy(e => e.LastName).ToList();
        Console.WriteLine($"\nPracownicy zatrudnieni na stanowisku {position1}: ");
        PrintEmployees(allEmployeesOnPosition);
    }
}
else
{
    Console.WriteLine("Niepoprawne stanowisko.");
}

var formattedHighEarners = employees
    .Where(e => e.Salary > 10000)
    .Select(e => $"{e.FirstName} {e.LastName} | {e.Position}")
    .ToList(); // zadanie 6.4

Console.WriteLine("\nPracownicy zarabiający więcej niż 10000 zł: \n");
foreach (var line in formattedHighEarners)
{
    Console.WriteLine(line);
}

var youngestEmployees = employees
    .OrderBy(e => e.BirthDate)
    .Take(3)
    .ToList(); // zadanie 6.5

Console.WriteLine("\nTrzech najmłodszych pracowników: \n");
foreach (var employee in youngestEmployees)
    {
        Console.WriteLine(employee.FirstName + " " + employee.LastName);
        Console.WriteLine(employee.Age);
        Console.WriteLine("--------------------\n");
    }

/*
---------------------------- LABORATORIUM 3 początek ----------------------------
*/

// --- zadanie 1 ---

IEnumerable<Employee> GetEmployeesOlderThan(IEnumerable<Employee> employees, int age) // zadanie 1.1
{
    ArgumentNullException.ThrowIfNull(employees);

    return employees.Where(e => e.Age > age);
}

IEnumerable<Employee> GetEmployeesWithSalaryHigherThanAndYoungerThanOrderBySalary( // zadanie 1.2
    IEnumerable<Employee> employees, decimal salary, int age)
{
    ArgumentNullException.ThrowIfNull(employees);

    return employees
        .Where(e => e.Salary > salary && e.Age < age)
        .OrderBy(e => e.Salary);

    // query syntax zadanie 1.7
    // return from employee in employees
    //        where employee.Salary > salary && employee.Age < age
    //        orderby employee.Salary
    //        select employee;
}

IEnumerable<Employee> GetDevelopersOrEmployeesWhoseLastNameStartsWith( // zadanie 1.3
    IEnumerable<Employee> employees, string firstLetter) 
{
    ArgumentNullException.ThrowIfNull(employees);

    return employees
        .Where(e => e.Position == Position.Developer || e.LastName.StartsWith(firstLetter));
}

decimal GetSumOfSalaries(IEnumerable<Employee> employees) // zadanie 1.4
{
    ArgumentNullException.ThrowIfNull(employees);

    return employees.Sum(e => e.Salary);
}

decimal GetAverageSalaryOfDevelopers(IEnumerable<Employee> employees) // zadanie 1.5
{
    ArgumentNullException.ThrowIfNull(employees);

    return employees
        .Where(e => e.Position == Position.Developer)
        .Select(e => e.Salary)
        .DefaultIfEmpty(0m)
        .Average();
}

IEnumerable<decimal> GetFirstFourLowestDifferentSalaries(IEnumerable<Employee> employees) // zadanie 1.6
{
    ArgumentNullException.ThrowIfNull(employees);

    return employees
        .Select(e => e.Salary)
        .Distinct()
        .OrderBy(salary => salary)
        .Take(4);

    // query syntax zadanie 1.7
    // var differentSalaries =
    //     from employee in employees
    //     group employee.Salary by employee.Salary into salaries
    //     orderby salaries.Key
    //     select salaries.Key;

    // return differentSalaries.Take(4);
}

// --- zadanie 2 ---

IEnumerable<IGrouping<Position, Employee>> GroupEmployeesByPosition(IEnumerable<Employee> employees) // zadanie 2.1
{
    ArgumentNullException.ThrowIfNull(employees);

    return employees.GroupBy(employee => employee.Position);
}

void PrintPositionStatistics(IEnumerable<Employee> employees) // zadanie 2.2
{
    ArgumentNullException.ThrowIfNull(employees);

    foreach (var group in GroupEmployeesByPosition(employees)
                 .OrderByDescending(group => group.Average(employee => employee.Salary))) // zadanie 2.3
    {
        Console.WriteLine(
            $"Stanowisko: {group.Key}, liczba pracowników: {group.Count()}, " +
            $"średnia pensja: {group.Average(employee => employee.Salary)}, " +
            $"najniższa pensja: {group.Min(employee => employee.Salary)}, " +
            $"najwyższa pensja: {group.Max(employee => employee.Salary)}");
    }
}

var allEmployeesHavePositiveSalary = employees.All(employee => employee.Salary > 0); // zadanie 2.4
Console.WriteLine(allEmployeesHavePositiveSalary
    ? "\nWszyscy pracownicy mają pensję większą od 0."
    : "\nNie wszyscy pracownicy mają pensję większą od 0.");

try
{
    _ = employees.SingleOrDefault(employee => employee.LastName == "Rapacz"); // zadanie 2.5
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"\nSingleOrDefault() zgłosił wyjątek dla wielu pasujących pracowników: {ex.Message}");
}

// --- zadanie 3 ---
/*
---------------------------- LABORATORIUM 3 koniec ----------------------------
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
