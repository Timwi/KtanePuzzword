﻿using System;
using System.Collections.Generic;

namespace PuzzleSolvers
{
    /// <summary>Can be used to describe any constraint that applies to the whole puzzle using a lambda expression.</summary>
    sealed class ThreeCellLambdaConstraint : Constraint
    {
        /// <summary>A function that determines whether a set of values is valid in the relevant cells.</summary>
        public Func<int, int, int, bool> IsValid { get; private set; }

        /// <summary>Constructor.</summary>
        public ThreeCellLambdaConstraint(int affectedCell1, int affectedCell2, int affectedCell3, Func<int, int, int, bool> isValid)
            : base(new[] { affectedCell1, affectedCell2, affectedCell3 })
        {
            IsValid = isValid;
        }

        /// <summary>Override; see base.</summary>
        public override IEnumerable<Constraint> MarkTakens(bool[][] takens, int?[] grid, int? ix, int minValue, int maxValue)
        {
            if (ix == null)
                return null;

            // “slot” is the value we just placed (index into AffectedCells, 0–2)
            var slot = Array.IndexOf(AffectedCells, ix.Value);
            if (slot == -1)
                return null;

            // “unknown” is the value that is yet to be placed (index into AffectedCells, 0–2)
            var unknown = AffectedCells.IndexOf(af => grid[af] == null);
            if (unknown == -1)
                return null;

            // Make sure that there is a second already-placed value
            if (grid[AffectedCells[3 - slot - unknown]] == null)
                return null;

            for (var v = 0; v < takens[AffectedCells[unknown]].Length; v++)
                if (!IsValid(
                    (grid[AffectedCells[0]] ?? v) + minValue,
                    (grid[AffectedCells[1]] ?? v) + minValue,
                    (grid[AffectedCells[2]] ?? v) + minValue
                ))
                    takens[AffectedCells[unknown]][v] = true;
            return null;
        }
    }
}
