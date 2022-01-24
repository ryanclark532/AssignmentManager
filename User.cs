using AssignmentManager;

using System.Data.SqlClient;

using System.Data;

public class User
{
	public int id;
	public string username;
	public string password;
	public User()
	{

	}

	public void signUp(string username, string password)
	{

		string query = String.Format("INSERT INTO Users OUTPUT Inserted.id VALUES ('{0}', '{1}')", username, Encryption.Encrypt(password));
		using (SqlConnection conn = new SqlConnection(Globals.connString))
		{
			SqlCommand cmd = new SqlCommand(query, conn);
			//cmd.Parameters.Add("username", SqlDbType.VarChar).Value = username;
			//cmd.Parameters.Add("password", SqlDbType.VarChar).Value = Encryption.Encrypt(password);

			conn.Open();
			SqlDataReader sqlDataReader = cmd.ExecuteReader();
			while (sqlDataReader.Read())
			{
				this.id = (int)sqlDataReader["id"];
				this.username = username;
				this.password = password;
			}
			conn.Close();

		}

	}
	public bool login(string username, string password)
	{
		string query = "SELECT * From Users";
		using (SqlConnection conn = new SqlConnection(Globals.connString))
		{
			SqlCommand cmd = new SqlCommand(query, conn);

			conn.Open();
			SqlDataReader sqlDataReader = cmd.ExecuteReader();
			while (sqlDataReader.Read())
			{
				if (sqlDataReader["username"].ToString() == username && Encryption.Decrypt(sqlDataReader["password"].ToString()) == password)
				{
					this.username = username;
					this.password = password;
					this.id = (int)sqlDataReader["id"];
					return true;
				}
			}
			conn.Close();

			return false;

		}
	}
}



	



