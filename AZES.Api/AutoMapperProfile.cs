using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRIT.Models.DtoEntities.Base;
using BRIT.Models.Entities.Base;
using BRIT.Models.Entities;
using BRIT.Models.DtoEntities;
using BRIT.Models.DtoEntities.DtoWhithId;
using System.Reflection;
using BRIT.Dal.Exceptions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AZES.Api
{
    
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            Type baseEntities = typeof(BaseEntities);
            Type baseDtoEntities = typeof(BaseDtoEntities);
            Type baseDtoEntitiesWithId = typeof(BaseDtoEntitiesWithId);
            

            var mapEntityAndDtos = GetAllMaps(baseEntities, baseDtoEntities);
            //Creating all maps
            foreach (var mapEntityAndDto in mapEntityAndDtos)
            {
                CreateMap(mapEntityAndDto.Value.source, mapEntityAndDto.Value.destination);
                CreateMap(mapEntityAndDto.Value.destination, mapEntityAndDto.Value.source);
            }


            var mapEntityAndDtoWithIds = GetAllMaps(baseEntities, baseDtoEntitiesWithId);
            //Creating all maps
            foreach (var mapEntityAndDtoWithId in mapEntityAndDtoWithIds)
            {
                CreateMap(mapEntityAndDtoWithId.Value.source, mapEntityAndDtoWithId.Value.destination);
                CreateMap(mapEntityAndDtoWithId.Value.destination, mapEntityAndDtoWithId.Value.source);
            }
            
        }

        private IEnumerable<Type> GetAllInheritors(Type baseType)
        {
            Type type = baseType;
            IEnumerable<Type>? list = Assembly.GetAssembly(type)?.GetTypes()?.Where(t => t.IsSubclassOf(baseType)) ?? null;
            return list = list != null ? list : throw new CustomException($"An error occurred: Type: {nameof(type)} is null.\n");
        }

        private Dictionary<object, (Type source, Type destination)> GetAllMaps(Type baseSourceType, Type baseDestinationType)
        {
            Dictionary<object, (Type x, Type y)> dictionary = new Dictionary<object, (Type x, Type y)>();
            
            IEnumerable<Type> baseSourceTypelist = GetAllInheritors(baseSourceType);
            IEnumerable<Type> baseDestinationTypelist = GetAllInheritors(baseDestinationType);

            foreach (var sourceType in baseSourceTypelist)
            {
                foreach (var destinationType in baseDestinationTypelist)
                {
                    if (destinationType.Name.ToString().ToLower().Contains(sourceType.Name.ToString().ToLower()))
                    {
                        dictionary.Add(new object(), (sourceType, destinationType));
                        Console.WriteLine($"{sourceType} : {destinationType}");
                    }
                }
            }
            return dictionary;
        }
    }

    /*
        public class AutoMapperProfile : Profile
        {

            public AutoMapperProfile()
            {
                CreateMap<Angestellte, AngestellteDto>();
                CreateMap<Arbeitsandauer, ArbeitsandauerDto>();
                CreateMap<Arbeitsort, ArbeitsortDto>();
                CreateMap<Arbeitszeiterfassung, ArbeitszeiterfassungDto>();
                CreateMap<Fundort, FundortDto>();
                CreateMap<Hausanschrift, HausanschriftDto>();
                CreateMap<Kennwort, KennwortDto>();
                CreateMap<Rolle, RolleDto>();
                CreateMap<Stadt, StadtDto>();


                CreateMap<AngestellteDto, Angestellte>();
                CreateMap<ArbeitsandauerDto, Arbeitsandauer>();
                CreateMap<ArbeitsortDto, Arbeitsort>();
                CreateMap<ArbeitszeiterfassungDto, Arbeitszeiterfassung>();
                CreateMap<Fundort, FundortDto>();
                CreateMap<HausanschriftDto, Hausanschrift>();
                CreateMap<KennwortDto, Kennwort>();
                CreateMap<RolleDto, Rolle>();
                CreateMap<StadtDto, Stadt>();


                CreateMap<StadtDtoWithId, Stadt>();
                CreateMap<Stadt, StadtDtoWithId>();


            }
        }
            */

}
