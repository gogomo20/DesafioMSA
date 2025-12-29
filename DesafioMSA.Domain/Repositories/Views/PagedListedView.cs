namespace DesafioMSA.Domain.Repositories.Views
{
    public class PagedListedView<T>
    {
        public int Page { get; init; }
        public int Size { get; init; }
        public int TotalRegisters { get; init; }
        public required ICollection<T> Data { get; init; }

    }
}
