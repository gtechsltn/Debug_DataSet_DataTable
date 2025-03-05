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

            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // The log file path
            string strMsgNativeFilePathLog = $@"{Path.GetTempPath()}\MANH.log";

            // TODO: Build DataTable HERE
            DataTable dataTable01 = new DataTable("MsgNative");

            // Step 1: Add Column to DataTable in C#
            dataTable01.Columns.Add("Id", typeof(int));
            dataTable01.Columns.Add("Name", typeof(string));

            // Step 2: Adding DataRow to DataTable in C#
            DataRow dataRow01 = dataTable01.NewRow();
            dataRow01["Id"] = 1;
            dataRow01["Name"] = "John Doe";
            dataTable01.Rows.Add(dataRow01);

            // TODO: Build DataSet HERE
            DataSet ds = new DataSet();
            ds.Tables.Add(dataTable01);

            // TODO: Debug DataTable HERE
            dataTable01 = ds.Tables["MsgNative"];

            // Debug on MANH machine only
            if (Environment.MachineName.Equals("MANH", StringComparison.OrdinalIgnoreCase))
            {
                // If DataSet has Table MsgNative and Table MsgNative has row
                if (ds.Tables.Contains("MsgNative") && dataTable01.Rows.Count > 0)
                {
                    // Using StreamWritter to append to a log file.
                    // ========================================================================================================================
                    using (streamWriter = new StreamWriter(strMsgNativeFilePathLog, true))
                    {
                        foreach (DataRow dr in dataTable01.Rows)
                        {
                            for (int i = 0; i < dataTable01.Columns.Count; i++)
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
                                if (i < dataTable01.Columns.Count - 1)
                                {
                                    streamWriter.Write(",");
                                }
                            }
                            streamWriter.Write(streamWriter.NewLine); // => WRITE TO FILE
                        }

                        // Loop through each row in the table.
                        foreach (DataRow row in dataTable01.Rows)
                        {
                            stringWriter = new StringWriter();

                            // Loop through each column.
                            foreach (DataColumn col in dataTable01.Columns)
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
                            streamWriter.WriteLine(output); // => WRITE TO FILE
                        }
                    }
                }
            }

            // Returning DataSet as DTO class
            // ========================================================================================================================

            // How to Create a DataTable using C#
            DataTable dataTable02 = new DataTable("Customers");

            // Step 1: Add Column to DataTable in C#
            dataTable02.Columns.Add("Id", typeof(int));
            dataTable02.Columns.Add("Name", typeof(string));

            // Step 2: Adding DataRow to DataTable in C#
            DataRow dataRow02 = dataTable02.NewRow();
            dataRow02["Id"] = 2;
            dataRow02["Name"] = "Nguyễn Viết Mạnh";
            dataTable02.Rows.Add(dataRow02);

            IList<DTOClass> dtos = dataTable02.AsEnumerable().Select(row =>
                                    new DTOClass
                                    {
                                        Id = row.Field<int>("Id"),
                                        Name = row.Field<string>("Name")
                                    }).ToList();

            // Print all items in a list of DTO class
            // ========================================================================================================================
            using (streamWriter = new StreamWriter(strMsgNativeFilePathLog, true))
            {
                foreach (var dto in dtos)
                {
                    Console.WriteLine($"Id: {dto.Id}, Name: {dto.Name}");
                    streamWriter.WriteLine($"Id: {dto.Id}, Name: {dto.Name}"); // => WRITE TO FILE
                }
            }

            // Print all items in a list of DTO class
            // ========================================================================================================================
            System.Diagnostics.Process.Start(@"C:\Program Files\Notepad++\notepad++.exe", strMsgNativeFilePathLog);

            // Obtains the next character or function key pressed by the user. The pressed key is displayed in the Console window.
            // ========================================================================================================================
            Console.WriteLine();
            Console.Write("DONE. Press any key to exit...");
            Console.ReadKey();
        }
    }

    public class DTOClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}