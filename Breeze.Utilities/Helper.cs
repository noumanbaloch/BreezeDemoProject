namespace Breeze.Utilities
{
    public static class Helper
    {
        public static DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        public static bool IsNullOrEmpty<T>(T obj)
        {
            return obj is null;
        }
    }
}
