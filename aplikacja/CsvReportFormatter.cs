public class CsvReportFormatter : IReportFormatter // zadanie 3.3
{
    public string Format(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        return string.Join(",",
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Position,
            employee.Salary);
    }
}
