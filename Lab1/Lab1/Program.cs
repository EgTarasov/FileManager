

var u = new FileManager();
u.Action();
public partial class FileManager
{
    public void Action()
    {
        while (true)
        {
            var msg = $"{Environment.NewLine}You can do the following things:{Environment.NewLine}" +
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
                {
                    using var sw = new StreamWriter($"{_curDir.FullName}\\trace.txt");
                    DrawTree(new DirectoryInfo(@"C:\"), Line0, sw);
                    break;
                }
                case "8":
                    return;
                default:
                    Console.WriteLine("You must enter number from 0 to 9(excluded)");
                break;
            }
        }

    }
}