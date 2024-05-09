using BlazorFastFood.DTO;

namespace BlazorFastFood.Pages.Gridview
{
    public class Gridervice
    {
       public static List<GridData> data;
        public Gridervice()
        {
            ini();
        }
        public static void ini()
        {
            data = new List<GridData>
        {
            new GridData { Id = 1, Name = "Alice", Designation = "Manager", Rating = 4 , Review="Consistently "},
            new GridData { Id = 2, Name = "Bob", Designation = "Engineer", Rating = 3 , Review="Often exceeds "},
            new GridData { Id = 3, Name = "Charlie", Designation = "Designer", Rating = 5, Review="Consistently " },
            new GridData { Id = 4, Name = "David", Designation = "Developer", Rating = 2 , Review="Needs d"},
            new GridData { Id = 5, Name = "Eva", Designation = "Architect", Rating = 4 ,Review="Consistently "},
            new GridData { Id = 6, Name = "Frank", Designation = "Tester", Rating = 3 ,Review="Often exceed"},
            new GridData { Id = 7, Name = "Grace", Designation = "Product Manager", Rating = 5 ,Review="Consistently"},
            new GridData { Id = 8, Name = "Henry", Designation = "Scrum Master", Rating = 2,Review="Needs development in meeting deadlines and organization" },
            new GridData { Id = 9, Name = "Ivy", Designation = "UX Designer", Rating = 4 , Review="Consistently "},
            new GridData { Id = 10, Name = "Jack", Designation = "Data Analyst", Rating = 3 ,Review="Consistently e"}
        };
        }
        public static List<GridData> getData()
        {
            ini();
            return (data);
        }
    }
}
