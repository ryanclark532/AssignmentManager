
using System.Data.SqlClient;
static void Main(string[] args)
{
    User u = new User();
    Console.WriteLine("1. Login\n2. Sign Up");
    String login = Console.ReadLine();
    bool auth = false;
    while (!auth)
    {
        switch (login)
        {
            case "1":
                Console.WriteLine("Enter your username");
                string loguinUser = Console.ReadLine();
                Console.WriteLine("Enter your password");
                string loginPass = Console.ReadLine();
                if (u.login(loguinUser, loginPass)==true)
                {
                    Console.WriteLine(String.Format("Welcome {0}", u.username));
                    auth = true;
                }
                else
                {
                    continue;
                }

                break;
            case "2":
                Console.WriteLine("Enter your username");
                string user = Console.ReadLine();
                Console.WriteLine("Enter your password");
                string pass = Console.ReadLine();
                u.signUp(user, pass);
                Console.WriteLine(String.Format("Welcome {0}",u.username));
                auth = true;
                break;
        }
    }
    
    bool exit = false;
    while (!exit)
    {
        


        Console.WriteLine("1. List Assignments \n2. Add A New Assignment \n3. List Due Dates\n4. Quit\nPlease Select One Option");
        String main = Console.ReadLine();
        switch (main)
        {
            
            case "1":
                foreach (Assignment assignment in Assignment.getAllAssignments(u.id))
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
                Assignment a = new Assignment(title, description, subject, dueDate);
                a.save(u.id);
                break;

            case "3":

                foreach (var item in Assignment.getDueDates(u.id))
                {
                    Console.WriteLine(String.Format("{0}    {1}", item.Key,item.Value.ToShortDateString()));
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