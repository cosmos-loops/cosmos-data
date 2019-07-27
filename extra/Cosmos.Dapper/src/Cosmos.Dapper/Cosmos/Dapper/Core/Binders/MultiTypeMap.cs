using System;
using System.Reflection;
using Dapper;

namespace Cosmos.Dapper.Core.Binders
{
    public abstract class MultiTypeMap : SqlMapper.ITypeMap
    {
        private readonly SqlMapper.ITypeMap[] _mappers;

        protected MultiTypeMap(params SqlMapper.ITypeMap[] mappers)
        {
            _mappers = mappers;
        }

        public ConstructorInfo FindConstructor(string[] names, Type[] types)
        {
            foreach (var map in _mappers)
            {
                try
                {
                    var ret = map.FindConstructor(names, types);
                    if (ret != null)
                        return ret;
                }
                catch (NotImplementedException)
                {
                    //Ignore
                }
            }

            return null;
        }

        public ConstructorInfo FindExplicitConstructor()
        {
            foreach (var map in _mappers)
            {
                try
                {
                    var ret = map.FindExplicitConstructor();
                    if (ret != null)
                        return ret;
                }
                catch (NotImplementedException)
                {
                    //Ignore
                }
            }

            return null;
        }

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            foreach (var map in _mappers)
            {
                try
                {
                    var ret = map.GetConstructorParameter(constructor, columnName);
                    if (ret != null)
                        return ret;
                }
                catch (NotImplementedException)
                {
                    //Ignore
                }
            }

            return null;
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            foreach (var map in _mappers)
            {
                try
                {
                    var ret = map.GetMember(columnName);
                    if (ret != null)
                        return ret;
                }
                catch (NotImplementedException)
                {
                    //Ignore
                }
            }

            return null;
        }
    }
}