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
            string dt = this.dt.ToString();
            string s = name + " " + dt;
            return s;
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
        private string name;
        private string date;
        public void WriteMessage(List<FileInf> filenames) 
        {
            Console.WriteLine ("{0,-70} {1,35}\n", "Filename", "Date");
            foreach (FileInf f in filenames)
            {
                name = Path.GetFileName(f.name);
                date = f.dt.ToString();
                Console.WriteLine("{0,-70} {1,35}", name, date);
            }
        }
    }

    class FileLog : iLog
    {
        public void WriteMessage(List<FileInf> filenames)
        {
            string path = "C:\\Users\\Марина\\Downloads\\My_files.txt";
            File.Delete(path);
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