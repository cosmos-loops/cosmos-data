using System;

namespace Cosmos.Dapper.Conventions
{
    public class PropertyConventionStrategy
    {
        #region ColumnName

        internal string ColumnNameValue { get; private set; }

        public PropertyConventionStrategy HasColumnName(string columnName)
        {
            ColumnNameValue = columnName;
            return this;
        }

        #endregion

        #region Prefix

        internal string PrefixValue { get; private set; }

        public PropertyConventionStrategy HasPrefix(string prefix)
        {
            PrefixValue = prefix;
            return this;
        }

        #endregion

        #region CaseSensitive

        internal bool CaseSensitiveValue { get; private set; } = true;

        public PropertyConventionStrategy CaseSensitive()
        {
            CaseSensitiveValue = true;
            return this;
        }

        public PropertyConventionStrategy CaseInsensitive()
        {
            CaseSensitiveValue = false;
            return this;
        }

        #endregion

        #region Transform

        internal Func<string, string> PropertyTransformer { get; private set; }

        public PropertyConventionStrategy Transform(Func<string, string> transformer)
        {
            PropertyTransformer = transformer;
            return this;
        }

        #endregion
    }
}