class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=192.168.88.11:9089;Database=ezbiz;UID=informix;PWD=in4mix;"; //enetr your connection credentials
        string query = "SELECT * FROM ezbiz:informix.excel limit 1000;";

        RetrieveDataFromDb2(connectionString, query);
      
    }
    static void RetrieveDataFromDb2(string connectionString, string query)
    {
        try
        {
            using (DB2Connection connection = new DB2Connection(connectionString))
            {
                connection.Open();
                using (DB2Command command = new DB2Command(query, connection))
                {
                    using (DB2DataReader reader = command.ExecuteReader())
                    {
                        DisplayData(reader);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
    static void DisplayData(IDataReader reader)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            Console.Write(reader.GetName(i) + "\t");
        }
        Console.WriteLine();

        // Display rows
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader[i].ToString() + "\t");
            }
            Console.WriteLine();
        }
    }

}
