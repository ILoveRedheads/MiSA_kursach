namespace Tp_CourseWork.Models
{
    public class Locality
    {
        public int id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public double NumberResidantsTh { get; set; }
        public double BudgetMlrd { get; set; }

        public double SquareKm { get; set; }
        public string? Mayor { get; set; }
        public bool NumberSqaureTh { get; internal set; }
    }
}
