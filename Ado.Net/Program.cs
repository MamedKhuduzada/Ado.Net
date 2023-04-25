using Azure;
using Microsoft.Data.SqlClient;
using System.Drawing;

internal class Program
{
    const string CONNECTION_STRING = "Server=B3-1\\SQLEXPRESS;Database=BB203;Trusted_Connection=True;";
    private static void Main(string[] args)
    {
        Create(1, "Salam",DateTime.Now);
    }

    public static void CreateTable()
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "Create Table Bb203(id int primary key identity (1,1),title nvarchar(255),Createdate Date)";
            using(SqlCommand command = new SqlCommand(query,connection))
            {
               int result=  command.ExecuteNonQuery();
            }
        }
    }
    public static void Create(int id, string title, DateTime CreateDate)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
            string query = "INSERT into NewStudents (id,title,createdDate) VALUES (@id,@title,@date)";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.Parameters.AddWithValue("@title", title);
                sqlCommand.Parameters.AddWithValue("@date", CreateDate);
                sqlCommand.ExecuteNonQuery();   
            }
        }
    }
    public static void DeleteById(int id)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();

            string query = "DELETE FROM Bb203 WHERE id=@Id";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlCommand.ExecuteNonQuery();
            }
        };
    }
    public static List<Posts> GetAllPost()
    {
        using (SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING))
        {
            sqlConnection.Open();
            string query = "select * from Bb203";
            using (SqlCommand command = new SqlCommand(query, sqlConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    List<Posts> posts = new List<Posts>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            posts.Add(new Posts()
                            {
                                Id = int.Parse(reader["Id"].ToString()),
                                Title = reader["Title"].ToString(),
                            }) ;
                        }
                    }
                    return posts;
                }
            }

        }
    }
    public static int GetPostsById(int id)
    {
        using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
        {
            connection.Open();
          
            string query = "SELECT id from Bb203 where Id = @Id";
            using (SqlCommand sqlCommand = new SqlCommand(query, connection))
            {
                sqlCommand.Parameters.AddWithValue("@Id", id);
                string? name = sqlCommand.ExecuteScalar()?.ToString();
                return id;
            }
        };
    }

}

public class Posts
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime CreateDate { get; set; }
    public override string ToString()
    {
        return $"Id: {Id},Title {Title}, Time{CreateDate}";
    }
}