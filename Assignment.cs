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
	public DateOnly dueDate;
	string connection;

	public Assignment(String title, String description, String subject, DateOnly dueDate, string connection)
	{
		this.title = title;
		this.description = description;
		this.subject = subject;
		this.dueDate = dueDate;
		this.connection = connection;


	}
    public override string ToString()
    {
		String s = String.Format("######################\n{0}\n{1}\n{2}	{3}", this.title,this.description,this.subject,this.dueDate);
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
				cmd.Parameters.AddWithValue("@dueDate", this.dueDate.ToString());
				conn.Open();
				int result = cmd.ExecuteNonQuery();
			    conn.Close();
			}
	}
	static void getAllAssignments()
    {

    }
}
