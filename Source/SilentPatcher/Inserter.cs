using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityPatcher
{
    public class FileInserter
    {
        public static string InsertLineToFile(string filePath, string lineText, int lineNumber)
        {
            if (!File.Exists(filePath))
                return "error: file not found";

            List<string> lines = File.ReadAllLines(filePath).ToList();
            int insertIndex = lineNumber - 1;

            if (insertIndex < 0 || insertIndex > lines.Count)
                return "error: line number out of range";

            try
            {
                lines.Insert(insertIndex, lineText);
                File.WriteAllLines(filePath, lines);
            }
            catch (IOException ex)
            {
                return $"error: io exception - {ex.Message}";
            }

            return "success";
        }

        public static string InsertMultipleLinesToFile(string filePath, IEnumerable<string> linesToInsert, int lineNumber)
        {
            if (!File.Exists(filePath))
                return "err1";

            List<string> lines = File.ReadAllLines(filePath).ToList();
            int insertIndex = lineNumber - 1;

            if (insertIndex < 0 || insertIndex > lines.Count)
                return "err2";

            try
            {
                lines.InsertRange(insertIndex, linesToInsert);
                File.WriteAllLines(filePath, lines);
            }
            catch (IOException)
            {
                return "err3";
            }

            return "success";
        }
    }
}
