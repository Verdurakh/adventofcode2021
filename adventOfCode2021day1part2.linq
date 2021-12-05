<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 5".Dump();
	CountList(GetSampleList());

	"Real".Dump();
	CountList(ReadMyFile(@"G:\Git\linq\adventofcode2021\input1.txt"));
}


private void CountList(List<int> input)
{
	int inc = 0;

	for (int i = 0; i < input.Count; i++)
	{
		if(i+2>=input.Count)
		break;
		
		var sum=input[i]+input[i+1]+input[i+2];

		if (i + 3 >= input.Count)
			break;

		var sumNext = input[i+1] + input[i + 2] + input[i + 3];
		
		if(sum<sumNext)
		inc++;
	}

	inc.Dump();
}


private List<int> ReadMyFile(string uri)
{
	var newList = new List<int>();
	string line;
	System.IO.StreamReader reader = new StreamReader(uri);
	while ((line = reader.ReadLine()) != null)
	{
		if (int.TryParse(line, out int res))
		{
			newList.Add(res);
		}
		else
		{
			"was not a int".Dump();
		}
	}

	return newList;
}



private List<int> GetSampleList()
{
	var newList = new List<int>();
	newList.Add(199);
	newList.Add(200);
	newList.Add(208);
	newList.Add(210);
	newList.Add(200);
	newList.Add(207);
	newList.Add(240);
	newList.Add(269);
	newList.Add(260);
	newList.Add(263);
	return newList;
}

// Define other methods and classes here
