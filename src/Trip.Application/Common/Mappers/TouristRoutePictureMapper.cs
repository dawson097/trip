using Mapster;
using Trip.Application.Dtos.TouristRoutePicture;
using Trip.Domain.Entities;

namespace Trip.Application.Common.Mappers;

/// <summary>
/// 旅游路线图片映射
/// </summary>
public class TouristRoutePictureMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TouristRoutePicture, TouristRoutePictureDto>();

        config.NewConfig<TouristRoutePictureCreateDto, TouristRoutePicture>();

        config.NewConfig<TouristRoutePictureUpdateDto, TouristRoutePicture>();
    }
}