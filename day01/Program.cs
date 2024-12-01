internal class Program
{
    private static async Task Main()
    {
        Console.WriteLine("Choose:");
        Console.WriteLine("1) Calculate distance");
        Console.WriteLine("2) Calculate similarity");

        string choice = Console.ReadLine() ?? "";
        switch (choice)
        {
            case "1":
                await CalculateDistance();
                break;

            case "2":
                await CalculateSimilarity(); 
                break;
        }
    }

    private static async Task CalculateDistance()
    {
        string[] entries = await GetEntries();
        int[] list1;
        int[] list2;
        int total = 0;

        list1 = [.. GetListByIndex(entries, 0).Order()];
        list2 = [.. GetListByIndex(entries, 1).Order()];

        for (int i = 0; i < entries.Length; i++)
        {
            total += Math.Abs(list1[i] - list2[i]);
        }

        Console.Write("Total distance: {0}", total);
        Console.ReadKey();
    }

    private static async Task CalculateSimilarity()
    {
        string[] entries = await GetEntries();
        int[] list1;
        int[] list2;
        int score = 0;

        list1 = GetListByIndex(entries, 0);
        list2 = GetListByIndex(entries, 1);

        for (int i = 0; i < entries.Length; i++)
        {
            var baseNumber = list1[i];
            score += baseNumber * list2.Count(x => x == baseNumber);
        }

        Console.Write("Similarity score: {0}", score);
        Console.ReadKey();
    }

    private static async Task<string[]> GetEntries()
    {
        Console.Write("Path to input file: ");
        var path = Console.ReadLine()?.Replace("\"", "") ?? "";

        try
        {
            return await File.ReadAllLinesAsync(path);
        }
        catch
        {
            Console.WriteLine("File could not be read, try again.");
            return await GetEntries();
        }
    }

    private static int[] GetListByIndex(string[] entries, int index)
    {
        return [.. entries.Select(e => int.Parse(e.Split("   ")[index]))];
    }
}