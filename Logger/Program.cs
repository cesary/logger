using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Logger
{
    class FileInf 
    {
        public string name;
        public DateTime dt;

        public FileInf(string name)
        {
            this.name = name;
            this.dt = File.GetCreationTime(name);
        }

        public static int NameCompare(FileInf obj1, FileInf obj2)
        {
            return obj1.name.CompareTo(obj2.name);
        }
        
        public static int DateTimeCompare(FileInf obj1, FileInf obj2)
        {
            return obj1.dt.CompareTo(obj2.dt);
        }
    //    public int compareto(object obj)
    //    {
    //        if (obj == null) return 1;
    //        fileinf otherfile = obj as fileinf;
    //        if (otherfile != null)
    //        {
    //            return this.name.compareto(otherfile.name);
    //        }
    //        else
    //        {
    //            throw new argumentexception("object is not a fileinf");
    //        }
    //    }
        public override string ToString()
        {
            string name = Path.GetFileName(this.name);
            string date = this.dt.ToString();
            string output = String.Format("{0,-55} {1,-20}\n", name, date);
            return output;
        }
    }
    
    
    class IOWork 
    {
        public static List<FileInf> GetNames()
        {
            string[] arr_names = Directory.GetFileSystemEntries("C:\\Users\\Марина\\Downloads");
            List<string> names = arr_names.OfType<string>().ToList();
            List<FileInf> fileInf_list = names.ConvertAll(
                new Converter<string, FileInf>(StrToFileInf));
            return fileInf_list;
        }
        private static FileInf StrToFileInf(string name) 
        {
            return new FileInf(name);
        }
    }

    interface iLog 
    {
        void WriteMessage(List<FileInf> filenames);
 
    }
    class ConsoleLog : iLog 
    {
        public void WriteMessage(List<FileInf> filenames) 
        {
            Console.WriteLine("{0,-55} {1,-20}\n\n", "Filename", "Date");
            foreach (FileInf f in filenames)
                Console.WriteLine(f.ToString());
        }
    }

    class FileLog : iLog
    {
        public void WriteMessage(List<FileInf> filenames)
        {
            string path = "C:\\Users\\Марина\\Downloads\\Sort_Downloads.txt";
            string header = String.Format("{0,-55} {1,-20}\r\n\r\n", "Filename", "Date");
            File.Delete(path);
            File.AppendAllText(path, header);
            foreach (FileInf f in filenames)
            {
                File.AppendAllText(path, f.ToString() + "\r\n");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            List<FileInf> filenames = IOWork.GetNames();
            string s;
            Console.Write("Sort files by name or date: ");
            s = Console.ReadLine();
            Console.WriteLine("\n");
            if (s == "name")
                filenames.Sort(FileInf.NameCompare);
            else
                if (s == "date")
                    filenames.Sort(FileInf.DateTimeCompare);
                else
                    Console.WriteLine("Error!");

            ConsoleLog console = new ConsoleLog();
            console.WriteMessage(filenames);
            FileLog filelog = new FileLog();
            filelog.WriteMessage(filenames);
            Console.ReadKey();
        }
    }
}


/*
* fileInf{
    * string Name;
    * datetime dT;
* }
* ioWork {
    * List<fileInf> GetNames(String ...);
* 
* iLog{ 
    * writeMessage(String ...) //2 класса с интерфейсом iLog: для вывода на консоль и для вывода в файл
* }
* 
* 
 
* 
* Main{
* res = 
* 
* ;
* res2 = res.sort
* foreach(R in res2)
    * iLog.writeMessage(R);
* }
*/

//result = d1.year.compareto(d2.year)
//if (result != 0) return result;

//result = d1.Month.CompareTo(d2.Month)
//if (result != 0) return result; 