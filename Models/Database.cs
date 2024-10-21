using MySql.Data.MySqlClient;
using System.Text.Json;

public class Database
{
  private Database() { }

  public static Database Instance { get; } = new Database();

  private string? connectionString;

  public void SetConnectionString(string? connectionString)
  {
    this.connectionString = connectionString;
  }


  private MySqlConnection GetOpenConnection()
  {
    if (connectionString == null)
    {
      throw new InvalidOperationException("Connection string not defined");
    }
    MySqlConnection conn = new MySqlConnection(connectionString);
    conn.Open();
    return conn;
  }

  public List<Event> GetEvents()
  {
    try
    {
      List<Event> events = new List<Event>();
      MySqlConnection conn = GetOpenConnection();

      string sql = "SELECT * FROM event ORDER BY date;";
      MySqlCommand command = new MySqlCommand(sql, conn);
      MySqlDataReader reader = command.ExecuteReader();

      
      while (reader.Read())
      {
        var calEvent = new Event{
          id = Convert.ToInt32(reader[0]),
          name = reader[1].ToString(),
          desc = reader[2].ToString(),
          date = reader[3].ToString(),
          colour = reader[4].ToString()
        };
        events.Add(calEvent);
      }

      reader.Close();
      conn.Close();

      return(events);
      
    }
    catch (Exception exception)
    {
      return ErrorList(exception.Message);
    }
  }

    public void AddEvent(Event calEvent)
  {
    try
    {
      MySqlConnection conn = GetOpenConnection();
      string sql = "INSERT INTO event (name, description, date) VALUES ('"+calEvent.name+"','"+calEvent.desc+"', '"+calEvent.date.ToString()+"');";
      Console.WriteLine(sql);
      MySqlCommand command = new MySqlCommand(sql, conn);
      command.ExecuteNonQuery();

      conn.Close();
    }
    catch (Exception exception)
    {
      Console.Error.WriteLine(exception.Message);
    }
  }

    private List<Event> ErrorList(string message)
  {
    Event calEvent = new Event();
    calEvent.name = message;
    return new List<Event>() { calEvent };
  }
   
}