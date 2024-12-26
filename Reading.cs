using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Reading
    {
        internal static T GetValue<T>(string str)
        {
            Console.Write(str);
            while (true)
            {
                try
                {
                    T value = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                    return value;
                }
                catch (Exception)
                {
                    Console.WriteLine($"Неверный тип данных. Введено не {typeof(T)}. Введите значение типа {typeof(T)}");
                }
            }
        }

    }
}
