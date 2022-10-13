
using System.Drawing;

var u = new UI();
u.Action();
class UI
{
    private DirectoryInfo _curDir;

    private static List<string> UserFolders;
    public void Trace(){
        
    }

    private const string RootFolder = "1 module 2022";

    private async void OpenTxt()
    {
        var files = _curDir.GetFiles("*.txt");
        if (files.Length == 0)
        {
            Console.WriteLine("There are no files for reading");
            return;
        }

        var name = GetName(files as FileSystemInfo[]);
        if (name == "Cancel")
        {
            return;
        }

        using var sr = new StreamReader($"{_curDir.FullName}/{name}");
        if (sr.BaseStream.Length > 1000)
        {
            Console.WriteLine("This file is too big");
            return;
        }
        var info = await sr.ReadToEndAsync();
        // Console.BackgroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Information from the file {name}" +
                          $"{Environment.NewLine}------------------------------{Environment.NewLine}" +
                          $"{info}{Environment.NewLine}------------------------------");
        // Console.ForegroundColor = ConsoleColor.White;
    }
    
    private void MoveUp()
    {
        if (_curDir.Parent is not null && _curDir.Parent.Name != RootFolder)
        {
            _curDir = _curDir.Parent;
            CurrentSurroundings();
            return;
        }
        Console.WriteLine("Your have no access to this folder");
    }

    private string GetName(FileSystemInfo[] folder)
    {
        Console.WriteLine("Choose file/folder from the list below");
        var list = new List<string>();
        foreach (var obj in folder)
        {
            Console.WriteLine("\t" + obj.Name);
            list.Add(obj.Name);
        }
        for(int attempt = 1; attempt < 5; attempt++)
        {
            var input = Console.ReadLine() ?? "cancel";
            if (input == "cancel")
            {
                return "Cancel";
            }

            if (list.Contains(input))
            {
                return input;
            }
            Console.WriteLine($"{input} does not exist. Try again!");
        }

        return "Cancel";
    }
    
private void OpenFolder()
    {
        var name = GetName(_curDir.GetDirectories() as FileSystemInfo[]);
        if (name != "Cancel")
        {
            _curDir = new DirectoryInfo($"{_curDir.FullName}/{name}");
            CurrentSurroundings();
        }
    }
    
    private void CurrentSurroundings()
    {
        Console.WriteLine($"You are in {_curDir.Name}");
        PrintFileOrDir<DirectoryInfo>(_curDir.GetDirectories());
        PrintFileOrDir<FileInfo>(_curDir.GetFiles());
    }

    private void PrintFilesInfo(FileInfo? file)
    {
        var sizeMb = (double)file!.Length / (1024 * 1024);
        var sizeKb = (double)file!.Length / 1024;
        Console.WriteLine($"\t\t{file!.Name} {sizeKb:F2} kb");
    }
    
    private void PrintFoldersInfo(DirectoryInfo dir)
    {
        var sizeKb = 0d;
        var count = 0;
        foreach (var file in dir.GetFiles("*", searchOption: SearchOption.AllDirectories))
        {
            count++;
            sizeKb += (double)file.Length / 1024;
        }
        Console.WriteLine($"\t\t{dir.Name} {count} elements of {sizeKb:F2} kb");
    }
    private void PrintFileOrDir<T>(T[] arr) where T: FileSystemInfo
    {
        if (arr.Length <= 0) return;
        var what = (arr is FileInfo[]) ? "files" : "folders";
        Console.WriteLine($"\tThese {what} are available:");
            
        foreach (var obj in arr)
        {
            if (obj is FileInfo infoFile)
            {
                PrintFilesInfo(infoFile);
            }
            else if(obj is DirectoryInfo infFolder)
            {
                PrintFoldersInfo(infFolder);
            }
        }
    }

    private void CreateFolder()
    {
        var name = Console.ReadLine() ?? "";
        if (name == "")
        {
            return;
        }

        try
        {
            var path = $"{_curDir.FullName}/{name}";
            Directory.CreateDirectory(path);
            UserFolders.Add(path);
            Console.WriteLine("Folder has been successfully created");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Cant create folder");
        }
    }

    private void DeleteFolder()
    {
        Console.WriteLine("Choose Folder from the list(you must enter the full path as given");
        foreach (var folder in UserFolders)
        {
            Console.WriteLine($"\t{folder}");
        }
        try
        {
            var path = Console.ReadLine() ?? "";
            if (path == $"{_curDir.FullName}" || UserFolders.Contains(path) is false)
            {
                throw new ArgumentException("You are in this Folder");
            }
            Directory.Delete(path);
            UserFolders.Remove(path);
            Console.WriteLine("Folder has been successfully deleted");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Cant delete folder");
        }
    }

    public void Action()
    {
        while (true)
        {
            var msg = $"You can do the following things:{Environment.NewLine}" +
                      $"\t1.See available Data in this Directory{Environment.NewLine}" +
                      $"\t2.Go to Parent Folder{Environment.NewLine}" +
                      $"\t3.Open Folder{Environment.NewLine}" +
                      $"\t4.ReadFile{Environment.NewLine}" +
                      $"\t5.CreateFolder{Environment.NewLine}" +
                      $"\t6.DeleteFolder{Environment.NewLine}" +
                      $"\t7.ShowTrace{Environment.NewLine}" +
                      $"\t8.Exit{Environment.NewLine}";
            Console.WriteLine(msg);
            
            var op = Console.ReadLine();
            
            switch (op)
            {
                case "1":
                    CurrentSurroundings();
                    break;
                case "2":
                    this.MoveUp();
                    break;
                case "3":
                    OpenFolder();
                    break;
                case "4":
                    OpenTxt();
                    break;
                case "5":
                    CreateFolder();
                    break;
                case "6":
                    DeleteFolder();
                    break;
                case "7":
                    Trace();
                    break;
                case "8":
                    return;
            }
        }

    }

    static UI()
    {
        UserFolders = new List<string>();
    }
    public UI()
    {
        _curDir = new DirectoryInfo("C:\\Desktop\\1 module 2022\\C#\\Lab1\\Lab1");
        CurrentSurroundings();
    }    
}