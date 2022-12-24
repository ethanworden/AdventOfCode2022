public class AOCDay2
{
    public static Dictionary<string, int> scoreTable;
    public static void Main()
    {
        StreamReader text = System.IO.File.OpenText(@"..\..\..\..\..\AOCDay2Input.txt");
        List<string> input = formatInput(text);
        populateTable1();
        Console.WriteLine($"Your score for Part 1 would be: {getScore1(input)}"); //11150
        scoreTable.Clear();
        populateTable2();
        Console.WriteLine($"Your score for Part 2 would be: {getScore2(input)}"); //8295
        



    }
    private static List<string> formatInput(StreamReader text)
    {
        List<string> list = new List<string>();
        string line = text.ReadLine();
        while (line != null)
        {
            list.Add(line);
            line = text.ReadLine();
        }
        return list;

    }
    /*
        A=65 
        B=66 
        C=67

        X=88    ->rock 1
        Y=89    ->paper 2 
        Z=90    ->scissors 3
        
        lost=0
        draw=3
        win=6

     */
    private static void populateTable1()
    {
        scoreTable = new Dictionary<string, int>();
        scoreTable.Add("A X", 1+3);
        scoreTable.Add("A Y", 2+6);
        scoreTable.Add("A Z", 3+0);
        scoreTable.Add("B X", 1+0);
        scoreTable.Add("B Y", 2+3);
        scoreTable.Add("B Z", 3+6);
        scoreTable.Add("C X", 1+6);
        scoreTable.Add("C Y", 2+0);
        scoreTable.Add("C Z", 3+3);
    }


    private static int getScore1(List<string> input)
    {
        int score = 0;
        for (int i = 0; i < input.Count; i++)
        {
            score += gameResult(input[i]);
        }

        return score;
    }

    private static void populateTable2()
    {
        scoreTable = new Dictionary<string, int>();
        scoreTable.Add("A X", 3); 
        scoreTable.Add("A Y", 4);  
        scoreTable.Add("A Z", 8);   
        scoreTable.Add("B X", 1);
        scoreTable.Add("B Y", 5);
        scoreTable.Add("B Z", 9);
        scoreTable.Add("C X", 2);
        scoreTable.Add("C Y", 6);
        scoreTable.Add("C Z", 7);
    }


    private static int getScore2(List<string> input)
    {
        int score = 0;
        for (int i = 0; i < input.Count; i++)
        {
            score += gameResult(input[i]);
        }

        return score;
    }

    private static int gameResult(string game)
    {
        return scoreTable[game];

    }
}