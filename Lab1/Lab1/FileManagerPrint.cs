
public partial class FileManager
{
    private void DrawTree(DirectoryInfo dir, string line, StreamWriter sw)
    {
        var text = $"{dir.Name}{Environment.NewLine}{line}";
        Console.Write(text);
        sw.Write($"{dir.Name}{Environment.NewLine}{line}");
        if (PrintFolders.Contains(dir.FullName) is false) return;
        
        foreach (var data in dir.GetDirectories())
        {
            DrawTree(data, line + Line0, sw);
        }
    }
    
    private void CurrentSurroundings()
    {
        Console.WriteLine($"You are in {_curDir.FullName}");
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
}