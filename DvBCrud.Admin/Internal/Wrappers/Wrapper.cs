namespace DvBCrud.Admin.Internal.Wrappers;

public class Wrapper<T> : IWrapper
{
    public T Value { get; set; }

    public Wrapper(T value)
    {
        Value = value;
    }
}