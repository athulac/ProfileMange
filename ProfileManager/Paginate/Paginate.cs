namespace ProfileManager.Paginate
{
    public class Paginate<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public List<T>? Data { get; set; }
    }
}
