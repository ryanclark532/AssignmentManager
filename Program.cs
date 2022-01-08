
using System.Data.SqlClient;
static void Main(string[] args)
{
    String conString = "Data Source=localhost;Initial Catalog=AssignmentDB;Persist Security Info=True;User ID=SA;Password=Ryan1234";

    bool exit = false;
    while (!exit)
    {

        Console.WriteLine("1. List Assignments \n2. Add A New Assignment \n3. List Due Dates\n4. Quit\nPlease Select One Option");
        String userCollection = Console.ReadLine();
        switch (userCollection)
        {
            case "1":
                foreach (Assignment assignment in Assignment.getAllAssignments())
                {
                    Console.WriteLine(assignment);
                }


                break;
            case "2":

                Console.WriteLine("Title: ");
                String title = Console.ReadLine();

                Console.WriteLine("Description");
                String description = Console.ReadLine();

                Console.WriteLine("Subject: ");
                String subject = Console.ReadLine();

                Console.WriteLine("Due Date, in dd/MM/YY format: ");
                String date = Console.ReadLine();

                DateTime dueDate =DateTime.Parse(date);
                Console.WriteLine(String.Format("{0}/{1}/{2}", dueDate.Day, dueDate.Month, dueDate.Year));
                Assignment a = new Assignment(title, description, subject, dueDate, conString);
                a.save();
                break;

            case "3":

                foreach (var item in Assignment.getDueDates())
                {
                    Console.WriteLine(String.Format("{0}    {1}", item.Key,item.Value.ToShortDateString());
                }
               
                break;

            case "4":
                exit = true;
                break;

            default:
               continue;
        }
    }
    
    Console.WriteLine("Thank You For Using!");
}




 

Main(args);