
public partial class FileManager
{
    static FileManager()
    {
        UserFolders = new List<string>();
        PrintFolders = new List<string>()
        {
            "C:\\",
            "C:\\Desktop",
            "C:\\Desktop\\1 module 2022",
            "C:\\Desktop\\1 module 2022\\C#",
            "C:\\Desktop\\1 module 2022\\C#\\Lab1",
            "C:\\Desktop\\1 module 2022\\C#\\Lab1\\Lab1",
        };
    }
    public FileManager()
    {
        _curDir = new DirectoryInfo("C:\\Desktop\\1 module 2022\\C#\\Lab1\\Lab1");
        CurrentSurroundings();
    }    
    
    private DirectoryInfo _curDir;

    private static readonly List<string> UserFolders;
    
    private static readonly List<string> PrintFolders;
    
    private const string RootFolder = "1 module 2022";

    private const string Line0 = "|___";

}