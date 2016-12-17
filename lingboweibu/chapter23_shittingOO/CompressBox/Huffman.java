package CompressBox;

public class Huffman implements Algorithm
{
    public void setLevel(int level)
    {
    	System.out.println("Huffman Set Level.");
    }

    public void setParameters(AlgorithmParameters ap)
    {
    	System.out.println("Huffman Set AlgorithmParameters.");
    }

    public void Compress(byte[] in, byte[] out)
        throws CompressException
    {
    	System.out.println("Huffman Compress.");
    }

    public void Decompress(byte[] in, byte[] out)
        throws CompressException
    {
    	System.out.println("Huffman Decompress.");
    }
}