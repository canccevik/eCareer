﻿using System;
using Career.MediatR.Command;
using MediatR;

namespace Company.Application.Commands.CompanyFollower.UnfollowCompany
{
    public class UnfollowCompanyCommand: ICommand
    {
        public UnfollowCompanyCommand(Guid userId, Guid companyId)
        {
            UserId = userId;
            CompanyId = companyId;
        }

        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
    }
}