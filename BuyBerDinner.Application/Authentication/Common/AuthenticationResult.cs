﻿using BuyBerDinner.Domain.Entities;

namespace BuyBerDinner.Application.Authentication.Common;

public record AuthenticationResult(User User, string Token);