using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Application.Common.Behaviours;
using SchoolSaas.Application.Common.Commands;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Helpers;
using System.Linq.Dynamic.Core;
using System.Reflection;

namespace SchoolSaas.Application.Common
{
    public static class CommonDependencyInjection
    {
        public static IServiceCollection AddCommonApplication(this IServiceCollection services)
        {
            var assembly = typeof(CommonDependencyInjection).Assembly;

            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);
           
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddMediatorAuthorization(assembly);
            //services.AddAuthorizersFromAssembly(assembly);
            //services.AddSignalR().AddJsonProtocol(options =>
            //{
            //    options.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            //});
            return services;
        }
       
        public static IServiceCollection AddGenericRequestHandlers(this IServiceCollection services, Type contextType, Type readOnlyContextType, Type entityType)
        {
            var method1 = typeof(CommonDependencyInjection).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(CommonDependencyInjection.RegisterGenericRequestHandlers));

            var method2 = typeof(CommonDependencyInjection).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(CommonDependencyInjection.RegisterGenericReadOnlyRequestHandlers));

            var method3 = typeof(CommonDependencyInjection).GetTypeInfo().DeclaredMethods
                .Single(m => m.Name == nameof(CommonDependencyInjection.RegisterGenericTitledMultiLingualReadOnlyRequestHandlers));


            var entities = Assembly.GetAssembly(entityType)?.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Namespace == entityType.Namespace)
                .ToList();

            if (entities != null)
            {
                foreach (var type in entities)
                    if (type.IsBaseEntity(out var t))
                    {
                        method1.MakeGenericMethod(contextType, type, t).Invoke(null, new[] { services });
                        method2.MakeGenericMethod(readOnlyContextType, type, t).Invoke(null, new[] { services });
                    }
            }

            var titledEntities = Assembly.GetAssembly(entityType)?.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(TitledMultiLingualEntity<,>))
                .ToList();

            if (titledEntities != null)
            {
                foreach (var type in titledEntities)
                    if (type.IsTitledMultiLingualEntity(out var traslationType, out var idType))
                    {
                        method3.MakeGenericMethod(readOnlyContextType, type, traslationType, idType).Invoke(null, new[] { services });
                    }
            }

            return services;
        }

        public static void RegisterGenericRequestHandlers<TContext, TEntity, T>(IServiceCollection services)
            where TEntity : BaseEntity<T>
            where TContext : IContext
        {
            services.AddTransient(typeof(IRequestHandler<CreateCommand<TContext, TEntity, T>, TEntity>),
                typeof(CreateCommandHandler<CreateCommand<TContext, TEntity, T>, TContext, TEntity, T>));
            //services.AddTransient(typeof(IRequestHandler<UpdateCommand<TContext, TEntity, T>>),
            //    typeof(UpdateCommandHandler<UpdateCommand<TContext, TEntity, T>, TContext, TEntity, T>));

            //services.AddTransient(typeof(IRequestHandler<DeleteCommand<TContext, TEntity, T>, Unit>),
            //    typeof(DeleteCommandHandler<DeleteCommand<TContext, TEntity, T>, TContext, TEntity, T>));
        }


        public static void RegisterGenericReadOnlyRequestHandlers<TContext, TEntity, T>(IServiceCollection services)
            where TEntity : BaseEntity<T>
            where TContext : IReadOnlyContext
        {
            //services.AddTransient(typeof(IRequestHandler<GetByIdQuery<TContext, TEntity, T>, TEntity>),
            //    typeof(GetByIdQueryHandler<GetByIdQuery<TContext, TEntity, T>, TContext, TEntity, T>));

            //services.AddTransient(typeof(IRequestHandler<GetPagedQuery<TContext, TEntity, T>, PagedResult<TEntity>>),
            //    typeof(GetPagedQueryHandler<GetPagedQuery<TContext, TEntity, T>, TContext, TEntity, T>));
        }

        public static void RegisterGenericTitledMultiLingualReadOnlyRequestHandlers<TContext, TEntity, TTranslation, T>(IServiceCollection services)
            where TEntity : TitledMultiLingualEntity<TTranslation, T>
            where TTranslation : TitledEntityTranslation<TEntity, T>
            where TContext : IReadOnlyContext
        {
            //services.AddTransient(typeof(IRequestHandler<TitledMultiLingualGetByIdQuery<TContext, TEntity, TTranslation, T>, TEntity>),
            //    typeof(TitledMultiLingualGetByIdQueryHandler<TitledMultiLingualGetByIdQuery<TContext, TEntity, TTranslation, T>, TContext, TEntity, TTranslation, T>));

            //services.AddTransient(typeof(IRequestHandler<TitledMultiLingualGetPagedQuery<TContext, TEntity, TTranslation, T>, PagedResult<TEntity>>),
            //    typeof(TitledMultiLingualGetPagedQueryHandler<TitledMultiLingualGetPagedQuery<TContext, TEntity, TTranslation, T>, TContext, TEntity, TTranslation, T>));
        }
    }
}