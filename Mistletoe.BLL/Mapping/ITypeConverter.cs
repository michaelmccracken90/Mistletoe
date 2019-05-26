namespace Mistletoe.BLL.Mapping
{
    using AutoMapper;

    public interface ITypeConverter<in TSource, TDestination>
    {
        TDestination Convert(TSource source, TDestination destination, ResolutionContext context);
    }
}
