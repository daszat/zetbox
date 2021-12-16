using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Search;
using Zetbox.API.Utils;
using Lucene.Net.Search.Spans;
using Lucene.Net.Index;

namespace Zetbox.API.Server.Fulltext
{
    public abstract class QueryTranslator
    {
        public virtual Query VisitQuery(Query qry)
        {
            if (qry == null) return null;

            if (qry is TermQuery)
            {
                return VisitTermQuery((TermQuery)qry);
            }
            //else if (qry is MultiTermQuery)
            //{
            //    -> abstract
            //}
            else if (qry is BooleanQuery)
            {
                return VisitBooleanQuery((BooleanQuery)qry);
            }
            else if (qry is WildcardQuery)
            {
                return VisitWildcardQuery((WildcardQuery)qry);
            }
            else if (qry is PhraseQuery)
            {
                return VisitPhraseQuery((PhraseQuery)qry);
            }
            else if (qry is PrefixQuery)
            {
                return VisitPrefixQuery((PrefixQuery)qry);
            }
            else if (qry is MultiPhraseQuery)
            {
                return VisitMultiPhraseQuery((MultiPhraseQuery)qry);
            }
            else if (qry is FuzzyQuery)
            {
                return VisitFuzzyQuery((FuzzyQuery)qry);
            }
            else if (qry is TermRangeQuery)
            {
                return VisitTermRangeQuery((TermRangeQuery)qry);
            }
            else if (qry.GetType().IsGenericType && qry.GetType().GetGenericTypeDefinition() == typeof(NumericRangeQuery<>))
            {
                return VisitNumericRangeQuery((MultiTermQuery)qry);
            }
            //else if (qry is SpanQuery)
            //{
            //    -> abstract
            //}

            Logging.Log.Warn(string.Format("Unknown query type: '{0}'", qry.GetType().FullName));
            return qry;
        }

        protected virtual Term VisitTerm(Term t)
        {
            return new Term(VisitField(t.Field), t.Bytes);
        }

        protected virtual string VisitField(string f)
        {
            return f;
        }

        protected virtual TermQuery VisitTermQuery(TermQuery t)
        {
            return new TermQuery(VisitTerm(t.Term));
        }

        protected virtual TermRangeQuery VisitTermRangeQuery(TermRangeQuery r)
        {
            return new TermRangeQuery(VisitField(r.Field), r.LowerTerm, r.UpperTerm, r.IncludesLower, r.IncludesUpper);
        }

        protected virtual FuzzyQuery VisitFuzzyQuery(FuzzyQuery f)
        {
            return new FuzzyQuery(VisitTerm(f.Term), f.MaxEdits, f.PrefixLength);
        }

        protected virtual MultiPhraseQuery VisitMultiPhraseQuery(MultiPhraseQuery m)
        {
            var qry = new MultiPhraseQuery();
            foreach (var tarray in m.GetTermArrays())
            {
                qry.Add(tarray.Select(t => VisitTerm(t)).ToArray());
            }
            return qry;
        }

        protected virtual PrefixQuery VisitPrefixQuery(PrefixQuery p)
        {
            return new PrefixQuery(VisitTerm(p.Prefix));
        }

        protected virtual PhraseQuery VisitPhraseQuery(PhraseQuery p)
        {
            var qry = new PhraseQuery();
            foreach (var t in p.GetTerms())
            {
                qry.Add(VisitTerm(t));
            }
            return qry;
        }

        protected virtual WildcardQuery VisitWildcardQuery(WildcardQuery w)
        {
            return new WildcardQuery(VisitTerm(w.Term));
        }

        protected virtual BooleanQuery VisitBooleanQuery(BooleanQuery b)
        {
            var qry = new BooleanQuery(b.CoordDisabled);
            foreach (var c in b.GetClauses())
            {
                qry.Add(new BooleanClause(VisitQuery(c.Query), c.Occur));
            }
            return qry;
        }

        private MultiTermQuery VisitNumericRangeQuery(MultiTermQuery n)
        {
            if (n is NumericRangeQuery<double>)
            {
                var q = (NumericRangeQuery<double>)n;
                return NumericRangeQuery.NewDoubleRange(VisitField(q.Field), q.Min, q.Max, q.IncludesMin, q.IncludesMax);
            }
            else if (n is NumericRangeQuery<float>)
            {
                var q = (NumericRangeQuery<float>)n;
                return NumericRangeQuery.NewSingleRange(VisitField(q.Field), q.Min, q.Max, q.IncludesMin, q.IncludesMax);
            }
            else if (n is NumericRangeQuery<int>)
            {
                var q = (NumericRangeQuery<int>)n;
                return NumericRangeQuery.NewInt32Range(VisitField(q.Field), q.Min, q.Max, q.IncludesMin, q.IncludesMax);
            }
            else if (n is NumericRangeQuery<long>)
            {
                var q = (NumericRangeQuery<long>)n;
                return NumericRangeQuery.NewInt64Range(VisitField(q.Field), q.Min, q.Max, q.IncludesMin, q.IncludesMax);
            }
            else
            {
                throw new NotSupportedException(string.Format("Numeric range of type '{0}' is not supported", n.GetType().FullName));
            }
        }
    }
}
