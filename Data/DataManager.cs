using System.Data;
using Microsoft.Data.SqlClient;

namespace BBU_SYSTEM.Data;

public static class DataManager
{ 
    public static Task<DataTable> DataTableCmdTypeSpAsync(string connectionString, string storedProcedureName, params SqlParameter[] parameters)
    {
        return DataTableAsync(connectionString, storedProcedureName, CommandType.StoredProcedure, 120, parameters);
    }

    public static Task<DataTable> DataTableRawSqlAsync(string connectionString, string sql, params SqlParameter[] parameters)
    {
        return DataTableAsync(connectionString, sql, CommandType.Text, 120, parameters);
    }
    
    private static async Task<DataTable> DataTableAsync(string connectionString, string commandText, CommandType commandType, int commandTimeout, params SqlParameter[] parameters)
    {
        if (string.IsNullOrWhiteSpace(connectionString)) 
            throw new ArgumentException("Connection string is required.", nameof(connectionString));
        if (string.IsNullOrWhiteSpace(commandText)) 
            throw new ArgumentException("Command text is required.", nameof(commandText));
        var dataTable = new DataTable();
        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(commandText, connection);
        command.CommandType = commandType;
        command.CommandTimeout = commandTimeout;
        if (parameters.Length > 0 ) 
            command.Parameters.AddRange(parameters);
        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();
        dataTable.Load(reader);
        return dataTable;
    }
}