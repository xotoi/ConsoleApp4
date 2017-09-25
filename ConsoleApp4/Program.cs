using System;
using System.Collections.Generic;
using System.Collections;
using ConsoleApp4;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList list = new LinkedList();
            list.AddLast(new ListItem("1"));
            list.AddLast(new ListItem("2"));
            list.AddLast(new ListItem("3"));
            list.AddLast(new ListItem("4"));

            //list.AddFirst(new ListItem("1"));
            //list.AddFirst(new ListItem("2"));
            //list.AddFirst(new ListItem("3"));
            //list.AddFirst(new ListItem("4"));

            list.Insert(new ListItem("1"), 10);
            list.Insert(new ListItem("1"), 2);

            list.Reverse();
            foreach (var item in list.GetAll())
            {
                Console.WriteLine(item);
                Console.ReadKey();
            }

            Console.ReadLine();
        }
    }
}
//прямой связанный список
public class LinkedList : ILinkedList
{
    private IUpgradedListItem first;
    private IUpgradedListItem last;

    //добавить элемент в начало
    public void AddFirst(IListItem item)
    {
        var tempItem = new ListItem(item.Value, null, first);
        if (last == null)
        {
            last = tempItem;
        }

        if (first == null)
        {
            first = tempItem;
        }
        else
        {
            first.SetPrev(tempItem);
            first = tempItem;
        }
    }

    //добавить элемент в конец
    public void AddLast(IListItem item)
    {
        var tempItem = new ListItem(item.Value, last);
        if (first == null)
        {
            first = tempItem;
        }

        if (last == null)
        {
            last = tempItem;
        }
        else
        {
            last.SetNext(tempItem);
            last = tempItem;
        }
    }

    //вставить элемент перед элементом с указанным индексом
    //если элемента нет - вставить в конец
    public void Insert(IListItem item, int index)
    {
        if (index < 0)
        {
            return;
        }

        var listItem = first;
        int i = 0;
        while(i != index)
        {
            i++;
            listItem = (IUpgradedListItem)listItem?.Next();

            if (listItem == null)
            {
                this.AddLast(item);
                return;
            }
        }

        var temp = (IUpgradedListItem)listItem.Prev();
        listItem.SetPrev(item);
        temp.SetNext(item);
        ((IUpgradedListItem)item).SetNext(listItem);
        ((IUpgradedListItem)item).SetPrev(temp);
    }

    //проверка есть ли элементы в списке
    public bool IsEmpty()
    {
        return first == null;
    }

    //вернуть первый элемент в списке
    public IListItem GetFirstItem()
    {
        return first;
    }

    //вернуть все элементы списка, кроме первого
    public IEnumerable<IListItem> GetAll()
    {
        var item = first;
        while (item != null)
        {
            yield return item;
            item = (IUpgradedListItem)item.Next();
        }
    }

    //очистить список
    public void Clear()
    {
        first = null;
        last = null;
    }

    public void Reverse()
    {
        var start = first;
        last = first;

        while (start != null)
        {
            var temp = (IUpgradedListItem)start.Next();
            start.SetNext(start.Prev());
            start.SetPrev(temp);

            if (start.Prev() == null)
            {
                this.first = start;
            }

            start = (IUpgradedListItem)start.Prev();
        }
    }
}

//элемент связанного списка
public class ListItem : IUpgradedListItem
{
    private IListItem prev;
    private IListItem next;

    //хранимое значение
    public object Value { get; }

    public ListItem(object obj, IListItem prev = null, IListItem next = null)
    {
        //логика инициализации
        this.Value = obj;
        this.prev = prev;
        this.next = next;
    }

    //предыдущий связанный элемент списка
    public IListItem Prev()
    {
        return prev;
    }

    //следующий связанный элемент списка
    public IListItem Next()
    {
        return next;
    }

    public void SetPrev(IListItem prev)
    {
        this.prev = prev;
    }

    public void SetNext(IListItem next)
    {
        this.next = next;
    }

    public override string ToString()
    {
        return this.Value.ToString();
    }
}

public interface IUpgradedListItem : IListItem
{
    void SetPrev(IListItem prev);
    void SetNext(IListItem next);
}