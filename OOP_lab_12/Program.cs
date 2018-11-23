using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Collections;


namespace lab12
{
    interface IInfoClass
    {
        double Sum();
        void Info();
        void Set(double d1, double d2);
    }



    // Тестовый класс, содержащий некоторые конструкции
    class MyTestClass : IInfoClass
    {
        double d, f;
        public int mmmmm = 500;

        public MyTestClass()
        {

        }

        public MyTestClass(double d, double f)
        {
            this.d = d;
            this.f = f;
        }

        public double Sum()
        {
            return d + f;
        }

        public void Info()
        {
            Console.WriteLine(@"d = {0}
                       f = {1}", d, f);
        }

        public void InfoOut(string str)
        {
            int j = Convert.ToInt32(str);
            j = j * 2;
            Console.WriteLine(j);
        }

        public void Set(int a, int b)
        {
            d = (double)a;
            f = (double)b;
        }

        public void Set(double a, double b)
        {
            d = a;
            f = b;
        }


        public static string h(string s)
        {
            return s + " !";
        }

    }
    class My
    {
        public static string q(string s)
        {
            return s + " !";
        }
    }


    // В данном классе определены методы использующие рефлексию
    class Reflect
    {

        public static void AllInfo(object m)
        {
            string text = "";
            Type t = m.GetType();
            Console.WriteLine("-------------- Вся информация о классе \n");
            string path = @"text.txt";
            foreach (MemberInfo mi in t.GetMembers())
            {
                Console.WriteLine(mi.DeclaringType + " " + mi.MemberType + " " + mi.Name);

                text = text + mi.DeclaringType + " " + mi.MemberType + " " + mi.Name + '\n';

            }

            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.Write(text);
            }

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }

            Console.WriteLine("\n");




        }

        // Информация о полях и реализуемых интерфейсах 
        public static void FieldInterfaceInfo<T>(T obj) where T : class
        {
            Type t = typeof(T);
            Console.WriteLine("\n---------- Реализуемые интерфейсы \n");
            var im = t.GetInterfaces();
            foreach (Type tp in im)
                Console.WriteLine(tp.Name);
            Console.WriteLine();
        }

        public static void Fields(object im)
        {
            Type t = im.GetType();
            Console.WriteLine("------------ Пoля и свойства \n");
            foreach (MemberInfo mi in t.GetFields())
            {
                Console.WriteLine(mi.DeclaringType + " " + mi.MemberType + " " + mi.Name);
            }

            foreach (MemberInfo mi in t.GetProperties())
            {
                Console.WriteLine(mi.DeclaringType + " " + mi.MemberType + " " + mi.Name);
            }
            Console.WriteLine("\n");
        }


        // Данный метод выводит информацию о содержащихся в классе методах
        public static void MethodReflectInfo<T>(T obj) where T : class
        {
            Type t = typeof(T);
            // Получаем коллекцию методов
            MethodInfo[] MArr = t.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            Console.WriteLine("------------- Список методов класса {0} \n", obj.ToString());

            // Вывести методы
            foreach (MethodInfo m in MArr)
            {
                Console.Write(m.ReturnType.Name + " \t" + m.Name + "(");
                // Вывести параметры методов
                ParameterInfo[] p = m.GetParameters();
                for (int i = 0; i < p.Length; i++)
                {
                    Console.Write(p[i].ParameterType.Name + " " + p[i].Name);
                    if (i + 1 < p.Length) Console.Write(", ");
                }
                Console.Write(")\n");
            }
        }


        public static void GetMethodParams(Object o, string s)
        {
            Type t = o.GetType();
            MethodInfo[] methods = t.GetMethods();
            Console.WriteLine();
            foreach (var f in methods)
            {
                ParameterInfo[] parms = f.GetParameters();
                if (parms.Length != 0)
                {
                    var paramsQuerry =
                        from sp in parms
                        where sp.Name.Contains(s)
                        select sp;
                    if (paramsQuerry.ToArray().Length != 0)
                    {
                        Console.WriteLine(f.Name);
                        foreach (var p in parms)
                        {
                            Console.WriteLine(p.Name);
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        public static void Taskf(object im, string par)
        {
            string path = @"text3.txt";
            string buf = "";
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                buf = sr.ReadToEnd();
            }
            int p;
            p = Convert.ToInt32(buf);
            Console.WriteLine(buf);
            Type t = im.GetType();

            // создаем экземпляр класса Program
            Object o = Activator.CreateInstance(typeof(MyTestClass));

            // получаем метод GetResult
            MethodInfo method = t.GetMethod(par);

            // вызываем метод, передаем ему значения для параметров и получаем результат
            method.Invoke(o, new object[] { buf });

        }


    }

    class Program
    {
        static void Main()
        {
            MyTestClass mtc = new MyTestClass(12.0, 3.5);
            Reflect.AllInfo(mtc);
            Reflect.MethodReflectInfo<MyTestClass>(mtc);//инф  о методах
            Reflect.Fields(mtc);
            Reflect.FieldInterfaceInfo<MyTestClass>(mtc);//инф о интерфесах
            Reflect.GetMethodParams(new List<int>(), "index");//по имени класса
            Reflect.Taskf(mtc, "InfoOut");



            Console.ReadLine();
        }
    }

}



