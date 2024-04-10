namespace CSharp12;

using System.Collections;

public class UnderdevelopedCollection<T> : IEnumerable<T>
{
    private readonly List<T> _list = new ();

    public UnderdevelopedCollection()
    {
        
    }
    
    public void Add(T number) => this._list.Add(number);

    public IEnumerator<T> GetEnumerator() => this._list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}