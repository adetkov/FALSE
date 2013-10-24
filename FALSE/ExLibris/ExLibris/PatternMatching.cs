using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExLibris
{
    public static class PatternMatching
    {
        public class MatchContext<TRes, T>
        {
            public class MatchStatement<TYpe> : MatchStatement
            {
                private Func<TYpe, TRes> _action;
                private readonly Func<TYpe, bool> _condition;
                private readonly MatchStatement<TYpe> _parent;
                protected MatchStatement<TYpe> _child;

                internal MatchStatement(MatchContext<TRes, T> context, Func<TYpe, bool> condition = null, MatchStatement<TYpe> parent = null)
                    : base(context)
                {
                    _condition = condition;
                    _parent = parent;
                }

                public MatchContext<TRes, T> then(Func<TYpe, TRes> action)
                {
                    _action = action;
                    return _context.AddStatement(this);
                }

                public MatchStatement<TYpe> when(Func<TYpe, bool> condition)
                {
                    return _child = new MatchStatement<TYpe>(_context, condition, this);
                }

                internal override TRes Eval(T value)
                {
                    return _action((TYpe)((object)value));
                }

                internal override bool IsMatch(T value)
                {
                    return
                        value is TYpe
                        && (_condition == null || _condition((TYpe)((object)value)))
                        && (_parent == null || _parent.Equals(value));
                }
            }

            public abstract class MatchStatement
            {
                protected readonly MatchContext<TRes, T> _context;

                internal MatchStatement(MatchContext<TRes, T> context)
                {
                    _context = context;
                }

                internal abstract TRes Eval(T value);

                internal abstract bool IsMatch(T value);
            }

            private readonly List<MatchStatement> _matchStatements = new List<MatchStatement>();

            internal MatchContext()
            {
            }

            public MatchStatement<M> of<M>()
            {
                return new MatchStatement<M>(this);
            }

            public MatchStatement<M> of<M>(Func<M, bool> condition)
            {
                return new MatchStatement<M>(this, condition);
            }

            public MatchStatement<T> when<T>(Func<T, bool> condition)
            {
                return new MatchStatement<T>(this, condition);
            }

            public MatchStatement<T> anyway()
            {
                return new MatchStatement<T>(this, x => true);
            }

            private MatchContext<TRes, T> AddStatement(MatchStatement statement)
            {
                _matchStatements.Add(statement);
                return this;
            }

            internal TRes GetResult(T value)
            {
                foreach (var statement in _matchStatements)
                {
                    if (statement.IsMatch(value))
                        return statement.Eval(value);
                }

                throw new Exception("");
            }
        }

        public static TRes match<TRes, T>(this T value, Func<MatchContext<TRes, T>, MatchContext<TRes, T>> matchExpression)
        {
            return matchExpression(new MatchContext<TRes, T>()).GetResult(value);
        }
    }
}