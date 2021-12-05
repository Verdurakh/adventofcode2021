<Query Kind="Program" />

void Main()
{
	var inputList = new List<int>();
	"Sample should be 150".Dump();
	Simulate(GetSampleList());

	"Real".Dump();
	Simulate(ReadMyFile(@"G:\Git\linq\adventofcode2021\input2.txt"));
}


private void Simulate(List<string> commands){
	int horPos=0;
	int depth=0;
	
	
	foreach (var command in commands)
	{
		var str= command.Split(' ');
		if(int.TryParse(str[1],out int steps))
		{
			if(str[0]=="forward"){
				horPos+=steps;
			}
			if (str[0] == "up")
			{
				depth-= steps;
			}
			if (str[0] == "down")
			{
				depth += steps;
			}
		}
	}
	
	horPos.Dump();
	depth.Dump();
	(horPos*depth).Dump();
	
	
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
	newList.Add("forward 5");
	newList.Add("down 5");
	newList.Add("forward 8");
	newList.Add("up 3");
	newList.Add("down 8");
	newList.Add("forward 2");
	return newList;
}

// Define other methods and classes here
