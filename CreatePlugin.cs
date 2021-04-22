using Microsoft.Xrm.Sdk;
 using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SampleVirtualEntity
{
    public class CreatePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                Entity entity = (Entity)context.InputParameters["Target"];
                Guid id = Guid.NewGuid();
                //change the table name below to the source table name you have created 
                string cmdString = "INSERT INTO VETicket (TicketID,Name,Severity) VALUES (@TicketID, @Name, @Severity)";
                SqlConnection connection = Connection.GetConnection();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = cmdString;
                    command.Parameters.AddWithValue("@TicketID", id);
                    command.Parameters.AddWithValue("@Name", entity["new_name"]);
                    command.Parameters.AddWithValue("@Severity", entity["new_severity"]);
                    connection.Open();
                    try
                    {
                        var numRecords = command.ExecuteNonQuery();
                        Console.WriteLine("inserted {0} records", numRecords);
                    }
                    finally
                    {
                        connection.Close();
                    }
                    // other codes. 
                }
                context.OutputParameters["id"] = id;
            }
        }
    }
}
