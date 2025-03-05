# How to Debug DataSet and/or DataTable
+ Debug DataSet (Dummy value to the .txt, .log, .csv file)
+ Debug DataTable (Dummy value to the .txt, .log, .csv file)
+ Case-Insensitive or Ignore Case (Không phân biệt chữ hoa chữ thường hoặc bỏ qua chữ hoa chữ thường)
+ Convert a DataTable to a string in C#
+ Print DataTable to Console in C#

```
using System;
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

            Console.WriteLine();
            Console.Write("DONE. Press any key to exit...");
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

C#: Case-Insensitive String Contains Best Practices (Oct 18, 2024)

https://www.pietschsoft.com/post/2024/10/18/csharp-case-insensitive-string-contains-best-practices

C# Contains Ignore Case: Quick guide (Feb 5, 2022)

https://josipmisko.com/posts/c-sharp-contains-ignore-case

Print DataTable to Console (and more)

https://www.codeproject.com/Tips/1147879/Print-DataTable-to-Console-and-more

Convert a DataTable to a string in C#

https://stackoverflow.com/questions/1104121/how-to-convert-a-datatable-to-a-string-in-c
