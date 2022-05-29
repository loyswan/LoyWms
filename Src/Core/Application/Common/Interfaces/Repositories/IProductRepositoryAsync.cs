﻿using LoyWms.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyWms.Application.Common.Interfaces.Repositories;

public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
{
    Task<bool> IsUniqueProductNoAsync(string productNo);
}
