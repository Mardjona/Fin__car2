namespace Fin_car2; 
internal class Avto
    {
        protected string _number; //Номер машины
        protected string _type; //Тип машины
        protected double _fuelCapacity; //Макс кол-во бензина в баке
        protected double _fuelConsumption; //Расход топлива
        protected double _currentFuel; //Текущее кол-во бензина
        protected double _startX; //Поля для координат (начальные и конечные)
        protected double _startY;
        protected double _endX;
        protected double _endY;
        protected double _distance;//расстояние
        protected double _milleage;//Пробег
        protected double _speed; //Скорость
        protected double _maxSpeed; //Максимальная скорость
        protected List<string> _coordinates = new(); //Список координат, по которым проезжает машина

        private void carStart(List<Avto> avtos)
        {
             Console.WriteLine(" Для начала  необходимо создать  минимум одну машину.");
             while (true)
             {
                 try
                 {
                     string answerType = "";
                     while (answerType == "")
                     {
                         Console.WriteLine("\nВыберите тип  машины:\n 1 - Легковой автомобиль\n 2 - Автобус\n 3 - Грузовой автомобиль\n");
                         answerType = Console.ReadLine();
                         switch (answerType)
                         {
                             case "1":
                             case "2":
                             case "3":
                                 break;
                             default:
                                 Console.WriteLine("\nНекорректный ввод. Пожалуйста, повторите попытку.");
                                 answerType = "";
                                 break;
                         }
                     }

                     Console.Write("Введите номер машины:");
                     string avtoNumber = Console.ReadLine();
                     double avtoFCapacity;
                     if (answerType == "1")
                     {
                         Console.Write("Введите объем бака :");
                         avtoFCapacity = Convert.ToDouble(Console.ReadLine());
                     }
                     else
                     {
                         if (answerType == "2")
                         {
                             avtoFCapacity = 60;
                         }
                         else
                         {
                             avtoFCapacity = 80;
                         }
                     }

                     foreach (var avto in avtos)
                     {
                         if (avtoNumber == avto._number)
                         {
                             avtoNumber = "";
                             Console.WriteLine("\nЭтот номер уже существует, введите другой");
                             break;
                         }
                     }

                     if (string.IsNullOrEmpty(avtoNumber) || avtoFCapacity <= 0) //Условие не позволяет создать аккаунт с пустым номером
                     {
                         Console.WriteLine("Неправильные данные, повторите попытку.\n");
                     }
                     else
                     {
                         var car = answerType switch
                         {
                             "1" => new Avto(),
                             "2" => new Bus(),
                             _ => new Gruz()
                         }; 
                         car.carCreation(avtoNumber, avtoFCapacity, answerType);
                         avtos.Add(car);
                         string answer = "";
                         while (answer == "")
                         {
                             Console.Write("\nХотите создать еще одну машину? (да/нет): ");
                             answer = Console.ReadLine();
                             switch (answer)
                             {
                                 case "да":
                                 case "нет":
                                     break;
                                 default:
                                     Console.WriteLine("\nНекорректный ввод. Пожалуйста, введите 'да' или 'нет'.\n");
                                     answer = "";
                                     break;
                             }
                         }
                         if (answer == "нет")
                         {
                             break;
                         }
                     }
                 }
                 catch
                 {
                     Console.WriteLine("\nОшибка ввода данных, повторите попытку.\n");
                 }
             }
        }
        protected void carList(List<Avto> avtos)
       {
        int i = 1;
        foreach (Avto avto in avtos)
        {
            string type = avto._type switch
            {
                "1" => "автомобиль",
                "2" => "автобус",
                _ => "грузовик"
            };
            // Выводим порядковый номер и номер автомобиля
            Console.Write($"\n {i}. Номер: {avto._number}");
            // Выводим тип автомобиля
            Console.Write($" - {type}");
            // Выводим информацию о топливе (текущее/максимальное)
            Console.Write($" - Топливо: {avto._currentFuel}/{avto._fuelCapacity}");
            // Выводим пробег автомобиля
            Console.Write($" - Пробег: {avto._milleage}");
            i++;
        }
       }

        public void AVTO(List<Avto> avtos)
        {
            carStart(avtos);
            Console.WriteLine("\nВыберите машину, с которой хотите взаимодействовать:");
            string answer = "";
            int nom = -1;
            carList(avtos);
            while (true)
            {
                try
                {
                    while (answer == "")
                    {
                        if (nom == 0)
                        {
                            carList(avtos);
                        }
                        while (true)
                        {
                            Console.Write("\n\nВведите порядковый  номер машины из списка:");
                            nom = Convert.ToInt32(Console.ReadLine());
                            if (nom > 0 && nom <= avtos.Count)
                            {
                                avtos[nom - 1].commandCenter(avtos);
                                answer = " ";
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nМашины с таким номером нет в списке. Попробуйте снова.\n");
                            }
                        }

                        while (answer == " ")
                        {
                            Console.WriteLine("\nХотите вернуться к выбору машины? (да/нет)\n");
                            answer = Console.ReadLine();
                            switch (answer)
                            {
                                case "да":
                                    answer = "";
                                    nom = 0;
                                    break;
                                case "нет":
                                    answer = ".";
                                    break;
                                default:
                                    answer = " ";
                                    break;
                            }
                        }
                    }
                    if (answer == ".")
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Ошибка ввода, повторите попытку.");
                }

            }
        }

        protected virtual void carCreation(string number, double fuelCapacity, string type) //Создание машины
        {
            _number = number;
            _fuelCapacity = fuelCapacity;
            _type = type;
            _fuelConsumption = 0;
            _currentFuel = 0;
            _distance = 0;
            _milleage = 0;
            _speed = 0;
            _maxSpeed = 180;
            string typeStr = _type switch
            {
                "1" => "автомобиль",
                "2" => "автобус",
                _ => "грузовик"
            };

            Console.WriteLine($"Создана машина: {typeStr}.");
        }
        protected void DisplayInfo() //Вывод информации о машине
        {
            string type = _type switch
            {
                "1" => "автомобиль",
                "2" => "автобус",
                _ => "грузовик"
            };
            Console.WriteLine($"Номер машины: {_number}\nТип: {type}\nМаксимальное количество бензина в баке: {Math.Round(_fuelCapacity, 2)} литров\nТекущее количество топлива: {Math.Round(_currentFuel, 2)} литров.\nПробег: {Math.Round(_milleage, 2)} км.");
        }

        protected virtual void speedUp() //Разгон
        {
            while (true)
            {
                try 
                {
                    Console.WriteLine("\nВведите значение скорости (от 1 до 180 км/ч), до которого хотите разогнаться:\n");
                    _speed = Convert.ToDouble(Console.ReadLine());
                    if (_speed > 0 && _speed <= 180)
                    {
                        fuelConsumption(_speed);
                        break; 
                    }
                    else
                    {
                        Console.WriteLine("\nВведено значение вне заданного диапазона. Попробуйте снова.");
                    }
                }
                catch
                {
                    Console.WriteLine("\nВведено некорректное значение, попробуйте снова.");
                }
            }
        }
        protected void FillFuel() //Заправка 
        {
            while (true)
            {
                try
                {
                    if (_currentFuel == _fuelCapacity)
                    {
                        Console.WriteLine($"\nБак машины полон.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\nСостояние бензобака: {_currentFuel}/{_fuelCapacity} л.\nВведите кол-во бензина (в литрах), на которое хотите заправить машину (макс значение:{Math.Round(_fuelCapacity, 2)}).\n");
                        double fuelAmount = Convert.ToDouble(Console.ReadLine());
                        if (_currentFuel + fuelAmount <= _fuelCapacity && fuelAmount > 0) //Условие не позволяет пользователю добавить топлива больше, чем машина может вместить, а также не позволяет ввести отрицательное значение
                        {
                            _currentFuel += fuelAmount;
                            Console.WriteLine($"\nМашина заправлена на {Math.Round(fuelAmount, 2)} л.\nТекущее количество топлива: {Math.Round(_currentFuel, 2)} л.");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nНевозможно добавить столько топлива. Попробуйте снова.");
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("\nВведено некорректное значение. Попробуйте снова.");
                }
            }
        }

        protected void fuelConsumption(double speed) //рассчет расхода топлива от скорости
        {
            if (speed > 0)
            {
                if (speed <= 45)
                {

                    _fuelConsumption = 12;
                }
                else if (speed > 45 && speed <= 100)
                {
                    _fuelConsumption = 9;
                }
                else if (speed > 101 && speed <= 180)
                {
                    _fuelConsumption = 12.5;
                }
                else Console.WriteLine("Невозможно ехать с такой скоротью");
            }
            else Console.WriteLine("Невозможно ехать с такой скоротью");
        }
        protected virtual void getDistance(List<Avto> avtos)//Вычисление дистанции по кординатам
        {
            while (true)
            {
                try
                {
                    Console.Write("Введите начальные координаты (x, y) через запятую: ");
                    string[] startCoords = Console.ReadLine().Split(',');
                    double _startX = double.Parse(startCoords[0].Trim());
                    double _startY = double.Parse(startCoords[1].Trim());
                    double startSample = _startX;

                    Console.Write("Введите конечные координаты (x, y) через запятую: ");
                    string[] endCoords = Console.ReadLine().Split(',');
                    double _endX = double.Parse(endCoords[0].Trim());
                    double _endY = double.Parse(endCoords[1].Trim());

                    double _distance = CalculateDistance(_startX, _startY, _endX, _endY);
                    Console.WriteLine($"Расстояние между точками: {_distance}");
                    double CalculateDistance(double x1, double y1, double x2, double y2)
                    {
                        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                    }

                    if (_startX != _endX) //Если начальный Х не равен конечному Х, то за основу построения маршрута берется Х. У находится по формуле прямой через две точки на пл-ти
                    {
                        if (_startX < _endX)//Условия для направления движения
                        {
                            while (startSample <= _endX)
                            {
                                _coordinates.Add(Convert.ToString(startSample));
                                startSample++;
                            }
                        }
                        else
                        {
                            while (startSample >= _endX)
                            {
                                _coordinates.Add(Convert.ToString(startSample));
                                startSample--;
                            }
                        }
                        for (int i = 0; i < _coordinates.Count; i++)
                        {
                            _coordinates[i] += $"; {Convert.ToString(_startY + ((_endY - _startY) * (Convert.ToDouble(_coordinates[i]) - _startX) / (_endX - _startX)))}"; //ко всем элементам списка добавляется сооттветствующая координата Y. которая высчитывается по формуле прямой через две точки на плоскости
                        }
                    }
                    else
                    {
                        if (_startY < _endY)
                        {
                            while (_startY <= _endY)
                            {
                                _coordinates.Add($"{Convert.ToString(_startX)};{Convert.ToString(_startY)}");
                                _startY++;
                            }
                        }
                        else
                        {
                            while (_startY >= _endY)
                            {
                                _coordinates.Add($"{Convert.ToString(_startX)};{Convert.ToString(_startY)}");
                                _startY--;
                            }
                        }
                    }


                    string answer = "";
                    while (answer == "")
                    {
                        Console.WriteLine("\nВы уверены, что ввели все правильно и хотите продолжить? (да/нет)\n");
                        answer = Console.ReadLine();
                        switch (answer)
                        {
                            case "да":
                            case "нет":
                                break;
                            default:
                                answer = "";
                                break;
                        }
                    }
                    if (answer == "да")
                    {
                        Console.WriteLine("\nМаршрут запланирован успешно.");
                        break;
                    }
                    else
                    {
                        _coordinates.Clear();
                        Console.WriteLine("\nВыполнен сброс списка остановок. Начните снова с самого начала");
                    }
                }
                catch
                {
                    Console.WriteLine("\nКоординаты введены некорректно. Попробуйте снова с самого начала.");
                }
            }
        }

      

        protected virtual void Drive(List<Avto> avtos) //Цикл езды
        {
            while (_coordinates.Count == 0) //Если маршрут не спланирован
            {
                Console.WriteLine("\n Маршрут не запланирован!");
                getDistance(avtos);
            }

            while (Math.Round(_currentFuel) == 0) //Если у машины нет топлива
            {
                Console.WriteLine("\nБак пуст. Заправьте машину!");
                FillFuel();
            }

            speedUp();

            double fuelDistance = _currentFuel / (_fuelConsumption / 100); //Расстояние, которое может проехать машина с заправленным баком
            Console.WriteLine($"\nДистанция равна: {Math.Round(_distance, 2)} км.");
            double needFuel = _distance * (_fuelConsumption / 100); //Количество топливо, которое необходимо для достижения цели
            _distance -= fuelDistance;
            _currentFuel = fuelDistance > _distance ? _currentFuel - needFuel : _currentFuel; 

            while (_distance > 0) //Цикл езды
            {
                _speed = 0;
                _currentFuel = 0; 
                _milleage += fuelDistance; 

                Console.WriteLine($"\nМашина проехала {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} л.\nРасход топлива: {_fuelConsumption} л .\nОсталось ехать {Math.Round(_distance, 2)} км.\n Топлива не хватает, чтобы продолжить поездку.");
                FillFuel(); //Обращение к методу заправки
                speedUp();
                fuelDistance = _currentFuel / (_fuelConsumption / 100); // расстояния, котрое может проехать машина с заправленным на текущее кол-во топлива баком
                _distance -= fuelDistance; // расстояния, которое необходимо проехать
            }
            _speed = 0;

            fuelDistance += _distance;
            _milleage += fuelDistance;  //Обновляем общий пробег
           
            Console.WriteLine($"Машина проехала {Math.Round(fuelDistance, 2)} км.\nПробег: {Math.Round(_milleage, 2)}.\nОстаток топлива: {Math.Round(_currentFuel, 2)} литров.\nРасход топлива: {_fuelConsumption} л.");
            _fuelConsumption = 0;
            _coordinates.Clear();
        }
        public virtual void commandCenter(List<Avto> avtos) //Главный метод
        {
            Console.WriteLine($"\nМеню  машины {_number}.");
            string continuation = "";
            while (continuation == "")
            {
                Console.WriteLine(" Необходимые действия \"1\".\n Чтобы запланировать маршрут, выберите \"2\".\n Чтобы заправить машину, выберите \"3\".\n Чтобы начать поездку, выберите \"4\".\n");
                string? option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.WriteLine("\n");
                        DisplayInfo();
                        break;
                    case "2":
                        getDistance(avtos);
                        break;
                    
                    case "3":
                        FillFuel();
                        break;
                    case "4":
                        Drive(avtos);
                        break;
                    default:
                        Console.WriteLine("\nНеправильный ввод.");
                        break;
                }
                Console.WriteLine("\nЧтобы продолжить нажмите \"Enter\".\nЧтобы выйти напишите что-нибудь и нажмите \"Enter\".\n");
                continuation = Console.ReadLine();
            }
        }
    }
