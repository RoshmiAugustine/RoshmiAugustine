// -----------------------------------------------------------------------
// <copyright file="ICache.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces
{
    public interface ICache
    {
        List<OpenIdConnectConfiguration> GetOpenIDConfig();
        /// <summary>
        /// Gets the Redis database number currently used.
        /// </summary>
        int DatabaseNumber { get; }

        /// <summary>
        /// Gets a value indicating whether True if Redis server connected successfully else Get false.
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Gets or sets a value indicating whether get or Set True/False to Enable/Disable encryption.
        /// By default encryption is Enabled.
        /// </summary>
        bool Encryption { get; set; }

        /// <summary>
        /// Read cached value from Redis server.
        /// </summary>
        /// <typeparam name="T">Type of cached value</typeparam>
        /// <param name="key">Key of cached value.</param>
        /// <returns>Cached value of type T.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Read cached string value from Redis server.
        /// </summary>
        /// <param name="key">Key of cached value.</param>
        /// <returns>Cached value as string.</returns>
        string Get(string key);

        /// <summary>
        /// Read cached values from Redis server.
        /// </summary>
        /// <typeparam name="T">Type of cached values.</typeparam>
        /// <param name="keys">string Collection of keys.</param>
        /// <returns>Dictionary of key-(cached)value pair.</returns>
        IDictionary<string, T> GetAll<T>(IEnumerable<string> keys);

        /// <summary>
        /// Insert a value/object to Redis server.
        /// </summary>
        /// <typeparam name="T">Type of value/object to be cached.</typeparam>
        /// <param name="key">Key of value/object to be cached.</param>
        /// <param name="objectToCache">value/object to be cached.</param>
        /// <returns>True or False.</returns>
        bool Post<T>(string key, T objectToCache);

        /// <summary>
        /// Insert a value/object to Redis server.
        /// </summary>
        /// <typeparam name="T">Type of value/object to be cached.</typeparam>
        /// <param name="key">Key of value/object to be cached.</param>
        /// <param name="objectToCache">Value/object to be cached.</param>
        /// <param name="expiresAt">Timespan for expiry of the object to be cached.</param>
        /// <returns>True or False.</returns>
        bool Post<T>(string key, T objectToCache, TimeSpan expiresAt);

        /// <summary>
        /// Insert a string value to Redis server.
        /// </summary>
        /// <param name="key">Key of value/object to be cached.</param>
        /// <param name="objectToCache">String value to be cached.</param>
        /// <returns>True or False.</returns>
        bool Post(string key, string objectToCache);

        /// <summary>
        /// Insert a string value to Redis server.
        /// </summary>
        /// <param name="key">Key of string value to be cached.</param>
        /// <param name="objectToCache">String value to be cached.</param>
        /// <param name="expiresAt">Timespan for expiry of the value to be cached.</param>
        /// <returns>True or False.</returns>
        bool Post(string key, string objectToCache, TimeSpan expiresAt);

        /// <summary>
        /// Insert a dictionary collection of key-values/objects to Redis server.
        /// </summary>
        /// <typeparam name="T">Type of values/objects to be cached.</typeparam>
        /// <param name="values">Dictionary collection of key-value/object to be cached</param>
        /// <returns>True or False.</returns>
        bool PostAll<T>(IDictionary<string, T> values);

        /// <summary>
        /// Insert a dictionary collection of key-value/object to Redis server.
        /// </summary>
        /// <typeparam name="T">Type of value/object to be cached.</typeparam>
        /// <param name="values">Dictionary collection of key-value/object</param>
        /// <param name="expiresAt">Timespan for expiry of the value/object.</param>
        /// <returns>True or False.</returns>
        bool PostAll<T>(IDictionary<string, T> values, TimeSpan expiresAt);

        /// <summary>
        /// Remove a value/object from Redis server.
        /// </summary>
        /// <param name="key">Key of cached value/object.</param>
        /// <returns>True or False.</returns>
        bool Delete(string key);

        /// <summary>
        /// Remove All Specified key-value/object from Redis Server.
        /// </summary>
        /// <param name="keys">Keys of value/object to be removed.</param>
        /// <returns>True or False.</returns>
        bool DeleteAll(IEnumerable<string> keys);

        /// <summary>
        /// FlushAll/Clear all key-values/objects from Redis Server.
        /// Available only if allowAdmin=True;
        /// </summary>
        /// <returns>True or False.</returns>
        bool FlushAll();

        /// <summary>
        /// Check a Key exists or not.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True or False</returns>
        bool IsKeyExists(string key);

        /// <summary>
        /// Check a Key exists or not.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>True or False</returns>
        Task<bool> IsKeyExistsAsync(string key);

        /// <summary>
        /// Read cached string value from Redis server.
        /// </summary>
        /// <param name="key">Key of cached value.</param>
        /// <returns>Cached value as string.</returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// Read cached value from Redis server.
        /// </summary>
        /// <typeparam name="T">Type of cached value</typeparam>
        /// <param name="key">Key of cached value.</param>
        /// <returns>Cached value of type T.</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// Insert a value/object to Redis server.
        /// </summary>
        /// <typeparam name="T">Type of value/object to be cached.</typeparam>
        /// <param name="key">Key of value/object to be cached.</param>
        /// <param name="objectToCache">value/object to be cached.</param>
        /// <returns>True or False.</returns>
        Task<bool> PostAsync<T>(string key, T objectToCache);

        /// <summary>
        /// Insert a string value to Redis server.
        /// </summary>
        /// <param name="key">Key of string value to be cached.</param>
        /// <param name="objectToCache">String value to be cached.</param>
        /// <param name="expiresAt">Timespan for expiry of the value to be cached.</param>
        /// <returns>True or False.</returns>
        Task<bool> PostAsync<T>(string key, T objectToCache, TimeSpan expiresAt);

        /// <summary>
        /// Insert a string value to Redis server.
        /// </summary>
        /// <param name="key">Key of value/object to be cached.</param>
        /// <param name="objectToCache">String value to be cached.</param>
        /// <returns>True or False.</returns>
        Task<bool> PostAsync(string key, string objectToCache);

        /// <summary>
        /// Insert a string value to Redis server.
        /// </summary>
        /// <param name="key">Key of string value to be cached.</param>
        /// <param name="objectToCache">String value to be cached.</param>
        /// <param name="expiresAt">Timespan for expiry of the value to be cached.</param>
        /// <returns>True or False.</returns>
        Task<bool> PostAsync(string key, string objectToCache, TimeSpan expiresAt);

        /// <summary>
        /// Remove a value/object from Redis server.
        /// </summary>
        /// <param name="key">Key of cached value/object.</param>
        /// <returns>True or False.</returns>
        Task<bool> DeleteAsync(string key);

        /// <summary>
        /// FlushAll/Clear all key-values/objects from Redis Server.
        /// Available only if allowAdmin=True;
        /// </summary>
        /// <returns>True or False.</returns>
        Task<bool> FlushAllAsync();
    }
}
