using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string output = string.Empty;
            StringWriter stringWriter = null;
            StreamWriter streamWriter = null;

            // TODO: Build DataSet HERE
            DataSet ds = new DataSet();

            // TODO: Build DataTable HERE
            DataTable table = ds.Tables["MsgNative"];

            // Debug on MANH machine only
            if (Environment.MachineName.Equals("MANH", StringComparison.OrdinalIgnoreCase))
            {
                // The log file path
                string strMsgNativeFilePathLog = $@"C:\TMP\MANH.log";

                // If DataSet has Table MsgNative and Table MsgNative has row
                if (ds.Tables.Contains("MsgNative") && table.Rows.Count > 0)
                {
                    // Write to log file only
                    // ====================================================================================================
                    // Using StreamWritter to append to a log file.
                    using (streamWriter = new StreamWriter(strMsgNativeFilePathLog, true))
                    {
                        foreach (DataRow dr in table.Rows)
                        {
                            for (int i = 0; i < table.Columns.Count; i++)
                            {
                                if (!Convert.IsDBNull(dr[i]))
                                {
                                    string value = dr[i].ToString();
                                    if (value.Contains(','))
                                    {
                                        value = String.Format("\"{0}\"", value);
                                        streamWriter.Write(value);
                                    }
                                    else
                                    {
                                        streamWriter.Write(dr[i].ToString());
                                    }
                                }
                                if (i < table.Columns.Count - 1)
                                {
                                    streamWriter.Write(",");
                                }
                            }
                            streamWriter.Write(streamWriter.NewLine);
                        }
                    }
                    System.Diagnostics.Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", strMsgNativeFilePathLog);

                    // Write to Console Output only
                    // ====================================================================================================
                    // Loop through each row in the table.
                    foreach (DataRow row in table.Rows)
                    {
                        stringWriter = new StringWriter();

                        // Loop through each column.
                        foreach (DataColumn col in table.Columns)
                        {
                            // Output the value of each column's data.
                            stringWriter.Write(row[col].ToString() + ", ");
                        }

                        // Write to the output
                        output = stringWriter.ToString();

                        // Trim off the trailing ", ", so the output looks correct.
                        if (output.Length > 2)
                        {
                            output = output.Substring(0, output.Length - 2);
                        }

                        // Display the row in the console window.
                        Console.WriteLine(output);
                    }
                }
            }

            // Returning DataSet as DTO class
            // ====================================================================================================

            // Build DataTable in C#
            // Step 1: How to Create a DataTable using C#
            DataTable dataTable = new DataTable("Customers");

            // Step 1: Add Column to DataTable in C#
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));

            // Step 3: Populating Your DataTable in C#

            // Adding DataRow to DataTable in C#
            DataRow dataRow = table.NewRow();
            dataRow["Id"] = 1;
            dataRow["Name"] = "John Doe";
            table.Rows.Add(dataRow);

            IList<DTOClass> items = dataTable.AsEnumerable().Select(row =>
                                    new DTOClass
                                    {
                                        Id = row.Field<string>("Id"),
                                        Name = row.Field<string>("Name")
                                    }).ToList();

            foreach (var dto in items)
            {
                Console.WriteLine($"Id: {dto.Id}");
                Console.WriteLine($"Name: {dto.Name}");
            }

            Console.WriteLine();
            Console.Write("DONE. Press any key to exit...");
            Console.ReadKey();
        }
    }

    public class DTOClass
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}