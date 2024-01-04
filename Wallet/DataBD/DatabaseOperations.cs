using System.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Data.Common;

public class DatabaseOperations
{
    private readonly SqlConnection DB_connection;

    public DatabaseOperations(SqlConnection connection)
    {
        DB_connection = connection;
    }


    private static bool ServerOnline { get; set; }

    private bool ValidateConnection()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(DB_connection.ConnectionString))
            {
                conn.Open();
                // Opcionalmente, puedes acceder a ServerVersion si es necesario
               
            }
            ServerOnline = true;
        }
        catch (Exception errorException)
        {
            Console.WriteLine(errorException);
            ServerOnline = false; // Establecer ServerOnline en false si hay una excepción
        }
        return ServerOnline;
    }


    public DataSet ExecuteCommand(SqlCommand command)
    {
        if (!ValidateConnection())
        {
            throw new Exception("La conexión al servidor se perdió. Intente de nuevo");
        }

        DataSet ds = new DataSet("Resultado");
        using (SqlConnection conn = new SqlConnection(DB_connection.ConnectionString))
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            SqlCommand sqlComm = command;
            sqlComm.Connection = conn;
            sqlComm.CommandTimeout = 15000;
            SqlDataAdapter da = new SqlDataAdapter { SelectCommand = sqlComm };
            da.Fill(ds);
        }
        return ds;
    }

}
