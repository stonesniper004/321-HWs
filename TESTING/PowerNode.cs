// <copyright file="PowerNode.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PowerNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PowerNode"/> class.
        /// </summary>
        public PowerNode()
        {
            this.Operator = '^';
            this.Precedence = 5;
            this.Associativity = 1;
        }

        /// <inheritdoc/>
        public override double DoOperator(Dictionary<string, double> variables)
        {
            return Math.Pow(this.Evaluate(this.Left, variables), this.Evaluate(this.Right, variables));
        }
    }
}
