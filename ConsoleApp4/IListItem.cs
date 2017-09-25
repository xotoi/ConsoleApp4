using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    public interface IListItem //элемент списка
    {
        IListItem Prev(); //предыдущий элемент
        IListItem Next(); //следующий элемент
        object Value { get; } //значение, хранимое в элементе
    }

    
}
