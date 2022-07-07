﻿using BuyBerDinner.Application.Common.Interfaces.Services;

namespace BuyBerDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.Now;
}