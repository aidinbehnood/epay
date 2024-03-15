
List<int> notes = new List<int>();
List<int> amounts = new List<int>() { 50, 10, 100 };
Change(notes, amounts, 0, 0, 100);

static void Change(List<int> notes, List<int> amounts, int highest, int sum, int goal)
{

    if (sum == goal)
    {
        Display(notes, amounts);
        return;
    }
    if (sum > goal)
    {
        return;
    }
    foreach (int value in amounts)
    {
        if (value >= highest)
        {
            List<int> copy = new List<int>(notes);
            copy.Add(value);
            Change(copy, amounts, value, sum + value, goal);
        }
    }
}
static void Display(List<int> notes, List<int> amounts)
{
    foreach (int amount in amounts)
    {
        int count = notes.Count(value => value == amount);
        Console.WriteLine("{0}: {1}",
        amount,
        count);
    }
    Console.WriteLine();
}