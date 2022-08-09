namespace IncomeManager
{
    public static class Utilities
    {
        public static int GetDayFromDateTime(string dateTime)
        {
            String[] breakApart = dateTime.Split('/');
            return int.Parse(breakApart[1]);
        }
    }
}
