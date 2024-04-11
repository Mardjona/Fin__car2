using Fin_car2;

Store avtoStore = new Store();

while (true)
{
    Avto schAvto = avtoStore.AccMenu();
    schAvto.Menu(avtoStore.Acc);
}

