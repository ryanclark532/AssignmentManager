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
	

	public Assignment(String title, String description, String subject, DateTime dueDate)
	{
		this.title = title;
		this.description = description;
		this.subject = subject;
		this.dueDate = dueDate;
	}
    public override string ToString()
    {
		String s = String.Format("######################\ntitle: {0}\ndescription: {1}\nsubject: {2}	duedate: {3}", this.title,this.description,this.subject,this.dueDate.ToShortDateString());
		return s;
    }
	public void save(int id)
    {
		string query = String.Format("INSERT INTO Assignment VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')", this.description,this.subject,this.dueDate.ToShortDateString(),this.title,id.ToString());
		using (SqlConnection conn = new SqlConnection(Globals.connString))
            {
				SqlCommand cmd = new SqlCommand(query, conn);
	
				conn.Open();
				int result = cmd.ExecuteNonQuery();
			    conn.Close();
			}
	}

	static public SortedDictionary<String, DateTime> getDueDates(int id)
    {
		
		SortedDictionary<String, DateTime> dates = new SortedDictionary<String, DateTime>();
		string query = String.Format("SELECT dueDate, title FROM Assignment WHERE userID={0}",id);
		using (SqlConnection conn = new SqlConnection(Globals.connString))
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


	public static List<Assignment> getAllAssignments(int id)
    {
		List<Assignment> assignments = new List<Assignment>();
		string query = String.Format("SELECT * FROM Assignment WHERE userID={0}", id);
		using (SqlConnection conn = new SqlConnection(Globals.connString))
		{
			SqlCommand cmd = new SqlCommand(query, conn);
			conn.Open();
			SqlDataReader sqlDataReader = cmd.ExecuteReader();
			while (sqlDataReader.Read())
            {
				assignments.Add(new Assignment(sqlDataReader["title"].ToString(), sqlDataReader["description"].ToString(), sqlDataReader["subject"].ToString(), DateTime.Parse(sqlDataReader["dueDate"].ToString())));
            }

		}
		return assignments;
	}
}
