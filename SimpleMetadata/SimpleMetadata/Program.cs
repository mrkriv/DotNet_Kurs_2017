using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Management;

namespace TestConsoleApplication
{
    class Program
    {
        class Range
        {
            public float From { get; set; }
            public float To { get; set; }
        }

        static void Main(string[] args)
        {
            var testObj = new TestObject()
            {
                Name = "Simple name",
                Number = 9532,
                FloatNumber = .87f,
                StringList = new List<string>() 
                {  
                    "Name1",
                    "Name2",
                    "Name3",
                }
            };

            var stringVal = Serealize(testObj);
            Console.WriteLine(stringVal);

            var v3 = Diserealize<TestObject>(stringVal);

            Console.ReadLine();
        }

        public static T Diserealize<T>(string data)
        {
            var type = typeof(T);
            
            var props_tuple = data.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            var obj = Activator.CreateInstance<T>();
            
            foreach (var tuple in props_tuple)
            {
                var t = tuple.Split('=');
                var prop = type.GetProperty(t[0]);

                var intefaces = prop.PropertyType.GetInterfaces();
                
                if (intefaces.Any(IsIEnumerableT))
                {
                    var listValues = t[1].Split(new char[] { '!' }, StringSplitOptions.RemoveEmptyEntries);
                    var List = new List<string>();  //Activator.CreateInstance(...)

                    foreach (var listItem in listValues)
                    {
                        List.Add(listItem);
                    }

                    prop.SetValue(obj, List);
                }
                else
                {
                    prop.SetValue(obj, Convert.ChangeType(t[1], prop.PropertyType));
                }
            }

            return obj;
        }

        public static string Serealize<T>(T obj)
        {
            var result = "";

            foreach (var prop in obj.GetType().GetProperties())
            {
                var val = prop.GetValue(obj);

                var intefaces = prop.PropertyType.GetInterfaces();

                if (intefaces.Any(IsIEnumerableT))
                {
                    result += string.Format("{0}=", prop.Name, val);

                    var ilist = val as IList<string>;
                    foreach(var item in ilist)
                    {
                        result += item + "!";
                    }
                    result += ";";
                }
                else
                {
                    result += string.Format("{0}={1};", prop.Name, val);
                }
            }

            return result;
        }

        private static bool IsIEnumerableT(Type type)
        {
            if (type == typeof(IList<string>))
                return true;

            return false;
        }
    }
}