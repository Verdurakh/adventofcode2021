<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 230(23*10)".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input3.txt"));
}


private void Simulate(List<string> commands)
{
	var list2=new List<string>();
	list2.AddRange(commands);

	var res = Dig(commands, 0, true).Dump();
	var oxygen = ByteToString(res).Dump();

	var res2 = Dig(list2, 0, false).Dump();
	var scrub = ByteToString(res2).Dump();


	(oxygen*scrub).Dump();
}

private string Dig(List<string> commands, int level, bool oxy)
{
	if (commands.Count == 1)
		return commands.FirstOrDefault();


	var array = GetArray(commands);

	int zeroCount = 0;
	int oneCount = 0;
	for (int ii = 0; ii <= commands.Count - 1; ii++)
	{
		if (array[ii, level] == '0')
			zeroCount++;
		else
			oneCount++;
	}

	if (oxy)
	{
		if (oneCount >= zeroCount)
		{
			commands.RemoveAll(w => w[level] != '1');
		}
		else if (oneCount < zeroCount)
		{
			commands.RemoveAll(w => w[level] != '0');
		}
	}
	else
	{
		if (zeroCount <= oneCount )
		{
			commands.RemoveAll(w => w[level] != '0');
		}
		else 
		{
			commands.RemoveAll(w => w[level] != '1');
		}
	}

	return Dig(commands, level + 1, oxy);
}

private char[,] GetArray(List<string> commands)
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

	return array;
}

private int ByteToString(string str)
{
	int number = 0;
	int inc = 1;
	foreach (var b in str.Reverse())
	{
		if (b == '1')
			number += inc;

		inc = inc * 2;
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
