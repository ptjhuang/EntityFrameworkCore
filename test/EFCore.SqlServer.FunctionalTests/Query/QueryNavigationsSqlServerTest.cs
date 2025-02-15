// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class QueryNavigationsSqlServerTest : QueryNavigationsTestBase<NorthwindQuerySqlServerFixture<NoopModelCustomizer>>
    {
        public QueryNavigationsSqlServerTest(
            NorthwindQuerySqlServerFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
        }

        public override async Task GroupJoin_with_nav_projected_in_subquery_when_client_eval(bool async)
        {
            await base.GroupJoin_with_nav_projected_in_subquery_when_client_eval(async);

            AssertSql(
                @"SELECT [od0].[OrderID], [od0].[ProductID], [od0].[Discount], [od0].[Quantity], [od0].[UnitPrice], [od.Product0].[ProductID], [od.Product0].[Discontinued], [od.Product0].[ProductName], [od.Product0].[SupplierID], [od.Product0].[UnitPrice], [od.Product0].[UnitsInStock]
FROM [Order Details] AS [od0]
INNER JOIN [Products] AS [od.Product0] ON [od0].[ProductID] = [od.Product0].[ProductID]",
                //
                @"SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate], [o.Customer0].[CustomerID], [o.Customer0].[Address], [o.Customer0].[City], [o.Customer0].[CompanyName], [o.Customer0].[ContactName], [o.Customer0].[ContactTitle], [o.Customer0].[Country], [o.Customer0].[Fax], [o.Customer0].[Phone], [o.Customer0].[PostalCode], [o.Customer0].[Region]
FROM [Orders] AS [o0]
LEFT JOIN [Customers] AS [o.Customer0] ON [o0].[CustomerID] = [o.Customer0].[CustomerID]",
                //
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]");
        }

        public override async Task GroupJoin_with_nav_in_predicate_in_subquery_when_client_eval(bool async)
        {
            await base.GroupJoin_with_nav_in_predicate_in_subquery_when_client_eval(async);

            AssertSql(
                @"SELECT [od0].[OrderID], [od0].[ProductID], [od0].[Discount], [od0].[Quantity], [od0].[UnitPrice], [od.Product0].[ProductID], [od.Product0].[Discontinued], [od.Product0].[ProductName], [od.Product0].[SupplierID], [od.Product0].[UnitPrice], [od.Product0].[UnitsInStock]
FROM [Order Details] AS [od0]
INNER JOIN [Products] AS [od.Product0] ON [od0].[ProductID] = [od.Product0].[ProductID]",
                //
                @"SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate], [o.Customer0].[CustomerID], [o.Customer0].[Address], [o.Customer0].[City], [o.Customer0].[CompanyName], [o.Customer0].[ContactName], [o.Customer0].[ContactTitle], [o.Customer0].[Country], [o.Customer0].[Fax], [o.Customer0].[Phone], [o.Customer0].[PostalCode], [o.Customer0].[Region]
FROM [Orders] AS [o0]
LEFT JOIN [Customers] AS [o.Customer0] ON [o0].[CustomerID] = [o.Customer0].[CustomerID]",
                //
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]");
        }

        public override async Task GroupJoin_with_nav_in_orderby_in_subquery_when_client_eval(bool async)
        {
            await base.GroupJoin_with_nav_in_orderby_in_subquery_when_client_eval(async);

            AssertSql(
                @"SELECT [od0].[OrderID], [od0].[ProductID], [od0].[Discount], [od0].[Quantity], [od0].[UnitPrice], [od.Product0].[ProductID], [od.Product0].[Discontinued], [od.Product0].[ProductName], [od.Product0].[SupplierID], [od.Product0].[UnitPrice], [od.Product0].[UnitsInStock]
FROM [Order Details] AS [od0]
INNER JOIN [Products] AS [od.Product0] ON [od0].[ProductID] = [od.Product0].[ProductID]",
                //
                @"SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate], [o.Customer0].[CustomerID], [o.Customer0].[Address], [o.Customer0].[City], [o.Customer0].[CompanyName], [o.Customer0].[ContactName], [o.Customer0].[ContactTitle], [o.Customer0].[Country], [o.Customer0].[Fax], [o.Customer0].[Phone], [o.Customer0].[PostalCode], [o.Customer0].[Region]
FROM [Orders] AS [o0]
LEFT JOIN [Customers] AS [o.Customer0] ON [o0].[CustomerID] = [o.Customer0].[CustomerID]",
                //
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]");
        }

        public override async Task Select_Where_Navigation(bool async)
        {
            await base.Select_Where_Navigation(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE [c].[City] = N'Seattle'");
        }

        public override async Task Select_Where_Navigation_Contains(bool async)
        {
            await base.Select_Where_Navigation_Contains(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE CHARINDEX(N'Sea', [c].[City]) > 0");
        }

        public override async Task Select_Where_Navigation_Deep(bool async)
        {
            await base.Select_Where_Navigation_Deep(async);

            AssertSql(
                @"@__p_0='1'

SELECT TOP(@__p_0) [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice]
FROM [Order Details] AS [o]
INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = [o0].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o0].[CustomerID] = [c].[CustomerID]
WHERE [c].[City] = N'Seattle'
ORDER BY [o].[OrderID], [o].[ProductID]");
        }

        public override async Task Take_Select_Navigation(bool async)
        {
            await base.Take_Select_Navigation(async);

            AssertSql(
                @"@__p_0='2'

SELECT [t1].[OrderID], [t1].[CustomerID], [t1].[EmployeeID], [t1].[OrderDate]
FROM (
    SELECT TOP(@__p_0) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    FROM [Customers] AS [c]
    ORDER BY [c].[CustomerID]
) AS [t]
LEFT JOIN (
    SELECT [t0].[OrderID], [t0].[CustomerID], [t0].[EmployeeID], [t0].[OrderDate]
    FROM (
        SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], ROW_NUMBER() OVER(PARTITION BY [o].[CustomerID] ORDER BY [o].[OrderID]) AS [row]
        FROM [Orders] AS [o]
    ) AS [t0]
    WHERE [t0].[row] <= 1
) AS [t1] ON [t].[CustomerID] = [t1].[CustomerID]
ORDER BY [t].[CustomerID]");
        }

        public override async Task Select_collection_FirstOrDefault_project_single_column1(bool async)
        {
            await base.Select_collection_FirstOrDefault_project_single_column1(async);

            AssertSql(
                @"@__p_0='2'

SELECT TOP(@__p_0) (
    SELECT TOP(1) [o].[CustomerID]
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
    ORDER BY [o].[OrderID])
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Select_collection_FirstOrDefault_project_single_column2(bool async)
        {
            await base.Select_collection_FirstOrDefault_project_single_column2(async);

            AssertSql(
                @"@__p_0='2'

SELECT TOP(@__p_0) (
    SELECT TOP(1) [o].[CustomerID]
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
    ORDER BY [o].[OrderID])
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Select_collection_FirstOrDefault_project_anonymous_type(bool async)
        {
            await base.Select_collection_FirstOrDefault_project_anonymous_type(async);

            AssertSql(
                @"@__p_0='2'

SELECT [t1].[CustomerID], [t1].[OrderID], [t1].[c]
FROM (
    SELECT TOP(@__p_0) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    FROM [Customers] AS [c]
    WHERE [c].[CustomerID] LIKE N'F%'
    ORDER BY [c].[CustomerID]
) AS [t]
LEFT JOIN (
    SELECT [t0].[CustomerID], [t0].[OrderID], [t0].[c]
    FROM (
        SELECT [o].[CustomerID], [o].[OrderID], 1 AS [c], ROW_NUMBER() OVER(PARTITION BY [o].[CustomerID] ORDER BY [o].[OrderID]) AS [row]
        FROM [Orders] AS [o]
    ) AS [t0]
    WHERE [t0].[row] <= 1
) AS [t1] ON [t].[CustomerID] = [t1].[CustomerID]
ORDER BY [t].[CustomerID]");
        }

        public override async Task Select_collection_FirstOrDefault_project_anonymous_type_client_eval(bool async)
        {
            await base.Select_collection_FirstOrDefault_project_anonymous_type_client_eval(async);

            AssertSql(
                @"@__p_0='2'

SELECT [t1].[CustomerID], [t1].[OrderID], [t1].[c]
FROM (
    SELECT TOP(@__p_0) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    FROM [Customers] AS [c]
    WHERE [c].[CustomerID] LIKE N'F%'
    ORDER BY [c].[CustomerID]
) AS [t]
LEFT JOIN (
    SELECT [t0].[CustomerID], [t0].[OrderID], [t0].[c]
    FROM (
        SELECT [o].[CustomerID], [o].[OrderID], 1 AS [c], ROW_NUMBER() OVER(PARTITION BY [o].[CustomerID] ORDER BY [o].[OrderID]) AS [row]
        FROM [Orders] AS [o]
    ) AS [t0]
    WHERE [t0].[row] <= 1
) AS [t1] ON [t].[CustomerID] = [t1].[CustomerID]
ORDER BY [t].[CustomerID]");
        }

        public override async Task Select_collection_FirstOrDefault_project_entity(bool async)
        {
            await base.Select_collection_FirstOrDefault_project_entity(async);

            AssertSql(
                @"@__p_0='2'

SELECT [t1].[OrderID], [t1].[CustomerID], [t1].[EmployeeID], [t1].[OrderDate]
FROM (
    SELECT TOP(@__p_0) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    FROM [Customers] AS [c]
    ORDER BY [c].[CustomerID]
) AS [t]
LEFT JOIN (
    SELECT [t0].[OrderID], [t0].[CustomerID], [t0].[EmployeeID], [t0].[OrderDate]
    FROM (
        SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], ROW_NUMBER() OVER(PARTITION BY [o].[CustomerID] ORDER BY [o].[OrderID]) AS [row]
        FROM [Orders] AS [o]
    ) AS [t0]
    WHERE [t0].[row] <= 1
) AS [t1] ON [t].[CustomerID] = [t1].[CustomerID]
ORDER BY [t].[CustomerID]");
        }

        public override async Task Skip_Select_Navigation(bool async)
        {
            await base.Skip_Select_Navigation(async);

            if (TestEnvironment.GetFlag(nameof(SqlServerCondition.SupportsOffset)) ?? true)
            {
                AssertSql(
                    @"@__p_0='20'

SELECT [t1].[OrderID], [t1].[CustomerID], [t1].[EmployeeID], [t1].[OrderDate]
FROM (
    SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    FROM [Customers] AS [c]
    ORDER BY [c].[CustomerID]
    OFFSET @__p_0 ROWS
) AS [t]
LEFT JOIN (
    SELECT [t0].[OrderID], [t0].[CustomerID], [t0].[EmployeeID], [t0].[OrderDate]
    FROM (
        SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], ROW_NUMBER() OVER(PARTITION BY [o].[CustomerID] ORDER BY [o].[OrderID]) AS [row]
        FROM [Orders] AS [o]
    ) AS [t0]
    WHERE [t0].[row] <= 1
) AS [t1] ON [t].[CustomerID] = [t1].[CustomerID]
ORDER BY [t].[CustomerID]");
            }
        }

        public override async Task Select_Where_Navigation_Included(bool async)
        {
            await base.Select_Where_Navigation_Included(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE [c].[City] = N'Seattle'");
        }

        public override async Task Include_with_multiple_optional_navigations(bool async)
        {
            await base.Include_with_multiple_optional_navigations(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice], [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Order Details] AS [o]
INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = [o0].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o0].[CustomerID] = [c].[CustomerID]
WHERE [c].[City] = N'London'");
        }

        public override async Task Select_Navigation(bool async)
        {
            await base.Select_Navigation(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]");
        }

        public override async Task Select_Navigations(bool async)
        {
            await base.Select_Navigations(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]");
        }

        public override async Task Select_Where_Navigation_Multiple_Access(bool async)
        {
            await base.Select_Where_Navigation_Multiple_Access(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE ([c].[City] = N'Seattle') AND (([c].[Phone] <> N'555 555 5555') OR [c].[Phone] IS NULL)");
        }

        public override async Task Select_Navigations_Where_Navigations(bool async)
        {
            await base.Select_Navigations_Where_Navigations(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE ([c].[City] = N'Seattle') AND (([c].[Phone] <> N'555 555 5555') OR [c].[Phone] IS NULL)");
        }

        public override async Task Select_Singleton_Navigation_With_Member_Access(bool async)
        {
            await base.Select_Singleton_Navigation_With_Member_Access(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE ([c].[City] = N'Seattle') AND (([c].[Phone] <> N'555 555 5555') OR [c].[Phone] IS NULL)");
        }

        public override async Task Select_count_plus_sum(bool async)
        {
            await base.Select_count_plus_sum(async);

            AssertSql(
                @"SELECT (
    SELECT SUM(CAST([o].[Quantity] AS int))
    FROM [Order Details] AS [o]
    WHERE [o1].[OrderID] = [o].[OrderID]) + (
    SELECT COUNT(*)
    FROM [Order Details] AS [o0]
    WHERE [o1].[OrderID] = [o0].[OrderID]) AS [Total]
FROM [Orders] AS [o1]");
        }

        public override async Task Singleton_Navigation_With_Member_Access(bool async)
        {
            await base.Singleton_Navigation_With_Member_Access(async);

            AssertSql(
                @"SELECT [c].[City] AS [B]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE ([c].[City] = N'Seattle') AND (([c].[Phone] <> N'555 555 5555') OR [c].[Phone] IS NULL)");
        }

        public override async Task Select_Where_Navigation_Scalar_Equals_Navigation_Scalar_Projected(bool async)
        {
            await base.Select_Where_Navigation_Scalar_Equals_Navigation_Scalar_Projected(async);

            AssertSql(
                @"SELECT [o].[CustomerID], [t].[CustomerID] AS [C2]
FROM [Orders] AS [o]
CROSS JOIN (
    SELECT [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
    FROM [Orders] AS [o0]
    WHERE [o0].[OrderID] < 10400
) AS [t]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
LEFT JOIN [Customers] AS [c0] ON [t].[CustomerID] = [c0].[CustomerID]
WHERE ([o].[OrderID] < 10300) AND (([c].[City] = [c0].[City]) OR ([c].[City] IS NULL AND [c0].[City] IS NULL))");
        }

        public override async Task Select_Where_Navigation_Equals_Navigation(bool async)
        {
            await base.Select_Where_Navigation_Equals_Navigation(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Orders] AS [o]
CROSS JOIN [Orders] AS [o0]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
LEFT JOIN [Customers] AS [c0] ON [o0].[CustomerID] = [c0].[CustomerID]
WHERE (([o].[CustomerID] IS NOT NULL AND ([o].[CustomerID] LIKE N'A%')) AND ([o0].[CustomerID] IS NOT NULL AND ([o0].[CustomerID] LIKE N'A%'))) AND (([c].[CustomerID] = [c0].[CustomerID]) OR ([c].[CustomerID] IS NULL AND [c0].[CustomerID] IS NULL))");
        }

        public override async Task Select_Where_Navigation_Null(bool async)
        {
            await base.Select_Where_Navigation_Null(async);

            AssertSql(
                @"SELECT [e].[EmployeeID], [e].[City], [e].[Country], [e].[FirstName], [e].[ReportsTo], [e].[Title]
FROM [Employees] AS [e]
LEFT JOIN [Employees] AS [e0] ON [e].[ReportsTo] = [e0].[EmployeeID]
WHERE [e0].[EmployeeID] IS NULL");
        }

        public override async Task Select_Where_Navigation_Null_Deep(bool async)
        {
            await base.Select_Where_Navigation_Null_Deep(async);

            AssertSql(
                @"SELECT [e].[EmployeeID], [e].[City], [e].[Country], [e].[FirstName], [e].[ReportsTo], [e].[Title]
FROM [Employees] AS [e]
LEFT JOIN [Employees] AS [e0] ON [e].[ReportsTo] = [e0].[EmployeeID]
LEFT JOIN [Employees] AS [e1] ON [e0].[ReportsTo] = [e1].[EmployeeID]
WHERE [e1].[EmployeeID] IS NULL");
        }

        public override async Task Select_Where_Navigation_Null_Reverse(bool async)
        {
            await base.Select_Where_Navigation_Null_Reverse(async);

            AssertSql(
                @"SELECT [e].[EmployeeID], [e].[City], [e].[Country], [e].[FirstName], [e].[ReportsTo], [e].[Title]
FROM [Employees] AS [e]
LEFT JOIN [Employees] AS [e0] ON [e].[ReportsTo] = [e0].[EmployeeID]
WHERE [e0].[EmployeeID] IS NULL");
        }

        public override async Task Select_collection_navigation_simple(bool async)
        {
            await base.Select_collection_navigation_simple(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
WHERE [c].[CustomerID] LIKE N'A%'
ORDER BY [c].[CustomerID], [o].[OrderID]");
        }

        public override async Task Select_collection_navigation_simple2(bool async)
        {
            await base.Select_collection_navigation_simple2(async);

            AssertSql(
                @"SELECT [c].[CustomerID], (
    SELECT COUNT(*)
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]) AS [Count]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] LIKE N'A%'
ORDER BY [c].[CustomerID]");
        }

        public override async Task Select_collection_navigation_simple_followed_by_ordering_by_scalar(bool async)
        {
            await base.Select_collection_navigation_simple_followed_by_ordering_by_scalar(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
WHERE [c].[CustomerID] LIKE N'A%'
ORDER BY [c].[CustomerID], [o].[OrderID]");
        }

        public override async Task Select_collection_navigation_multi_part(bool async)
        {
            await base.Select_collection_navigation_multi_part(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
LEFT JOIN [Orders] AS [o0] ON [c].[CustomerID] = [o0].[CustomerID]
WHERE [o].[CustomerID] = N'ALFKI'
ORDER BY [o].[OrderID], [o0].[OrderID]");
        }

        public override async Task Select_collection_navigation_multi_part2(bool async)
        {
            await base.Select_collection_navigation_multi_part2(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[ProductID], [o0].[OrderID], [o1].[OrderID], [o1].[CustomerID], [o1].[EmployeeID], [o1].[OrderDate]
FROM [Order Details] AS [o]
INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = [o0].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o0].[CustomerID] = [c].[CustomerID]
LEFT JOIN [Orders] AS [o1] ON [c].[CustomerID] = [o1].[CustomerID]
WHERE ([o0].[CustomerID] = N'ALFKI') OR ([o0].[CustomerID] = N'ANTON')
ORDER BY [o].[OrderID], [o].[ProductID], [o0].[OrderID], [o1].[OrderID]");
        }

        public override async Task Collection_select_nav_prop_any(bool async)
        {
            await base.Collection_select_nav_prop_any(async);

            AssertSql(
                @"SELECT CASE
    WHEN EXISTS (
        SELECT 1
        FROM [Orders] AS [o]
        WHERE [c].[CustomerID] = [o].[CustomerID]) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END AS [Any]
FROM [Customers] AS [c]");
        }

        public override async Task Collection_select_nav_prop_predicate(bool async)
        {
            await base.Collection_select_nav_prop_predicate(async);

            AssertSql(
                @"SELECT CASE
    WHEN (
        SELECT COUNT(*)
        FROM [Orders] AS [o]
        WHERE [c].[CustomerID] = [o].[CustomerID]) > 0 THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END
FROM [Customers] AS [c]");
        }

        public override async Task Collection_where_nav_prop_any(bool async)
        {
            await base.Collection_where_nav_prop_any(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE EXISTS (
    SELECT 1
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID])");
        }

        public override async Task Collection_where_nav_prop_any_predicate(bool async)
        {
            await base.Collection_where_nav_prop_any_predicate(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE EXISTS (
    SELECT 1
    FROM [Orders] AS [o]
    WHERE ([c].[CustomerID] = [o].[CustomerID]) AND ([o].[OrderID] > 0))");
        }

        public override async Task Collection_select_nav_prop_all(bool async)
        {
            await base.Collection_select_nav_prop_all(async);

            AssertSql(
                @"SELECT CASE
    WHEN NOT EXISTS (
        SELECT 1
        FROM [Orders] AS [o]
        WHERE ([c].[CustomerID] = [o].[CustomerID]) AND (([o].[CustomerID] <> N'ALFKI') OR [o].[CustomerID] IS NULL)) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END AS [All]
FROM [Customers] AS [c]");
        }

        public override async Task Collection_where_nav_prop_all(bool async)
        {
            await base.Collection_where_nav_prop_all(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE NOT EXISTS (
    SELECT 1
    FROM [Orders] AS [o]
    WHERE ([c].[CustomerID] = [o].[CustomerID]) AND (([o].[CustomerID] <> N'ALFKI') OR [o].[CustomerID] IS NULL))");
        }

        public override async Task Collection_select_nav_prop_count(bool async)
        {
            await base.Collection_select_nav_prop_count(async);

            AssertSql(
                @"SELECT (
    SELECT COUNT(*)
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]) AS [Count]
FROM [Customers] AS [c]");
        }

        public override async Task Collection_where_nav_prop_count(bool async)
        {
            await base.Collection_where_nav_prop_count(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE (
    SELECT COUNT(*)
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]) > 5");
        }

        public override async Task Collection_where_nav_prop_count_reverse(bool async)
        {
            await base.Collection_where_nav_prop_count_reverse(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE 5 < (
    SELECT COUNT(*)
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID])");
        }

        public override async Task Collection_orderby_nav_prop_count(bool async)
        {
            await base.Collection_orderby_nav_prop_count(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY (
    SELECT COUNT(*)
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]), [c].[CustomerID]");
        }

        public override async Task Collection_select_nav_prop_long_count(bool async)
        {
            await base.Collection_select_nav_prop_long_count(async);

            AssertSql(
                @"SELECT (
    SELECT COUNT_BIG(*)
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]) AS [C]
FROM [Customers] AS [c]");
        }

        public override async Task Select_multiple_complex_projections(bool async)
        {
            await base.Select_multiple_complex_projections(async);

            AssertSql(
                @"SELECT (
    SELECT COUNT(*)
    FROM [Order Details] AS [o]
    WHERE [o3].[OrderID] = [o].[OrderID]) AS [collection1], [o3].[OrderDate] AS [scalar1], CASE
    WHEN EXISTS (
        SELECT 1
        FROM [Order Details] AS [o0]
        WHERE ([o3].[OrderID] = [o0].[OrderID]) AND ([o0].[UnitPrice] > 10.0)) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END AS [any], CASE
    WHEN [o3].[CustomerID] = N'ALFKI' THEN N'50'
    ELSE N'10'
END AS [conditional], [o3].[OrderID] AS [scalar2], CASE
    WHEN NOT EXISTS (
        SELECT 1
        FROM [Order Details] AS [o1]
        WHERE ([o3].[OrderID] = [o1].[OrderID]) AND ([o1].[OrderID] <> 42)) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END AS [all], (
    SELECT COUNT_BIG(*)
    FROM [Order Details] AS [o2]
    WHERE [o3].[OrderID] = [o2].[OrderID]) AS [collection2]
FROM [Orders] AS [o3]
WHERE [o3].[CustomerID] IS NOT NULL AND ([o3].[CustomerID] LIKE N'A%')");
        }

        public override async Task Collection_select_nav_prop_sum(bool async)
        {
            await base.Collection_select_nav_prop_sum(async);

            AssertSql(
                @"SELECT (
    SELECT SUM([o].[OrderID])
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]) AS [Sum]
FROM [Customers] AS [c]");
        }

        public override async Task Collection_select_nav_prop_sum_plus_one(bool async)
        {
            await base.Collection_select_nav_prop_sum_plus_one(async);

            AssertSql(
                "");
        }

        public override async Task Collection_where_nav_prop_sum(bool async)
        {
            await base.Collection_where_nav_prop_sum(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE (
    SELECT SUM([o].[OrderID])
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]) > 1000");
        }

        public override async Task Collection_select_nav_prop_first_or_default(bool async)
        {
            await base.Collection_select_nav_prop_first_or_default(async);

            AssertSql(
                @"SELECT [t0].[OrderID], [t0].[CustomerID], [t0].[EmployeeID], [t0].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN (
    SELECT [t].[OrderID], [t].[CustomerID], [t].[EmployeeID], [t].[OrderDate]
    FROM (
        SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], ROW_NUMBER() OVER(PARTITION BY [o].[CustomerID] ORDER BY [o].[OrderID]) AS [row]
        FROM [Orders] AS [o]
    ) AS [t]
    WHERE [t].[row] <= 1
) AS [t0] ON [c].[CustomerID] = [t0].[CustomerID]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Collection_select_nav_prop_first_or_default_then_nav_prop(bool async)
        {
            await base.Collection_select_nav_prop_first_or_default_then_nav_prop(async);

            AssertSql(
                @"SELECT [t0].[CustomerID], [t0].[Address], [t0].[City], [t0].[CompanyName], [t0].[ContactName], [t0].[ContactTitle], [t0].[Country], [t0].[Fax], [t0].[Phone], [t0].[PostalCode], [t0].[Region]
FROM [Customers] AS [c]
LEFT JOIN (
    SELECT [t].[CustomerID], [t].[Address], [t].[City], [t].[CompanyName], [t].[ContactName], [t].[ContactTitle], [t].[Country], [t].[Fax], [t].[Phone], [t].[PostalCode], [t].[Region], [t].[OrderID], [t].[CustomerID0]
    FROM (
        SELECT [c0].[CustomerID], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region], [o].[OrderID], [o].[CustomerID] AS [CustomerID0], ROW_NUMBER() OVER(PARTITION BY [o].[CustomerID] ORDER BY [o].[OrderID]) AS [row]
        FROM [Orders] AS [o]
        LEFT JOIN [Customers] AS [c0] ON [o].[CustomerID] = [c0].[CustomerID]
        WHERE [o].[OrderID] IN (10643, 10692, 10702, 10835, 10952, 11011)
    ) AS [t]
    WHERE [t].[row] <= 1
) AS [t0] ON [c].[CustomerID] = [t0].[CustomerID0]
WHERE [c].[CustomerID] LIKE N'A%'
ORDER BY [c].[CustomerID]");
        }

        public override async Task Collection_select_nav_prop_first_or_default_then_nav_prop_nested(bool async)
        {
            await base.Collection_select_nav_prop_first_or_default_then_nav_prop_nested(async);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [c].[City]
    FROM [Orders] AS [o]
    LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
    WHERE [o].[CustomerID] = N'ALFKI')
FROM [Customers] AS [c0]
WHERE [c0].[CustomerID] LIKE N'A%'");
        }

        public override async Task Collection_select_nav_prop_single_or_default_then_nav_prop_nested(bool async)
        {
            await base.Collection_select_nav_prop_single_or_default_then_nav_prop_nested(async);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [c].[City]
    FROM [Orders] AS [o]
    LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
    WHERE [o].[OrderID] = 10643)
FROM [Customers] AS [c0]
WHERE [c0].[CustomerID] LIKE N'A%'");
        }

        public override async Task Collection_select_nav_prop_first_or_default_then_nav_prop_nested_using_property_method(bool async)
        {
            await base.Collection_select_nav_prop_first_or_default_then_nav_prop_nested_using_property_method(async);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [c].[City]
    FROM [Orders] AS [o]
    LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
    WHERE [o].[CustomerID] = N'ALFKI')
FROM [Customers] AS [c0]
WHERE [c0].[CustomerID] LIKE N'A%'");
        }

        public override async Task Collection_select_nav_prop_first_or_default_then_nav_prop_nested_with_orderby(bool async)
        {
            await base.Collection_select_nav_prop_first_or_default_then_nav_prop_nested_with_orderby(async);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [c].[City]
    FROM [Orders] AS [o]
    LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
    WHERE [o].[CustomerID] = N'ALFKI'
    ORDER BY [o].[CustomerID])
FROM [Customers] AS [c0]
WHERE [c0].[CustomerID] LIKE N'A%'");
        }

        public override async Task Navigation_fk_based_inside_contains(bool async)
        {
            await base.Navigation_fk_based_inside_contains(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE [c].[CustomerID] IN (N'ALFKI')");
        }

        public override async Task Navigation_inside_contains(bool async)
        {
            await base.Navigation_inside_contains(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE [c].[City] IN (N'Novigrad', N'Seattle')");
        }

        public override async Task Navigation_inside_contains_nested(bool async)
        {
            await base.Navigation_inside_contains_nested(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice]
FROM [Order Details] AS [o]
INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = [o0].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o0].[CustomerID] = [c].[CustomerID]
WHERE [c].[City] IN (N'Novigrad', N'Seattle')");
        }

        public override async Task Navigation_from_join_clause_inside_contains(bool async)
        {
            await base.Navigation_from_join_clause_inside_contains(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice]
FROM [Order Details] AS [o]
INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = [o0].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o0].[CustomerID] = [c].[CustomerID]
WHERE [c].[Country] IN (N'USA', N'Redania')");
        }

        public override async Task Where_subquery_on_navigation(bool async)
        {
            await base.Where_subquery_on_navigation(async);

            AssertSql(
                @"SELECT [p].[ProductID], [p].[Discontinued], [p].[ProductName], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock]
FROM [Products] AS [p]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT [o].[OrderID], [o].[ProductID]
        FROM [Order Details] AS [o]
        WHERE [p].[ProductID] = [o].[ProductID]
    ) AS [t0]
    INNER JOIN (
        SELECT TOP(1) [o0].[OrderID], [o0].[ProductID]
        FROM [Order Details] AS [o0]
        WHERE [o0].[Quantity] = CAST(1 AS smallint)
        ORDER BY [o0].[OrderID] DESC, [o0].[ProductID]
    ) AS [t1] ON ([t0].[OrderID] = [t1].[OrderID]) AND ([t0].[ProductID] = [t1].[ProductID]))");
        }

        public override async Task Where_subquery_on_navigation2(bool async)
        {
            await base.Where_subquery_on_navigation2(async);

            AssertSql(
                @"SELECT [p].[ProductID], [p].[Discontinued], [p].[ProductName], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock]
FROM [Products] AS [p]
WHERE EXISTS (
    SELECT 1
    FROM (
        SELECT [o].[OrderID], [o].[ProductID]
        FROM [Order Details] AS [o]
        WHERE [p].[ProductID] = [o].[ProductID]
    ) AS [t0]
    INNER JOIN (
        SELECT TOP(1) [o0].[OrderID], [o0].[ProductID]
        FROM [Order Details] AS [o0]
        ORDER BY [o0].[OrderID] DESC, [o0].[ProductID]
    ) AS [t1] ON ([t0].[OrderID] = [t1].[OrderID]) AND ([t0].[ProductID] = [t1].[ProductID]))");
        }

        public override void Navigation_in_subquery_referencing_outer_query()
        {
            base.Navigation_in_subquery_referencing_outer_query();

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE ((
    SELECT COUNT(*)
    FROM [Order Details] AS [o0]
    INNER JOIN [Orders] AS [o1] ON [o0].[OrderID] = [o1].[OrderID]
    LEFT JOIN [Customers] AS [c0] ON [o1].[CustomerID] = [c0].[CustomerID]
    WHERE ([c].[Country] = [c0].[Country]) OR ([c].[Country] IS NULL AND [c0].[Country] IS NULL)) > 0) AND (([o].[OrderID] = 10643) OR ([o].[OrderID] = 10692))");
        }

        public override async Task GroupBy_on_nav_prop(bool async)
        {
            await base.GroupBy_on_nav_prop(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], [o.Customer].[City]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [o.Customer] ON [o].[CustomerID] = [o.Customer].[CustomerID]
ORDER BY [o.Customer].[City]");
        }

        public override async Task Where_nav_prop_group_by(bool async)
        {
            await base.Where_nav_prop_group_by(async);

            AssertSql(
                @"SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
INNER JOIN [Orders] AS [od.Order] ON [od].[OrderID] = [od.Order].[OrderID]
WHERE [od.Order].[CustomerID] = N'ALFKI'
ORDER BY [od].[Quantity]");
        }

        public override async Task Let_group_by_nav_prop(bool async)
        {
            await base.Let_group_by_nav_prop(async);

            AssertSql(
                @"SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice], [od.Order].[CustomerID] AS [customer]
FROM [Order Details] AS [od]
INNER JOIN [Orders] AS [od.Order] ON [od].[OrderID] = [od.Order].[OrderID]
ORDER BY [od.Order].[CustomerID]");
        }

        public override async Task Select_anonymous_type_order_by_field_group_by_same_field(bool async)
        {
            await base.Select_anonymous_type_order_by_field_group_by_same_field(async);

            AssertSql(
                "");
        }

        public override async Task Project_single_scalar_value_subquery_is_properly_inlined(bool async)
        {
            await base.Project_single_scalar_value_subquery_is_properly_inlined(async);

            AssertSql(
                @"SELECT [c].[CustomerID], (
    SELECT TOP(1) [o].[OrderID]
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
    ORDER BY [o].[OrderID]) AS [OrderId]
FROM [Customers] AS [c]");
        }

        public override async Task Project_single_entity_value_subquery_works(bool async)
        {
            await base.Project_single_entity_value_subquery_works(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [t0].[OrderID], [t0].[CustomerID], [t0].[EmployeeID], [t0].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN (
    SELECT [t].[OrderID], [t].[CustomerID], [t].[EmployeeID], [t].[OrderDate]
    FROM (
        SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate], ROW_NUMBER() OVER(PARTITION BY [o].[CustomerID] ORDER BY [o].[OrderID]) AS [row]
        FROM [Orders] AS [o]
    ) AS [t]
    WHERE [t].[row] <= 1
) AS [t0] ON [c].[CustomerID] = [t0].[CustomerID]
WHERE [c].[CustomerID] LIKE N'A%'
ORDER BY [c].[CustomerID]");
        }

        public override async Task Project_single_scalar_value_subquery_in_query_with_optional_navigation_works(bool async)
        {
            await base.Project_single_scalar_value_subquery_in_query_with_optional_navigation_works(async);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [o0].[OrderID], (
    SELECT TOP(1) [o].[OrderID]
    FROM [Order Details] AS [o]
    WHERE [o0].[OrderID] = [o].[OrderID]
    ORDER BY [o].[OrderID], [o].[ProductID]) AS [OrderDetail], [c].[City]
FROM [Orders] AS [o0]
LEFT JOIN [Customers] AS [c] ON [o0].[CustomerID] = [c].[CustomerID]
ORDER BY [o0].[OrderID]");
        }

        public override async Task GroupJoin_with_complex_subquery_and_LOJ_gets_flattened(bool async)
        {
            await base.GroupJoin_with_complex_subquery_and_LOJ_gets_flattened(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
LEFT JOIN (
    SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice], [o0].[OrderID] AS [OrderID0], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate], [c0].[CustomerID] AS [CustomerID0], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
    FROM [Order Details] AS [o]
    INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = 10260
    INNER JOIN [Customers] AS [c0] ON [o0].[CustomerID] = [c0].[CustomerID]
) AS [t] ON [c].[CustomerID] = [t].[CustomerID0]");
        }

        public override async Task GroupJoin_with_complex_subquery_and_LOJ_gets_flattened2(bool async)
        {
            await base.GroupJoin_with_complex_subquery_and_LOJ_gets_flattened2(async);

            AssertSql(
                @"SELECT [c].[CustomerID]
FROM [Customers] AS [c]
LEFT JOIN (
    SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice], [o0].[OrderID] AS [OrderID0], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate], [c0].[CustomerID] AS [CustomerID0], [c0].[Address], [c0].[City], [c0].[CompanyName], [c0].[ContactName], [c0].[ContactTitle], [c0].[Country], [c0].[Fax], [c0].[Phone], [c0].[PostalCode], [c0].[Region]
    FROM [Order Details] AS [o]
    INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = 10260
    INNER JOIN [Customers] AS [c0] ON [o0].[CustomerID] = [c0].[CustomerID]
) AS [t] ON [c].[CustomerID] = [t].[CustomerID0]");
        }

        public override async Task Navigation_with_collection_with_nullable_type_key(bool async)
        {
            await base.Navigation_with_collection_with_nullable_type_key(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
WHERE (
    SELECT COUNT(*)
    FROM [Orders] AS [o0]
    WHERE ([c].[CustomerID] IS NOT NULL AND ([c].[CustomerID] = [o0].[CustomerID])) AND ([o0].[OrderID] > 10260)) > 30");
        }

        public override async Task Client_groupjoin_with_orderby_key_descending(bool async)
        {
            await base.Client_groupjoin_with_orderby_key_descending(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
WHERE [c].[CustomerID] LIKE N'A%'
ORDER BY [c].[CustomerID] DESC");
        }

        public override async Task Navigation_projection_on_groupjoin_qsre(bool async)
        {
            await base.Navigation_projection_on_groupjoin_qsre(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
WHERE [c].[CustomerID] = N'ALFKI'
ORDER BY [c].[CustomerID]",
                //
                @"@_outer_OrderID='10643'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10692'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10702'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10835'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10952'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='11011'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]");
        }

        public override async Task Navigation_projection_on_groupjoin_qsre_no_outer_in_final_result(bool async)
        {
            await base.Navigation_projection_on_groupjoin_qsre_no_outer_in_final_result(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
WHERE [c].[CustomerID] = N'ALFKI'
ORDER BY [c].[CustomerID]",
                //
                @"@_outer_OrderID='10643'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10692'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10702'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10835'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10952'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='11011'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]");
        }

        public override async Task Navigation_projection_on_groupjoin_qsre_with_empty_grouping(bool async)
        {
            await base.Navigation_projection_on_groupjoin_qsre_with_empty_grouping(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [t].[OrderID], [t].[CustomerID], [t].[EmployeeID], [t].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN (
    SELECT [oo].[OrderID], [oo].[CustomerID], [oo].[EmployeeID], [oo].[OrderDate]
    FROM [Orders] AS [oo]
    WHERE [oo].[OrderID] NOT IN (10308, 10625, 10759, 10926)
) AS [t] ON [c].[CustomerID] = [t].[CustomerID]
WHERE [c].[CustomerID] LIKE N'A%'
ORDER BY [c].[CustomerID]",
                //
                @"@_outer_OrderID='10643'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10692'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10702'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10835'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10952'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='11011'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10365'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10507'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]");
        }

        public override void Include_on_inner_projecting_groupjoin()
        {
            base.Include_on_inner_projecting_groupjoin();

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [oo].[OrderID], [oo].[CustomerID], [oo].[EmployeeID], [oo].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN [Orders] AS [oo] ON [c].[CustomerID] = [oo].[CustomerID]
WHERE [c].[CustomerID] = N'ALFKI'
ORDER BY [c].[CustomerID]",
                //
                @"@_outer_OrderID='10643'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10692'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10702'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10835'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='10952'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]",
                //
                @"@_outer_OrderID='11011'

SELECT [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE @_outer_OrderID = [od].[OrderID]");
        }

        public override void Include_on_inner_projecting_groupjoin_complex()
        {
            base.Include_on_inner_projecting_groupjoin_complex();

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
WHERE [c].[CustomerID] = N'ALFKI'
ORDER BY [c].[CustomerID]",
                //
                @"@_outer_OrderID='10643'

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice], [o.Product].[ProductID], [o.Product].[Discontinued], [o.Product].[ProductName], [o.Product].[SupplierID], [o.Product].[UnitPrice], [o.Product].[UnitsInStock]
FROM [Order Details] AS [o0]
INNER JOIN [Products] AS [o.Product] ON [o0].[ProductID] = [o.Product].[ProductID]
WHERE @_outer_OrderID = [o0].[OrderID]",
                //
                @"@_outer_OrderID='10692'

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice], [o.Product].[ProductID], [o.Product].[Discontinued], [o.Product].[ProductName], [o.Product].[SupplierID], [o.Product].[UnitPrice], [o.Product].[UnitsInStock]
FROM [Order Details] AS [o0]
INNER JOIN [Products] AS [o.Product] ON [o0].[ProductID] = [o.Product].[ProductID]
WHERE @_outer_OrderID = [o0].[OrderID]",
                //
                @"@_outer_OrderID='10702'

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice], [o.Product].[ProductID], [o.Product].[Discontinued], [o.Product].[ProductName], [o.Product].[SupplierID], [o.Product].[UnitPrice], [o.Product].[UnitsInStock]
FROM [Order Details] AS [o0]
INNER JOIN [Products] AS [o.Product] ON [o0].[ProductID] = [o.Product].[ProductID]
WHERE @_outer_OrderID = [o0].[OrderID]",
                //
                @"@_outer_OrderID='10835'

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice], [o.Product].[ProductID], [o.Product].[Discontinued], [o.Product].[ProductName], [o.Product].[SupplierID], [o.Product].[UnitPrice], [o.Product].[UnitsInStock]
FROM [Order Details] AS [o0]
INNER JOIN [Products] AS [o.Product] ON [o0].[ProductID] = [o.Product].[ProductID]
WHERE @_outer_OrderID = [o0].[OrderID]",
                //
                @"@_outer_OrderID='10952'

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice], [o.Product].[ProductID], [o.Product].[Discontinued], [o.Product].[ProductName], [o.Product].[SupplierID], [o.Product].[UnitPrice], [o.Product].[UnitsInStock]
FROM [Order Details] AS [o0]
INNER JOIN [Products] AS [o.Product] ON [o0].[ProductID] = [o.Product].[ProductID]
WHERE @_outer_OrderID = [o0].[OrderID]",
                //
                @"@_outer_OrderID='11011'

SELECT [o0].[OrderID], [o0].[ProductID], [o0].[Discount], [o0].[Quantity], [o0].[UnitPrice], [o.Product].[ProductID], [o.Product].[Discontinued], [o.Product].[ProductName], [o.Product].[SupplierID], [o.Product].[UnitPrice], [o.Product].[UnitsInStock]
FROM [Order Details] AS [o0]
INNER JOIN [Products] AS [o.Product] ON [o0].[ProductID] = [o.Product].[ProductID]
WHERE @_outer_OrderID = [o0].[OrderID]");
        }

        public override async Task Group_join_doesnt_get_bound_directly_to_group_join_qsre(bool async)
        {
            await base.Group_join_doesnt_get_bound_directly_to_group_join_qsre(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Customers] AS [c]
LEFT JOIN [Orders] AS [o] ON [c].[CustomerID] = [o].[CustomerID]
WHERE [c].[CustomerID] LIKE N'A%'
ORDER BY [c].[CustomerID]");
        }

        public override async Task Multiple_include_with_multiple_optional_navigations(bool async)
        {
            await base.Multiple_include_with_multiple_optional_navigations(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice], [o0].[OrderID], [o0].[CustomerID], [o0].[EmployeeID], [o0].[OrderDate], [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region], [p].[ProductID], [p].[Discontinued], [p].[ProductName], [p].[SupplierID], [p].[UnitPrice], [p].[UnitsInStock]
FROM [Order Details] AS [o]
INNER JOIN [Orders] AS [o0] ON [o].[OrderID] = [o0].[OrderID]
LEFT JOIN [Customers] AS [c] ON [o0].[CustomerID] = [c].[CustomerID]
INNER JOIN [Products] AS [p] ON [o].[ProductID] = [p].[ProductID]
WHERE [c].[City] = N'London'");
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        protected override void ClearLog()
            => Fixture.TestSqlLoggerFactory.Clear();
    }
}
