// <copyright file="OperatorNode.cs" company="Alex Puga">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Inherited from OperatorNode: Used for Multiplying.
    /// </summary>
    public abstract class OperatorNode : Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperatorNode"/> class.
        /// </summary>
        public OperatorNode()
        {
            this.Left = this.Right = null;
        }

        /// <summary>
        /// Gets or sets Operator.
        /// </summary>
        public char Operator { get; set; }

        public int Precedence { get; set; }

        public int Associativity { get; set; }

        /// <summary>
        /// Gets or sets Left Node.
        /// </summary>
        public Node Left { get; set; }

        /// <summary>
        /// Gets or sets Right node.
        /// </summary>
        public Node Right { get; set; }

        /// <summary>
        /// Evaluates the Tree and returns a double value.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="variables">Dictionary.</param>
        /// <returns>Double.</returns>
        public double Evaluate(Node node, Dictionary<string, double> variables)
        {
            // try to evaluate the node as a constant
            // the "as" operator is evaluated to null 
            // as opposed to throwing an exception
            ConstantNode constantNode = node as ConstantNode;
            if (constantNode != null)
            {
                return constantNode.Value;
            }

            // as a variable
            VariableNode variableNode = node as VariableNode;
            if (variableNode != null)
            {
                return variables[variableNode.Name];
            }

            // it is an operator node if we came here
            OperatorNode operatorNode = node as OperatorNode;
            if (operatorNode != null)
            {
                return operatorNode.DoOperator(variables);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Performs the given operator depending on what kind of OperatorNode the caller is.
        /// </summary>
        /// <param name="variables">Dictionary.</param>
        /// <returns>Double.</returns>
        public abstract double DoOperator(Dictionary<string, double> variables);
    }
}
