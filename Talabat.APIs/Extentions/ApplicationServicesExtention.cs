using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Helpers;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;
using Talabat.Repository;
using Microsoft.EntityFrameworkCore;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection addExtentionServices(this IServiceCollection Services)
        {

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); //controller dependency Injection

            //builder.Services.AddAutoMapper(m=>m.AddProfile(new MappingProfiles()));   Auto Mapper
            Services.AddAutoMapper(typeof(MappingProfiles));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    //model State => dictunary [Key Value Pair]
                    //Key - name
                    //Value - Error
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(e => e.ErrorMessage)
                                                         .ToArray();

                    var ValidationErrorResponce = new ApiValidationErrorResponce()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponce);

                };

            });
            return Services;

        }
    }
}
