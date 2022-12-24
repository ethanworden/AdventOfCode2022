public class AOCDay1
{
    public static void Main()
    {
        StreamReader text = System.IO.File.OpenText(@"..\..\..\..\..\AOCDay1Input.txt");
        List<List<string>> input = formatInput(text);
        Console.WriteLine($"Elf with most calories has {part1GetMost(input)} calories"); //Outputs 68775 for AOC input
        Console.WriteLine($"The top 3 elves have a combined total of {part2GetTopThree(input)} calories");//Outputs 202585 for AOC input

    }
    private static List<List<string>> formatInput(StreamReader text)
    {
        List<List<string>> list = new List<List<string>>();
        string line = text.ReadLine();
        List<string> adder = new List<string>();
        while (line != null)
        {
            
            if (line == "")
            {
                list.Add(adder);
                adder = new List<string>();

            }
            else
            {
                adder.Add(line);
            }
            line = text.ReadLine();
            
            
        }
        return list;

    }

    private static int part1GetMost(List<List<string>> elves)
    {
        int[] elfCals = new int[elves.Count];
        for(int i = 0; i < elfCals.Length; i++)
        {
            int calories = 0;
            for(int j = 0; j < elves[i].Count; j++)
            {
                calories+=Convert.ToInt32(elves[i][j]);
            }
            elfCals[i] = calories;
        }  
        Array.Sort(elfCals);
        return elfCals[elfCals.Length - 1];
        
    }
    private static int part2GetTopThree(List<List<string>> elves)
    {
        int[] elfCals = new int[elves.Count];
        for (int i = 0; i < elfCals.Length; i++)
        {
            int calories = 0;
            for (int j = 0; j < elves[i].Count; j++)
            {
                calories += Convert.ToInt32(elves[i][j]);
            }
            elfCals[i] = calories;
        }
        Array.Sort(elfCals);
        return elfCals[elfCals.Length - 1] + elfCals[elfCals.Length - 2] + elfCals[elfCals.Length - 3];
    }


}