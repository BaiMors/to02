using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace to02
{
    public class dextra
    {
        public void Main()
        {
            dijkstraAlgorithm();
        }

        public void dijkstraAlgorithm()
        {
            List<rebra> graph = new();
            string path = "d_2.csv";
            string pathVersh = "v_1.csv";

            List<vershini> vershini = new();
            List<bool> meets = new();
            using (StreamReader sr = new StreamReader(pathVersh))
            {
                while (!sr.EndOfStream)
                {
                    string text = sr.ReadLine();
                    string[] str = text.Split(';');
                    vershini v = new();

                    for (int i = 0; i < str.Length; i++)
                    {
                        v.name = int.Parse(str[i]);
                        v.znach = 999999999;
                        meets.Add(false);
                        vershini.Add(v);
                    }
                }
            }
            Console.WriteLine("Введите номер вершины, от которой нужно найти расстояние до остальных");
            int start = Convert.ToInt32(Console.ReadLine());
            vershini[start - 1] = new vershini(vershini[start - 1].name, 0);

            string[] lines = File.ReadAllLines(path);
            int stroki = lines.Length;
            graph = readToList(path, stroki);

            int next = start;
            while (meets.Count(x => x == false) > 1)
            {
                for (int i = 0; i < graph.Count; i++)
                {
                    if (graph[i].nachalo == vershini[next - 1].name)
                    {
                        int buf = vershini[next - 1].znach + graph[i].ves;
                        if (vershini[graph[i].konec - 1].znach > buf) vershini[graph[i].konec - 1] = new vershini(graph[i].konec, buf);
                    }
                }
                meets[next - 1] = true;
                next = vershini.FindIndex(x => x.znach == (from p in vershini where p.znach != 0 && meets[p.name - 1] == false select p.znach).Min()) + 1;
            }

            Console.WriteLine("От начальной вершины до: ");
            for (int i = 0; i < vershini.Count; i++)
            {
                Console.WriteLine(vershini[i].name + ": " + vershini[i].znach);
            }
        }

        public static List<rebra> readToList(string path, int stroki)
        {
            int stolbci = 3;
            List<rebra> bufAr = new();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string text = sr.ReadLine();
                    string[] str = text.Split(';');
                    rebra r = new();

                    r.nachalo = int.Parse(str[0]);
                    r.konec = int.Parse(str[1]);
                    r.ves = int.Parse(str[2]);

                    bufAr.Add(r);

                }
            };
            return bufAr;
        }
    }

    struct vershini
    {
        public int name;
        public int znach;

        public vershini(int name, int znach)
        {
            this.name = name;
            this.znach = znach;
        }
    }

    public struct rebra
    {
        public int nachalo;
        public int konec;
        public int ves;

        public rebra(int nachalo, int konec, int ves)
        {
            this.nachalo = nachalo;
            this.konec = konec;
            this.ves = ves;
        }
    }
}
