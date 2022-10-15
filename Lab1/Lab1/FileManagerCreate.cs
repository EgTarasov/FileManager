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
    
}