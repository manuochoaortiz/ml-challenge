﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Infrastructure.ExtServices
{
    public interface ICountriesService : IExtServices<Country>
    {
    }
}