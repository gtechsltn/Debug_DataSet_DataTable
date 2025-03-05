# How to Debug DataSet and/or DataTable

```
if (Environment.MachineName.Equals("MANH", StringComparison.OrdinalIgnoreCase))
{
    string strMsgNativeFilePathLog = $@"C:\TMP\MANH.log";
    using (StreamWriter sw = new StreamWriter(strMsgNativeFilePathLog, true))
    {
        foreach (DataRow dr in ds.Tables["MsgNative"].Rows)
        {
            for (int i = 0; i < ds.Tables["MsgNative"].Columns.Count; i++)
            {
                if (!Convert.IsDBNull(dr[i]))
                {
                    string value = dr[i].ToString();
                    if (value.Contains(','))
                    {
                        value = String.Format("\"{0}\"", value);
                        sw.Write(value);
                    }
                    else
                    {
                        sw.Write(dr[i].ToString());
                    }
                }
                if (i < ds.Tables["MsgNative"].Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
        }
    }
    System.Diagnostics.Process.Start(AppConstants.TextEditorProgram, strMsgNativeFilePathLog);
}
```

```

if (!Environment.MachineName.Equals("MANH", StringComparison.OrdinalIgnoreCase))
{
    DeleteOneGZandAllBinFilesFromFileSystem(iMailID_Directory);
}
```
