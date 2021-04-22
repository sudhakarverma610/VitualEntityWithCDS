﻿using Microsoft.Xrm.Sdk; 
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SampleVirtualEntity
{
    public class RetrieveMultiplePlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            EntityCollection collection = new EntityCollection();
            //change the table name below to the source table name you have created  
            string cmdString = "SELECT TicketID, Severity, Name FROM VETicket";
            SqlConnection connection = Connection.GetConnection();
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = cmdString;
                connection.Open();
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Entity e = new Entity("new_ticket");
                            e.Attributes.Add("new_ticketid", reader.GetGuid(0));
                            e.Attributes.Add("new_severity", reader.GetInt32(1));
                            e.Attributes.Add("new_name", reader.GetString(2));
                            collection.Entities.Add(e);
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
                context.OutputParameters["BusinessEntityCollection"] = collection;
            }
        }
    }
}
