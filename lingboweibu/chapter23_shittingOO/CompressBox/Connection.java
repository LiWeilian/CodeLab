package CompressBox;

public class Connection
{
    public static final int NO_COMPRESSION = 0;
    public static final int BEST_SPEED = 1;
    public static final int DEFAULT_COMPRESSION = 2;
    public static final int BEST_COMPRESSION = 3;

    protected Algorithm box;

    public Connection(String AlgorithmName)
        throws CompressException
    {
        try
        {
            if (!AlgorithmName.startsWith("CompressBox."))
                AlgorithmName = "CompressBox." + AlgorithmName;
            Class c = Class.forName(AlgorithmName);
            box = (Algorithm)c.newInstance();
        }
        catch (Throwable ex)
        {
            ex.printStackTrace();
            throw new CompressException("Does not support this algorithm.");
        }
    }

    public void setLevel(int level)
    {
        box.setLevel(level);
    }

    public void setParameters(AlgorithmParameters ap)
    {
        box.setParameters(ap);
    }

    //鍘嬬缉锛屽唴瀛樺埌鍐呭瓨
    private void Compress(byte[] in, byte[] out)
        throws CompressException
    {
        box.Compress(in, out);
    }

    //瑙ｅ帇锛屽唴瀛樺埌鍐呭瓨
    private void Decompress(byte[] in, byte[] out)
        throws CompressException
    {
        box.Decompress(in, out);
    }

    //鍘嬬缉锛屾枃浠跺埌鏂囦欢
    public void Compress(String inFileName, String outFIleName)
        throws CompressException
    {
        byte[] in = null;
        byte[] out = null;

        //鐪佺暐銆傘�傘��

        Compress(in, out);

        //鐪佺暐銆傘�傘�傘��
    }

    //瑙ｅ帇锛屾枃浠跺埌鏂囦欢
    public void Decompress(String inFileName, String outFIleName)
        throws CompressException
    {
        byte[] in = null;
        byte[] out = null;

        //鐪佺暐銆傘�傘��

        Decompress(in, out);

        //鐪佺暐銆傘�傘�傘��
    }
}