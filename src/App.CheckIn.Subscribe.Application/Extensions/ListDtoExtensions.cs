using System;
using System.Collections.Generic;

namespace Tnf.Dto
{
    public static class ListDtoExtensions
    {
        public static IListDto<TTarget> Map<TTarget, TSource>(this IListDto<TSource> source, Func<TSource, TTarget> mapper)
        {
            var listDto = new ListDto<TTarget>();

            var items = new List<TTarget>(source.Items.Count);
            foreach (var item in source.Items)
            {
                items.Add(mapper(item));
            }

            listDto.Items = items;
            listDto.HasNext = source.HasNext;

            return listDto;
        }
    }
}
