using System.ComponentModel.DataAnnotations;
using Trip.Application.Dtos.TouristRoute;

namespace Trip.Application.Common.ValidationAttributes;

/// <summary>
/// 标题和描述一致性校验
/// </summary>
public class TitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var routeDto = (TouristRouteManipulationDto)validationContext.ObjectInstance;

        if (routeDto.Title == routeDto.Description)
        {
            return new ValidationResult("标题必须和描述不一致", ["TouristRouteManipulationDto"]);
        }

        return ValidationResult.Success;
    }
}