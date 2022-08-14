using System;
using System.Collections.Generic;
using System.Linq;

namespace ECA.CBF.Demo.Util;

public static class EnumerableUtil
{
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> lista)
    {
        return lista == null || !lista.Any();
    }
    public static void AddNonNullOrEmptyRange<T>(this List<T> listaOriginal, IEnumerable<T> listaAdicional)
    {
        if (listaOriginal != null && !listaAdicional.IsNullOrEmpty())
        {
            listaOriginal.AddRange(listaAdicional);
        }
    }

}
