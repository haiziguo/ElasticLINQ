﻿// Licensed under the Apache 2.0 License. See LICENSE.txt in the project root for more information.

using ElasticLinq.Request.Expressions;
using ElasticLinq.Request.Criteria;
using Xunit;

namespace ElasticLinq.Test.Request.Expressions
{
    public class CriteriaExpressionTests
    {
        [Fact]
        public void ConstructorSetsCriteria()
        {
            var criteria = new TermCriteria("field", "value");

            var expression = new CriteriaExpression(criteria);

            Assert.Same(criteria, expression.Criteria);
        }

        [Fact]
        public void ExpressionsTypeIsBool()
        {
            var criteria = new TermCriteria("field", "value");

            var expression = new CriteriaExpression(criteria);

            Assert.Equal(typeof(bool), expression.Type);
        }

        [Fact]
        public void CanReduceIsAlwaysFalse()
        {
            var criteria = new TermCriteria("field", "value");

            var expression = new CriteriaExpression(criteria);

            Assert.False(expression.CanReduce);
        }

        [Fact]
        public void ToStringReturnsCriteriaToString()
        {
            var criteria = new TermCriteria("field", "value");

            var expression = new CriteriaExpression(criteria);

            Assert.Equal(criteria.ToString(), expression.ToString());
        }
    }
}