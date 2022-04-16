

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class to create an ExpressionTree via user input.
    /// </summary>
    public class ExpressionTree
    {
        private Node root;
        private string expression;
        public Dictionary<string, double> variables = new Dictionary<string, double>();
        public static Stack<Node> expressionStack = new Stack<Node>();
        public static Stack<Node> operatorStack = new Stack<Node>();
        public static Stack<string> varStack = new Stack<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">String.</param>
        public ExpressionTree(string expression)
        {
            this.expression = expression;
            if (expression != string.Empty)
            {
                this.Complie(expression);
                this.root = this.CompilePostExpression();
            }
        }

        /// <summary>
        /// Gets or sets Expression.
        /// </summary>
        public string Expression
        {
            get
            {
                return this.expression;
            }

            set
            {
                this.expression = value;
                this.variables.Clear();
                varStack.Clear();
                expressionStack.Clear();
            }
        }

        /// <summary>
        /// Sets a variable to a value.
        /// </summary>
        /// <param name="variableName">String.</param>
        /// <param name="variableValue">Double.</param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variables[variableName] = variableValue;
        }

        /// <summary>
        /// Evalueates a Tree.
        /// </summary>
        /// <returns>Double.</returns>
        public double Evaluate()
        {
            return this.Evaluate(this.root);
        }

        /// <summary>
        /// Complies and returns the PostFixExpression to a Tree.
        /// </summary>
        /// <returns>Node.</returns>
        private Node CompilePostExpression()
        {
            this.ConvertPostExpression();
            Stack<Node> result = new Stack<Node>();
            Node temp;

            while (expressionStack.Count > 0)
            {
                temp = expressionStack.Pop();

                VariableNode variable = temp as VariableNode;
                if (variable != null)
                {
                    result.Push(variable);
                }

                ConstantNode con = temp as ConstantNode;
                if (con != null)
                {
                    result.Push(con);
                }

                OperatorNode op = temp as OperatorNode;
                if (op != null)
                {
                    op.Right = result.Pop();
                    op.Left = result.Pop();

                    result.Push(op);
                }
            }

            return result.Pop();
        }

        private double Evaluate(Node node)
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
                return this.variables[variableNode.Name];
            }

            // it is an operator node if we came here
            OperatorNode operatorNode = node as OperatorNode;
            if (operatorNode != null)
            {
                return operatorNode.DoOperator(this.variables);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Compiles the given string to a PostFixExpression.
        /// </summary>
        /// <param name="expression">String.</param>
        private void Complie(string expression)
        {
            bool entered = true;
            string[] words = expression.Split();
            foreach (string word in words)
            {
                if (this.CheckOperators(word) == true && this.CheckParneth(word) == true)
                {
                    expressionStack.Push(ExpressionTreeFactory.CreateVariableOrConstantNode(word));
                    entered = false;
                }

                if (word == "(")
                {
                    varStack.Push(word);
                }

                if (word == ")")
                {
                    this.PopAndPush();
                }

                if ((this.CheckOperators(word) == false && varStack.Count == 0) || (this.CheckOperators(word) == false && varStack.Peek() == "("))
                {
                    varStack.Push(word);
                    entered = false;
                }

                if (this.CheckOperators(word) == false && entered == true)
                {
                    OperatorNode temp = ExpressionTreeFactory.CreateOperatorNode(char.Parse(word));
                    OperatorNode tstack = ExpressionTreeFactory.CreateOperatorNode(char.Parse(varStack.Peek()));
                    if ((temp.Precedence > tstack.Precedence) || (temp.Precedence == tstack.Precedence && temp.Associativity == 1))
                    {
                        varStack.Push(word);
                    }

                    if ((temp.Precedence < tstack.Precedence) || (temp.Precedence == tstack.Precedence && temp.Associativity == 0))
                    {
                        this.PopForLeft(temp);
                        varStack.Push(word);
                    }
                }

                entered = true;
            }

            while (varStack.Count > 0)
            {
                expressionStack.Push(ExpressionTreeFactory.CreateOperatorNode(char.Parse(varStack.Pop())));
            }
        }

        /// <summary>
        /// Continues to pop from varStck until the precedence is greater than or equal to the one being added.
        /// Every node popped will be added to the result of PostFixExpression expressionStack.
        /// </summary>
        /// <param name="temp">OperatorNode.</param>
        private void PopForLeft(OperatorNode temp)
        {
            while (varStack.Count > 0)
            {
                if (ExpressionTreeFactory.CreateOperatorNode(char.Parse(varStack.Peek())).Precedence >= temp.Precedence)
                {
                    expressionStack.Push(ExpressionTreeFactory.CreateOperatorNode(char.Parse(varStack.Pop())));
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Checks if a string is an operator.
        /// </summary>
        /// <param name="word">String.</param>
        /// <returns>bool.</returns>
        private bool CheckOperators(string word)
        {
            switch (word)
            {
                case "^":
                    return false;
                case "*":
                    return false;
                case "/":
                    return false;
                case "+":
                    return false;
                case "-":
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if string is a paranthesis.
        /// </summary>
        /// <param name="word">String.</param>
        /// <returns>bool.</returns>
        private bool CheckParneth(string word)
        {
            switch (word)
            {
                case "(":
                    return false;
                case ")":
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Pops and pushes values until a '(' is found.
        /// Used when a ')' is found.
        /// </summary>
        private void PopAndPush()
        {
            while (varStack.Count > 0)
            {
                if (varStack.Peek() == "(")
                {
                    varStack.Pop();
                    break;
                }
                else
                {
                    expressionStack.Push(ExpressionTreeFactory.CreateOperatorNode(char.Parse(varStack.Pop())));
                }
            }
        }

        /// <summary>
        /// changes the post expression so that pop would be grabbing the first element instead of last.
        /// </summary>
        private void ConvertPostExpression()
        {
            Stack<Node> temp = new Stack<Node>();
            while (expressionStack.Count > 0)
            {
                OperatorNode checker = expressionStack.Peek() as OperatorNode;
                if (checker != null && (checker.Operator == '(' || checker.Operator == ')'))
                {
                    throw new Exception("Invalid expression entered.");
                }

                temp.Push(expressionStack.Pop());
            }

            expressionStack = temp;
        }
    }
}