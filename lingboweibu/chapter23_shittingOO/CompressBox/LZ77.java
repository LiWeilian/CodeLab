package CompressBox;

public class LZ77 implements Algorithm
{
	public void setLevel(int level)
    {
    	System.out.println("LZ77 Set Level.");
    }

    public void setParameters(AlgorithmParameters ap)
    {
    	System.out.println("LZ77 Set AlgorithmParameters.");
    }

    public void Compress(byte[] in, byte[] out)
        throws CompressException
    {
    	System.out.println("LZ77 Compress.");
    }

    public void Decompress(byte[] in, byte[] out)
        throws CompressException
    {
    	System.out.println("LZ77 Decompress.");
    }
}