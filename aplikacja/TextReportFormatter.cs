public class TextReportFormatter : IReportFormatter // zadanie 3.2
{
    public string Format(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        return $"{employee.FirstName} {employee.LastName} | {employee.Position} | {employee.Salary} zł";
    }
}
