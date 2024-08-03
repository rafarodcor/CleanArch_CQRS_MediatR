using CleanArch.Domain.Abstractions;
using CleanArch.Domain.Entities;
using MediatR;

namespace CleanArch.Application.Members.Queries;

public class GetMemberByIdQuery : IRequest<Member>
{
    #region Properties

    public int Id { get; set; }

    #endregion

    #region Handler   

    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, Member>
    {
        private readonly IMemberDapperRepository _memberDapperRepository;

        public GetMemberByIdQueryHandler(IMemberDapperRepository memberDapperRepository) => _memberDapperRepository = memberDapperRepository;

        public async Task<Member> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            return await _memberDapperRepository.GetMemberById(request.Id);
        }
    }

    #endregion
}