namespace AirlineTicketingAPI.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>
        {
            new UserModel
            {
                Username = "admin",
                Password = "123",
                Role = "Admin"
            },

            new UserModel
            {
                Username = "staff",
                Password = "456",
                Role = "Staff"
            }
        };
    }

    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
