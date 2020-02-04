using System;
using System.Data;
using System.Data.SqlClient;
namespace GenerateScriptSQL
{
    class Program
    {
        static void Main(string[] args)
        {
            string conectionString = @"Data Source=localhost;Initial Catalog=HubQueryEngine;User ID=sa;Password=admin";
            SqlConnection connection = new SqlConnection(conectionString);

            connection.Open();
            Console.WriteLine("Connection opened");

            string cmdText = "SELECT cliente FROM dbo.ListaRestrictivaCliente GROUP BY cliente";

            SqlCommand command = new SqlCommand(cmdText, connection);
            SqlDataReader reader = command.ExecuteReader();
            int count = 0;

            Console.WriteLine("Leyendo datos...");
            while (reader.Read())
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\RBDM\setear_verificado_listas.sql", true))
                {
                    file.WriteLine("UPDATE Cliente SET verificadoEnListas = 1 WHERE id = " + reader.GetValue(0));
                    count++;
                }
            }

            Console.Write("Total de iteraciones: ");
            Console.WriteLine(count);
            connection.Close();

            Console.WriteLine("Terminado, Presiona ENTER para continuar...");
            Console.ReadLine();
        }
    }
}
