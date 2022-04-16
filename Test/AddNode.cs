
namespace SpreadsheetEngine
{
    using System.Collections.Generic;

    /// <summary>
    /// Inherited from OperatorNode: Used for Adding.
    /// </summary>
    public class AddNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddNode"/> class.
        /// </summary>
        public AddNode()
        {
            this.Operator = '+';
            this.Precedence = 2;
            this.Associativity = 0;
        }

        /// <inheritdoc/>
        public override double DoOperator(Dictionary<string, double> variables)
        {
            return this.Evaluate(this.Left, variables) + this.Evaluate(this.Right, variables);
        }
    }
}
