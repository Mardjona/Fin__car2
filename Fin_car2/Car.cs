namespace Fin_car2;

internal class Avto
{
    protected string _number; //номер авто
    protected double _fuelCount; //количество топлива
    protected double _fuelMax; //максимум топлива
    protected double _fuelRate; //расход на 100 км
    protected double _sumDistance; //пробег
    protected int[] _corA = new int[] { 0, 0 }; //начальная координата
    protected int[] _corB; //конечная координата
    protected int _weight; //вес груза
    protected int _weightMax = 2000; //максимальный вес
    protected double _kf; //Коэффициент расхода топлива
    protected double Dist; // 

    public string Number
    {
        get { return _number; }
    }

    //Ввод информации
    public List<Avto> _acc = new List<Avto>(
    );

    public Avto()
    {

    }

    public Avto(int a)

    {
        Info();
    }

    protected void Info()
    {
        Console.Write("Введите номер машины: ");
        _number = Console.ReadLine();
        Console.Write("Введите объем бака: ");
        _fuelMax = Convert.ToInt32(Console.ReadLine());
        Console.Write("текущее количество топлива в бензобаке: ");
        _fuelCount = Convert.ToDouble(Console.ReadLine());
        /// AmountFuel = MaxBak;
        Console.Write("Введите расход топлива на 100км: ");
        _fuelRate = Convert.ToDouble(Console.ReadLine());
        if (_fuelCount > _fuelMax)
        {
            Console.WriteLine("Ошибка. Текущее значение не может превышать максимальное");
            return;
        }

        // _acc.Add(new Avto());
        Console.WriteLine("Авто добавлено");
    }

    //Вывод информации
    protected virtual void Out()
    {
        Console.WriteLine
        ($"---------ЛЕГКОВУШКА-------------\n" +
         $"Номер: {_number}\n" +
         $"Толпиво: {_fuelCount}\n" +
         $"Местоположение: {_corA[0]},{_corA[1]}\n" +
         $"Максимум топлива: {_fuelCount}\n+" +
         $"Суммарный пробег: {_sumDistance}\n" +
         "--------------------------------"
        );
    }

    //Заправка
    protected void Refill()
    {
        while (true)
        {
            Console.Write("Сколько топлива заправить: ");
            double top = Convert.ToInt32(Console.ReadLine());
            if (top >= 0)
                if (top + _fuelCount <= _fuelMax)
                {
                    _fuelCount += top;
                    return;
                }
                else Console.WriteLine("Невозможно заправить больше максимума, попробуйте еще раз");
            else Console.WriteLine("Неверный формат ввода, попробуйте еще раз");
        }
    }

    //Цикл езды
    protected virtual void Move()
    {
        SpeedDeterm();
        double dis = Distance();
        double prob = dis;
        while (true)
        {
            double rem = Remains(dis);
            if (rem <= _fuelCount)
            {
                _fuelCount -= rem;
                _corA = _corB;
                _sumDistance += prob;
                Console.WriteLine(
                    $"Вы проехали: {prob} км, топлива осталось: {_fuelCount} л, местоположение: {_corA[0]},{_corB[1]}");
                return;
            }
            else
            {
                Console.Write("Вам не хватило топлива, хотите заправиться: +/-\n" + ">");
                string ans = Console.ReadLine();
                if (ans == "+")
                {
                    dis -= _fuelCount / (_fuelRate / 100);
                    _fuelCount = 0;
                    Console.WriteLine($"Топливо кончилось, вы проехали: {prob - dis}км");
                    Refill();
                }
                else
                {
                    Console.WriteLine("Вы заглохли");
                    return;
                }
            }
        }
    }

    //Остаток топлива
    protected virtual void SpeedDeterm()
    {
        while (true)
        {
            Console.Write("Введите с какой скоростью поедете: ");
            int speed = Convert.ToInt32(Console.ReadLine());
            if (speed > 0)
            {
                if (speed <= 45)
                {
                    _fuelRate = 12;
                    return;
                }
                else if (speed > 46 && speed <= 100)
                {
                    _fuelRate = 9;
                    return;
                }
                else if (speed > 101 && speed <= 180)
                {
                    _fuelRate = 12.5;
                    return;
                }
                else Console.WriteLine("Невозможно ехать с такой скоротью");
            }
            else Console.WriteLine("Невозможно ехать с такой скоротью");
        }
    }

    protected double Remains(double dis) => Math.Round(_fuelRate / 100 * dis, 2);

    //Расчет дистанции 
    protected virtual double Distance()
    {
        while (true)
        {
            try
            {
                _corB = new int[2];
                Console.Write("Введите координаты ( через пробел ) : ");
                string[] s = Console.ReadLine().Split(',', ' ', ';');
                for (int i = 0; i < _corB.Length; i++)
                    _corB[i] = Int32.Parse(s[i]);
                double c = Math.Sqrt(Math.Pow(_corB[0] - _corA[0], 2) + Math.Pow(_corB[1] - _corA[1], 2));
                return Math.Round(c, 2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
    // Меню 

    //Пользовательский интерфейс
    public virtual void Menu(List<Avto> avto)
    {
        while (true)
        {
            Console.Write
            ("--------------------------------\n" +
             "Выберете необходимое действие:\n" +
             "1. Показать данные авто\n" +
             "2. Заправиться\n" +
             "3. Передвижение\n" +
             "4. Выход\n" +
             ">");
            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Out();
                    break;
                case 2:
                    Refill();
                    break;
                case 3:
                    Move();
                    break;
                case 4: return;
            }
        }
    }
}