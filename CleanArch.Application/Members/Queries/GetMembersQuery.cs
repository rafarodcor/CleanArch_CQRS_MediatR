﻿using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Queries;

public class GetMembersQuery : IRequest<IEnumerable<Member>>
{
    #region Properties
    #endregion

    #region Handler    

    public class GetMembersQueryHandler : IRequestHandler<GetMembersQuery, IEnumerable<Member>>
    {
        private readonly IMemberDapperRepository _memberDapperRepository;

        public GetMembersQueryHandler(IMemberDapperRepository memberDapperRepository) => _memberDapperRepository = memberDapperRepository;

        public async Task<IEnumerable<Member>> Handle(GetMembersQuery request, CancellationToken cancellationToken)
            => await _memberDapperRepository.GetMembers();
    }

    #endregion
}