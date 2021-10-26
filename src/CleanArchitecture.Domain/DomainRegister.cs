using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;

using Mapster;

namespace CleanArchitecture.Application;

public class DomainRegister : ICodeGenerationRegister
{
    public void Register(CodeGenerationConfig config)
    {
        config.AdaptTo("[name]Dto", MapType.Map | MapType.MapToTarget | MapType.Projection)
            .ApplyDefaultRule();

        config.AdaptFrom("[name]Add", MapType.Map)
            .ApplyDefaultRule()
            .IgnoreNoModifyProperties();

        config.AdaptFrom("[name]Update", MapType.MapToTarget)
            .ApplyDefaultRule()
            .IgnoreNoModifyProperties();

        config.AdaptFrom("[name]Merge", MapType.MapToTarget)
            .ApplyDefaultRule()
            .IgnoreNoModifyProperties()
            .IgnoreNullValues(true);

        config.GenerateMapper("[name]Mapper")
            .ForAllTypesInNamespace(typeof(AppEntity).Assembly, "CleanArchitecture.Domain.Entities");

    }
}

static class RegisterExtensions
{
    public static AdaptAttributeBuilder ApplyDefaultRule(this AdaptAttributeBuilder builder)
    {
        return builder
            .ForAllTypesInNamespace(typeof(AppEntity).Assembly, "CleanArchitecture.Domain.Entities")
            .ExcludeTypes(type => type.IsEnum)
            .AlterType(type => type.IsEnum || Nullable.GetUnderlyingType(type)?.IsEnum == true, typeof(string))
            .MaxDepth(2)
            .ShallowCopyForSameType(true)
            .ForType<TodoItem>(cfg => cfg.Ignore(m => m.List).Ignore(m => m.ListId));
    }

    public static AdaptAttributeBuilder IgnoreNoModifyProperties(this AdaptAttributeBuilder builder)
    {
        return builder
            .ForType<TodoItem>(cfg => cfg
                .IgnoreEntity()
                .Ignore(m => m.List)
                .Ignore(m => m.ListId)
            )
            .ForType<TodoList>(cfg => cfg
                .IgnoreEntity()
                .Ignore(m => m.Items)
            );
    }

    public static PropertySettingBuilder<T> IgnoreEntity<T>(this PropertySettingBuilder<T> builder) where T : AppEntity
    {
        return builder.Ignore(m => m.Id)
            .Ignore(m => m.CreationDate);
    }
}