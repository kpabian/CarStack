﻿using CarStack.Models;
using System.Linq.Expressions;

namespace CarStack.Services.IServices;

public interface ICarService
{
    Task<IEnumerable<Car>> GetAll();
    Task<Car?> GetById(int id);
    Task<IEnumerable<Car>> GetByFilter(Expression<Func<Car, bool>> expression);
    Task Create(Car car);
    Task Update(Car car);
    Task Delete(int id);
}
