namespace Domain.Entity
{
    public class Overtime
    {
        public string State { get; set; }
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal Hours { get; set; }
        public DateTime Date { get; set; }
    }
}