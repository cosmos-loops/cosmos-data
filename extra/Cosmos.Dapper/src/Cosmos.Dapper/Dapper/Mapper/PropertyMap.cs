/*
 * Copyright 2011 Thad Smith, Page Brooks and contributors
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Reflection;

/*
 * Reference to:
 *      tmsmith/Dapper-Extensions
 *      Author: Thad Smith
 *      Url: https://github.com/tmsmith/Dapper-Extensions
 *      Apache License 2.0
 *          http://www.apache.org/licenses/LICENSE-2.0
 *
 * Changed and updated by Alex Lewis
 */

namespace Dapper.Mapper
{
    public class PropertyMap : IPropertyMap
    {
        public PropertyMap(PropertyInfo property)
        {
            PropertyInfo = property;
            ColumnName = property.Name;

            var (name, caseSensitive) = PropertyMapperHelper.GetColumnName(property);
            if (!string.IsNullOrWhiteSpace(name))
                ToColumn(name, caseSensitive);

            if (PropertyMapperHelper.IsReadOnly(property))
                ReadOnly();

            if (PropertyMapperHelper.IsIgnore(property))
                Ignore();

            var keyType = PropertyMapperHelper.GetPracticeKey(property);
            if (keyType != KeyType.NotAKey)
                PrimaryKey(keyType);

            if (PropertyMapperHelper.IsIgnoreConvention(property))
                IgnoreConvention();
        }

        public string Name => PropertyInfo.Name;

        public string ColumnName { get; private set; }

        public bool Ignored { get; private set; }

        public bool IsIgnoreConvention { get; private set; }

        public bool IsReadOnly { get; private set; }

        public bool IsCaseSensitive { get; private set; }

        public KeyType KeyType { get; private set; }

        public PropertyInfo PropertyInfo { get; }

        public PropertyMap ToColumn(string columnName, bool caseSensitive = true)
        {
            ColumnName = columnName;
            IsCaseSensitive = caseSensitive;
            return this;
        }

        public PropertyMap PrimaryKey(KeyType type)
        {
            if (Ignored)
                throw new ArgumentException($"'{Name}' is ignored and cannot be made a key field.");

            if (IsReadOnly)
                throw new ArgumentException($"'{Name}' is readonly and cannot be made a key field.");

            KeyType = type;
            return this;
        }

        public PropertyMap Ignore()
        {
            if (KeyType != KeyType.NotAKey)
                throw new ArgumentException($"'{Name}' is a key field and cannot be ignored.");

            Ignored = true;
            return this;
        }

        public PropertyMap IgnoreConvention()
        {
            IsIgnoreConvention = true;
            return this;
        }

        public PropertyMap ReadOnly()
        {
            if (KeyType != KeyType.NotAKey)
                throw new ArgumentException($"'{Name}' is a key field and cannot be ignored.");

            IsReadOnly = true;
            return this;
        }

        public PropertyMap CaseSensitive()
        {
            IsCaseSensitive = true;
            return this;
        }

        public PropertyMap CaseInsensitive()
        {
            IsCaseSensitive = false;
            return this;
        }
    }
}