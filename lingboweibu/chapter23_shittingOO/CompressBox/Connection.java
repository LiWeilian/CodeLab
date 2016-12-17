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

    //压缩，内存到内存
    private void Compress(byte[] in, byte[] out)
        throws CompressException
    {
        box.Compress(in, out);
    }

    //解压，内存到内存
    private void Decompress(byte[] in, byte[] out)
        throws CompressException
    {
        box.Decompress(in, out);
    }

    //压缩，文件到文件
    public void Compress(String inFileName, String outFIleName)
        throws CompressException
    {
        byte[] in = null;
        byte[] out = null;

        //省略。。。

        Compress(in, out);

        //省略。。。
    }

    //解压，文件到文件
    public void Decompress(String inFileName, String outFIleName)
        throws CompressException
    {
        byte[] in = null;
        byte[] out = null;

        //省略。。。

        Decompress(in, out);

        //省略。。。
    }
}