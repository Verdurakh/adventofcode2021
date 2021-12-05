<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 198".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input3.txt"));
}


private void Simulate(List<string> commands)
{

	int width = commands[0].Length;
	int height = commands.Count;

	var array = new char[height, width];

	int x = 0;
	int y = 0;
	foreach (var command in commands)
	{
		foreach (var by in command)
		{
			array[y, x] = by;
			x++;
		}
		x = 0;
		y++;
	}


	string gamma = "";
	string epsilon = "";
	x = 0;
	y = 0;
	for (int i = 0; i < width; i++)
	{
		int zeroCount = 0;
		int oneCount = 0;
		for (int ii = 0; ii <= height - 1; ii++)
		{
			if (array[ii, x] == '0')
				zeroCount++;
			else
				oneCount++;
		}
		x++;

		if (zeroCount > oneCount)
		{
			gamma += "0";
			epsilon += "1";
		}
		else
		{
			gamma += "1";
			epsilon += "0";
		}
	}


	(ByteToString(gamma)*ByteToString(epsilon)).Dump();
}

private int ByteToString(string str){
	int number=0;
	int inc=1;
	foreach (var b in str.Reverse())
	{
		if(b=='1')
		number+=inc;
		
		inc=inc*2;
	}
	
	return number;
}



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
	newList.Add("00100");
	newList.Add("11110");
	newList.Add("10110");
	newList.Add("10111");
	newList.Add("10101");
	newList.Add("01111");
	newList.Add("00111");
	newList.Add("11100");
	newList.Add("10000");
	newList.Add("11001");
	newList.Add("00010");
	newList.Add("01010");

	return newList;
}

// Define other methods and classes here
