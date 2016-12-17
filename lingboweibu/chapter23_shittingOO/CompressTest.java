import java.io.*;

import CompressBox.Connection;
import CompressBox.LZ77Parameters;

public class CompressTest
{
    public static void main(String[] args)
    {
        try {
			CompressBox.Connection c = new CompressBox.Connection("Huffman");
			c.setLevel(Connection.BEST_COMPRESSION);
			c.Compress("source.dat", "dest.dat");	
			c.Decompress("source.dat", "dest.dat");	
			
			c = new CompressBox.Connection("LZ77");
			c.setLevel(Connection.BEST_SPEED);
			LZ77Parameters lz77p = new CompressBox.LZ77Parameters(123, LZ77Parameters.INDEX_BINARY_TREE);
			c.setParameters(lz77p);
			c.Compress("source.dat", "dest.dat");	
			c.Decompress("source.dat", "dest.dat");
		} catch (Throwable ex) {
			ex.printStackTrace();
		}
    }
}