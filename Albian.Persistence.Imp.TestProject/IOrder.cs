﻿namespace Albian.Persistence.Imp.TestProject
{
    public interface IOrder : IAlbianObject
    {
        //string Id { get; set; }
        string Name { get; set; }
        string Buyer { get; set; }
        decimal Money { get; set; }
        string Seller { get; set; }
    }
}