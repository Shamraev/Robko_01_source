
namespace ArrayHelp
{
    public class ArrayHelper<T>
    {
        private T[] _arr;

        public T this[bool isStart]
        {

            get
            {
                if (isStart)
                    return _arr[0];
                else
                    return _arr[_arr.Length - 1];
            }
            set
            {
                if (isStart)
                    _arr[0] = value;
                else
                    _arr[_arr.Length - 1] = value;
            }
        }
        public T this[int index]
        {
            get { return _arr[index]; }
            set { _arr[index] = value; }
        }
        public ArrayHelper(int Count)
        {
            _arr = new T[Count];
        }

    }

}
