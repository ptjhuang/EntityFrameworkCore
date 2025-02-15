﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    public partial class SimpleQuerySqlServerTest
    {
        public override void Select_All()
        {
            base.Select_All();

            AssertSql(
                @"SELECT CASE
    WHEN NOT EXISTS (
        SELECT 1
        FROM [Orders] AS [o]
        WHERE ([o].[CustomerID] <> N'ALFKI') OR [o].[CustomerID] IS NULL) THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END");
        }

        public override async Task Sum_with_no_arg(bool async)
        {
            await base.Sum_with_no_arg(async);

            AssertSql(
                @"SELECT SUM([o].[OrderID])
FROM [Orders] AS [o]");
        }

        public override async Task Sum_with_binary_expression(bool async)
        {
            await base.Sum_with_binary_expression(async);

            AssertSql(
                @"SELECT SUM([o].[OrderID] * 2)
FROM [Orders] AS [o]");
        }

        public override async Task Sum_with_arg(bool async)
        {
            await base.Sum_with_arg(async);

            AssertSql(
                @"SELECT SUM([o].[OrderID])
FROM [Orders] AS [o]");
        }

        public override async Task Sum_with_arg_expression(bool async)
        {
            await base.Sum_with_arg_expression(async);

            AssertSql(
                @"SELECT SUM([o].[OrderID] + [o].[OrderID])
FROM [Orders] AS [o]");
        }

        public override async Task Sum_with_division_on_decimal(bool async)
        {
            await base.Sum_with_division_on_decimal(async);

            AssertSql(
                @"SELECT SUM(CAST([o].[Quantity] AS decimal(18,2)) / 2.09)
FROM [Order Details] AS [o]");
        }

        public override async Task Sum_with_division_on_decimal_no_significant_digits(bool async)
        {
            await base.Sum_with_division_on_decimal_no_significant_digits(async);

            AssertSql(
                @"SELECT SUM(CAST([o].[Quantity] AS decimal(18,2)) / 2.0)
FROM [Order Details] AS [o]");
        }

        public override async Task Sum_with_coalesce(bool async)
        {
            await base.Sum_with_coalesce(async);

            AssertSql(
                @"SELECT SUM(COALESCE([p].[UnitPrice], 0.0))
FROM [Products] AS [p]
WHERE [p].[ProductID] < 40");
        }

        public override async Task Sum_over_subquery_is_client_eval(bool async)
        {
            await base.Sum_over_subquery_is_client_eval(async);

            AssertSql(
                @"SELECT (
    SELECT SUM([o].[OrderID])
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
)
FROM [Customers] AS [c]");
        }

        public override async Task Sum_over_nested_subquery_is_client_eval(bool async)
        {
            await base.Sum_over_nested_subquery_is_client_eval(async);
            AssertSql(
                @"SELECT [c].[CustomerID]
FROM [Customers] AS [c]");
        }

        public override async Task Sum_over_min_subquery_is_client_eval(bool async)
        {
            await base.Sum_over_min_subquery_is_client_eval(async);
            AssertSql(
                @"SELECT [c].[CustomerID]
FROM [Customers] AS [c]");
        }

        public override async Task Sum_on_float_column(bool async)
        {
            await base.Sum_on_float_column(async);

            AssertSql(
                @"SELECT CAST(SUM([o].[Discount]) AS real)
FROM [Order Details] AS [o]
WHERE [o].[ProductID] = 1");
        }

        public override async Task Sum_on_float_column_in_subquery(bool async)
        {
            await base.Sum_on_float_column_in_subquery(async);

            AssertSql(
                @"SELECT [o0].[OrderID], (
    SELECT CAST(SUM([o].[Discount]) AS real)
    FROM [Order Details] AS [o]
    WHERE [o0].[OrderID] = [o].[OrderID]) AS [Sum]
FROM [Orders] AS [o0]
WHERE [o0].[OrderID] < 10300");
        }

        public override async Task Average_with_no_arg(bool async)
        {
            await base.Average_with_no_arg(async);

            AssertSql(
                @"SELECT AVG(CAST([o].[OrderID] AS float))
FROM [Orders] AS [o]");
        }

        public override async Task Average_with_binary_expression(bool async)
        {
            await base.Average_with_binary_expression(async);

            AssertSql(
                @"SELECT AVG(CAST(([o].[OrderID] * 2) AS float))
FROM [Orders] AS [o]");
        }

        public override async Task Average_with_arg(bool async)
        {
            await base.Average_with_arg(async);

            AssertSql(
                @"SELECT AVG(CAST([o].[OrderID] AS float))
FROM [Orders] AS [o]");
        }

        public override async Task Average_with_arg_expression(bool async)
        {
            await base.Average_with_arg_expression(async);

            AssertSql(
                @"SELECT AVG(CAST(([o].[OrderID] + [o].[OrderID]) AS float))
FROM [Orders] AS [o]");
        }

        public override async Task Average_with_division_on_decimal(bool async)
        {
            await base.Average_with_division_on_decimal(async);

            AssertSql(
                @"SELECT AVG(CAST([o].[Quantity] AS decimal(18,2)) / 2.09)
FROM [Order Details] AS [o]");
        }

        public override async Task Average_with_division_on_decimal_no_significant_digits(bool async)
        {
            await base.Average_with_division_on_decimal_no_significant_digits(async);

            AssertSql(
                @"SELECT AVG(CAST([o].[Quantity] AS decimal(18,2)) / 2.0)
FROM [Order Details] AS [o]");
        }

        public override async Task Average_with_coalesce(bool async)
        {
            await base.Average_with_coalesce(async);

            AssertSql(
                @"SELECT AVG(COALESCE([p].[UnitPrice], 0.0))
FROM [Products] AS [p]
WHERE [p].[ProductID] < 40");
        }

        public override async Task Average_over_subquery_is_client_eval(bool async)
        {
            await base.Average_over_subquery_is_client_eval(async);

            AssertSql(
                @"SELECT (
    SELECT SUM([o].[OrderID])
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
)
FROM [Customers] AS [c]");
        }

        public override async Task Average_over_nested_subquery_is_client_eval(bool async)
        {
            await base.Average_over_nested_subquery_is_client_eval(async);
            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [c].[CustomerID]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Average_over_max_subquery_is_client_eval(bool async)
        {
            await base.Average_over_max_subquery_is_client_eval(async);
            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [c].[CustomerID]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Average_on_float_column(bool async)
        {
            await base.Average_on_float_column(async);

            AssertSql(
                @"SELECT CAST(AVG([o].[Discount]) AS real)
FROM [Order Details] AS [o]
WHERE [o].[ProductID] = 1");
        }

        public override async Task Average_on_float_column_in_subquery(bool async)
        {
            await base.Average_on_float_column_in_subquery(async);

            AssertSql(
                @"SELECT [o0].[OrderID], (
    SELECT CAST(AVG([o].[Discount]) AS real)
    FROM [Order Details] AS [o]
    WHERE [o0].[OrderID] = [o].[OrderID]) AS [Sum]
FROM [Orders] AS [o0]
WHERE [o0].[OrderID] < 10300");
        }

        public override async Task Average_on_float_column_in_subquery_with_cast(bool async)
        {
            await base.Average_on_float_column_in_subquery_with_cast(async);

            AssertSql(
                @"SELECT [o0].[OrderID], (
    SELECT CAST(AVG([o].[Discount]) AS real)
    FROM [Order Details] AS [o]
    WHERE [o0].[OrderID] = [o].[OrderID]) AS [Sum]
FROM [Orders] AS [o0]
WHERE [o0].[OrderID] < 10300");
        }

        public override async Task Min_with_no_arg(bool async)
        {
            await base.Min_with_no_arg(async);

            AssertSql(
                @"SELECT MIN([o].[OrderID])
FROM [Orders] AS [o]");
        }

        public override async Task Min_with_arg(bool async)
        {
            await base.Min_with_arg(async);

            AssertSql(
                @"SELECT MIN([o].[OrderID])
FROM [Orders] AS [o]");
        }

        public override async Task Min_with_coalesce(bool async)
        {
            await base.Min_with_coalesce(async);

            AssertSql(
                @"SELECT MIN(COALESCE([p].[UnitPrice], 0.0))
FROM [Products] AS [p]
WHERE [p].[ProductID] < 40");
        }

        public override async Task Min_over_subquery_is_client_eval(bool async)
        {
            await base.Min_over_subquery_is_client_eval(async);

            AssertSql(
                @"SELECT (
    SELECT SUM([o].[OrderID])
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
)
FROM [Customers] AS [c]");
        }

        public override async Task Min_over_nested_subquery_is_client_eval(bool async)
        {
            await base.Min_over_nested_subquery_is_client_eval(async);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [c].[CustomerID]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Min_over_max_subquery_is_client_eval(bool async)
        {
            await base.Min_over_max_subquery_is_client_eval(async);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [c].[CustomerID]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Max_with_no_arg(bool async)
        {
            await base.Max_with_no_arg(async);

            AssertSql(
                @"SELECT MAX([o].[OrderID])
FROM [Orders] AS [o]");
        }

        public override async Task Max_with_arg(bool async)
        {
            await base.Max_with_arg(async);

            AssertSql(
                @"SELECT MAX([o].[OrderID])
FROM [Orders] AS [o]");
        }

        public override async Task Max_with_coalesce(bool async)
        {
            await base.Max_with_coalesce(async);

            AssertSql(
                @"SELECT MAX(COALESCE([p].[UnitPrice], 0.0))
FROM [Products] AS [p]
WHERE [p].[ProductID] < 40");
        }

        public override async Task Max_over_subquery_is_client_eval(bool async)
        {
            await base.Max_over_subquery_is_client_eval(async);

            AssertSql(
                @"SELECT (
    SELECT SUM([o].[OrderID])
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
)
FROM [Customers] AS [c]");
        }

        public override async Task Max_over_nested_subquery_is_client_eval(bool async)
        {
            await base.Max_over_nested_subquery_is_client_eval(async);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [c].[CustomerID]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Max_over_sum_subquery_is_client_eval(bool async)
        {
            await base.Max_over_sum_subquery_is_client_eval(async);

            AssertSql(
                @"@__p_0='3'

SELECT TOP(@__p_0) [c].[CustomerID]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task Count_with_predicate(bool async)
        {
            await base.Count_with_predicate(async);

            AssertSql(
                @"SELECT COUNT(*)
FROM [Orders] AS [o]
WHERE [o].[CustomerID] = N'ALFKI'");
        }

        public override async Task Where_OrderBy_Count(bool async)
        {
            await base.Where_OrderBy_Count(async);

            AssertSql(
                @"SELECT COUNT(*)
FROM [Orders] AS [o]
WHERE [o].[CustomerID] = N'ALFKI'");
        }

        public override async Task OrderBy_Where_Count(bool async)
        {
            await base.OrderBy_Where_Count(async);

            AssertSql(
                @"SELECT COUNT(*)
FROM [Orders] AS [o]
WHERE [o].[CustomerID] = N'ALFKI'");
        }

        public override async Task OrderBy_Count_with_predicate(bool async)
        {
            await base.OrderBy_Count_with_predicate(async);

            AssertSql(
                @"SELECT COUNT(*)
FROM [Orders] AS [o]
WHERE [o].[CustomerID] = N'ALFKI'");
        }

        public override async Task OrderBy_Where_Count_with_predicate(bool async)
        {
            await base.OrderBy_Where_Count_with_predicate(async);

            AssertSql(
                @"SELECT COUNT(*)
FROM [Orders] AS [o]
WHERE ([o].[OrderID] > 10) AND (([o].[CustomerID] <> N'ALFKI') OR [o].[CustomerID] IS NULL)");
        }

        public override async Task Distinct(bool async)
        {
            await base.Distinct(async);

            AssertSql(
                @"SELECT DISTINCT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]");
        }

        public override async Task Distinct_Scalar(bool async)
        {
            await base.Distinct_Scalar(async);

            AssertSql(
                @"SELECT DISTINCT [c].[City]
FROM [Customers] AS [c]");
        }

        public override async Task OrderBy_Distinct(bool async)
        {
            await base.OrderBy_Distinct(async);

            // Ordering not preserved by distinct when ordering columns not projected.
            AssertSql(
                @"SELECT DISTINCT [c].[City]
FROM [Customers] AS [c]");
        }

        public override async Task Distinct_OrderBy(bool async)
        {
            await base.Distinct_OrderBy(async);

            AssertSql(
                @"SELECT [t].[Country]
FROM (
    SELECT DISTINCT [c].[Country]
    FROM [Customers] AS [c]
) AS [t]
ORDER BY [t].[Country]");
        }

        public override async Task Distinct_OrderBy2(bool async)
        {
            await base.Distinct_OrderBy2(async);

            AssertSql(
                @"SELECT [t].[CustomerID], [t].[Address], [t].[City], [t].[CompanyName], [t].[ContactName], [t].[ContactTitle], [t].[Country], [t].[Fax], [t].[Phone], [t].[PostalCode], [t].[Region]
FROM (
    SELECT DISTINCT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    FROM [Customers] AS [c]
) AS [t]
ORDER BY [t].[CustomerID]");
        }

        public override async Task Distinct_OrderBy3(bool async)
        {
            await base.Distinct_OrderBy3(async);

            AssertSql(
                @"SELECT [t].[CustomerID]
FROM (
    SELECT DISTINCT [c].[CustomerID]
    FROM [Customers] AS [c]
) AS [t]
ORDER BY [t].[CustomerID]");
        }

        public override async Task Distinct_Count(bool async)
        {
            await base.Distinct_Count(async);

            AssertSql(
                @"SELECT COUNT(*)
FROM (
    SELECT DISTINCT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    FROM [Customers] AS [c]
) AS [t]");
        }

        public override async Task Select_Select_Distinct_Count(bool async)
        {
            await base.Select_Select_Distinct_Count(async);

            AssertSql(
                @"SELECT COUNT(*)
FROM (
    SELECT DISTINCT [c].[City]
    FROM [Customers] AS [c]
) AS [t]");
        }

        public override async Task Single_Predicate(bool async)
        {
            await base.Single_Predicate(async);

            AssertSql(
                @"SELECT TOP(2) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] = N'ALFKI'");
        }

        public override async Task FirstOrDefault_inside_subquery_gets_server_evaluated(bool async)
        {
            await base.FirstOrDefault_inside_subquery_gets_server_evaluated(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE ([c].[CustomerID] = N'ALFKI') AND ((
    SELECT TOP(1) [o].[CustomerID]
    FROM [Orders] AS [o]
    WHERE ([c].[CustomerID] = [o].[CustomerID]) AND ([o].[CustomerID] = N'ALFKI')) = N'ALFKI')");
        }

        public override async Task Multiple_collection_navigation_with_FirstOrDefault_chained(bool async)
        {
            await base.Multiple_collection_navigation_with_FirstOrDefault_chained(async);

            AssertSql(
                @"SELECT [c].[CustomerID]
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]",
                //
                @"@_outer_CustomerID='ALFKI' (Size = 5)

SELECT TOP(1) [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE [od].[OrderID] = COALESCE((
    SELECT TOP(1) [o].[OrderID]
    FROM [Orders] AS [o]
    WHERE @_outer_CustomerID = [o].[CustomerID]
    ORDER BY [o].[OrderID]
), 0)
ORDER BY [od].[ProductID]",
                //
                @"@_outer_CustomerID='ANATR' (Size = 5)

SELECT TOP(1) [od].[OrderID], [od].[ProductID], [od].[Discount], [od].[Quantity], [od].[UnitPrice]
FROM [Order Details] AS [od]
WHERE [od].[OrderID] = COALESCE((
    SELECT TOP(1) [o].[OrderID]
    FROM [Orders] AS [o]
    WHERE @_outer_CustomerID = [o].[CustomerID]
    ORDER BY [o].[OrderID]
), 0)
ORDER BY [od].[ProductID]");
        }

        public override async Task Multiple_collection_navigation_with_FirstOrDefault_chained_projecting_scalar(bool async)
        {
            await base.Multiple_collection_navigation_with_FirstOrDefault_chained_projecting_scalar(async);

            AssertSql(
                @"SELECT (
    SELECT TOP(1) [o].[ProductID]
    FROM [Order Details] AS [o]
    WHERE (
        SELECT TOP(1) [o0].[OrderID]
        FROM [Orders] AS [o0]
        WHERE [c].[CustomerID] = [o0].[CustomerID]
        ORDER BY [o0].[OrderID]) IS NOT NULL AND ((
        SELECT TOP(1) [o1].[OrderID]
        FROM [Orders] AS [o1]
        WHERE [c].[CustomerID] = [o1].[CustomerID]
        ORDER BY [o1].[OrderID]) = [o].[OrderID])
    ORDER BY [o].[ProductID])
FROM [Customers] AS [c]
ORDER BY [c].[CustomerID]");
        }

        public override async Task First_inside_subquery_gets_client_evaluated(bool async)
        {
            await base.First_inside_subquery_gets_client_evaluated(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE ([c].[CustomerID] = N'ALFKI') AND ((
    SELECT TOP(1) [o].[CustomerID]
    FROM [Orders] AS [o]
    WHERE ([c].[CustomerID] = [o].[CustomerID]) AND ([o].[CustomerID] = N'ALFKI')) = N'ALFKI')");
        }

        public override async Task Last(bool async)
        {
            await base.Last(async);

            AssertSql(
                @"SELECT TOP(1) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[ContactName] DESC");
        }

        public override async Task Last_Predicate(bool async)
        {
            await base.Last_Predicate(async);

            AssertSql(
                @"SELECT TOP(1) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[City] = N'London'
ORDER BY [c].[ContactName] DESC");
        }

        public override async Task Where_Last(bool async)
        {
            await base.Where_Last(async);

            AssertSql(
                @"SELECT TOP(1) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[City] = N'London'
ORDER BY [c].[ContactName] DESC");
        }

        public override async Task LastOrDefault(bool async)
        {
            await base.LastOrDefault(async);

            AssertSql(
                @"SELECT TOP(1) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
ORDER BY [c].[ContactName] DESC");
        }

        public override async Task LastOrDefault_Predicate(bool async)
        {
            await base.LastOrDefault_Predicate(async);

            AssertSql(
                @"SELECT TOP(1) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[City] = N'London'
ORDER BY [c].[ContactName] DESC");
        }

        public override async Task Where_LastOrDefault(bool async)
        {
            await base.Where_LastOrDefault(async);

            AssertSql(
                @"SELECT TOP(1) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[City] = N'London'
ORDER BY [c].[ContactName] DESC");
        }

        public override async Task Contains_with_subquery(bool async)
        {
            await base.Contains_with_subquery(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (
    SELECT [o].[CustomerID]
    FROM [Orders] AS [o]
)");
        }

        public override async Task Contains_with_local_array_closure(bool async)
        {
            await base.Contains_with_local_array_closure(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI')",
                //
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE')");
        }

        public override async Task Contains_with_subquery_and_local_array_closure(bool async)
        {
            await base.Contains_with_subquery_and_local_array_closure(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE EXISTS (
    SELECT 1
    FROM [Customers] AS [c0]
    WHERE [c0].[City] IN (N'London', N'Buenos Aires') AND ([c0].[CustomerID] = [c].[CustomerID]))",
                //
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE EXISTS (
    SELECT 1
    FROM [Customers] AS [c0]
    WHERE [c0].[City] IN (N'London') AND ([c0].[CustomerID] = [c].[CustomerID]))");
        }

        public override async Task Contains_with_local_uint_array_closure(bool async)
        {
            await base.Contains_with_local_uint_array_closure(async);

            AssertSql(
                @"SELECT [e].[EmployeeID], [e].[City], [e].[Country], [e].[FirstName], [e].[ReportsTo], [e].[Title]
FROM [Employees] AS [e]
WHERE [e].[EmployeeID] IN (0, 1)",
                //
                @"SELECT [e].[EmployeeID], [e].[City], [e].[Country], [e].[FirstName], [e].[ReportsTo], [e].[Title]
FROM [Employees] AS [e]
WHERE [e].[EmployeeID] IN (0)");
        }

        public override async Task Contains_with_local_nullable_uint_array_closure(bool async)
        {
            await base.Contains_with_local_nullable_uint_array_closure(async);

            AssertSql(
                @"SELECT [e].[EmployeeID], [e].[City], [e].[Country], [e].[FirstName], [e].[ReportsTo], [e].[Title]
FROM [Employees] AS [e]
WHERE [e].[EmployeeID] IN (0, 1)",
                //
                @"SELECT [e].[EmployeeID], [e].[City], [e].[Country], [e].[FirstName], [e].[ReportsTo], [e].[Title]
FROM [Employees] AS [e]
WHERE [e].[EmployeeID] IN (0)");
        }

        public override async Task Contains_with_local_array_inline(bool async)
        {
            await base.Contains_with_local_array_inline(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI')");
        }

        public override async Task Contains_with_local_list_closure(bool async)
        {
            await base.Contains_with_local_list_closure(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI')");
        }

        public override async Task Contains_with_local_object_list_closure(bool async)
        {
            await base.Contains_with_local_object_list_closure(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI')");
        }

        public override async Task Contains_with_local_list_closure_all_null(bool async)
        {
            await base.Contains_with_local_list_closure_all_null(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE CAST(0 AS bit) = CAST(1 AS bit)");
        }

        public override async Task Contains_with_local_list_inline(bool async)
        {
            await base.Contains_with_local_list_inline(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI')");
        }

        public override async Task Contains_with_local_list_inline_closure_mix(bool async)
        {
            await base.Contains_with_local_list_inline_closure_mix(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI')",
                //
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ANATR')");
        }

        public override async Task Contains_with_local_non_primitive_list_inline_closure_mix(bool async)
        {
            await base.Contains_with_local_non_primitive_list_inline_closure_mix(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI')",
                //
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ANATR')");
        }

        public override async Task Contains_with_local_non_primitive_list_closure_mix(bool async)
        {
            await base.Contains_with_local_non_primitive_list_closure_mix(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI')");
        }

        public override async Task Contains_with_local_collection_false(bool async)
        {
            await base.Contains_with_local_collection_false(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] NOT IN (N'ABCDE', N'ALFKI')");
        }

        public override async Task Contains_with_local_collection_complex_predicate_and(bool async)
        {
            await base.Contains_with_local_collection_complex_predicate_and(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE (([c].[CustomerID] = N'ALFKI') OR ([c].[CustomerID] = N'ABCDE')) AND [c].[CustomerID] IN (N'ABCDE', N'ALFKI')");
        }

        public override async Task Contains_with_local_collection_complex_predicate_or(bool async)
        {
            await base.Contains_with_local_collection_complex_predicate_or(async);

            // issue #18791
            //            AssertSql(
            //                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
            //FROM [Customers] AS [c]
            //WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI', N'ALFKI', N'ABCDE')");
        }

        public override async Task Contains_with_local_collection_complex_predicate_not_matching_ins1(bool async)
        {
            await base.Contains_with_local_collection_complex_predicate_not_matching_ins1(async);

            // issue #18791
            //            AssertSql(
            //                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
            //FROM [Customers] AS [c]
            //WHERE [c].[CustomerID] IN (N'ALFKI', N'ABCDE') OR [c].[CustomerID] NOT IN (N'ABCDE', N'ALFKI')");
        }

        public override async Task Contains_with_local_collection_complex_predicate_not_matching_ins2(bool async)
        {
            await base.Contains_with_local_collection_complex_predicate_not_matching_ins2(async);

            // issue #18791
            //            AssertSql(
            //                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
            //FROM [Customers] AS [c]
            //WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI') AND [c].[CustomerID] NOT IN (N'ALFKI', N'ABCDE')");
        }

        public override async Task Contains_with_local_collection_sql_injection(bool async)
        {
            await base.Contains_with_local_collection_sql_injection(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ALFKI', N'ABC'')); GO; DROP TABLE Orders; GO; --') OR (([c].[CustomerID] = N'ALFKI') OR ([c].[CustomerID] = N'ABCDE'))");
        }

        public override async Task Contains_with_local_collection_empty_closure(bool async)
        {
            await base.Contains_with_local_collection_empty_closure(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE CAST(1 AS bit) = CAST(0 AS bit)");
        }

        public override async Task Contains_with_local_collection_empty_inline(bool async)
        {
            await base.Contains_with_local_collection_empty_inline(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE CAST(1 AS bit) = CAST(1 AS bit)");
        }

        public override async Task Contains_top_level(bool async)
        {
            await base.Contains_top_level(async);

            AssertSql(
                @"@__p_0='ALFKI' (Size = 4000)

SELECT CASE
    WHEN @__p_0 IN (
        SELECT [c].[CustomerID]
        FROM [Customers] AS [c]
    )
     THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END");
        }

        public override async Task Contains_with_local_anonymous_type_array_closure(bool async)
        {
            await base.Contains_with_local_anonymous_type_array_closure(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice]
FROM [Order Details] AS [o]",
                //
                @"SELECT [o].[OrderID], [o].[ProductID], [o].[Discount], [o].[Quantity], [o].[UnitPrice]
FROM [Order Details] AS [o]");
        }

        public override void OfType_Select()
        {
            base.OfType_Select();

            AssertSql(
                @"SELECT TOP(1) [c].[City]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
ORDER BY [o].[OrderID]");
        }

        public override void OfType_Select_OfType_Select()
        {
            base.OfType_Select_OfType_Select();

            AssertSql(
                @"SELECT TOP(1) [c].[City]
FROM [Orders] AS [o]
LEFT JOIN [Customers] AS [c] ON [o].[CustomerID] = [c].[CustomerID]
ORDER BY [o].[OrderID]");
        }

        public override async Task Average_with_non_matching_types_in_projection_doesnt_produce_second_explicit_cast(bool async)
        {
            await base.Average_with_non_matching_types_in_projection_doesnt_produce_second_explicit_cast(async);

            AssertSql(
                @"SELECT AVG(CAST(CAST([o].[OrderID] AS bigint) AS float))
FROM [Orders] AS [o]
WHERE [o].[CustomerID] IS NOT NULL AND ([o].[CustomerID] LIKE N'A%')");
        }

        public override async Task Max_with_non_matching_types_in_projection_introduces_explicit_cast(bool async)
        {
            await base.Max_with_non_matching_types_in_projection_introduces_explicit_cast(async);

            AssertSql(
                @"SELECT MAX(CAST([o].[OrderID] AS bigint))
FROM [Orders] AS [o]
WHERE [o].[CustomerID] IS NOT NULL AND ([o].[CustomerID] LIKE N'A%')");
        }

        public override async Task Min_with_non_matching_types_in_projection_introduces_explicit_cast(bool async)
        {
            await base.Min_with_non_matching_types_in_projection_introduces_explicit_cast(async);

            AssertSql(
                @"SELECT MIN(CAST([o].[OrderID] AS bigint))
FROM [Orders] AS [o]
WHERE [o].[CustomerID] IS NOT NULL AND ([o].[CustomerID] LIKE N'A%')");
        }

        public override async Task OrderBy_Take_Last_gives_correct_result(bool async)
        {
            await base.OrderBy_Take_Last_gives_correct_result(async);

            AssertSql(
                @"@__p_0='20'

SELECT TOP(1) [t].[CustomerID], [t].[Address], [t].[City], [t].[CompanyName], [t].[ContactName], [t].[ContactTitle], [t].[Country], [t].[Fax], [t].[Phone], [t].[PostalCode], [t].[Region]
FROM (
    SELECT TOP(@__p_0) [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    FROM [Customers] AS [c]
    ORDER BY [c].[CustomerID]
) AS [t]
ORDER BY [t].[CustomerID] DESC");
        }

        public override async Task OrderBy_Skip_Last_gives_correct_result(bool async)
        {
            await base.OrderBy_Skip_Last_gives_correct_result(async);

            AssertSql(
                @"@__p_0='20'

SELECT TOP(1) [t].[CustomerID], [t].[Address], [t].[City], [t].[CompanyName], [t].[ContactName], [t].[ContactTitle], [t].[Country], [t].[Fax], [t].[Phone], [t].[PostalCode], [t].[Region]
FROM (
    SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
    FROM [Customers] AS [c]
    ORDER BY [c].[CustomerID]
    OFFSET @__p_0 ROWS
) AS [t]
ORDER BY [t].[CustomerID] DESC");
        }

        public override void Contains_over_entityType_should_rewrite_to_identity_equality()
        {
            base.Contains_over_entityType_should_rewrite_to_identity_equality();

            AssertSql(
                @"SELECT TOP(2) [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
WHERE [o].[OrderID] = 10248",
                //
                @"@__entity_equality_p_0_OrderID='10248' (Nullable = true)

SELECT CASE
    WHEN @__entity_equality_p_0_OrderID IN (
        SELECT [o].[OrderID]
        FROM [Orders] AS [o]
        WHERE [o].[CustomerID] = N'VINET'
    )
     THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END");
        }

        public override async Task List_Contains_over_entityType_should_rewrite_to_identity_equality(bool async)
        {
            await base.List_Contains_over_entityType_should_rewrite_to_identity_equality(async);

            AssertSql(
                @"@__entity_equality_someOrder_0_OrderID='10248' (Nullable = true)

SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE @__entity_equality_someOrder_0_OrderID IN (
    SELECT [o].[OrderID]
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
)");
        }

        public override async Task List_Contains_with_constant_list(bool async)
        {
            await base.List_Contains_with_constant_list(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ALFKI', N'ANATR')");
        }

        public override async Task List_Contains_with_parameter_list(bool async)
        {
            await base.List_Contains_with_parameter_list(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ALFKI', N'ANATR')");
        }

        public override async Task Contains_with_parameter_list_value_type_id(bool async)
        {
            await base.Contains_with_parameter_list_value_type_id(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
WHERE [o].[OrderID] IN (10248, 10249)");
        }

        public override async Task Contains_with_constant_list_value_type_id(bool async)
        {
            await base.Contains_with_constant_list_value_type_id(async);

            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]
WHERE [o].[OrderID] IN (10248, 10249)");
        }

        public override async Task HashSet_Contains_with_parameter(bool async)
        {
            await base.HashSet_Contains_with_parameter(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ALFKI')");
        }

        public override async Task ImmutableHashSet_Contains_with_parameter(bool async)
        {
            await base.ImmutableHashSet_Contains_with_parameter(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ALFKI')");
        }

        public override void Contains_over_entityType_with_null_should_rewrite_to_identity_equality()
        {
            base.Contains_over_entityType_with_null_should_rewrite_to_identity_equality();

            AssertSql(
                @"@__entity_equality_p_0_OrderID=NULL (DbType = Int32)

SELECT CASE
    WHEN @__entity_equality_p_0_OrderID IN (
        SELECT [o].[OrderID]
        FROM [Orders] AS [o]
        WHERE [o].[CustomerID] = N'VINET'
    )
     THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END");
        }

        public override async Task String_FirstOrDefault_in_projection_does_client_eval(bool async)
        {
            await base.String_FirstOrDefault_in_projection_does_client_eval(async);

            AssertSql(
                @"SELECT [c].[CustomerID]
FROM [Customers] AS [c]");
        }

        public override async Task Project_constant_Sum(bool async)
        {
            await base.Project_constant_Sum(async);

            AssertSql(
                @"SELECT SUM(1)
FROM [Employees] AS [e]");
        }

        public override async Task Where_subquery_any_equals_operator(bool async)
        {
            await base.Where_subquery_any_equals_operator(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI', N'ANATR')");
        }

        public override async Task Where_subquery_any_equals(bool async)
        {
            await base.Where_subquery_any_equals(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI', N'ANATR')");
        }

        public override async Task Where_subquery_any_equals_static(bool async)
        {
            await base.Where_subquery_any_equals_static(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] IN (N'ABCDE', N'ALFKI', N'ANATR')");
        }

        public override async Task Where_subquery_where_any(bool async)
        {
            await base.Where_subquery_where_any(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE ([c].[City] = N'México D.F.') AND [c].[CustomerID] IN (N'ABCDE', N'ALFKI', N'ANATR')",
                //
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE ([c].[City] = N'México D.F.') AND [c].[CustomerID] IN (N'ABCDE', N'ALFKI', N'ANATR')");
        }

        public override async Task Where_subquery_all_not_equals_operator(bool async)
        {
            await base.Where_subquery_all_not_equals_operator(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] NOT IN (N'ABCDE', N'ALFKI', N'ANATR')");
        }

        public override async Task Where_subquery_all_not_equals(bool async)
        {
            await base.Where_subquery_all_not_equals(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] NOT IN (N'ABCDE', N'ALFKI', N'ANATR')");
        }

        public override async Task Where_subquery_all_not_equals_static(bool async)
        {
            await base.Where_subquery_all_not_equals_static(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE [c].[CustomerID] NOT IN (N'ABCDE', N'ALFKI', N'ANATR')");
        }

        public override async Task Where_subquery_where_all(bool async)
        {
            await base.Where_subquery_where_all(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE ([c].[City] = N'México D.F.') AND [c].[CustomerID] NOT IN (N'ABCDE', N'ALFKI', N'ANATR')",
                //
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE ([c].[City] = N'México D.F.') AND [c].[CustomerID] NOT IN (N'ABCDE', N'ALFKI', N'ANATR')");
        }

        public override async Task Cast_to_same_Type_Count_works(bool async)
        {
            await base.Cast_to_same_Type_Count_works(async);

            AssertSql(
                @"SELECT COUNT(*)
FROM [Customers] AS [c]");
        }

        public override async Task Cast_before_aggregate_is_preserved(bool async)
        {
            await base.Cast_before_aggregate_is_preserved(async);

            AssertSql(
                @"SELECT (
    SELECT AVG(CAST([o].[OrderID] AS float))
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID])
FROM [Customers] AS [c]");
        }

        public override async Task DefaultIfEmpty_selects_only_required_columns(bool async)
        {
            await base.DefaultIfEmpty_selects_only_required_columns(async);

            AssertSql(
                @"SELECT [p].[ProductName]
FROM (
    SELECT NULL AS [empty]
) AS [empty]
LEFT JOIN [Products] AS [p] ON 1 = 1");
        }

        public override async Task Collection_Last_member_access_in_projection_translated(bool async)
        {
            await base.Collection_Last_member_access_in_projection_translated(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE ([c].[CustomerID] LIKE N'F%') AND ((
    SELECT TOP(1) [o].[CustomerID]
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
    ORDER BY [o].[OrderID]) = [c].[CustomerID])");
        }

        public override async Task Collection_LastOrDefault_member_access_in_projection_translated(bool async)
        {
            await base.Collection_LastOrDefault_member_access_in_projection_translated(async);

            AssertSql(
                @"SELECT [c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
FROM [Customers] AS [c]
WHERE ([c].[CustomerID] LIKE N'F%') AND ((
    SELECT TOP(1) [o].[CustomerID]
    FROM [Orders] AS [o]
    WHERE [c].[CustomerID] = [o].[CustomerID]
    ORDER BY [o].[OrderID]) = [c].[CustomerID])");
        }

        public override async Task Sum_over_explicit_cast_over_column(bool async)
        {
            await base.Sum_over_explicit_cast_over_column(async);

            AssertSql(
                @"SELECT SUM(CAST([o].[OrderID] AS bigint))
FROM [Orders] AS [o]");
        }

        public override async Task Count_on_projection_with_client_eval(bool async)
        {
            await base.Count_on_projection_with_client_eval(async);

            AssertSql(
                @"SELECT COUNT(*)
FROM [Orders] AS [o]",
                //
                @"SELECT COUNT(*)
FROM [Orders] AS [o]",
                //
                @"SELECT COUNT(*)
FROM [Orders] AS [o]");
        }
    }
}
