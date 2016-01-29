using System.Linq;

namespace AdoManager
{
    public class Condition
    {
        

        public Operators OperateByBeforeConditions { get; set; } = Operators.AND;
        public string Key { get; set; }
        public Operators Operate { get; set; }
        public object[] Values { get; set; }
        public bool Not { get; set; }

        public Condition(bool not, string key, Operators opt,  params object[] values)
        {
            Key = key;
            Operate = opt;
            Values = values;
            Not = not;
        }

        public static Condition Factory(bool not, string key, Operators opt, params object[] values)
        {
            return new Condition(not, key, opt, values);
        }

        public static Condition Factory(string key, Operators opt, params object[] values)
        {
            return new Condition(false, key, opt, values);
        }


        public override string ToString()
        {
            var hasQuotation = (Values[0] is string);

            string result = Not ? "NOT " : "";

            switch (Operate)
            {
                case Operators.BETWEEN:
                    result += $@"{Key} {Operate} {Values[0]} AND {Values[1]}";
                    break;
                case Operators.IN:
                    var strValues = Values.Select(x => $"{x.ToSqlString()}");
                    result += $@"{Key} {Operate} ({string.Join(",", strValues)})";
                    break;
                case Operators.LIKE:
                    result += $@"{Key} {Operate} '%{Values[0]}%'";
                    break;
                case Operators.Equal:
                    result += $@"{Key} = {Values[0].ToSqlString()}";
                    break;
                case Operators.NotEqual:
                    result += $@"{Key} <> {Values[0].ToSqlString()}";
                    break;
                case Operators.GreaterThan:
                    result += $@"{Key} > {Values[0].ToSqlString()}";
                    break;
                case Operators.LessThan:
                    result += $@"{Key} < {Values[0].ToSqlString()}";
                    break;
                case Operators.GreaterThanOrEqual:
                    result += $@"{Key} >= {Values[0].ToSqlString()}";
                    break;
                case Operators.LessThanOrEqual:
                    result += $@"{Key} <= {Values[0].ToSqlString()}";
                    break;
                default:
                    result += $@"{Key} {Operate} {Values[0].ToSqlString()}";
                    break;
            }

            return result;
        }


        
    }
}
