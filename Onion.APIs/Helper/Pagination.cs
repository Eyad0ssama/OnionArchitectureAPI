namespace Onion.APIs.Helper
{
    public class Pagination<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public int Count { get; set; }
        public Pagination(int PageIndex, int PageSize, IReadOnlyList<T> Data,int Count)
        {
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
            this.Data = Data;
            this.Count = Count;

        }
        
    }
}
