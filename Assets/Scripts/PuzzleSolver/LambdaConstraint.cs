﻿using System.Collections.Generic;

namespace PuzzleSolvers
{
    /// <summary>
    ///     Describes the function signature required for a <see cref="LambdaConstraint"/>. See <see
    ///     cref="Constraint.MarkTakens(bool[][], int?[], int?, int, int)"/> for parameter and return value documentation.</summary>
    delegate IEnumerable<Constraint> CustomConstraint(bool[][] takens, int?[] grid, int? ix, int minValue, int maxValue);

    /// <summary>Can be used to describe any constraint that applies to the whole puzzle using a lambda expression.</summary>
    sealed class LambdaConstraint : Constraint
    {
        /// <summary>The function used to evaluate this constraint.</summary>
        public CustomConstraint Lambda { get; private set; }

        /// <summary>Constructor.</summary>
        public LambdaConstraint(CustomConstraint lambda, IEnumerable<int> affectedCells = null) : base(affectedCells)
        {
            Lambda = lambda;
        }

        /// <summary>Override; see base.</summary>
        public override IEnumerable<Constraint> MarkTakens(bool[][] takens, int?[] grid, int? ix, int minValue, int maxValue) { return Lambda(takens, grid, ix, minValue, maxValue); }
    }
}
