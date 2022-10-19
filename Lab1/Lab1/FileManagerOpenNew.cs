public partial class FileManager
{
    
    private void MoveUp()
    {
        if (_curDir.Parent is not null && _curDir.Parent.Name != RootFolder)
        {
            if (PrintFolders.Contains(_curDir.FullName))
            {
                PrintFolders.Remove(_curDir.FullName);
            }
            _curDir = _curDir.Parent;
            CurrentSurroundings();
            return;
        }
        Console.WriteLine("Your have no access to this folder");
    }
    
    private void OpenFolder()
    {
        var name = GetName(_curDir.GetDirectories() as FileSystemInfo[]);
        if (name == "Cancel") return;
        
        _curDir = new DirectoryInfo($"{_curDir.FullName}/{name}");
        PrintFolders.Add(_curDir.FullName);
        CurrentSurroundings();
    }

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
        
        //await using var sw = new StreamWriter($"{_curDir.FullName}/InfoFrom{name}");
        Console.WriteLine($"Information from the file {name}"
                + $"{Environment.NewLine}------------------------------{Environment.NewLine}"
                + $"{info}{Environment.NewLine}------------------------------");
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

        for (int attempt = 1; attempt < 5; attempt++)
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
    
}