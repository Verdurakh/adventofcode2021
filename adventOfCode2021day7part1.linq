<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 37".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input7.txt"));
}


private void Simulate(List<string> commands)
{
	var positions = commands.FirstOrDefault().Split(',').Select(int.Parse).ToList();
	GetCheapestPosition(positions).Dump();


}


private int GetCheapestPosition(List<int> positions) =>
	Enumerable.Range(0, positions.Max() + 1)
		.Aggregate(int.MaxValue, (lowestFuel, pos) => Math
			.Min(lowestFuel, positions
				.Where(x => x != pos)
				.Aggregate(0, (a, b) =>
				{
					var distance = Math.Abs(pos - b);
					return a + distance;
				}))
		);



private List<string> ReadMyFile(string uri)
{
	var newList = new List<string>();
	string line;
	System.IO.StreamReader reader = new StreamReader(uri);
	while ((line = reader.ReadLine()) != null)
	{
		newList.Add(line);

	}

	return newList;
}



private List<string> GetSampleList()
{
	var newList = new List<string>();
	newList.Add("16,1,2,0,4,2,7,1,2,14");

	return newList;
}

// Define other methods and classes here