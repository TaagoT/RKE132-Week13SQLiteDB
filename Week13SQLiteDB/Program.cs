using System.Data;
using System.Data.SQLite;
using System.Net.NetworkInformation;

ReadData(CreateConnetion());
//InsertCustomer(CreateConnetion());
//RemoveCustomer(CreateConnetion());
//FindCustomer(CreateConnetion());
//DisplayProduct(CreateConnetion());
//DisplayProductWithCategory(CreateConnetion())
//InsertCustomer(CreateConnetion());

static SQLiteConnection CreateConnetion()
{
    SQLiteConnection connection = new SQLiteConnection("Data Source=mydb.db; Version = 3; New = True; Compress = True;");

    try
    {
        connection.Open();
        Console.WriteLine("DB found.");
    }
    catch
    {
        Console.WriteLine("DB not found.");
    }

    return connection;
}


static void ReadData(SQLiteConnection myConnection)
{
    Console.Clear();
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();
    command.CommandText = "SELECT rowid, * FROM customer";

    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowId = reader["rowid"].ToString();
        string readerStringFirstname = reader.GetString(1);
        string readerStringLastname = reader.GetString(2);
        string readerStringDoB = reader.GetString(3);

        Console.WriteLine($"{readerRowId}. Full name: {readerStringFirstname} {readerStringLastname}; DoB: {readerStringDoB}");
    }

    myConnection.Close();
}


static void InsertCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;
    string fName, lName, DoB;

    Console.WriteLine("Enter first name:");
    fName = Console.ReadLine();
    Console.WriteLine("Enter last name:");
    lName = Console.ReadLine();
    Console.WriteLine("Enter Date of Birth (mm-dd-yyyy):");
    DoB = Console.ReadLine();

    command = myConnection.CreateCommand();
    command.CommandText = $"INSERT INTO customer(firstname, lastname, dateOfBirth) " +
    $"VALUES ('{fName}', '{lName}', '{DoB}')";

    int rowInserted = command.ExecuteNonQuery();
    Console.WriteLine($"Row inserted: {rowInserted}");


    ReadData(myConnection);
}

static void RemoveCustomer(SQLiteConnection myConnection)
{
    SQLiteCommand command;

    string idToDelete;
    Console.WriteLine("Enter and id to delete a customer:");
    idToDelete = Console.ReadLine();

    command=myConnection.CreateCommand();
    command.CommandText = $"DELETE FROM customer WHERE rowid = {idToDelete}";
    int rowRemoved = command.ExecuteNonQuery();
    Console.WriteLine($"{rowRemoved} was removed from the table customer. ");

    ReadData(myConnection);
}

static void FindCustomer(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;
    string searchName;
    Console.WriteLine("Enter a first name to display customer data:");

    searchName = Console.ReadLine();
    command = myConnection.CreateCommand();
    command.
    CommandText= $"SELECT customer.rowid, customer.firstName, customer.lastName, status.customerId " +
    $"FROM customerStatus " +
    $"JOIN customer ON customer. = customerStatus.customerId " +
    $"JOIN status ON status.rowid = customerStatus.statusId " +
    $"WHERE firstname LIKE '{searchName}'";
    reader = command.ExecuteReader();
    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerStringName = reader.GetString(1);
        string readerStringLastName = reader.GetString(2);
        string readerStringStatus = reader.GetString(3);
        Console.WriteLine($"Search result: ID: {readerRowid}. {readerStringName} {readerStringLastName}. Status: {readerStringStatus}");
    }
    myConnection.Close();
}


static void DisplayProduct(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();

    command.CommandText = "SELECT rowid, productId, id FROM product";
    reader = command.ExecuteReader();

    while (reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerProductName = reader.GetString(1);
        int readerProductPrice = reader.GetInt32(2);
        //hinna tüüp andmebaasis on int, nii et siin loeme andmebaasis ka int-tüüpi andmeid
        Console.WriteLine($"{readerRowid}. {readerProductName}. Price: {readerProductPrice}");

    }
    myConnection.Close();
}




static void DisplayProductWithCategory(SQLiteConnection myConnection)
{
    SQLiteDataReader reader;
    SQLiteCommand command;

    command = myConnection.CreateCommand();

    command.CommandText = "SELECT Product.rowid, product.categoryId, productcategory.Categoryname From Product " +
    "JOIN productCategory on ProductCategory.id = Product.CategoryId";

    reader = command.ExecuteReader();

    while(reader.Read())
    {
        string readerRowid = reader["rowid"].ToString();
        string readerProductName = reader.GetString(1);
        string readerProductCategory = reader.GetString(2);

        Console.WriteLine($"{readerRowid}. {readerProductName}. Category: {readerProductCategory}");

    }
    myConnection.Close();
}


