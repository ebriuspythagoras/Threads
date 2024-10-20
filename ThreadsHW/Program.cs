namespace ThreadsHW;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter the number of threads: ");
        int numberOfThreads = int.Parse(Console.ReadLine());

        int[] array = new int[100000];
        Random rand = new Random();
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = rand.Next(1, 1000);
        }

        string text = "This is a simple example text for counting word and character frequencies. This text will be analyzed using parallel processing.";

        var tasks = new ParallelArrayTasks(array, text);

        Task<int> minTask = tasks.FindMinAsync();
        Task<int> maxTask = tasks.FindMaxAsync();
        Task<int> sumTask = tasks.FindSumAsync();
        Task<double> avgTask = tasks.FindAverageAsync();
        Task<int[]> copyTask = tasks.CopyArrayPartAsync(0, 100);
        Task<Dictionary<char, int>> charFreqTask = tasks.BuildCharFrequencyDictionaryAsync();
        Task<Dictionary<string, int>> wordFreqTask = tasks.BuildWordFrequencyDictionaryAsync();

        await Task.WhenAll(minTask, maxTask, sumTask, avgTask, copyTask, charFreqTask, wordFreqTask);

        Console.WriteLine($"Minimum value in array: {minTask.Result}");
        Console.WriteLine($"Maximum value in array: {maxTask.Result}");
        Console.WriteLine($"Sum of array elements: {sumTask.Result}");
        Console.WriteLine($"Average of array elements: {avgTask.Result}");

        Console.WriteLine("\nPart of array (first 100 elements):");
        foreach (var item in copyTask.Result)
        {
            Console.Write(item + " ");
        }

        Console.WriteLine("\nCharacter frequency in text:");
        foreach (var entry in charFreqTask.Result)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }

        Console.WriteLine("\nWord frequency in text:");
        foreach (var entry in wordFreqTask.Result)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }

}

