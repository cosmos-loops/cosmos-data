﻿using System;
using System.Data;
using System.Threading.Tasks;
using Cosmos;

namespace MySql.Data.MySqlClient
{
    public static partial class MySqlClientExtensions
    {
        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
            return (T) command.ExecuteScalar();
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this MySqlConnection conn, Action<MySqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
            using var command = conn.CreateCommand(commandFactory);
            return (T) command.ExecuteScalar();
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this MySqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAs<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this MySqlConnection conn, string cmdText, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAs<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this MySqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAs<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this MySqlConnection conn, string cmdText, CommandType commandType, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAs<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAs<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAs<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ExecuteScalarAs<T>(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAs<T>(cmdText, parameters, commandType, null);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsAsync<T>(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType,
            MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#else
            await using var command = conn.CreateCommand(cmdText, commandType, transaction, parameters);
#endif
            return (T) await command.ExecuteScalarAsync();
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="commandFactory"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async Task<T> ExecuteScalarAsAsync<T>(this MySqlConnection conn, Action<MySqlCommand> commandFactory)
        {
            conn.CheckNull(nameof(conn));
#if NETFRAMEWORK || NETSTANDARD2_0
            using var command = conn.CreateCommand(commandFactory);
#else
            await using var command = conn.CreateCommand(commandFactory);
#endif
            return (T) await command.ExecuteScalarAsync();
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarAsAsync<T>(this MySqlConnection conn, string cmdText)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsAsync<T>(cmdText, null, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarAsAsync<T>(this MySqlConnection conn, string cmdText, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsAsync<T>(cmdText, null, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarAsAsync<T>(this MySqlConnection conn, string cmdText, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsAsync<T>(cmdText, null, commandType, null);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarAsAsync<T>(this MySqlConnection conn, string cmdText, CommandType commandType, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsAsync<T>(cmdText, null, commandType, transaction);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarAsAsync<T>(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsAsync<T>(cmdText, parameters, CommandType.Text, null);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="transaction"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarAsAsync<T>(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, MySqlTransaction transaction)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsAsync<T>(cmdText, parameters, CommandType.Text, transaction);
        }

        /// <summary>
        /// Execute Scalar as...
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Task<T> ExecuteScalarAsAsync<T>(this MySqlConnection conn, string cmdText, MySqlParameter[] parameters, CommandType commandType)
        {
            conn.CheckNull(nameof(conn));
            return conn.ExecuteScalarAsAsync<T>(cmdText, parameters, commandType, null);
        }
    }
}