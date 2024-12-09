const string inputFile = @"../../../../input09.txt";

Console.WriteLine("Day 09 - ???");
Console.WriteLine("Star 1");
Console.WriteLine();

string line = File.ReadAllText(inputFile);

List<int> blocks = new List<int>();
List<DiskSegment> diskSemgents = new List<DiskSegment>();

bool data = true;
int id = 0;


foreach (char c in line)
{
    for (int i = 0; i < int.Parse(c.ToString()); i++)
    {
        if (data)
        {
            blocks.Add(id);
        }
        else
        {
            blocks.Add(-1);
        }
    }

    if (data)
    {
        diskSemgents.Add(new DiskSegment(id, int.Parse(c.ToString())));
        id++;
    }
    else
    {
        diskSemgents.Add(new DiskSegment(-1, int.Parse(c.ToString())));
    }

    data = !data;
}

//Reorganize
Rearrange(blocks);


int m = 0;

long checksum = 0;

while (blocks[m] >= 0)
{
    checksum += blocks[m] * m;
    m++;
}

Console.WriteLine($"The answer is: {checksum}");

Console.WriteLine();
Console.WriteLine("Star 2");
Console.WriteLine();

Rearrange2(diskSemgents);

m = 0;
checksum = 0;

for (int i = 0; i < diskSemgents.Count; i++)
{
    for (int j = 0; j < diskSemgents[i].Size; j++)
    {
        if (diskSemgents[i].IsEmpty)
        {
            m++;
        }
        else
        {
            checksum += diskSemgents[i].Id * m++;
        }
    }
}

Console.WriteLine($"The answer is: {checksum}");

Console.WriteLine();
Console.ReadKey();


void Rearrange(List<int> blocks)
{
    int lastInsertion = 0;

    for (int k = blocks.Count - 1; k >= lastInsertion; k--)
    {
        if (blocks[k] >= 0)
        {
            for (int i = lastInsertion; i <= k; i++)
            {
                if (blocks[i] == -1)
                {
                    lastInsertion = i;
                    blocks[lastInsertion] = blocks[k];
                    blocks[k] = -1;
                    break;
                }
            }
        }
    }
}

void Rearrange2(List<DiskSegment> diskSegments)
{
    for (int i = diskSegments.Max(x => x.Id); i > 0; i--)
    {
        DiskSegment segment = diskSegments.First(x => x.Id == i);
        int index = diskSegments.IndexOf(segment);


        for (int j = 0; j < index; j++)
        {
            if (diskSegments[j].Id == -1 && diskSegments[j].Size >= segment.Size)
            {
                //Replace!
                int gapSize = diskSegments[j].Size;

                diskSegments.RemoveAt(index);
                diskSegments.Insert(index, new DiskSegment(-1, segment.Size));
                diskSegments.RemoveAt(j);
                diskSegments.Insert(j, segment);

                if (gapSize > segment.Size)
                {
                    diskSegments.Insert(j + 1, new DiskSegment(-1, gapSize - segment.Size));
                }

                break;
            }
        }

        //Console.WriteLine(string.Join("", diskSegments));
    }
}

class DiskSegment
{
    public int Id { get; }
    public int Size { get; }
    public bool IsEmpty => Id == -1;

    public DiskSegment(int id, int size)
    {
        Id = id;
        Size = size;
    }

    public override string ToString() => IsEmpty ? new string('.', Size) : new string(Id.ToString()[0], Size);
}
