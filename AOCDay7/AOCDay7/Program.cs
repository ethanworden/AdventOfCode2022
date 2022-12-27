/*
 --- Day 7: No Space Left On Device ---
You can hear birds chirping and raindrops hitting leaves as the expedition proceeds. Occasionally, you can even hear much louder sounds in the distance; how big do the animals get out here, anyway?

The device the Elves gave you has problems with more than just its communication system. You try to run a system update:

$ system-update --please --pretty-please-with-sugar-on-top
Error: No space left on device
Perhaps you can delete some files to make space for the update?

You browse around the filesystem to assess the situation and save the resulting terminal output (your puzzle input). For example:

$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k
The filesystem consists of a tree of files (plain data) and directories (which can contain other directories or files). The outermost directory is called /. You can navigate around the filesystem, moving into or out of directories and listing the contents of the directory you're currently in.

Within the terminal output, lines that begin with $ are commands you executed, very much like some modern computers:

cd means change directory. This changes which directory is the current directory, but the specific result depends on the argument:
cd x moves in one level: it looks in the current directory for the directory named x and makes it the current directory.
cd .. moves out one level: it finds the directory that contains the current directory, then makes that directory the current directory.
cd / switches the current directory to the outermost directory, /.
ls means list. It prints out all of the files and directories immediately contained by the current directory:
123 abc means that the current directory contains a file named abc with size 123.
dir xyz means that the current directory contains a directory named xyz.
Given the commands and output in the example above, you can determine that the filesystem looks visually like this:

- / (dir)
  - a (dir)
    - e (dir)
      - i (file, size=584)
    - f (file, size=29116)
    - g (file, size=2557)
    - h.lst (file, size=62596)
  - b.txt (file, size=14848514)
  - c.dat (file, size=8504156)
  - d (dir)
    - j (file, size=4060174)
    - d.log (file, size=8033020)
    - d.ext (file, size=5626152)
    - k (file, size=7214296)
Here, there are four directories: / (the outermost directory), a and d (which are in /), and e (which is in a). These directories also contain files of various sizes.

Since the disk is full, your first step should probably be to find directories that are good candidates for deletion. To do this, you need to determine the total size of each directory. The total size of a directory is the sum of the sizes of the files it contains, directly or indirectly. (Directories themselves do not count as having any intrinsic size.)

The total sizes of the directories above can be found as follows:

The total size of directory e is 584 because it contains a single file i of size 584 and no other directories.
The directory a has total size 94853 because it contains files f (size 29116), g (size 2557), and h.lst (size 62596), plus file i indirectly (a contains e which contains i).
Directory d has total size 24933642.
As the outermost directory, / contains every file. Its total size is 48381165, the sum of the size of every file.
To begin, find all of the directories with a total size of at most 100000, then calculate the sum of their total sizes. In the example above, these directories are a and e; the sum of their total sizes is 95437 (94853 + 584). (As in this example, this process can count files more than once!)

Find all of the directories with a total size of at most 100000. What is the sum of the total sizes of those directories?
*/

public class AOCDay7
{
    public static string line;
    public static Directory C;
    public static Directory current;
    public static StreamReader text;
    public static int total;
    public static int memory;
    public static List<int> list;
    public static void Main()
    {
        text = System.IO.File.OpenText(@"..\..\..\..\..\AOCDay7Input.txt");

        C = new Directory("/", C);
        current = C;
        List<string> input = ReadInput();
        total = 0;  
        list = new List<int>();
        memory = 70000000;
        Solve(C);
        Console.WriteLine($"Part 1 Answer: {total}");
        list.Sort();
        Console.WriteLine($"For Part 2, {list.Count} are possible....");
        if (list.Count != 0)
            Console.WriteLine($"\t{list[0]} would be the best option");
    }

    private static void Solve(Directory root)
    {
        if (root != null && !root.isFile)
        {
            if (root.GetSize() <= 100000)
                total += root.GetSize();

            if (memory - C.GetSize() + root.GetSize() >= 30000000)
            {
           //     Console.WriteLine($"{root.GetSize()} would be enough");
                list.Add(root.GetSize());
            }
            
            foreach (KeyValuePair<string, Directory> kvp in root.files)
            {
               //     Console.WriteLine($"{kvp.Key}, which has a size of {root.files[kvp.Key].GetSize()}");


            }
            foreach (KeyValuePair<string, Directory> kvp in root.files)
            {

                Solve(kvp.Value);


            }

        }
    }
    public static List<string> ReadInput()
    {
        List<string> input = new();
        line = text.ReadLine();
  
        while (line != null)
        {
            string[] lines = line.Split(' ');
            if (line.StartsWith("$"))
            {
                ExecuteInput(lines);

            }
            input.Add(line);
            line = text.ReadLine();
          
        }
        return input;

    }


    public static void ExecuteInput(string[] lines)
    {
        if (lines[1].Equals("cd")){
     //       Console.WriteLine($"Attempting to go to directory {lines[2]}");
            GoToDirectory(lines[2]);


        }else if (lines[1].Equals("ls"))
        {
     //       Console.WriteLine("Printing Directory");
            GetTheList();
        }
    }

    public static void GetTheList()
    {
        string line = text.ReadLine();
       // Console.WriteLine("--List starts now--");
        while (line!=null)
        {
            
           // Console.WriteLine(line);
            string[] data = line.Split(' ');
            if (line.StartsWith("$"))
            {
                ExecuteInput(data);
                break;
            }
            if (!current.files.ContainsKey(data[1]))
            {
                if (Char.IsDigit(data[0][0]))
                {

                    Directory adder = new Directory(data[1], Convert.ToInt32(data[0]), current);
                }
                else
                {
                    Directory adder = new Directory(data[1], current);
                }
            }
            line = text.ReadLine();
        }
        //Console.WriteLine("--We have reach the end of the list-- \n\n");
    }

    public static void GoToDirectory(string dir)
    {
        if (dir == "/")
        {
            current = current.GetRoot();
        }else if(dir == "..")
        {
            current = current.GetParent();
        }
        else
        {
            current= current.files[dir];
        }
    }


}

public class Directory{
    public string name;
    public Dictionary<string, Directory> files;
    private int size;
    private Directory parent;
    public bool isFile;
    private Directory root;
    public Directory(string name, Directory parent)
    {
        this.name = name;
        this.files = new();
        this.size= 0;
        this.parent = parent;
        this.isFile = false;
        if(parent != null)
        {
            parent.AddFile(this);
            this.root = parent.root;
        }
        else
        {
            this.root = this;
        }
    }
    public Directory(string name, int fileSize, Directory parent)
    {
        this.name = name;
        this.files = new();
        this.size = fileSize;
        this.parent = parent;
        this.isFile = true;
        if (parent != null)
        {
            parent.AddFile(this);
            this.root = parent.root;
        }
        else
        {
            this.root= this;
        }

    }
    public Directory GetRoot()
    {
        return this.root;
    }

    public void AddFile(Directory file)
    {
        this.files.Add(file.name,file);
        UpdateSize();
       
        
    }
    public Directory GetParent()
    {
        return this.parent;
    }

    public void SetParent(Directory parent)
    {
        this.parent = parent;
    }
    public int UpdateSize()
    {
        int current = this.size;
        foreach(Directory file in this.files.Values)
        {
            this.size+=file.GetSize();
        }
        this.size -= current;

        if (parent != null) { parent.UpdateSize(); }
        return this.size;
    }
    public int GetSize()
    {
        return this.size;
    }

}