using System;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
using System.Data.SqlClient;
public class Assignment
{
	public String title;
	String description;
	String subject;
	public DateTime dueDate;
	string connection;

	public Assignment(String title, String description, String subject, DateTime dueDate, string connection)
	{
		this.title = title;
		this.description = description;
		this.subject = subject;
		this.dueDate = dueDate;
		this.connection = connection;


	}
    public override string ToString()
    {
		String s = String.Format("######################\n{0}\n{1}\n{2}	{3}", this.title,this.description,this.subject,this.dueDate.ToShortDateString());
		return s;
    }
	public void save()
    {
		string query = "INSERT INTO Assignment VALUES ('@description', '@subject', '@dueDate', '@title')";
		using (SqlConnection conn = new SqlConnection(connection))
            {
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@title", this.title);
				cmd.Parameters.AddWithValue("@description", this.description);
				cmd.Parameters.AddWithValue("@subject", this.subject);
				cmd.Parameters.AddWithValue("@dueDate", this.dueDate.ToShortDateString());
				conn.Open();
				int result = cmd.ExecuteNonQuery();
			    conn.Close();
			}
	}

	static public SortedDictionary<String, DateTime> getDueDates()
    {
		
		SortedDictionary<String, DateTime> dates = new SortedDictionary<String, DateTime>();
		String conString = "Data Source=localhost;Initial Catalog=AssignmentDB;Persist Security Info=True;User ID=SA;Password=Ryan1234";
		string query = "SELECT dueDate, title FROM Assignment";
		using (SqlConnection conn = new SqlConnection(conString))
		{
			SqlCommand cmd = new SqlCommand(query, conn);
			conn.Open();
			SqlDataReader sqlDataReader = cmd.ExecuteReader();
			while (sqlDataReader.Read())
			{
				dates.Add(sqlDataReader["title"].ToString(), DateTime.Parse(sqlDataReader["dueDate"].ToString()));
			}

		}
		return dates;
	}


	public static List<Assignment> getAllAssignments()
    {
		List<Assignment> assignments = new List<Assignment>();
		String conString = "Data Source=localhost;Initial Catalog=AssignmentDB;Persist Security Info=True;User ID=SA;Password=Ryan1234";
		string query = "SELECT * FROM Assignment";
		using (SqlConnection conn = new SqlConnection(conString))
		{
			SqlCommand cmd = new SqlCommand(query, conn);
			conn.Open();
			SqlDataReader sqlDataReader = cmd.ExecuteReader();
			while (sqlDataReader.Read())
            {
				assignments.Add(new Assignment(sqlDataReader["title"].ToString(), sqlDataReader["description"].ToString(), sqlDataReader["subject"].ToString(), DateTime.Parse(sqlDataReader["dueDate"].ToString()), conString));
            }

		}
		return assignments;
	}
}
