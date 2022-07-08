// -----------------------------------------------------------------------
// <copyright file="ICache.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;

namespace Opeeka.PICS.Domain.Interfaces
{
    public interface IQueue
    {
        public Task<bool> Push(string queueName, string value);
    }
}