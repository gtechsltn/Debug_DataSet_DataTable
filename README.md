# How to Debug DataSet and/or DataTable
+ System.Data.DataTable in C#
+ Debug System.Data.DataSet (Dummy value to the .txt, .log, .csv file)
+ Debug System.Data.DataTable (Dummy value to the .txt, .log, .csv file)
+ Case-Insensitive or Ignore Case (Không phân biệt chữ hoa chữ thường hoặc bỏ qua chữ hoa chữ thường)
+ Convert a DataTable to a string in C#
+ Print DataTable to Console in C#
+ Returning DataSet as DTO class in C#
+ Mapping DataTables and DataRows to Objects in C# and .NET
+ How to debugging the DataSet and DataTable
+ Compare 2 Lists to see the Differences and Duplicates

Duplicates

https://stackoverflow.com/questions/21148175/compare-two-liststring-and-print-the-duplicates

Differences

https://stackoverflow.com/questions/61705868/how-to-find-difference-between-lists

https://stackoverflow.com/questions/35043947/comparing-two-lists-with-linq-and-retrieve-the-different-items

Compare Two Lists Via LINQ

https://stackoverflow.com/questions/38643794/compare-two-lists-via-linq

Compare two list using linq in C#

https://stackoverflow.com/questions/58482533/compare-two-list-using-linq-in-c-sharp

## How to map data in DataRow and DataTable objects to full C# classes

https://github.com/exceptionnotfound/DataNamesMappingDemo

https://github.com/gtechsltn/DataNamesMappingDemo

# ConsoleApp1: Program.cs
```
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
```

# ConsoleApp2: Program.cs
```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        /// <summary>
        /// Difference between two lists
        /// https://stackoverflow.com/questions/5636438/difference-between-two-lists
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            var sb = new StringBuilder();
            var list1 = new List<int> { 1, 2, 3, 4, 5 };
            var list2 = new List<int> { 3, 4, 5, 6, 7 };

            var list3 = list1.Except(list2); //list3 contains only 1, 2
            var list4 = list2.Except(list1); //list4 contains only 6, 7
            var differences = list3.Concat(list4).ToList(); //differences contains 1, 2, 6, 7
            var duplicates = list1.Intersect(list2).ToList(); //duplicates contains 3, 4, 5
            //====================================================================================================
            Console.WriteLine();
            Console.Write("list1 contains: ");
            sb.Length = 0;
            foreach (var item in list1)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("list2 only: ");
            sb.Length = 0;
            foreach (var item in list2)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("list3 contains only: ");
            sb.Length = 0;
            foreach (var item in list3)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("list4 contains only: ");
            sb.Length = 0;
            foreach (var item in list4)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("differences items: ");
            sb.Length = 0;
            foreach (var item in differences)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.WriteLine();
            Console.Write("duplicates items: ");
            sb.Length = 0;
            foreach (var item in duplicates)
            {
                sb.Append($",{item}");
            }
            Console.Write(sb.ToString().TrimStart(','));
            //====================================================================================================
            Console.ReadKey();
        }
    }
}
```

## C# Contains Ignore Case: Quick guide
```
public static class StringExtensions
{
    public static bool ContainsCaseInsensitive(this string source, string substring)
    {
        return source?.IndexOf(substring, StringComparison.OrdinalIgnoreCase) > -1;
    }
}
```

### Usage
```
string sentence = "Hello world";
Console.WriteLine(sentence.ContainsCaseInsensitive("hello")); // => True
Console.WriteLine(sentence.ContainsCaseInsensitive("world")); // => True
```

## C#: Case-Insensitive String Contains Best Practices
```
string title = "Hello World";
bool contains = title.Contains("hello", StringComparison.OrdinalIgnoreCase);
```

## C#: Case-Insensitive String Contains Best Practices
```
string title = "Hello World";
bool contains = title.IndexOf("hello", StringComparison.OrdinalIgnoreCase) >= 0;
```

## Compare 2 strings ignore case (c#)
```
if (!Environment.MachineName.Equals("MANH", StringComparison.OrdinalIgnoreCase))
{
    //TODO: Do smth
}
```

# References

DataTable in C#

https://www.bytehide.com/blog/datatable-csharp

Manually Create DataTable in C#

https://www.codeproject.com/Articles/30490/How-to-Manually-Create-a-Typed-DataTable

ADO.NET DataTable in C#

https://dotnettutorials.net/lesson/ado-net-datatable/

Mapping DataTables and DataRows to Objects in C# and .NET

https://exceptionnotfound.net/mapping-datatables-and-datarows-to-objects-in-csharp-and-net-using-reflection/

C# Case-Insensitive String Contains Best Practices (Oct 18, 2024)

https://www.pietschsoft.com/post/2024/10/18/csharp-case-insensitive-string-contains-best-practices

C# Contains Ignore Case: Quick guide (Feb 5, 2022)

https://josipmisko.com/posts/c-sharp-contains-ignore-case

How to Perform Case-Insensitive Substring Search in C#

https://code-maze.com/substring-in-string-case-insensitive-csharp/

Print DataTable to Console (and more)

https://www.codeproject.com/Tips/1147879/Print-DataTable-to-Console-and-more

Convert a DataTable to a string in C#

https://stackoverflow.com/questions/1104121/how-to-convert-a-datatable-to-a-string-in-c

Returning DataSet as DTO class

https://stackoverflow.com/questions/30365682/returning-dataset-as-dto-class
