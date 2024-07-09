using iCubeTrain.Models;
using Mapster;

namespace iCubeTrain.Configurations
{
    public static class MappingConfig
    {
        public static void RegisterMapping()
        {
            TypeAdapterConfig<TagValueData, TagValue>.NewConfig()
            // .Map(dest => dest.TagValueId, src => src.TagValueId)
            .Map(dest => dest.Value, src => src.Value)
            .Map(dest => dest.Quality, src => src.Quality)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.TimeStamp, src => src.TimeStamp);

            TypeAdapterConfig<IncomingData, TransformedData>.NewConfig()
                .Map(dest => dest.Data, src => src.Data
                    .GroupBy(d => d.Tagname.ToString())
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(d => d.Adapt<TagValue>()).ToList()
                    ));
        }
    }
}