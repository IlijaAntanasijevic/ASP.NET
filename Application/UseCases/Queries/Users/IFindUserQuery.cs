﻿using Application.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Queries.Users
{
    public interface IFindUserQuery : IQuery<UserDto,  int>
    {
    }
}
