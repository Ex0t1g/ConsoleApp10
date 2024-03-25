using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp10.Program.HDD;

namespace ConsoleApp10
{
    internal class Program
    {
        public abstract class Storage
        {
            protected string name;
            protected string model;

            public Storage(string name, string model)
            {
                this.name = name;
                this.model = model;
            }

            public abstract double GetMemory();

            public abstract double CopyData(double dataSize);

            public abstract double GetFreeSpace();

            public abstract void GetDeviceInfo();
        }

        public class Flash : Storage
        {
            private double usb30Speed;
            private double memory;

            public Flash(string name, string model, double usb30Speed, double memory) : base(name, model)
            {
                this.usb30Speed = usb30Speed;
                this.memory = memory;
            }

            public override double GetMemory()
            {
                return memory;
            }

            public override double CopyData(double dataSize)
            {
                double copyTime = dataSize / usb30Speed;
                Console.WriteLine($"Копирование данных на Flash-память займет {copyTime} сек.");
                return copyTime;
            }

            public override double GetFreeSpace()
            {
                return memory;
            }

            public override void GetDeviceInfo()
            {
                Console.WriteLine($"Носитель информации: Flash-память");
                Console.WriteLine($"Наименование: {name}");
                Console.WriteLine($"Модель: {model}");
                Console.WriteLine($"Скорость USB 3.0: {usb30Speed} Мб/с");
                Console.WriteLine($"Объем памяти: {memory} Гб");
            }
        }

        public class DVD : Storage
        {
            private double readWriteSpeed;
            private DVDType type;

            public DVD(string name, string model, double readWriteSpeed, DVDType type) : base(name, model)
            {
                this.readWriteSpeed = readWriteSpeed;
                this.type = type;
            }

            public override double GetMemory()
            {
                if (type == DVDType.SingleLayer)
                    return 4.7;
                else
                    return 9;
            }

            public override double CopyData(double dataSize)
            {
                double copyTime = dataSize / readWriteSpeed;
                Console.WriteLine($"Запись данных на DVD займет {copyTime} сек.");
                return copyTime;
            }

            public override double GetFreeSpace()
            {
                return 0;
            }

            public override void GetDeviceInfo()
            {
                Console.WriteLine($"Носитель информации: DVD-диск");
                Console.WriteLine($"Наименование: {name}");
                Console.WriteLine($"Модель: {model}");
                Console.WriteLine($"Скорость чтения/записи: {readWriteSpeed} Мб/с");
                Console.WriteLine($"Тип диска: {type}");
            }
        }

        public class HDD : Storage
        {
            private double usb20Speed;
            private int partitionCount;
            private double partitionSize;

            public HDD(string name, string model, double usb20Speed, int partitionCount, double partitionSize) : base(name, model)
            {
                this.usb20Speed = usb20Speed;
                this.partitionCount = partitionCount;
                this.partitionSize = partitionSize;
            }

            public override double GetMemory()
            {
                return partitionCount * partitionSize;
            }

            public override double CopyData(double dataSize)
            {
                double copyTime = dataSize / usb20Speed;
                Console.WriteLine($"Копирование данных на HDD займет {copyTime} сек.");
                return copyTime;
            }

            public override double GetFreeSpace()
            {
                return 0;
            }

            public override void GetDeviceInfo()
            {
                Console.WriteLine($"Носитель информации: Съемный HDD");
                Console.WriteLine($"Наименование: {name}");
                Console.WriteLine($"Модель: {model}");
                Console.WriteLine($"Скорость USB 2.0: {usb20Speed} Мб/с");
                Console.WriteLine($"Количество разделов: {partitionCount}");
                Console.WriteLine($"Объем разделов: {partitionSize} Гб");

            }

            public enum DVDType
            {
                SingleLayer,
                DualLayer
            }
        }
        static void Main(string[] args)
        {
            Storage[] storageDevices = new Storage[]
            {
            new Flash("Flash Drive 1", "Flash1", 200, 8),
            new DVD("DVD Drive 1", "DVD1", 8, DVDType.SingleLayer),
            new HDD("External HDD 1", "HDD1", 60, 2, 500)
            };

            double totalMemory = 0;
            double totalCopyTime = 0;
            double dataSize = 565 * 1024; 
            double maxDataSizePerDevice = 780;

            foreach (Storage device in storageDevices)
            {
                totalMemory += device.GetMemory();
                totalCopyTime += device.CopyData(maxDataSizePerDevice);
                device.GetDeviceInfo();
                Console.WriteLine();
            }

            int requiredDeviceCount = (int)Math.Ceiling(dataSize / maxDataSizePerDevice);
            Console.WriteLine($"Общий объем памяти всех устройств: {totalMemory} Гб");
            Console.WriteLine($"Время необходимое для копирования: {totalCopyTime} сек");
            Console.WriteLine($"Необходимое количество носителей информации: {requiredDeviceCount}");
        }
    }
}
