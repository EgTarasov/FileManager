public partial class FileManager
{
    private void CreateFolder()
    {
        Console.WriteLine("Enter the name:");
        var name = Console.ReadLine() ?? "";
        if (name == "")
        {
            return;
        }

        try
        {
            var path = $"{_curDir.FullName}\\{name}";
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
        if (UserFolders.Count == 0)
        {
            Console.WriteLine("You cant delete any folders");
        }
        Console.WriteLine("Choose Folder from the list(you must enter the whole name)");
        var deleteFolders = new List<string>();
        
        foreach (var folder in UserFolders)
        {
            if (_curDir.FullName.Contains(folder) is false)
            {
                deleteFolders.Add(folder);
                Console.WriteLine($"\t{folder}");
            }
        }
        try
        {
            if (deleteFolders.Count == 0)
            {
                throw new Exception("Empty list");
            }
            var path = Console.ReadLine() ?? "";
            Console.WriteLine(_curDir.FullName + "\t" + path +  "\t" + path == _curDir.FullName);
            if (path == _curDir.FullName || deleteFolders.Contains(path) is false)
            {
                throw new ArgumentException("Folder is not available");
            }
            Directory.Delete(path);
            UserFolders.Remove(path);
            Console.WriteLine("Folder has been successfully deleted");
        }
        catch
        {
            Console.WriteLine("Cant delete any folder");
        }
    }
    
}