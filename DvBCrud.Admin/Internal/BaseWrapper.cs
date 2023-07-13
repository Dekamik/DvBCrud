namespace DvBCrud.Admin.Internal;

public abstract class BaseWrapper<T> : IWrapper
{
    public T Value { get; set; }

    protected BaseWrapper(T value)
    {
        Value = value;
    }
}