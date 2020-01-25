// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Codidact.Auth.Models
{
    public class LoginViewModel : LoginInputModel
    {
        public bool AllowRememberLogin { get; set; } = true;
    }
}