namespace CSharp12;

using System.Collections;
using System.Runtime.CompilerServices;

[CollectionBuilder(typeof(UnderdevelopedBufferBuilder), nameof(UnderdevelopedBufferBuilder.Create))]
public class UnderdevelopedBuffer<T> : IEnumerable<T>
{
    private readonly T[] _values;

    public UnderdevelopedBuffer(ReadOnlySpan<T> span) => this._values = span.ToArray();

    public IEnumerator<T> GetEnumerator() => this._values.AsEnumerable().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}

public static class UnderdevelopedBufferBuilder
{
    public static UnderdevelopedBuffer<T> Create<T>(ReadOnlySpan<T> values) => new (values);
}