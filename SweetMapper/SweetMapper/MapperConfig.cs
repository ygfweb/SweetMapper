using System;
using System.Collections.Generic;
using System.Text;

namespace SweetMapper
{
    internal class MapperConfig<TSource, TTarget> where TSource : class where TTarget : class, new()
    {
        public Action<TSource, TTarget> action { get; private set; }

        public bool IsDisableAutoMppaer { get; private set; } = false;

        public MapperConfig(Action<TSource, TTarget> action, bool isDisableAutoMppaer = false)
        {
            this.action = action;
            IsDisableAutoMppaer = isDisableAutoMppaer;
        }
    }
}
