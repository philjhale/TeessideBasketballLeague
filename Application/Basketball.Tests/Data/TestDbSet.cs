using System.Data.Entity;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.ObjectModel;
using System.Reflection;

public class TestDbSet<T> : IDbSet<T> where T : class
{
    private HashSet<T> _data;

    public TestDbSet()
    {
        _data = new HashSet<T>();
    }

    /// <summary>
    /// Overwridden Find method so it's done in memory using reflection
    /// </summary>
    /// <param name="keyValues"></param>
    /// <returns></returns>
    public virtual T Find(params object[] keyValues)
    {
        Console.WriteLine("Start");
        //throw new NotImplementedException();
        if (keyValues.Length > 1)
            throw new ArgumentException("TestDbSet.Find - Cannot pass more than one argument");

        int key;
        if (!int.TryParse(keyValues[0].ToString(), out key))
            throw new ArgumentException("TestDbset.Find - keyValues is not an int: " + keyValues[0]);

        Console.WriteLine("key = " + key);

        foreach (T someType in _data)
        {
            Console.WriteLine("In loop: " + someType);
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                Console.WriteLine("prop name: " + property.Name);

                //if (property.Name == "Id")
                //    Console.WriteLine("is id " + property.GetValue(someType, null));

                //if ((int)property.GetValue(someType, null) == key)
                //    Console.WriteLine("equals id");

                if (property.Name == "Id" && (int)property.GetValue(someType, null) == key)
                    return someType;

                    //return property.GetValue(this, null) as IDbSet<T>;
            }
        }

        return null;
    }

    public T Add(T item)
    {
        _data.Add(item);
        return item;
    }

    public T Remove(T item)
    {
        _data.Remove(item);
        return item;
    }

    public T Attach(T item)
    {
        _data.Add(item);
        return item;
    }

    public void Detach(T item)
    {
        _data.Remove(item);
    }

    Type IQueryable.ElementType
    {
        get { return _data.AsQueryable().ElementType; }
    }

    Expression IQueryable.Expression
    {
        get { return _data.AsQueryable().Expression; }
    }

    IQueryProvider IQueryable.Provider
    {
        get { return _data.AsQueryable().Provider; }
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    public T Create()
    {
        return Activator.CreateInstance<T>();
    }

    public ObservableCollection<T> Local
    {
        get
        {
            return new ObservableCollection<T>(_data);
        }
    }

    public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
    {
        return Activator.CreateInstance<TDerivedEntity>();
    }
}
