namespace Fin_car2;

public class Store
    {
        private List<Avto> avto = new List<Avto>();

        internal List<Avto> Acc
        {
            get { return avto; }
        }

        //Добавление счета
        void AddAcc()
        {
            Console.WriteLine(":\n" +
                              "1. Базовое\n" +
                              "2. Грузовое\n" +
                              "3. Автобус\n" +
                              "->");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    avto.Add(new Avto(0));
                    break;
                case 2:
                    avto.Add(new Trucks());
                    break;
                case 3:
                    avto.Add(new Bus());
                    break;
            }

            Console.WriteLine("Выберите дальнейшее действие");
        }


        //Выбор счета
        Avto GetAcc()
        {
            Console.Write("Введите индекс авто: ");
            int index = Convert.ToInt32(Console.ReadLine());
            if (index >= 0 && index < avto.Count)
            {
                return avto[index];
            }

            return null;
        }

        //Интефейс управления счетами
        internal Avto AccMenu()
        {
            while (true)
            {
                try
                {
                    Console.Write
                    ("------------------------------\n" +
                     "Выберете необходимое действие:\n" +
                     "0. Просмореть доступные авто\n" +
                     "1. Добавить авто\n" +
                     "2. Удалить авто\n" +
                     "3. Выбрать авто\n" +
                     "4. Выход\n" +
                     "->");
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        //  case 0: Info(); break;
                        case 1:
                            AddAcc();
                            break;
                        case 3: return GetAcc();
                        // case 4: return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
