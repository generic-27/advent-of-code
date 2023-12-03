var signalData = File.ReadAllText("input.txt").Split("\r\n\r\n");

var packetsList = new List<List<List<int>>>();

void RunPartOne()
{
    foreach (var signalInfo in signalData)
    {
        var packets = new List<List<int>>();

        var packetsArray = signalInfo.Split("\r\n");

        var packetOneArray = packetsArray[0].Substring(1, packetsArray[0].Length - 2);

        var packetTwoArray = packetsArray[1].Substring(1, packetsArray[1].Length - 2);

        for (int i = 0; i < packetOneArray.Length; i++)
        {


            if (packetOneArray[i] == '[')
            {
                int index = i;

                while (packetOneArray[index] != ']')
                {
                    index++;
                }

                var innerList = packetsArray[0].Substring(i + 2, index - i - 1)
                                                .Split(",")
                                                .Select(x => int.Parse(x))
                                                .ToList();

                packets.Add(innerList);

                i = index;
            }
        }
    }
}

RunPartOne();