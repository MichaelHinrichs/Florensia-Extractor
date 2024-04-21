//Written for Florensia. https://store.steampowered.com/app/384030
using System.IO;

namespace Florensia_Extractor
{
    class Program
    {
        static BinaryReader br;
        static string path;
        static void Main(string[] args)
        {
            br = new BinaryReader(File.OpenRead(args[0]));
            int count = br.ReadInt32();

            System.Collections.Generic.List<Subfile> subfiles = new();
            for (int i = 0; i < count; i++)
            {
                subfiles.Add(new());
                br.BaseStream.Position += 28;//unknown
            }

            string path = Path.GetDirectoryName(args[0]) + "//" + Path.GetFileNameWithoutExtension(args[0]);
            Directory.CreateDirectory(path);

            foreach (Subfile file in subfiles)
            {
                br.BaseStream.Position = file.start;
                BinaryWriter bw = new(File.Create(path + "//" + file.name));
                bw.Write(br.ReadBytes(file.size));
            }
        }

        class Subfile
        {
            public string name = new string(br.ReadChars(260)).TrimEnd('\0');
            public int start = br.ReadInt32();
            public int size = br.ReadInt32();
        }
    }
}
