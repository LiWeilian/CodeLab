package CompressBox;

public class LZ77Parameters extends AlgorithmParameters
{
    public static final int INDEX_TTIE = 1;
    public static final int INDEX_HASH_TABLE = 2;
    public static final int INDEX_BINARY_TREE = 3;

    int theWindowSize;
    int theIndexType;

    public LZ77Parameters(int windowSize, int indexType)
    {
        theWindowSize = windowSize;
        theIndexType = indexType;
    }
}