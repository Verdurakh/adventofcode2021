<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 26984457539".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input6.txt"));
}


private void Simulate(List<string> commands)
{
	int day = 0;
	var initialState = commands.FirstOrDefault();
	var dic = new Dictionary<int, long>();

	for (int i = 0; i <= 8; i++)
	{
		dic.Add(i, 0);
	}
	var split = initialState.Split(',');



	foreach (var state in split)
	{
		var con = int.Parse(state);
		if (dic.ContainsKey(con))
		{
			dic[con] = dic[con] + 1;
		}
		else
		{
			dic.Add(con, 1);
		}
	}

	for (int i = 0; i < 256; i++)
	{
		DoDay(dic, 8, 0);
	}
	
dic.Dump();
}

private void DoDay(Dictionary<int, long> dic, int stage, long toAdd)
{
		
	bool goToNextIfZero=dic[0]>0;
	if (stage == 0)
	{
		dic[6] = dic[6] + dic[0];
		dic[8] = dic[8] + dic[0];
		dic[0] = toAdd;
		
		return;
	}

	var temp = dic[stage];
	dic[stage] = dic[stage] + toAdd;
	dic[stage] = dic[stage] - temp;
	if((stage==0 && goToNextIfZero) || stage>0) 
	DoDay(dic, stage - 1, temp);
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
	newList.Add("3,4,3,1,2");

	return newList;
}

// Define other methods and classes here