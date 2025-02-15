// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    public partial class SimpleQueryCosmosTest
    {
        public override async Task Projection_when_arithmetic_expression_precedence(bool async)
        {
            await base.Projection_when_arithmetic_expression_precedence(async);

            AssertSql(
                @"SELECT (c[""OrderID""] / (c[""OrderID""] / 2)) AS A, ((c[""OrderID""] / c[""OrderID""]) / 2) AS B
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Projection_when_arithmetic_expressions(bool async)
        {
            await base.Projection_when_arithmetic_expressions(async);

            AssertSql(
                @"SELECT c[""OrderID""], (c[""OrderID""] * 2) AS Double, (c[""OrderID""] + 23) AS Add, (100000 - c[""OrderID""]) AS Sub, (c[""OrderID""] / (c[""OrderID""] / 2)) AS Divide, 42 AS Literal, c AS o
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override async Task Projection_when_arithmetic_mixed(bool async)
        {
            await base.Projection_when_arithmetic_mixed(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Projection_when_arithmetic_mixed_subqueries(bool async)
        {
            await base.Projection_when_arithmetic_mixed_subqueries(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Projection_when_null_value(bool async)
        {
            await base.Projection_when_null_value(async);

            AssertSql(
                @"SELECT c[""Region""]
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalTheory(Skip = "Issue$14935")]
        public override async Task Projection_when_client_evald_subquery(bool async)
        {
            await base.Projection_when_client_evald_subquery(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Project_to_object_array(bool async)
        {
            await base.Project_to_object_array(async);

            AssertSql(
                @"SELECT c[""EmployeeID""], c[""ReportsTo""], c[""Title""]
FROM root c
WHERE ((c[""Discriminator""] = ""Employee"") AND (c[""EmployeeID""] = 1))");
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override async Task Projection_of_entity_type_into_object_array(bool async)
        {
            await base.Projection_of_entity_type_into_object_array(async);

            AssertSql(
                @"SELECT c[""CustomerID""], c[""Address""], c[""City""], c[""CompanyName""], c[""ContactName""], c[""ContactTitle""], c[""Country""], c[""Fax""], c[""Phone""], c[""PostalCode""], c[""Region""]
FROM root c
WHERE ((c[""Discriminator""] = ""Employee"") AND c[""CustomerID""] LIKE N'A%'
ORDER BY c[""CustomerID""]");
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override async Task Projection_of_multiple_entity_types_into_object_array(bool async)
        {
            await base.Projection_of_multiple_entity_types_into_object_array(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Projection_of_entity_type_into_object_list(bool async)
        {
            await base.Projection_of_entity_type_into_object_list(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Project_to_int_array(bool async)
        {
            await base.Project_to_int_array(async);

            AssertSql(
                @"SELECT c[""EmployeeID""], c[""ReportsTo""]
FROM root c
WHERE ((c[""Discriminator""] = ""Employee"") AND (c[""EmployeeID""] = 1))");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Select_bool_closure_with_order_by_property_with_cast_to_nullable(bool async)
        {
            await base.Select_bool_closure_with_order_by_property_with_cast_to_nullable(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Select_bool_closure_with_order_parameter_with_cast_to_nullable(bool async)
        {
            await base.Select_bool_closure_with_order_parameter_with_cast_to_nullable(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_scalar(bool async)
        {
            await base.Select_scalar(async);

            AssertSql(
                @"SELECT c[""City""]
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_anonymous_one(bool async)
        {
            await base.Select_anonymous_one(async);

            AssertSql(
                @"SELECT c[""City""]
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_anonymous_two(bool async)
        {
            await base.Select_anonymous_two(async);

            AssertSql(
                @"SELECT c[""City""], c[""Phone""]
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_anonymous_three(bool async)
        {
            await base.Select_anonymous_three(async);

            AssertSql(
                @"SELECT c[""City""], c[""Phone""], c[""Country""]
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_anonymous_bool_constant_true(bool async)
        {
            await base.Select_anonymous_bool_constant_true(async);

            AssertSql(
                @"SELECT c[""CustomerID""], true AS ConstantTrue
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_anonymous_constant_in_expression(bool async)
        {
            await base.Select_anonymous_constant_in_expression(async);

            AssertSql(
                @"SELECT c[""CustomerID""]
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_anonymous_conditional_expression(bool async)
        {
            await base.Select_anonymous_conditional_expression(async);

            AssertSql(
                @"SELECT c[""ProductID""], (c[""UnitsInStock""] > 0) AS IsAvailable
FROM root c
WHERE (c[""Discriminator""] = ""Product"")");
        }

        public override async Task Select_anonymous_with_object(bool async)
        {
            await base.Select_anonymous_with_object(async);

            AssertSql(
                @"SELECT c[""City""], c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_constant_int(bool async)
        {
            await base.Select_constant_int(async);

            AssertSql(
                @"SELECT 0 AS c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_constant_null_string(bool async)
        {
            await base.Select_constant_null_string(async);

            AssertSql(
                @"SELECT null AS c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_local(bool async)
        {
            await base.Select_local(async);

            AssertSql(
                @"@__x_0='10'

SELECT @__x_0 AS c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_scalar_primitive_after_take(bool async)
        {
            await base.Select_scalar_primitive_after_take(async);

            AssertSql(
                @"@__p_0='9'

SELECT c[""EmployeeID""]
FROM root c
WHERE (c[""Discriminator""] = ""Employee"")
OFFSET 0 LIMIT @__p_0");
        }

        public override async Task Select_project_filter(bool async)
        {
            await base.Select_project_filter(async);

            AssertSql(
                @"SELECT c[""CompanyName""]
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""City""] = ""London""))");
        }

        public override async Task Select_project_filter2(bool async)
        {
            await base.Select_project_filter2(async);

            AssertSql(
                @"SELECT c[""City""]
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""City""] = ""London""))");
        }

        public override async Task Select_nested_collection(bool async)
        {
            await base.Select_nested_collection(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""City""] = ""London""))");
        }

        [ConditionalFact(Skip = "Issue #17246")]
        public override void Select_nested_collection_multi_level()
        {
            base.Select_nested_collection_multi_level();

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalFact(Skip = "Issue#17246")]
        public override void Select_nested_collection_multi_level2()
        {
            base.Select_nested_collection_multi_level2();

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalFact(Skip = "Issue#17246")]
        public override void Select_nested_collection_multi_level3()
        {
            base.Select_nested_collection_multi_level3();

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalFact(Skip = "Issue#17246")]
        public override void Select_nested_collection_multi_level4()
        {
            base.Select_nested_collection_multi_level4();

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalFact(Skip = "Issue#17246")]
        public override void Select_nested_collection_multi_level5()
        {
            using (var context = CreateContext())
            {
                var customers = context.Customers
                    .Where(c => c.CustomerID == "ALFKI")
                    .Select(
                        c => new
                        {
                            Order = (int?)c.Orders
                                .Where(o => o.OrderID < 10500)
                                .Select(
                                    o => o.OrderDetails
                                        .Where(od => od.OrderID != c.Orders.Count)
                                        .Select(od => od.ProductID)
                                        .FirstOrDefault())
                                .FirstOrDefault()
                        })
                    .ToList();

                Assert.Single(customers);
                Assert.Equal(0, customers.Count(c => c.Order != null && c.Order != 0));
            }

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""CustomerID""] = ""ALFKI""))");
        }

        [ConditionalFact(Skip = "Issue#17246")]
        public override void Select_nested_collection_multi_level6()
        {
            base.Select_nested_collection_multi_level6();

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override async Task Select_nested_collection_count_using_anonymous_type(bool async)
        {
            await base.Select_nested_collection_count_using_anonymous_type(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override async Task New_date_time_in_anonymous_type_works(bool async)
        {
            await base.New_date_time_in_anonymous_type_works(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_non_matching_value_types_int_to_long_introduces_explicit_cast(bool async)
        {
            await base.Select_non_matching_value_types_int_to_long_introduces_explicit_cast(async);

            AssertSql(
                @"SELECT c[""OrderID""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_nullable_int_to_long_introduces_explicit_cast(bool async)
        {
            await base.Select_non_matching_value_types_nullable_int_to_long_introduces_explicit_cast(async);

            AssertSql(
                @"SELECT c[""EmployeeID""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_nullable_int_to_int_doesnt_introduce_explicit_cast(bool async)
        {
            await base.Select_non_matching_value_types_nullable_int_to_int_doesnt_introduce_explicit_cast(async);

            AssertSql(
                @"SELECT c[""EmployeeID""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_int_to_nullable_int_doesnt_introduce_explicit_cast(bool async)
        {
            await base.Select_non_matching_value_types_int_to_nullable_int_doesnt_introduce_explicit_cast(async);

            AssertSql(
                @"SELECT c[""OrderID""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_from_binary_expression_introduces_explicit_cast(bool async)
        {
            await base.Select_non_matching_value_types_from_binary_expression_introduces_explicit_cast(async);

            AssertSql(
                @"SELECT (c[""OrderID""] + c[""OrderID""]) AS c
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_from_binary_expression_nested_introduces_top_level_explicit_cast(
            bool async)
        {
            await base.Select_non_matching_value_types_from_binary_expression_nested_introduces_top_level_explicit_cast(async);

            AssertSql(
                @"SELECT c[""OrderID""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_from_unary_expression_introduces_explicit_cast1(bool async)
        {
            await base.Select_non_matching_value_types_from_unary_expression_introduces_explicit_cast1(async);

            AssertSql(
                @"SELECT -(c[""OrderID""]) AS c
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_from_unary_expression_introduces_explicit_cast2(bool async)
        {
            await base.Select_non_matching_value_types_from_unary_expression_introduces_explicit_cast2(async);

            AssertSql(
                @"SELECT c[""OrderID""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_from_length_introduces_explicit_cast(bool async)
        {
            await base.Select_non_matching_value_types_from_length_introduces_explicit_cast(async);

            AssertSql(
                @"SELECT c[""CustomerID""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_from_method_call_introduces_explicit_cast(bool async)
        {
            await base.Select_non_matching_value_types_from_method_call_introduces_explicit_cast(async);

            AssertSql(
                @"SELECT c[""OrderID""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        public override async Task Select_non_matching_value_types_from_anonymous_type_introduces_explicit_cast(bool async)
        {
            await base.Select_non_matching_value_types_from_anonymous_type_introduces_explicit_cast(async);

            AssertSql(
                @"SELECT c[""OrderID""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))
ORDER BY c[""OrderID""]");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task
            Project_single_element_from_collection_with_OrderBy_Distinct_and_FirstOrDefault_followed_by_projecting_length(
                bool async)
        {
            await base.Project_single_element_from_collection_with_OrderBy_Distinct_and_FirstOrDefault_followed_by_projecting_length(
                async);

            AssertSql("");
        }

        public override async Task Select_conditional_with_null_comparison_in_test(bool async)
        {
            await base.Select_conditional_with_null_comparison_in_test(async);

            AssertSql(
                @"SELECT ((c[""CustomerID""] = null) ? true : (c[""OrderID""] < 100)) AS c
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""CustomerID""] = ""ALFKI""))");
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override async Task Projection_in_a_subquery_should_be_liftable(bool async)
        {
            await base.Projection_in_a_subquery_should_be_liftable(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Employee"")");
        }

        public override async Task Projection_containing_DateTime_subtraction(bool async)
        {
            await base.Projection_containing_DateTime_subtraction(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""OrderID""] < 10300))");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault(bool async)
        {
            await AssertQuery(
                async,
                ss => ss.Set<Customer>().Where(c => c.CustomerID == "ALFKI").Select(
                    c => c.Orders.OrderBy(o => o.OrderID).Select(o => o.CustomerID).Take(1).FirstOrDefault()));

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""CustomerID""] = ""ALFKI""))");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Project_single_element_from_collection_with_OrderBy_Skip_and_FirstOrDefault(bool async)
        {
            await AssertQuery(
                async,
                ss => ss.Set<Customer>().Where(c => c.CustomerID == "ALFKI").Select(
                    c => c.Orders.OrderBy(o => o.OrderID).Select(o => o.CustomerID).Skip(1).FirstOrDefault()));

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""CustomerID""] = ""ALFKI""))");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Project_single_element_from_collection_with_OrderBy_Distinct_and_FirstOrDefault(bool async)
        {
            await AssertQuery(
                async,
                ss => ss.Set<Customer>().Where(c => c.CustomerID == "ALFKI").Select(
                    c => c.Orders.OrderBy(o => o.OrderID).Select(o => o.CustomerID).Distinct().FirstOrDefault()));

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""CustomerID""] = ""ALFKI""))");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Project_single_element_from_collection_with_OrderBy_Take_and_SingleOrDefault(bool async)
        {
            await base.Project_single_element_from_collection_with_OrderBy_Take_and_SingleOrDefault(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""CustomerID""] = ""ALFKI""))");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Project_single_element_from_collection_with_OrderBy_Take_and_FirstOrDefault_with_parameter(bool async)
        {
            await AssertQuery(
                async,
                ss => ss.Set<Customer>().Where(c => c.CustomerID == "ALFKI").Select(
                    c => c.Orders.OrderBy(o => o.OrderID).Select(o => o.CustomerID).Take(1).FirstOrDefault()));

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""CustomerID""] = ""ALFKI""))");
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override async Task Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault(bool async)
        {
            await AssertQuery(
                async,
                ss => ss.Set<Customer>().Where(c => c.CustomerID == "ALFKI").Select(
                    c => c.Orders.OrderBy(o => o.OrderID)
                        .ThenByDescending(o => o.OrderDate)
                        .Select(o => o.CustomerID)
                        .Take(2)
                        .FirstOrDefault()));

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""CustomerID""] = ""ALFKI""))");
        }

        public override async Task
            Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault_followed_by_projection_of_length_property(
                bool async)
        {
            await base
                .Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault_followed_by_projection_of_length_property(
                    async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override async Task Project_single_element_from_collection_with_multiple_OrderBys_Take_and_FirstOrDefault_2(bool async)
        {
            await AssertQuery(
                async,
                ss => ss.Set<Customer>().Where(c => c.CustomerID == "ALFKI").Select(
                    c => c.Orders.OrderBy(o => o.CustomerID)
                        .ThenByDescending(o => o.OrderDate)
                        .Select(o => o.CustomerID)
                        .Take(2)
                        .FirstOrDefault()));

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Customer"") AND (c[""CustomerID""] = ""ALFKI""))");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Project_single_element_from_collection_with_OrderBy_over_navigation_Take_and_FirstOrDefault(bool async)
        {
            await AssertQueryScalar(
                async,
                ss => ss.Set<Order>().Where(o => o.OrderID < 10250)
                    .Select(o => o.OrderDetails.OrderBy(od => od.Product.ProductName).Select(od => od.OrderID).Take(1).FirstOrDefault()));

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""OrderID""] < 10250))");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Project_single_element_from_collection_with_OrderBy_over_navigation_Take_and_FirstOrDefault_2(
            bool async)
        {
            await base.Project_single_element_from_collection_with_OrderBy_over_navigation_Take_and_FirstOrDefault_2(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE ((c[""Discriminator""] = ""Order"") AND (c[""OrderID""] < 10250))");
        }

        public override async Task Select_datetime_year_component(bool async)
        {
            await base.Select_datetime_year_component(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_datetime_month_component(bool async)
        {
            await base.Select_datetime_month_component(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_datetime_day_of_year_component(bool async)
        {
            await base.Select_datetime_day_of_year_component(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_datetime_day_component(bool async)
        {
            await base.Select_datetime_day_component(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_datetime_hour_component(bool async)
        {
            await base.Select_datetime_hour_component(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_datetime_minute_component(bool async)
        {
            await base.Select_datetime_minute_component(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_datetime_second_component(bool async)
        {
            await base.Select_datetime_second_component(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_datetime_millisecond_component(bool async)
        {
            await base.Select_datetime_millisecond_component(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_byte_constant(bool async)
        {
            await base.Select_byte_constant(async);

            AssertSql(
                @"SELECT ((c[""CustomerID""] = ""ALFKI"") ? 1 : 2) AS c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_short_constant(bool async)
        {
            await base.Select_short_constant(async);

            AssertSql(
                @"SELECT ((c[""CustomerID""] = ""ALFKI"") ? 1 : 2) AS c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Select_bool_constant(bool async)
        {
            await base.Select_bool_constant(async);

            AssertSql(
                @"SELECT ((c[""CustomerID""] = ""ALFKI"") ? true : false) AS c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        public override async Task Anonymous_projection_AsNoTracking_Selector(bool async)
        {
            await base.Anonymous_projection_AsNoTracking_Selector(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Anonymous_projection_with_repeated_property_being_ordered(bool async)
        {
            await base.Anonymous_projection_with_repeated_property_being_ordered(async);

            AssertSql(
                @"SELECT c[""CustomerID""] AS A
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")
ORDER BY c[""CustomerID""]");
        }

        [ConditionalTheory(Skip = "Issue #17246")]
        public override async Task Anonymous_projection_with_repeated_property_being_ordered_2(bool async)
        {
            await base.Anonymous_projection_with_repeated_property_being_ordered_2(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_GetValueOrDefault_on_DateTime(bool async)
        {
            await base.Select_GetValueOrDefault_on_DateTime(async);

            AssertSql(
                @"SELECT c[""OrderDate""]
FROM root c
WHERE (c[""Discriminator""] = ""Order"")");
        }

        public override async Task Select_GetValueOrDefault_on_DateTime_with_null_values(bool async)
        {
            await base.Select_GetValueOrDefault_on_DateTime_with_null_values(async);

            AssertSql(
                @"SELECT c
FROM root c
WHERE (c[""Discriminator""] = ""Customer"")");
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override Task Client_method_in_projection_requiring_materialization_1(bool async)
        {
            return base.Client_method_in_projection_requiring_materialization_1(async);
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override Task Client_method_in_projection_requiring_materialization_2(bool async)
        {
            return base.Client_method_in_projection_requiring_materialization_2(async);
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override Task Project_non_nullable_value_after_FirstOrDefault_on_empty_collection(bool async)
        {
            return base.Project_non_nullable_value_after_FirstOrDefault_on_empty_collection(async);
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override Task Filtered_collection_projection_is_tracked(bool async)
        {
            return base.Filtered_collection_projection_is_tracked(async);
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override Task Filtered_collection_projection_with_to_list_is_tracked(bool async)
        {
            return base.Filtered_collection_projection_with_to_list_is_tracked(async);
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override Task ToList_Count_in_projection_works(bool async)
        {
            return base.ToList_Count_in_projection_works(async);
        }

        [ConditionalTheory(Skip = "Issue#17246")]
        public override Task LastOrDefault_member_access_in_projection_translates_to_server(bool async)
        {
            return base.LastOrDefault_member_access_in_projection_translates_to_server(async);
        }
    }
}
