namespace Fin_car2;

internal class Bus : Avto {
    private int _passCount = 0;
    private int _passMax = 30;
    private int[] _cordEnd = new int[2];
   
    //вывод информации
    public Bus()
    {
        EnterInfo();
    }

    protected void EnterInfo()
    {
        {
            Console.Write("Введите номер автобуса : ");
            _number = Console.ReadLine();
            _fuelMax = 60; // максимальное колво топлива в баке
            Console.Write("Текущее количество топлива в бензобаке( Не более 60 л): ");
            _fuelCount = Convert.ToDouble(Console.ReadLine());
            if (_fuelCount > _fuelMax)
            {
                Console.WriteLine("Ошибка. Текущее значение не может превышать максимальное");
                return;
            }
            Console.WriteLine("Автобус добавлен");
        }
    }
    protected override void Out() {
        Console.WriteLine
        ($"-------АВТОБУС---------- \n" +
         $"Номер: {_number}\n" +
         $"Толпиво: {_fuelCount}\n" +
         $"Местоположение: {_corA[0]},{_corA[1]}\n" +
         $"Максимум топлива: {_fuelMax}\n" +
         $"Суммарный пробег: {_sumDistance}\n" +
         $"Количество пассажиров: {_passCount}\n" +
         "--------------------------------");
    }
    //Движение до точки
    private void MoveToEnd() {
        if (_weight >= 100 && _weight < 1000) {
            _kf = 0.4;
        } else if (_weight >= 1000 && _weight <= 2000) {
            _kf = 0.8;
        }
        double dis = DistanceToPlace();
        double prob = dis;
        SpeedDeterm();
        while (true) {
            double rem = Remains(dis);
            if (rem <= _fuelCount) {
                _fuelCount -= rem;
                _corA = _corB;
                _sumDistance += prob;
                Console.WriteLine($"Вы проехали: {dis} км, топлива осталось: {_fuelCount} л, местоположение: {_corA[0]},{_corB[1]}, раcход: {_fuelRate}");
                return;
            }
            else {
                Console.Write("Вам не хватило топлива, хотите заправиться: +/-\n" + ">");
                string ans = Console.ReadLine();
                if (ans == "+") {
                    dis -= _fuelCount / (_fuelRate / 100);
                    _fuelCount = 0;
                    Console.WriteLine($"Топливо кончилось, вы проехали: {prob - dis:f}км");
                    Refill();
                }
                else {
                    Console.WriteLine("Вы заглохли");
                    return;
                }
            }
        }
    }
    private double DistanceToPlace() {
        while (true) {
            try {
                _corB = new int[2];
                string[] s = Console.ReadLine().Split(',',' ',';');
                for(int i = 0; i < _corB.Length; i++)
                    _corB[i] = Int32.Parse(s[i]);
                double c = Math.Sqrt(Math.Pow(_corB[0] - _corA[0], 2) + Math.Pow(_corB[1] - _corA[1], 2));
                return Math.Round(c, 2);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }
    }
    // Метод для посадки пассажиров
    public void EnterPassenger(int count)
    {
           Console.Write("Введите количество вошедших пассажиров ( максимальная вместимость 30 пассажиров) :  ");
            count = int.Parse(Console.ReadLine());
            if (_passCount + count <= 30)
            {
                _passCount += count;
                _weight = _passCount * 70;
            }
            else
            {
                Console.WriteLine("Невозможно посадить столько пассажиров, автобус переполнен");
                EnterPassenger(0);
            }
        
    }

    // Метод для высадки пассажиров
    public void ExitPassenger(int count)
    {
           Console.WriteLine("Введите количество вышедших пассажиров: ");
            count = int.Parse(Console.ReadLine());
            if (_passCount - count >= 0)
            {
                _passCount -= count;
               _weight =  _passCount * 70;
            }
            else
            {
                Console.WriteLine("Невозможно высадить больше пассажиров, в автобусе нет столько пассажиров");
                ExitPassenger(0);
            }
        
    }
    //Скорость
    protected override void SpeedDeterm() {
        while (true) {
            Console.Write("Введите с какой скоростью поедете: ");
            double speed = Convert.ToInt32(Console.ReadLine());
            speed -= speed * _kf;
            if (speed > 0) {
                if (speed <= 45) 
                {
                    _fuelRate = 12; return;
                } else if (speed > 46 && speed <= 100) 
                {
                    _fuelRate = 9; return;
                } 
                else if (speed > 101 && speed <= 180)
                {
                    _fuelRate = 12.5; return;
                } 
                else Console.WriteLine("Невозможно ехать с такой скоротью");
            } 
            else Console.WriteLine("Невозможно ехать с такой скоротью");
        }
    }
    // Езда
    protected override void Move() {
        while (true) {
            Console.WriteLine("Начальная остановка");
            EnterPassenger(0);
            Console.Write("Введите координаты до первой остановки(через пробел): ");
            MoveToEnd();
            ExitPassenger(0);
            EnterPassenger(0);
            Console.Write("Введите координаты до второй остановки(через пробел): ");
            MoveToEnd();
            ExitPassenger(0);
            EnterPassenger(0);
            Console.Write("Введите координаты до конечной остановки(через пробел): ");
            MoveToEnd();
            ExitPassenger(0);
            EnterPassenger(0);
            Console.Write("Хотите продолжить: +/-\n" + ">");
            if (Console.ReadLine() == "-")
                return;
        }
    }
}