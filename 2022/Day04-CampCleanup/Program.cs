var sectionData = File.ReadAllLines("input.txt");

void RunPartOne()
{
    int totalConatainedSections = 0;

    foreach (var sectionInformation in sectionData)
    {
        var sectionData = sectionInformation.Split(',');

        var firstSectionAssignment = sectionData[0].Split('-').Select(x => int.Parse(x)).ToArray();
        var secondSectionAssignment = sectionData[1].Split('-').Select(x => int.Parse(x)).ToArray();

        
        if (firstSectionAssignment[0] >= secondSectionAssignment[0] && 
            firstSectionAssignment[0] <= secondSectionAssignment[1] &&
            firstSectionAssignment[1] >= secondSectionAssignment[0] &&
            firstSectionAssignment[1] <= secondSectionAssignment[1])
        {
            totalConatainedSections++;
        }
        else if (secondSectionAssignment[0] >= firstSectionAssignment[0] &&
                 secondSectionAssignment[0] <= firstSectionAssignment[1] &&
                 secondSectionAssignment[1] >= firstSectionAssignment[0] &&
                 secondSectionAssignment[1] <= firstSectionAssignment[1])
        {
            totalConatainedSections++;
        }
        
    }

    Console.WriteLine(totalConatainedSections);
}

void RunPartTwo()
{
    int totalOverlaps = 0;

    foreach (var sectionInformation in sectionData)
    {
        var sectionData = sectionInformation.Split(',');

        var firstSectionAssignment = sectionData[0].Split('-').Select(x => int.Parse(x)).ToArray();
        var secondSectionAssignment = sectionData[1].Split('-').Select(x => int.Parse(x)).ToArray();

        if (firstSectionAssignment[0] >= secondSectionAssignment[0] && firstSectionAssignment[0] <= secondSectionAssignment[1] ||
            firstSectionAssignment[1] >= secondSectionAssignment[1] && firstSectionAssignment[0] <= secondSectionAssignment[1])
        {
            totalOverlaps++;
        }
        else if (secondSectionAssignment[0] >= firstSectionAssignment[0] && secondSectionAssignment[0] <= firstSectionAssignment[1] ||
                 secondSectionAssignment[1] >= firstSectionAssignment[0] && secondSectionAssignment[1] <= firstSectionAssignment[1])
        {
            totalOverlaps++;
        }
    }

    Console.WriteLine(totalOverlaps);
}

RunPartOne();
RunPartTwo();