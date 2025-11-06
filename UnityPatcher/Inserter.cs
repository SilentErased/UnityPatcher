using System.IO;
using System.Linq;
using System.Collections.Generic;

public class FileInserter
{
    public static string InsertLineToFile(string tempPath, string testText, int tempLineNumber)
    {
        if (!File.Exists(tempPath))
        {
            return "delete your shit code right now (error: 3)";
        }
        List<string> temp = File.ReadAllLines(tempPath).ToList();
        int test = tempLineNumber - 1;
        if (test < 0 || test > temp.Count)
        {
            return $"delete your shit code right now (error: 1)";
        }

        try
        {
            temp.Insert(test, testText);
            File.WriteAllLines(tempPath, temp);
        }
        catch (IOException e)
        {
            return $"delete your shit code right now (error: 2)";
        }

        return "success";
    }
    public static string InsertMultipleLinesToFile(string tempPath, IEnumerable<string> testLines, int tempLineNumber)
    {
        if (!File.Exists(tempPath))
        {
            return "err1";
        }

        List<string> temp = File.ReadAllLines(tempPath).ToList();

        int test = tempLineNumber - 1;

        if (test < 0 || test > temp.Count)
        {
            return "err2";
        }

        try
        {
            temp.InsertRange(test, testLines);
            File.WriteAllLines(tempPath, temp);
        }
        catch (IOException)
        {
            return "err3";
        }

        return "success";
    }
}