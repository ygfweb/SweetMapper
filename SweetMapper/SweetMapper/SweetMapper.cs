using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace SweetMapper
{
    public static class SweetMapper<TSource, TTarget> where TSource : class where TTarget : class, new()
    {
        private static readonly Func<TSource, TTarget> cache = GetFunc();
        private static MapperConfig<TSource, TTarget> MapperConfig { get; set; }
        private static Func<TSource, TTarget> GetFunc()
        {
            var sourceType = typeof(TSource);
            var targetType = typeof(TTarget);
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TSource), "p");
            List<MemberBinding> memberBindingList = new List<MemberBinding>();
            var targetTypes = targetType.GetProperties().Where(x => x.PropertyType.IsPublic && x.CanWrite);
            foreach (var targetItem in targetTypes)
            {
                var sourceItem = sourceType.GetProperty(targetItem.Name);
                if (sourceItem == null || !sourceItem.CanRead || sourceItem.PropertyType.IsNotPublic)
                {
                    continue;
                }
                if (sourceItem.PropertyType != targetItem.PropertyType)
                {
                    continue;
                }

                MemberExpression property = Expression.Property(parameterExpression, sourceType.GetProperty(targetItem.Name));
                MemberBinding memberBinding = Expression.Bind(targetItem, property);
                memberBindingList.Add(memberBinding);
            }

            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(targetType), memberBindingList.ToArray());
            Expression<Func<TSource, TTarget>> lambda = Expression.Lambda<Func<TSource, TTarget>>(memberInitExpression, new ParameterExpression[] { parameterExpression });
            return lambda.Compile();
        }

        public static TTarget Map(TSource source)
        {
            if (source == null)
            {
                return null;
            }
            else
            {
                if (MapperConfig == null)
                {
                    return cache(source);
                }
                else
                {
                    if (MapperConfig.IsDisableAutoMppaer)
                    {
                        TTarget target = new TTarget();
                        MapperConfig.action.Invoke(source, target);
                        return target;
                    }
                    else
                    {
                        TTarget target = cache(source);
                        MapperConfig.action(source, target);
                        return target;
                    }
                }
            }           
        }

        public static void SetConfig(Action<TSource, TTarget> action, bool isDisableAutoMapper = false)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            MapperConfig = new MapperConfig<TSource, TTarget>(action, isDisableAutoMapper);
        }

        public static void ClearConfig()
        {
            MapperConfig = null;
        }

        public static List<TTarget> MapList(List<TSource> sources)
        {
            if (sources == null)
            {
                return null;
            }
            else
            {
                List<TTarget> targets = new List<TTarget>();
                foreach (var item in sources)
                {
                    TTarget target = Map(item);
                    targets.Add(target);
                }
                return targets;
            }
        }

        public static TTarget[] MapArray(TSource[] sources)
        {
            if (sources == null)
            {
                return null;
            }
            else
            {
                TTarget[] targets = new TTarget[sources.Length];
                for (int i = 0; i < sources.Length; i++)
                {
                    TTarget target = Map(sources[i]);
                    targets[i] = target;                   
                }
                return targets;
            }
        }
    }
}




