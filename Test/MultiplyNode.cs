

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Inherited from OperatorNode: Used for Multiplying.
    /// </summary>
    public class MultiplyNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiplyNode"/> class.
        /// </summary>
        public MultiplyNode()
        {
            this.Operator = '*';
            this.Precedence = 4;
            this.Associativity = 1;
        }

        /// <inheritdoc/>
        public override double DoOperator(Dictionary<string, double> variables)
        {
            return this.Evaluate(this.Left, variables) * this.Evaluate(this.Right, variables);
        }
    }
}
