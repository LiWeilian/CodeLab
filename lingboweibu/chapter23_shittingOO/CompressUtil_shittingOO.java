public class CompressUtil_shittingOO
{
    public static final int NO_COMPRESSION = 0;
    public static final int BEST_SPEED = 1;
    public static final int DEFAULT_COMPRESSION = 2;
    public static final int BEST_COMPRESSION = 3;

    public static final int LZ77_INDEX_TRIE = 1;
    public static final int LZ77_INDEX_TABLE = 2;
    public static final int LZ77_INDEX_BINARY_TREE = 3;

    //Huffman
    public static boolean CompressFile_Huffman(String inFileName,
        String outFileName, int blockSize)
    {return true;}

    public static boolean DecompressFile_Huffman(String inFileName,
        String outFileName, int blockSize)
    {return true;}

    public static boolean CompressFile2_Huffman(String inFileName,
        String outFileName, int level)
    {return true;}

    public static boolean DecompressFile2_Huffman(String inFileName,
        String outFileName, int level)
    {return true;}

    public static boolean CompressMemory_Huffman(byte[] in,
        byte[] out, int blockSize)
    {return true;}

    public static boolean DecompressMemory_Huffman(byte[] in,
        byte[] out, int blockSize)
    {return true;}

    public static boolean CompressMemory2_Huffman(byte[] in,
        byte[] out, int level)
    {return true;}

    public static boolean DecompressMemory2_Huffman(byte[] in,
        byte[] out, int level)
    {return true;}

    //LZ77 ==================================================
    public static boolean CompressFile_LZ77(String inFileName,
        String outFileName, int windowSize, int indexType)
    {return true;}

    public static boolean DecompressFile_LZ77(String inFileName,
        String outFileName, int windowSize, int indexType)
    {return true;}

    public static boolean CompressFile2_LZ77(String inFileName,
        String outFileName, int level)
    {return true;}

    public static boolean DecompressFile2_LZ77(String inFileName,
        String outFileName, int level)
    {return true;}

    public static boolean CompressMemory_LZ77(byte[] in,
        byte[] out, int windowSize, int indexType)
    {return true;}

    public static boolean DecompressMemory_LZ77(byte[] in,
        byte[] out, int windowSize, int indexType)
    {return true;}

    public static boolean CompressMemory2_LZ77(byte[] in,
        byte[] out, int level)
    {return true;}

    public static boolean DecompressMemory2_LZ77(byte[] in,
        byte[] out, int level)
    {return true;}

    public static String getLastError()
    {return "";}

    public static void main(String[] args)
    {
        CompressUtil_shittingOO compress_shit = new CompressUtil_shittingOO();
        compress_shit.CompressFile2_LZ77("", "", LZ77_INDEX_TRIE);
    }
}