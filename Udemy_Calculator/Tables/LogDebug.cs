namespace Udemy_Calculator
{
    public class LogDebug
    {
        public string DetailDate { get; set; }

        public string DetailText { get; set; }

        public int DetailCategory { get; set; }

        public override string ToString()
        {
            return $"{DetailDate}: ({(LogCategory)DetailCategory}) - {DetailText}";
        }
    }
}
