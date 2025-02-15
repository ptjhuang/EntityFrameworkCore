// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

// ReSharper disable AccessToDisposedClosure
// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Query
{
    public abstract class QueryNoClientEvalTestBase<TFixture> : IClassFixture<TFixture>
        where TFixture : NorthwindQueryRelationalFixture<NoopModelCustomizer>, new()
    {
        protected QueryNoClientEvalTestBase(TFixture fixture) => Fixture = fixture;

        protected TFixture Fixture { get; }

        [ConditionalFact]
        public virtual void Throws_when_where()
        {
            using var context = CreateContext();
            AssertTranslationFailed(() => context.Customers.Where(c => c.IsLondon).ToList());
        }

        [ConditionalFact]
        public virtual void Throws_when_orderby()
        {
            using var context = CreateContext();
            AssertTranslationFailed(() => context.Customers.OrderBy(c => c.IsLondon).ToList());
        }

        [ConditionalFact]
        public virtual void Throws_when_orderby_multiple()
        {
            using var context = CreateContext();
            AssertTranslationFailed(
                () => context.Customers
                    .OrderBy(c => c.IsLondon)
                    .ThenBy(c => ClientMethod(c))
                    .ToList());
        }

        private static object ClientMethod(object o) => o.GetHashCode();

        [ConditionalFact]
        public virtual void Throws_when_where_subquery_correlated()
        {
            using var context = CreateContext();
            AssertTranslationFailed(
                () => context.Customers
                    .Where(c1 => context.Customers.Any(c2 => c1.CustomerID == c2.CustomerID && c2.IsLondon))
                    .ToList());
        }

        [ConditionalFact]
        public virtual void Throws_when_all()
        {
            using var context = CreateContext();
            AssertTranslationFailed(() => context.Customers.All(c => c.IsLondon));
        }

        [ConditionalFact]
        public virtual void Throws_when_from_sql_composed()
        {
            using var context = CreateContext();
            AssertTranslationFailed(
                () => context.Customers
                    .FromSqlRaw(NormalizeDelimitersInRawString("select * from [Customers]"))
                    .Where(c => c.IsLondon)
                    .ToList());
        }

        [ConditionalFact]
        public virtual void Doesnt_throw_when_from_sql_not_composed()
        {
            using var context = CreateContext();
            var customers
                = context.Customers
                    .FromSqlRaw(NormalizeDelimitersInRawString("select * from [Customers]"))
                    .ToList();

            Assert.Equal(91, customers.Count);
        }

        [ConditionalFact]
        public virtual void Throws_when_subquery_main_from_clause()
        {
            using var context = CreateContext();
            AssertTranslationFailed(
                () => (from c1 in context.Customers
                           .Where(c => c.IsLondon)
                           .OrderBy(c => c.CustomerID)
                           .Take(5)
                       select c1)
                    .ToList());
        }

        [ConditionalFact]
        public virtual void Throws_when_select_many()
        {
            using var context = CreateContext();
            Assert.Equal(
                CoreStrings.QueryFailed(
                    "c1 => int[] { 1, 2, 3, }",
                    "NavigationExpandingExpressionVisitor"),
                Assert.Throws<InvalidOperationException>(
                    () => (from c1 in context.Customers
                           from i in new[] { 1, 2, 3 }
                           select c1)
                        .ToList()).Message);
        }

        [ConditionalFact]
        public virtual void Throws_when_join()
        {
            using var context = CreateContext();
            var message = Assert.Throws<InvalidOperationException>(
                () => (from e1 in context.Employees
                       join i in new uint[] { 1, 2, 3 } on e1.EmployeeID equals i
                       select e1)
                    .ToList()).Message;

            Assert.Equal(
                CoreStrings.QueryFailed(
                    @"DbSet<Employee>
    .Join(
        inner: __p_0, 
        outerKeySelector: e1 => e1.EmployeeID, 
        innerKeySelector: i => i, 
        resultSelector: (e1, i) => e1)",
                    "NavigationExpandingExpressionVisitor"),
                message, ignoreLineEndingDifferences: true);
        }

        [ConditionalFact]
        public virtual void Throws_when_group_join()
        {
            using var context = CreateContext();
            var message = Assert.Throws<InvalidOperationException>(
                () => (from e1 in context.Employees
                       join i in new uint[] { 1, 2, 3 } on e1.EmployeeID equals i into g
                       select e1)
                    .ToList()).Message;

            Assert.Equal(
                CoreStrings.QueryFailed(
                    @"DbSet<Employee>
    .GroupJoin(
        inner: __p_0, 
        outerKeySelector: e1 => e1.EmployeeID, 
        innerKeySelector: i => i, 
        resultSelector: (e1, g) => e1)",
                    "NavigationExpandingExpressionVisitor"),
                message, ignoreLineEndingDifferences: true);
        }

        [ConditionalFact(Skip = "Issue#17068")]
        public virtual void Throws_when_group_by()
        {
            using var context = CreateContext();
            context.Customers
                .GroupBy(c => c.CustomerID)
                .ToList();

            AssertTranslationFailed(
                () => context.Customers
                    .GroupBy(c => c.CustomerID)
                    .ToList());
        }

        [ConditionalFact]
        public virtual void Throws_when_first()
        {
            using var context = CreateContext();
            AssertTranslationFailed(() => context.Customers.First(c => c.IsLondon));
        }

        [ConditionalFact]
        public virtual void Throws_when_single()
        {
            using var context = CreateContext();
            AssertTranslationFailed(() => context.Customers.Single(c => c.IsLondon));
        }

        [ConditionalFact]
        public virtual void Throws_when_first_or_default()
        {
            using var context = CreateContext();
            AssertTranslationFailed(() => context.Customers.FirstOrDefault(c => c.IsLondon));
        }

        [ConditionalFact]
        public virtual void Throws_when_single_or_default()
        {
            using var context = CreateContext();
            AssertTranslationFailed(() => context.Customers.SingleOrDefault(c => c.IsLondon));
        }

        private string NormalizeDelimitersInRawString(string sql)
            => Fixture.TestStore.NormalizeDelimitersInRawString(sql);

        private void AssertTranslationFailed(Action testCode)
        {
            Assert.Contains(
                CoreStrings.TranslationFailed("").Substring(21),
                Assert.Throws<InvalidOperationException>(testCode).Message);
        }

        protected NorthwindContext CreateContext() => Fixture.CreateContext();
    }
}
