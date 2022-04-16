// <copyright file="ExpressionTreeFactory.cs" company="Alex Puga">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;

    /// <summary>
    /// ExpressionTreeFactory to create each type of OperatorNode.
    /// </summary>
    public static class ExpressionTreeFactory
    {
        /// <summary>
        /// Creates a specific OperatorNode depending on the operator.
        /// </summary>
        /// <param name="op">Char.</param>
        /// <returns>OperatorNode.</returns>
        public static OperatorNode CreateOperatorNode(char op)
        {
            if (op != null)
            {
                // but which one?
                switch (op)
                {
                    case '+':
                        return new AddNode { };
                    case '-':
                        return new SubNode { };
                    case '*':
                        return new MultiplyNode { };
                    case '/':
                        return new DivideNode { };
                    case '^':
                        return new PowerNode { };
                    default: // if it is not any of the operators that we support, throw an exception:
                        throw new NotSupportedException(
                            "Operator " + op + " not supported.");
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a variable or constant node depending on the string value.
        /// </summary>
        /// <param name="value">String.</param>
        /// <returns>Node.</returns>
        public static Node CreateVariableOrConstantNode(string value)
        {
            double number = 0;

            // tries to see if the string is a number
            bool isNumber = double.TryParse(value, out number);
            if (isNumber)
            {
                return new ConstantNode()
                {
                    Value = number,
                };
            }
            else
            {
                // we know its a variableNode
                return new VariableNode()
                {
                    Name = value,
                };
            }
        }
    }
}
