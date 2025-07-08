using System;
using System.Collections.Generic;

namespace Game.Utility.Conditions
{
    public class CompositeCondition : ICompositeCondition
    {
        private readonly List<ICondition> _conditions = new();
        private readonly Func<bool, bool, bool> _standardLogicOperation;

        public CompositeCondition(Func<bool, bool, bool> standardLogicOperation)
        {
            _standardLogicOperation = standardLogicOperation;
        }

        public CompositeCondition() : this(LogicOperations.And)
        {
            
        }

        public bool Evaluate()
        {
            if (_conditions.Count == 0)
                return false;
            
            bool result = _conditions[0].Evaluate();

            for (int i = 1; i < _conditions.Count; i++)
            {
                var condition = _conditions[i];
                
                result = _standardLogicOperation.Invoke(result, condition.Evaluate());
            }

            return result;
        }

        public ICompositeCondition Add(ICondition condition)
        {
            _conditions.Add(condition);
            return this;
        }

        public ICompositeCondition Remove(ICondition condition)
        {
            _conditions.Remove(condition);
            return this;
        }
    }
}