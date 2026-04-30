namespace Trip.Application.Dtos.Pagination;

public class PaginationDto<T>(int totalCount, int currentPage, int pageSize, List<T> pageItems) : List<T>(pageItems)
{
    public int CurrentPage { get; set; } = currentPage;

    public int PageSize { get; set; } = pageSize;

    public int TotalPages { get; set; } = (int)Math.Ceiling(totalCount / (double)pageSize);

    public int TotalCount { get; set; } = totalCount;

    public bool HasPrevious => CurrentPage > 1;

    public bool HasNext => CurrentPage < TotalPages; 
}