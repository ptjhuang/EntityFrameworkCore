// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class GearsOfWarQueryInMemoryTest : GearsOfWarQueryTestBase<GearsOfWarQueryInMemoryFixture>
    {
        public GearsOfWarQueryInMemoryTest(GearsOfWarQueryInMemoryFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            //TestLoggerFactory.TestOutputHelper = testOutputHelper;
        }

        [ConditionalTheory(Skip = "issue #17386")]
        public override Task Correlated_collection_order_by_constant_null_of_non_mapped_type(bool async)
        {
            return base.Correlated_collection_order_by_constant_null_of_non_mapped_type(async);
        }

        [ConditionalTheory(Skip = "issue #17386")]
        public override Task Client_side_equality_with_parameter_works_with_optional_navigations(bool async)
        {
            return base.Client_side_equality_with_parameter_works_with_optional_navigations(async);
        }

        [ConditionalTheory(Skip = "issue #17386")]
        public override Task Where_coalesce_with_anonymous_types(bool async)
        {
            return base.Where_coalesce_with_anonymous_types(async);
        }

        [ConditionalTheory(Skip = "issue #17386")]
        public override Task Where_conditional_with_anonymous_type(bool async)
        {
            return base.Where_conditional_with_anonymous_type(async);
        }

        [ConditionalTheory(Skip = "issue #17386")]
        public override Task GetValueOrDefault_on_DateTimeOffset(bool async)
        {
            return base.GetValueOrDefault_on_DateTimeOffset(async);
        }

        [ConditionalFact(Skip = "issue #17537")]
        public override void Include_on_GroupJoin_SelectMany_DefaultIfEmpty_with_coalesce_result1()
        {
            base.Include_on_GroupJoin_SelectMany_DefaultIfEmpty_with_coalesce_result1();
        }

        [ConditionalFact(Skip = "issue #17537")]
        public override void Include_on_GroupJoin_SelectMany_DefaultIfEmpty_with_coalesce_result2()
        {
            base.Include_on_GroupJoin_SelectMany_DefaultIfEmpty_with_coalesce_result2();
        }

        [ConditionalTheory(Skip = "issue #17540")]
        public override Task
            Null_semantics_is_correctly_applied_for_function_comparisons_that_take_arguments_from_optional_navigation_complex(bool async)
        {
            return base.Null_semantics_is_correctly_applied_for_function_comparisons_that_take_arguments_from_optional_navigation_complex(
                async);
        }

        [ConditionalTheory(Skip = "issue #18284")]
        public override Task GroupBy_with_boolean_groupin_key_thru_navigation_access(bool async)
        {
            return GroupBy_with_boolean_groupin_key_thru_navigation_access(async);
        }

        [ConditionalTheory(Skip = "issue #17260")]
        public override Task Select_subquery_projecting_single_constant_inside_anonymous(bool async)
        {
            return base.Select_subquery_projecting_single_constant_inside_anonymous(async);
        }
    }
}
