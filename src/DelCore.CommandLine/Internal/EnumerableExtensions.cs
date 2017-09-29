#region Copyright 2017 Shane Delany

// Filename:  EnumerableExtensions.cs
// Modified:  30/09/2017

#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace DelCore.CommandLine.Internal
{
    internal static class EnumerableExtensions
    {
        #region Methods

        public static IEnumerable<T> AsReadOnly<T>(this IEnumerable<T> enumerable) => AsReadOnlyCollection(enumerable).AsEnumerable();

        public static async Task<IEnumerable<T>> AsReadOnlyAsync<T>(this IEnumerable<T> enumerable) => (await AsReadOnlyCollectionAsync(enumerable)).AsEnumerable();

        public static ReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T> enumerable) => new ReadOnlyCollection<T>(enumerable.ToList());

        public static async Task<ReadOnlyCollection<T>> AsReadOnlyCollectionAsync<T>(this IEnumerable<T> enumerable) => new ReadOnlyCollection<T>(await Task.Run(() => enumerable.ToList()));

        #endregion
    }
}
