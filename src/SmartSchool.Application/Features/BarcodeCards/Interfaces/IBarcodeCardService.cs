using SmartSchool.Application.Features.BarcodeCards.Contracts;

namespace SmartSchool.Application.Features.BarcodeCards.Interfaces;

public interface IBarcodeCardService
{
    Task<PagedBarcodeCardResponse> GetPagedAsync(
        BarcodeCardFilterRequest request);

    Task<BarcodeCardDto> GetByIdAsync(Guid id);

    Task<Guid> CreateAsync(
        CreateBarcodeCardRequest request);

    Task UpdateAsync(
        Guid id,
        UpdateBarcodeCardRequest request);

    Task DeleteAsync(Guid id);
}