<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	inputList = GetSampleList();
	
	var list=ReadMyFile(@"G:\Git\linq\adventofcode2021\input1.txt");
	CountList(list);
}


private void CountList(List<int> input)
{
	int? prev = null;
	int inc=0;
	int dec=0;

	foreach (var value in input)
	{
		if (prev == null)
		{
			prev = value;
			continue;
		}

		if(value>prev)
		inc++;
		if(value<prev)
		dec++;
		
		prev = value;
	}
	
	inc.Dump();
	dec.Dump();
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
