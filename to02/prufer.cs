using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace to02
{
    public class kod_prufera
    {
        public void Main()
        {
            prufer();
        }

        public void prufer()
        {
            //подготовка к считыванию из файла и само считывание
            string pathOgran = "kp_2.csv";
            string[] lines = File.ReadAllLines(pathOgran);
            int stroki = lines.Length;
            int stolbci = lines[0].Split(';').Length;
            int[,] mas = readToIntDvumMas(pathOgran, stroki, stolbci);

            //преобразование в лист
            graph obj = new();
            List<graph> listReber = new();
            for (int i = 0; i < stroki; i++)
            {
                for (int j = 0; j < stolbci; j++)
                {
                    if (j == 0) obj.nachalo = mas[i, j];
                    else obj.konec = mas[i, j];
                }
                listReber.Add(obj);
            }

            int index = 0;
            List<int> resultKod = new();
            while (listReber.Count > 1)
            {
                List<int> unikalnieNachalo = (from p in listReber where (from n in listReber where n.nachalo == p.nachalo select n).Count() == 1 && (from n in listReber where n.konec == p.nachalo select n).Count() == 0 select p.nachalo).ToImmutableList().ToList();
                List<int> unikalnieKonec = (from p in listReber where (from n in listReber where n.konec == p.konec select n).Count() == 1 && (from n in listReber where n.nachalo == p.konec select n).Count() == 0 select p.konec).ToImmutableList().ToList();
                if (unikalnieNachalo.Count == 0 || unikalnieNachalo.Min() > unikalnieKonec.Min())
                {
                    index = listReber.FindIndex(p => p.konec == unikalnieKonec.Min());
                    resultKod.Add(listReber[index].nachalo);
                    listReber.RemoveAt(index);
                }
                else if (unikalnieKonec.Count == 0 || unikalnieNachalo.Min() < unikalnieKonec.Min())
                {
                    index = listReber.FindIndex(p => p.nachalo == unikalnieNachalo.Min());
                    resultKod.Add(listReber[index].konec);
                    listReber.RemoveAt(index);
                }
            }

            using (StreamWriter sw = new StreamWriter("result.csv"))
            {
                foreach (int p in resultKod)
                {
                    sw.WriteLine(p);
                }
            }
        }

        public int[,] readToIntDvumMas(string path, int stroki, int stolbci)
        {
            int[,] mas = new int[stroki, stolbci];
            List<int> bufAr = new();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string text = sr.ReadLine();
                    string[] str = text.Split(';');

                    for (int i = 0; i < str.Length; i++)
                    {
                        bufAr.Add(Convert.ToInt32(str[i]));
                    }
                }
            };

            int schBuf = 0;
            for (int i = 0; i < stroki; i++)
            {
                for (int j = 0; j < stolbci; j++)
                {
                    mas[i, j] = bufAr[schBuf];
                    schBuf++;
                }
            }

            return mas;
        }
    }

    public struct graph
    {
        public int nachalo;
        public int konec;

        public graph(int nachalo, int konec)
        {
            this.nachalo = nachalo;
            this.konec = konec;
        }
    }
}
