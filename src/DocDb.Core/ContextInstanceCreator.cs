using System;
using System.Linq;
using System.Reflection;
using DocDb.Core.Abstracts;
using DocDb.Core.DI.Extensions;
using DocDb.Core.Extensions;

namespace DocDb.Core
{
    internal static class ContextInstanceCreator<TContext> where TContext : DocumentDbContext
    {
        private static readonly ConstructorInfo ActionCtor;
        private static readonly ConstructorInfo OptionCtor;

        static ContextInstanceCreator()
        {
            ConstructorInfo[] ctors = typeof(TContext).GetConstructors();
            ActionCtor = GetConstructor(ctors, p => p.ParameterType == typeof(Action<DocumentDbOptionsBuilder>));

            if (ActionCtor.IsNull())
            {
                OptionCtor = GetConstructor(ctors, p => p.ParameterType == typeof(IDocumentDbOptions));
            }
        }

        private static ConstructorInfo GetConstructor(ConstructorInfo[] ctors, Func<ParameterInfo, bool> func)
        {
            return ctors.FirstOrDefault(ct =>
                ct.GetParameters().FirstOrDefault(func) != null);
        }

        public static TContext CreateContextInstance(IServiceProvider serviceProvider, Action<DocumentDbOptionsBuilder> builderFunc)
        {
            if (ActionCtor.IsNotNull())
            {
                return (TContext) serviceProvider.CreateInstance(ActionCtor, typeof(TContext), builderFunc);
            }
            else if (OptionCtor.IsNotNull())
            {
                var builder = new DocumentDbOptionsBuilder();

                builderFunc(builder);

                var options = builder.DocumentDbOptions;

                return (TContext)serviceProvider.CreateInstance(OptionCtor, typeof(TContext), options);
            }

            throw new Exception();
        }
    }
}
