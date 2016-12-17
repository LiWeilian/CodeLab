package CompressBox;

interface Algorithm
{
    int level = Connection.DEFAULT_COMPRESSION;

    public void setLevel(int level);
    public void setParameters(AlgorithmParameters ap);
    public void Compress(byte[] in, byte[] out)
        throws CompressException;
    public void Decompress(byte[] in, byte[] out)
        throws CompressException;
}