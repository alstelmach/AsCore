﻿namespace ASCore.Domain.Abstractions.BuildingBlocks
{
    public interface IRepository<in TAggregateRoot> where TAggregateRoot : AggregateRoot
    {
    }
}