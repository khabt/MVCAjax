using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteFile
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryReadWriter();
            Console.WriteLine("___________");
            DirectoryAndFile();
            Console.WriteLine("___________");
            StreamFiles();
            Console.WriteLine("___________");
            StreamReadWrite();


            Console.ReadKey();
        }

        static void BinaryReadWriter()
        {
            string strA = "ABCD";
            byte byteA = 12;
            double doubA = 11.13;

            using (BinaryWriter bw = new BinaryWriter(new FileStream("testBinaryWriter.dat", FileMode.Create, FileAccess.ReadWrite)))
            {
                try
                {
                    bw.Write(strA);
                    bw.Write(byteA);
                    bw.Write(doubA);
                }
                catch (IOException ioe)
                {
                    Console.Write(ioe.Message);
                    Console.ReadKey();
                }
            }

            using (BinaryReader br = new BinaryReader(new FileStream("testBinaryWriter.dat", FileMode.Open, FileAccess.Read)))
            {
                Console.WriteLine(br.ReadString());
                Console.WriteLine(br.ReadByte());
                Console.WriteLine(br.ReadDouble());
            }

            Console.WriteLine("ok");
            //Console.ReadKey();
        }

        static void DirectoryAndFile()
        {
            string filePath = @"D:\KBT";
            DirectoryInfo myDir = new DirectoryInfo(filePath);

            if (myDir.Exists)
            {
                DirectoryInfo[] files = myDir.GetDirectories();
                foreach (DirectoryInfo file in files)
                {
                    Console.WriteLine(file.FullName);
                }

                Console.WriteLine(files.Length);
            }
            else { Console.WriteLine("Directory is not existed"); }

            //Console.ReadKey();
        }

        static void StreamFiles()
        {
            using (FileStream fileStream = new FileStream(@"testFileStream.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                for (int i = 1; i <= 20; i++)
                {
                    fileStream.WriteByte((byte)i);
                }

                Console.WriteLine("Write ok");

                fileStream.Position = 0;

                fileStream.Position = 0;
                for (int i = 0; i <= 20; i++)
                {
                    Console.Write(fileStream.ReadByte() + "\t");
                }
                Console.WriteLine();
                //fileStream.Close();
                //Console.ReadKey();
            }
        }

        static void StreamReadWrite()
        {
            string filePath = @"data.txt";
            Console.WriteLine("Input");
            string input = Console.ReadLine();

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(input);
            }

            Console.WriteLine("Do you want to read? Y/N");

            string choice = Console.ReadLine();

            if ("Y".Equals(choice))
            {
                string data = string.Empty;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while ((data = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(data);
                    }
                }
            }
        }
    }
}



