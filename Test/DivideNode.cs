

namespace SpreadsheetEngine
{
    using System.Collections.Generic;

    /// <summary>
    /// Inherited from OperatorNode: Used for Dividing
    /// </summary>
    public class DivideNode : OperatorNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DivideNode"/> class.
        /// </summary>
        public DivideNode()
        {
            this.Operator = '/';
            this.Precedence = 3;
            this.Associativity = 0;
        }

        /// <inheritdoc/>
        public override double DoOperator(Dictionary<string, double> variables)
        {
            return this.Evaluate(this.Left, variables) / this.Evaluate(this.Right, variables);
        }
    }
}
