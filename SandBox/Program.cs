using zenllama.llamadoom.WAD;

namespace zenllama.llamadoom.SandBox
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string fn = "C:\\Games\\DOOM\\DOOM.WAD";

            WadFile w = new WadFile(fn);

            w.ReadFile();

            Console.WriteLine("Header: {0}", w.header);
            Console.WriteLine("Lump Count: {0}", w.Lumps.Count);

            foreach(LumpInfo l in w.Lumps)
            {
                Console.WriteLine("{0} \t {1} \t {2}", l.Name, l.Offset, l.Size);
            }
        }
    }
}