namespace BlazorFastFood.Pages.Authorization
{
    public class Authorize
    {
        private static int empid { get; set; } = -999;
        private static string usertype { get; set; } = "Not";
        public static bool setempid(int id)
        {
            empid = id;
            usertype = "User";
            return true;
        }
        public static int getempid()
        {
            return(empid);
        }
        public static string getusertype()
        {
            return (usertype);
        }
        public static bool setadmin()
        {
            usertype = "Admin";
            empid = 0;
            return true;
        }
    }
}
