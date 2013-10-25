﻿// Copyright (c) Tier 3 Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. 

using System;
using System.Linq;
using System.Linq.Expressions;

namespace ElasticLinq.Request.Visitors
{
    /// <summary>
    /// PartialEvaluator determines which part of the tree can be locally
    /// evaluated before execution and substitutes those parts with constant
    /// values obtained from that local execution.
    /// </summary>
    internal static class PartialEvaluator
    {
        private static readonly Type[] doNotEvaluateMembersDeclaredOn = { typeof(ElasticFields) };
        private static readonly Type[] doNotEvaluateMethodsDeclaredOn = { typeof(Enumerable), typeof(ElasticQueryExtensions), typeof(Queryable) };

        public static Expression Evaluate(Expression e)
        {
            var chosenForEvaluation = BranchSelectExpressionVisitor.Select(e, ShouldEvaluate);
            return EvaluatingExpressionVisitor.Evaluate(e, chosenForEvaluation);
        }

        internal static bool ShouldEvaluate(Expression e)
        {
            if (e.NodeType == ExpressionType.Parameter || e.NodeType == ExpressionType.Lambda)
                return false;

            if (e is MemberExpression && doNotEvaluateMembersDeclaredOn.Contains(((MemberExpression)e).Member.DeclaringType) ||
               (e is MethodCallExpression && doNotEvaluateMethodsDeclaredOn.Contains(((MethodCallExpression)e).Method.DeclaringType)))
                return false;

            return true;
        }
    }
}