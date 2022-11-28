namespace zenllama.llamadoom.WAD
{
    public class wadinfo_t
    {
        // Should be "IWAD" or "PWAD".
        char[] identification = new char[4];
        int numlumps;
        int infotableofs;
    }

    public class filelump_t
    {
        int filepos;
        int size;
        char[] name= new char[8];
    }

    public class lumpinfo_t
    {
        char[] name = new char[8];
        int handle;
        int position;
        int size;
    }

    public class LumpInfo
    {
        public int Size;
        public int Offset;
        public string Name;
    }

    public class WadFile
    {
        private string filename;
        private int numberOfLumps;
        private int directoryPos;
        public string header;
        public List<LumpInfo> Lumps = new List<LumpInfo>();


        public WadFile(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("WAD file doesn't exist");
            }

            this.filename = filename;
        }

        public bool ReadFile()
        {
            Stream s = File.Open(this.filename, FileMode.Open);

            this.ReadHeader(s);

            this.ReadDirectory(s);
            
            return true;
        }

        private void ReadHeader(Stream s)
        {
            s.Seek(0, SeekOrigin.Begin);

            byte[] head = new byte[4];
            byte[] lumpCount = new byte[4];
            byte[] directory = new byte[4];

            s.Read(head, 0, 4);
            s.Read(lumpCount, 0, 4);
            s.Read(directory, 0, 4);

            //Console.WriteLine(System.Text.Encoding.ASCII.GetString(head));
            //Console.WriteLine(BitConverter.ToInt32(lumpCount, 0));
            //Console.WriteLine(BitConverter.ToInt32(directory, 0));

            this.header = System.Text.Encoding.ASCII.GetString(head);
            this.numberOfLumps = BitConverter.ToInt32(lumpCount, 0);
            this.directoryPos = BitConverter.ToInt32(directory, 0);
        }

        private void ReadDirectory(Stream s)
        {
            s.Seek(this.directoryPos, SeekOrigin.Begin);

            for (int i = 0; i < this.numberOfLumps; i++)
            {
                byte[] offset = new byte[4];
                byte[] size = new byte[4];
                byte[] name = new byte[8];

                s.Read(offset, 0, 4);
                s.Read(size, 0, 4);
                s.Read(name, 0, 8);

                LumpInfo l = new LumpInfo();
                l.Offset = BitConverter.ToInt32(offset, 0);
                l.Size = BitConverter.ToInt32(size, 0);
                l.Name = System.Text.Encoding.ASCII.GetString(name);

                this.Lumps.Add(l);
            }
        }

    }
}