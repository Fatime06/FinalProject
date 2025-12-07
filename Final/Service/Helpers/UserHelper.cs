namespace Service.Helpers
{
    public static class UserHelper
    {
        private static Random _random = new Random();

        public static string GenerateCustomerNumber()
        {
            return _random.Next(1000000, 10000000).ToString();
        }
    }
}
